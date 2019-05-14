using Caliburn.Micro;
using GRMDesktopUI.Library.Api;
using GRMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        IProductEndPoint _productEndPoint;

       public SalesViewModel(IProductEndPoint productEndpoint)
        {
            _productEndPoint = productEndpoint;          
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        public async Task LoadProducts()
        {
            var productList = await _productEndPoint.GetAll();
            Products = new BindingList<ProductModell>(productList);
        }

        private BindingList<ProductModell> _products;

        public  BindingList<ProductModell> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private ProductModell _selectedProduct;

        public ProductModell SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();

        public BindingList<CartItemModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private int _iItemQuantity = 1;

        public int  ItemQuantity
        {
            get { return _iItemQuantity; }
            set
            {
                _iItemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public string SubTotal
        {
            get
            {
                decimal subTotal = 0;

                foreach (var item in Cart)
                {
                    subTotal += (item.Product.RetailPrice * item.QuantityInCart);
                }
                return subTotal.ToString("C");
            }
        }

        public string Tax
        {
            get
            {
                //ToDo - Replace with calculation
                return "$0.00";
            }
        }

        public string Total
        {
            get
            {
                //ToDo - Replace with calculation
                return "$0.00";
            }
        }

        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                //Make sure something is selected
                //Make sure there is an item quantity
                if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
                {
                    output = true;
                }
                return output;
            }
        }

        public void AddToCart()
        {
            CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);

            if (existingItem != null)
            {
                existingItem.QuantityInCart += ItemQuantity;
                //Hack - There should be a better way of refreshing the cart display
                Cart.Remove(existingItem);
                Cart.Add(existingItem);
            }
            else
            {
                CartItemModel item = new CartItemModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                };
                Cart.Add(item);
            }
            
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            //NotifyOfPropertyChange(() => Cart);
        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;

                //Make sure an item is selected

                return output;
            }
        }

        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;

                //Make sure an item is in the cart

                return output;
            }
        }

        public void CheckOut()
        {

        }









    }
}
