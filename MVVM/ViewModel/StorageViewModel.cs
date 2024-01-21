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
            Stocks = db.Stocks.Include("Product").OrderBy(o=>o.Product.Name).ToList();
        }
        public RelayCommand AddQuantityCommand
        {
            get
            {
                return addQuantityCommand ??= new RelayCommand(async (selectedItem) =>
                {
                    // получаем выделенный объект
                    Stock? stock = selectedItem as Stock;
                    if (stock == null) return;
                    using StoreDbContext db = new();
                    stock.Quantity++;
                    db.Entry(stock).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    Stocks = db.Stocks.Include("Product").OrderBy(o => o.Product.Name).ToList();
                });
            }
        }
        // команда добавления
        public RelayCommand AddProductCommand
        {
            get
            {
                return addProductCommand ??= new RelayCommand(async (o) =>
                  {
                      Product product = new() { CreateAt = DateTime.Now };
                      Stock stock = new() { Product = product };
                      ProductView productWindow = new(stock, false);
                      if (productWindow.ShowDialog() == true)
                      {
                          using StoreDbContext db = new();
                          db.Products.Add(stock.Product);
                          await db.SaveChangesAsync();
                          stock.ProductId = db.Products.Entry(stock.Product).Entity.Id;
                          db.Stocks.Add(stock);
                          await db.SaveChangesAsync();
                          Stocks = db.Stocks.Include("Product").OrderBy(o => o.Product.Name).ToList();
                      }
                  });
            }
        }
        // команда редактирования
        public RelayCommand EditProductCommand
        {
            get
            {
                return editProductCommand ??= new RelayCommand(async (selectedItem) =>
                  {
                      // получаем выделенный объект
                      Stock? stock = selectedItem as Stock;
                      if (stock == null) return;
                      
                      ProductView productWindow = new ProductView(stock, false);


                      if (productWindow.ShowDialog() == true)
                      {
                          using StoreDbContext db = new();
                          db.Entry(stock.Product).State = EntityState.Modified;
                          db.Entry(stock).State = EntityState.Modified;
                          await db.SaveChangesAsync();
                          Stocks = db.Stocks.Include("Product").OrderBy(o => o.Product.Name).ToList();
                      }
                  });
            }
        }
        // команда удаления
        public RelayCommand DeleteProductCommand
        {
            get
            {
                return deleteProductCommand ??= new RelayCommand(async (selectedItem) =>
                  {
                      using StoreDbContext db = new();
                      // получаем выделенный объект
                      if (selectedItem is not Stock stock) return;
                      //Точно удалить?
                      CustomMessageBoxView dialog = new($"Вы точно хотите удалить {stock.Product.Name}?");
                      if(dialog.ShowDialog() == true)
                      {
                          db.Stocks.Remove(stock);
                          await db.SaveChangesAsync();
                      }
                      //Stocks.Remove(stock);
                      Stocks = db.Stocks.Include("Product").OrderBy(o => o.Product.Name).ToList();
                  });
            }
        }
    }
}
