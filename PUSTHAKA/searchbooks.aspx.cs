using System.Data.SqlClient;
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
    public partial class searchbooks : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            TextBox5.Text = today.ToString();
            DateTime after15days = today.AddDays(15);
            TextBox6.Text += after15days.ToString();
        }

        
        protected void Gridview1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["role"] != null)
            {
                if (Session["role"].Equals("user"))
                {
                    Label l1 = GridView1.SelectedRow.FindControl("Label1") as Label;
                    TextBox2.Text = l1.Text;

                    TextBox4.Text = Session["username"].ToString();

                    try
                    {
                        SqlConnection con = new SqlConnection(strcon);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlCommand cmd = new SqlCommand("SELECT userId from user_table where username='" + Session["username"].ToString() + "';", con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count >= 1)
                        {
                            TextBox3.Text = dt.Rows[0]["userId"].ToString();

                        }

                        SqlCommand cmd1 = new SqlCommand("SELECT BookId from book_table where BookName='" + l1.Text + "';", con);
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);

                        if (dt1.Rows.Count >= 1)
                        {
                            TextBox1.Text = dt1.Rows[0]["BookId"].ToString();

                        }


                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");

                    }
                }

            }

            else
            {
                Response.Redirect("searchbooks.aspx");
            }



        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO reservations_table(reserveId, username, BookName, reserveDate, dueDate) values" +
                    "(@reserveId,@username,@BookName,@reserveDate,@dueDate)", con);
                cmd.Parameters.AddWithValue("@username", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@BookName", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@reserveDate", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@dueDate", TextBox6.Text.Trim());
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Sign Up Successful. Go to User Login to Login');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
    }
