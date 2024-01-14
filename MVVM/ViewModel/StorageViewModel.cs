using AFPStore.Core;
using AFPStore.MVVM.Model.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AFPStore.MVVM.View.DialogViews;

namespace AFPStore.MVVM.ViewModel
{
    class StorageViewModel: ObservableObject
    {
        RelayCommand? addProductCommand;
        RelayCommand? editProductCommand;
        RelayCommand? deleteProductCommand;
        RelayCommand? addQuantityCommand;
        Stock selectedStock;
        List<Stock> stocks;

        public List<Stock> Stocks 
        { 
            get => stocks;
            set 
            {
                stocks = value;
                OnPropertyChanged("Stocks");
            } 
        }       

        public Stock SelectedStock
        {
            get => selectedStock;
            set
            {
                selectedStock = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public StorageViewModel() 
        {
            using StoreDbContext db = new();
            Stocks = db.Stocks.Include("Product").ToList();
        }
        public RelayCommand AddQuantityCommand
        {
            get
            {
                return addQuantityCommand ??= new RelayCommand((selectedItem) =>
                {
                    // получаем выделенный объект
                    Stock? stock = selectedItem as Stock;
                    if (stock == null) return;
                    using StoreDbContext db = new();
                    stock.Quantity++;
                    db.Entry(stock).State = EntityState.Modified;
                    db.SaveChanges();
                    Stocks = db.Stocks.Include("Product").ToList();
                });
            }
        }
        // команда добавления
        public RelayCommand AddProductCommand
        {
            get
            {
                return addProductCommand ??= new RelayCommand((o) =>
                  {
                      Product product = new() { CreateAt = DateTime.Now };
                      Stock stock = new() { Product = product };
                      ProductView productWindow = new(stock);
                      if (productWindow.ShowDialog() == true)
                      {
                          using StoreDbContext db = new();
                          db.Products.Add(stock.Product);
                          db.SaveChanges();
                          stock.ProductId = db.Products.Entry(stock.Product).Entity.Id;
                          db.Stocks.Add(stock);
                          db.SaveChanges();
                          Stocks = db.Stocks.Include("Product").ToList();
                      }
                  });
            }
        }
        // команда редактирования
        public RelayCommand EditProductCommand
        {
            get
            {
                return editProductCommand ??= new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      Stock? stock = selectedItem as Stock;
                      if (stock == null) return;
                      
                      ProductView productWindow = new ProductView(stock);


                      if (productWindow.ShowDialog() == true)
                      {
                          using StoreDbContext db = new();
                          db.Entry(stock.Product).State = EntityState.Modified;
                          db.Entry(stock).State = EntityState.Modified;
                          db.SaveChanges();
                          Stocks = db.Stocks.Include("Product").ToList();
                      }
                  });
            }
        }
        // команда удаления
        public RelayCommand DeleteProductCommand
        {
            get
            {
                return deleteProductCommand ??= new RelayCommand((selectedItem) =>
                  {
                      using StoreDbContext db = new();
                      // получаем выделенный объект
                      if (selectedItem is not Stock stock) return;
                      //Точно удалить?
                      CustomMessageBoxView dialog = new($"Вы точно хотите удалить {stock.Product.Name}?");
                      if(dialog.ShowDialog() == true)
                      {
                          db.Stocks.Remove(stock);
                          db.SaveChanges();
                      }
                      //Stocks.Remove(stock);
                      Stocks = db.Stocks.Include("Product").ToList();
                  });
            }
        }
    }
}
