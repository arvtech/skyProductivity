using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TestSkyProduct.Models
{
    public class SalesModel
    {
        //Properties Sales Activity
        [Required(ErrorMessage ="Enter Customer Name")]
        [Display(Name ="Customer Name")]
        public string CustomerName { get; set; }


        [Required(ErrorMessage = "Select Product")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

      


        [Required(ErrorMessage = "Enter Quantity")]
        public int Quantity { get; set; }


        
        [Display(Name = "Total Amount")]
        public Decimal TotalAmount { get; set; }



        public List<SelectListItem> ProductList { get; set; }
        public string ProductNumber { get; set; }

        public List<SalesModel> SalesList { get; set; }
     

    }
}