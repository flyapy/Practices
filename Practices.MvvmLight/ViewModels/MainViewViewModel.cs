using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Practices.MvvmLight.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace Practices.MvvmLight.ViewModels
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewViewModel : ViewModelBase
    {
        private List<ProductInfo> products;

        public List<ProductInfo> Products
        {
            get { return products; }
            set { products = value; RaisePropertyChanged(nameof(Products)); }
        }

        public RelayCommand UpdateProductsCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            Products = new List<ProductInfo>();
            for (int i = 0; i < 10; i++)
            {
                ProductInfo productinfo = new ProductInfo();
                productinfo.IsChecked = true;
                productinfo.ProductName = "MVVM";
                productinfo.ProductIcon = "MVVM";
                productinfo.ProductUrl = "WWW.BAIDU.COM";
                productinfo.OldVersion = $"0.0.{i}";
                productinfo.NewVersion = $"0.0.{i}+";
                Products.Add(productinfo);
            }

            UpdateProductsCommand = new RelayCommand(UpdateProducts);
        }

        private void UpdateProducts()
        {
            Random rnd = new Random();
            var list = products;
            //MessageBox.Show(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
            Task.Run(() =>
            {
                //MessageBox.Show(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
                foreach (var product in list)
                {
                    product.ProductName = $"MVVM: {rnd.Next().ToString()[0]}";
                }

                //Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                //{
                //    MessageBox.Show(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
                //    Products = null;
                //    Products = list;
                //}));
                Application.Current.Dispatcher.Invoke(() =>
                {
                    //MessageBox.Show(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
                    Products = null;
                    Products = list;
                });
            });
            
            

        }



    }
}