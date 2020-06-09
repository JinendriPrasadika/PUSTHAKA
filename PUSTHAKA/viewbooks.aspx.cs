using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PUSTHAKA
{
    public partial class viewbooks : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["addbook"] = "false";
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Session["addbook"] = "true";
            if (e.CommandName == "AddToCart")
            {
                Response.Redirect("UserProfile.aspx?id=" + e.CommandArgument.ToString());
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            

        }


        
        
    }
}