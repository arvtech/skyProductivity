using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using TestSkyProduct.Models;

namespace TestSkyProduct.DBAccess
{
    public class ProductDBAccess
    {
       
            //sql object declaration

            SqlConnection con;
            SqlCommand cmd;
            SqlDataAdapter adpt;
            DataSet ds;


            //constructor for connection 
            public ProductDBAccess()
            {
                con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ProductMaster";
                cmd.Connection = con;

            }

            //Add Department 
            public int AddProduct(ProductModel product)
            {


                cmd.Parameters.AddWithValue("@name", product.Product_Name);
                cmd.Parameters.AddWithValue("@number", product.Prduct_Number);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@unitprice",product.Unit_Price);
                cmd.Parameters.AddWithValue("@checktype", 1);
                int result = cmd.ExecuteNonQuery();
                return result;


            }

        //View all product
        public DataSet ViewProductList()
        {
            cmd.Parameters.AddWithValue("@checktype", 2);
            DataSet ds = new DataSet();
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(ds);
            return ds;

        }



        //Update Product 
        public int UpdateProduct(ProductModel product, string prevproductnumber)
        {


            cmd.Parameters.AddWithValue("@name", product.Product_Name);
            cmd.Parameters.AddWithValue("@number", product.Prduct_Number);
            cmd.Parameters.AddWithValue("@description", product.Description);
            cmd.Parameters.AddWithValue("@unitprice", product.Unit_Price);
            cmd.Parameters.AddWithValue("@prevpnumber", prevproductnumber);
            cmd.Parameters.AddWithValue("@checktype", 3);
            int result = cmd.ExecuteNonQuery();
            return result;


        }


        //Delete Product
        public int DeleteProduct(string prevproductnumber)
        {


        
            cmd.Parameters.AddWithValue("@prevpnumber", prevproductnumber);
            cmd.Parameters.AddWithValue("@checktype", 7);
            int result = cmd.ExecuteNonQuery();
            return result;


        }

        //Select product for Edit  [ comment out due selected through jquery directly]
        //public DataSet SelectProduct(string productnumber)
        //{
        //    cmd.Parameters.AddWithValue("@checktype", 2);
        //    cmd.Parameters.AddWithValue("@productnumber", productnumber);
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        //    adpt.Fill(ds);
        //    return ds;


        //}

    }
}