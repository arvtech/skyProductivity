using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TestSkyProduct.Models
{
    public class ProductModel
    {
        //Properties Declaration

        [Required(ErrorMessage = "Please Enter Product Name")]
        [Display(Name = "Product Name")]
        public string Product_Name { get; set; }


        [Required(ErrorMessage = "Please Enter Product Number")]
        [Display(Name = "Product Number")]
        public string Prduct_Number { get; set; }


        [Required(ErrorMessage = "Please Enter Product Decscription")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Please Enter Product Unit Price")]
        [Display(Name = "Unit Price")]
        public decimal Unit_Price { get; set; }


        public List<ProductModel> ProductList { get; set; }
    }
}