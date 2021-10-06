using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using TestSkyProduct.Models;
using System.Web.Mvc;

namespace TestSkyProduct.DBAccess
{
    public class SalesDBAccess
    {

        //sql object declaration

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataSet ds;


        //constructor for connection 
        public SalesDBAccess()
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



        //Insert Sales Activity
        public int InsertSalesActivity(SalesModel sales)
        {
           
            cmd.Parameters.AddWithValue("@number", sales.ProductNumber);  
            cmd.Parameters.AddWithValue("@quantity", sales.Quantity);
            
            cmd.Parameters.AddWithValue("@amount",sales.TotalAmount);
            cmd.Parameters.AddWithValue("@checktype", 4);
        
            int result = cmd.ExecuteNonQuery();
         
            return result;


        }


        //Get TotalAmount = Quantity * UnitPrice
        public decimal GetUnitPrice(string pnumber)
        {
            cmd.Parameters.AddWithValue("@checktype", 8);
            cmd.Parameters.AddWithValue("@number", pnumber);
            DataSet ds = new DataSet();
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            
            adpt.Fill(ds);
            decimal price = Convert.ToDecimal(ds.Tables[0].Rows[0]["Unit_Price"]);
            return price;

        }


        //Select Product To fill into Dropdownlist
        public List<SelectListItem> PopulateProduct()

        {
            List<SelectListItem> items = new List<SelectListItem>();
            cmd.Parameters.AddWithValue("@checktype", 5);
            cmd.Connection = con; 
            SqlDataReader dr = cmd.ExecuteReader();
           
            while (dr.Read())
            {
                items.Add(new SelectListItem
                {
                    Text = dr["Product_Name"].ToString(),
                    Value = dr["Product_Number"].ToString()
                });
            }
                    return items;

        }

        //Select all sales activity 
        public DataSet ViewProductList()
        {
            cmd.Parameters.AddWithValue("@checktype", 6);
            DataSet ds = new DataSet();
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(ds);
            return ds;

        }

    }
}