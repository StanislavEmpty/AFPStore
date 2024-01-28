using AFPStore.Core;
using AFPStore.MVVM.Model.Main;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFPStore.MVVM.ViewModel
{
    class CheckViewModel : ObservableObject
    {
        ObservableCollection<Sale> sales;

        public ObservableCollection<Sale> Sales
        {
            get => sales;
            set
            {
                sales = value;
                OnPropertyChanged("Sales");
            }
        }
        double allSum;
        public double AllSum
        {
            get => allSum;
            set
            {
                allSum = value;
                OnPropertyChanged("AllSum");
            }
        }
        RelayCommand? saveTofileCommand;
        public CheckViewModel()
        {
            AllSum = 0;
        }
        public RelayCommand SaveToFileCommand
        { 
            get
            {
                return saveTofileCommand ??= new RelayCommand((_) =>
                {
                    SaveFileDialog dialog = new()
                    {
                        AddExtension = true,
                        Filter = " MSWord файлы(*.docx)|*.docx",
                        Title = "Сохранить файл",
                        CheckPathExists = true
                    };
                    if (dialog.ShowDialog() == true)
                    {
                        FileInfo file = new(dialog.FileName);
                        using (WordprocessingDocument doc = WordprocessingDocument.Create(file.FullName, WordprocessingDocumentType.Document, true))
                        {
                            MainDocumentPart mainPart = doc.AddMainDocumentPart();
                            mainPart.Document = new Document();
                            Body body = mainPart.Document.AppendChild(new Body());
                            SectionProperties props = new SectionProperties();
                            body.AppendChild(props);
                            var par = AddText(doc, $"Чек от {Sales.First().SaleDate}");
                            par.ParagraphProperties = new();
                            par.ParagraphProperties.AddChild(new Justification()
                            {
                                Val = JustificationValues.Center
                            });

                            Table dTable = new();
                            TableProperties tprops = new();
                            dTable.AppendChild(tprops);
                            //headers/////
                            string[] headers = new string[] { "Товар", "К-во", "Цена", "Сумма" };
                            var tr = new TableRow();
                            foreach (var item in headers)
                            {
                                var tc = new TableCell();
                                tc.Append(new Paragraph(new Run(new Text(item))));

                                tc.Append(new TableCellProperties());

                                tr.Append(tc);
                            }
                            dTable.Append(tr);
                            //headers/////
                            foreach (var item in Sales)
                            {
                                tr = new TableRow();
                                //Product.Name
                                var tc = new TableCell();
                                tc.Append(new Paragraph(new Run(new Text(item.Product.Name))));
                                tc.Append(new TableCellProperties());
                                tr.Append(tc);
                                //Quantity
                                tc = new TableCell();
                                tc.Append(new Paragraph(new Run(new Text(item.Quantity.ToString()))));
                                tc.Append(new TableCellProperties());
                                tr.Append(tc);
                                //Product.Price
                                tc = new TableCell();
                                tc.Append(new Paragraph(new Run(new Text(item.Product.Price.ToString()))));
                                tc.Append(new TableCellProperties());
                                tr.Append(tc);
                                //TotalPrice
                                tc = new TableCell();
                                tc.Append(new Paragraph(new Run(new Text(item.TotalPrice.ToString()))));
                                tc.Append(new TableCellProperties());
                                tr.Append(tc);
                                dTable.Append(tr);
                            }
                            var borderValues = new EnumValue<BorderValues>(BorderValues.Single);
                            var tableBorders = new TableBorders(
                                                 new TopBorder { Val = borderValues, Size = 4 },
                                                 new BottomBorder { Val = borderValues, Size = 4 },
                                                 new LeftBorder { Val = borderValues, Size = 4 },
                                                 new RightBorder { Val = borderValues, Size = 4 },
                                                 new InsideHorizontalBorder { Val = borderValues, Size = 4 },
                                                 new InsideVerticalBorder { Val = borderValues, Size = 4 });
                            tprops.Append(tableBorders);
                            var tableWidth = new TableWidth()
                            {
                                Width = "5000",
                                Type = TableWidthUnitValues.Pct
                            };
                            tprops.Append(tableWidth);
                            doc.MainDocumentPart.Document.Body.Append(dTable);
                            AddText(doc, "");
                            AddText(doc, $"Общая сумма: {AllSum} у.е.");
                            doc.Save();
                        }
                    }
            });
            }
        }
        private static Paragraph AddText(WordprocessingDocument doc, string text)
        {
            MainDocumentPart mainPart = doc.MainDocumentPart;
            Body body = mainPart.Document.Body;
            Paragraph paragraph = body.AppendChild(new Paragraph());

            Run run = paragraph.AppendChild(new Run());
            run.AppendChild(new Text(text));
            run.PrependChild(new RunProperties());
            return paragraph;
        }
    }
}
