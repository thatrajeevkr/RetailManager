using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Library.API;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        IProductEndpoint _productEndpoint;

        public SalesViewModel(IProductEndpoint productEndpoint)
        {
            _productEndpoint = productEndpoint;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var productsList = await _productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(productsList);
        }
        private BindingList<ProductModel> _products;

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set { 
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private BindingList<ProductModel> _cart;

        public BindingList<ProductModel> Cart
        {
            get { return _cart; }
            set { 
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }


        private int _itemQuantity;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }


        public string SubTotal
        {
            get 
            { 
                //TODO - Replace with Calculation
                return "$0.00"; 
            }
        }

        public string TAX
        {
            get
            {
                //TODO - Replace with Calculation
                return "$0.00";
            }
        }

        public string Total
        {
            get
            {
                //TODO - Replace with Calculation
                return "$0.00";
            }
        }


        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                //Make sure something is selected
                //Make sure there is an Item quantity
                return output;
            }
        }

        public void AddToCart()
        {

        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;

                //Make sure something is selected
                return output;
            }
        }

        public void RemoveFromCart()
        {

        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;

                //Make sure something is in the cart
                return output;
            }
        }

        public void CheckOut()
        {

        }

    }
}
