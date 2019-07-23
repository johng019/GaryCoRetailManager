using GRMDataManager.Library.Internal.DataAccess;
using GRMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRMDataManager.Library.DataAccess
{
    public class SaleData
    {
        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            //TODO: Make this SOLID/ DRY/ Better
            //Start filling in the sale detail models i will save to the database
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            ProductData products = new ProductData();
            var taxrate = ConfigHelper.GetTaxRate()/100;

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                //Get the info about this product
                var productInfo = products.GetProductById(detail.ProductId);

                if(productInfo == null)
                {
                    throw new Exception($"The prouduct Id of { detail.ProductId }  could not be found in the database.");
                }
                detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);

                if (productInfo.IsTaxable)
                {
                    detail.Tax = (detail.PurchasePrice * taxrate);
                }

                details.Add(detail);
            }

            //Create the sale model
            SaleDBModel sale = new SaleDBModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };

            sale.Total = sale.SubTotal + sale.Tax;
            //save the sale model
            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData("dbo.spSale_Insert", sale, "GRMData");

            //get the ID from the sale model
            sale.Id = sql.LoadData<int, dynamic>("spSale_Lookup", new { sale.CashierId, sale.SaleDate }, "GRMData").FirstOrDefault();

            //Finish filling in the saledetail models
            foreach (var item in details)
            {
                item.SaleId = sale.Id;
                //Save the sle detail models
                sql.SaveData("dbo.spSaleDetail_Insert", item, "GRMData");
            }          
            
        }
        //public List<ProductModel> GetProducts()
        //{
        //    SqlDataAccess sql = new SqlDataAccess();

        //    var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "GRMData");

        //    return output;
        //}
    }
}
