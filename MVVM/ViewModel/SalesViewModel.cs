using AFPStore.Core;
using AFPStore.MVVM.Model.Main;
using AFPStore.MVVM.View.DialogViews;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFPStore.MVVM.ViewModel
{
    class SalesViewModel : ObservableObject
    {
        RelayCommand? addsStockToBacketCommand;
        RelayCommand? removeStockToBacketCommand;
        RelayCommand? showProductCommand;
        RelayCommand? addQuantityProductCommand;
        RelayCommand? saleProductsCommand;

        ObservableCollection<Stock> storageStocks;
        Stock selectedStorageStock;
        ObservableCollection<Stock> backetStocks;
        Stock selectedBacketStock;
        public ObservableCollection<Stock> StorageStocks
        { 
            get=> storageStocks;
            set
            {
                storageStocks = value;
                OnPropertyChanged("StorageStocks");
            }
        }
        public Stock SelectedStorageStock
        {
            get => selectedStorageStock;
            set
            {
                selectedStorageStock = value;
                OnPropertyChanged("SelectedStorageStock");
            }
        }
        public ObservableCollection<Stock> BacketStocks
        { 
            get=> backetStocks;
            set
            {
                backetStocks = value;
                OnPropertyChanged("BacketStocks");
            }
        }
        public Stock SelectedBacketStock
        {
            get => selectedBacketStock;
            set
            {
                selectedBacketStock = value;
                OnPropertyChanged("SelectedBacketStock");
            }
        }
        public SalesViewModel() 
        {
            using StoreDbContext db = new();
            db.Stocks.Where(o => o.Quantity > 0).Include(s => s.Product).OrderBy(o => o.Product.Name).Load();
            StorageStocks = db.Stocks.Local.ToObservableCollection();
            BacketStocks = new();
        }
        public RelayCommand AddsStockToBacketCommand
        {
            get
            {
                return addsStockToBacketCommand ??= new RelayCommand((selectedItem) =>
                {
                    if (selectedItem is not Stock stock) return;
                    if (stock.Quantity == 0) return;
                    var selectedStock = BacketStocks.Where(o => o.Product.Name == stock.Product.Name).FirstOrDefault();
                    if (selectedStock == null) 
                        BacketStocks.Add(new Stock() { ProductId=stock.ProductId, Product = new() { Name = stock.Product.Name, Price = stock.Product.Price }, Quantity = 1 });
                    else
                    {
                        if (stock.Quantity == selectedStock.Quantity)
                        {
                            new CustomMessageBoxWithOnlyOKView("Нельзя добавить количества больше чем есть в наличии!").ShowDialog();
                        }
                        else
                            selectedStock.Quantity++;
                    }
                });
            }
        }
        public RelayCommand RemoveStockToBacketCommand
        {
            get
            {
                return removeStockToBacketCommand ??= new RelayCommand((selectedItem) =>
                {
                    if (selectedItem is not Stock stock) return;
                    BacketStocks.Remove(stock);
                });
            }
        }
        public RelayCommand ShowProductCommand
        {
            get
            {
                return showProductCommand ??= new RelayCommand((selectedItem) =>
                {
                    if (selectedItem is not Stock stock) return;
                    ProductView view = new(stock, true);
                    view.ShowDialog();
                });
            }
        }
        public RelayCommand AddQuantityProductCommand
        {
            get
            {
                return addQuantityProductCommand ??= new RelayCommand((selectedItem) =>
                {
                    if (selectedItem is not Stock stock) return;
                    var selectedStock = StorageStocks.Where(o => o.Product.Name == stock.Product.Name).FirstOrDefault();
                    if (selectedStock == null)
                        return;
                    if (stock.Quantity == selectedStock.Quantity)
                    {
                        new CustomMessageBoxWithOnlyOKView("Нельзя добавить количества больше чем есть в наличии!").ShowDialog();
                    }
                    else
                        stock.Quantity++;
                });
            }
        }
        public RelayCommand SaleProductsCommand
        {
            get
            {
                return saleProductsCommand ??= new RelayCommand((o) =>
                {
                    DateTime now = DateTime.Now;
                    using StoreDbContext db = new();
                    foreach(var stock in BacketStocks)
                    {
                        Sale sale = new()
                        {
                            ProductId = stock.ProductId,
                            Quantity = stock.Quantity
                        };
                        sale.TotalPrice = sale.Quantity * stock.Product.Price;
                        sale.SaleDate = now;
                        db.Sales.Add(sale);
                        db.SaveChanges();
                        var stockStorage = db.Stocks.Where(o=>o.Product.Name == stock.Product.Name).FirstOrDefault();
                        if (stockStorage != null)
                        {
                            stockStorage.Quantity -= stock.Quantity;
                            db.Entry(stockStorage).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    BacketStocks = new();
                    db.Stocks.Where(o => o.Quantity > 0).Include(s => s.Product).OrderBy(o => o.Product.Name).Load();
                    StorageStocks = db.Stocks.Local.ToObservableCollection();
                    CustomMessageBoxView view = new("Расспечатать чек?");
                    if(view.ShowDialog() == true)
                    {
                        //если да, то сформировать печатную форму и показать её

                    }
                });
            }
        }
    }
}
