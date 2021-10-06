using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSkyProduct.Models;
using TestSkyProduct.DBAccess;
using System.Data;

namespace TestSkyProduct.Controllers
{
    public class ProductController : Controller
    {
        ProductDBAccess db = new ProductDBAccess();



        //Get Product List
        public ActionResult Index()
        {
            List<ProductModel> plist = new List<ProductModel>();
            ProductModel productmodel = new ProductModel();
            DataSet ds = new DataSet();
            ds = db.ViewProductList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    plist.Add(new ProductModel
                    {
                        Product_Name = dr["Product_Name"].ToString(),
                        Prduct_Number = dr["Product_Number"].ToString(),
                        Description =dr["Description"].ToString(),
                        Unit_Price = Convert.ToDecimal(dr["Unit_Price"])

                    });

                    productmodel.ProductList = plist;
                }
            }
            return View(productmodel);
        }



        // GET: Product
        public ActionResult Create()
        {
            return View();
        }


        //Add New Product 
        [HttpPost]
        public ActionResult Create(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                int result = db.AddProduct(product);
                if (result != 0)
                {
                    TempData["Message"] = "<script>alert('Product Added Successfully')</script>";
                    ModelState.Clear();
                }

            }

            return View();
        }



        //Update Product
        [HttpPost]
        public JsonResult UpdateProduct(string pname, string pnumber, string description, string unitprice, string prevpnumber)
        {
            ProductModel product = new Models.ProductModel();
            product.Prduct_Number = pnumber;
            product.Product_Name = pname;
            product.Description = description;
            product.Unit_Price = Convert.ToDecimal(unitprice);
            int result = db.UpdateProduct(product, prevpnumber);
            
            return Json(result, JsonRequestBehavior.AllowGet);



        }

        //Delete Product
        [HttpPost]
        public JsonResult DeleteProduct( string pnumber)
        {
         
            int result = db.DeleteProduct(pnumber);
            return Json(result, JsonRequestBehavior.AllowGet);

        }




        //Select Product for Update [No need of this : directly selected through Jqeury ]
        [HttpPost]
        public JsonResult SelectProduct(string productnumber)
        {
            List<ProductModel> plist = new List<ProductModel>();
            DataSet ds = new DataSet();
            ds = db.ViewProductList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    plist.Add(new ProductModel
                    {
                        Product_Name = dr["Product_Name"].ToString(),
                        Prduct_Number = dr["Product_Number"].ToString(),
                        Description = dr["Description"].ToString(),
                        Unit_Price = Convert.ToDecimal(dr["Unit_Price"])

                    });

                   
                }
            }
            return Json(plist,JsonRequestBehavior.AllowGet);

        }
    }
}