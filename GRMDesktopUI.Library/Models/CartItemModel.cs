using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRMDesktopUI.Library.Models
{
    public class CartItemModel
    {
        public ProductModell Product { get; set; }

        public int QuantityInCart { get; set; }

        public string DisplayText
        {
            get
            {
                return $"{Product.ProductName} ({QuantityInCart})";
            }
        }     
    }
}
