using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSkyProduct.DBAccess;
using TestSkyProduct.Models;

namespace TestSkyProduct.Controllers
{
    public class SalesController : Controller
    {
        SalesDBAccess db = new SalesDBAccess();
        SalesModel sales = new SalesModel();
        
        // GET: Sales
        public ActionResult Create()
        {

            sales.ProductList = db.PopulateProduct();
            return View(sales);
            
        }


        //Insert Sales Activity
        [HttpPost]
        public ActionResult Create(SalesModel sale)
        {
            

            int result = db.InsertSalesActivity(sale);
            if (result > 0)
            {
                TempData["Message"] = "<script>alert('Sales Order Submitted Successfully')<script>";
                
            }

            // sales.ProductList = db.PopulateProduct();
            return RedirectToAction("Activity", "Sales");
           
        }

        //Get all Sales activity 
      
        public ActionResult Activity()
        {
            DataSet ds = new DataSet();
            List<SalesModel> slit = new List<SalesModel>();
            ds = db.ViewProductList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                   
                    slit.Add(new SalesModel { 
                    ProductName = dr["Product_Name"].ToString(),
                    Quantity = Convert.ToInt32(dr["Qty"]),
                    TotalAmount = Convert.ToDecimal(dr["Total_Amount"]),
                   
                });
                    sales.SalesList = slit;
                }
            }
            return View(sales);
        }


        //Get TotalAmount on Qty and unitprice
        [HttpPost]
        public JsonResult GetTotalAmount(string qty, string pnumber)
        {
            decimal uprice = db.GetUnitPrice(pnumber);
            decimal Totalamount = Convert.ToDecimal(uprice * Convert.ToInt32(qty));
            List<SalesModel> slist = new List<SalesModel>();
            slist.Add(new SalesModel(){

                TotalAmount = Totalamount
            });
            return Json(slist, JsonRequestBehavior.AllowGet);
        }
    }
}