using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Schema;

namespace PUSTHAKA
{
    public partial class UserProfile : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["username"].ToString() != "" || Session["username"] != null)
                {
                    getUserBookData();

                    if (!Page.IsPostBack)
                    {
                        getUserPersonalDetails();
                    }

                }
                else
                {
                    Response.Write("<script>alert('Session Expired Login Again');</script>");
                    Response.Redirect("userlogin.aspx");

                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Session Expired Login Again');</script>");
                Response.Redirect("userlogin.aspx");
            }


        }


            // update button click
            protected void Button1_Click(object sender, EventArgs e)
            {
                if (Session["username"].ToString() == "" || Session["username"] == null)
                {
                    Response.Write("<script>alert('Session Expired Login Again');</script>");
                    Response.Redirect("userlogin.aspx");
                }
                else
                {
                    updateUserPersonalDetails();

                }
            }



            // user defined function


            void updateUserPersonalDetails()
            {
                string password = "";
                if (TextBox10.Text.Trim() == "")
                {
                    password = TextBox9.Text.Trim();
                }
                else
                {
                    password = TextBox10.Text.Trim();
                }
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }


                    SqlCommand cmd = new SqlCommand("update user_table set fullname=@fullname, dob=@dob, contactno=@contactno, email=@email, address=@address,  password=@password, status=@status WHERE username='" + Session["username"].ToString().Trim() + "'", con);

                    cmd.Parameters.AddWithValue("@fullname", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@dob", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@contactno", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@address", TextBox5.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@account_status", "pending");

                    int result = cmd.ExecuteNonQuery();
                    con.Close();
                    if (result > 0)
                    {

                        Response.Write("<script>alert('Your Details Updated Successfully');</script>");
                        getUserPersonalDetails();
                        getUserBookData();
                    }
                    else
                    {
                        Response.Write("<script>alert('Invaid entry');</script>");
                    }

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }


            void getUserPersonalDetails()
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * from user_table where username='" + Session["username"].ToString() + "';", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    TextBox1.Text = dt.Rows[0]["fullname"].ToString();
                    TextBox2.Text = dt.Rows[0]["dob"].ToString();
                    TextBox3.Text = dt.Rows[0]["contactno"].ToString();
                    TextBox4.Text = dt.Rows[0]["email"].ToString();
                    TextBox5.Text = dt.Rows[0]["address"].ToString();
                    TextBox8.Text = dt.Rows[0]["username"].ToString();
                    TextBox9.Text = dt.Rows[0]["password"].ToString();

                    // Label1.Text = dt.Rows[0]["account_status"].ToString().Trim();

                    //if (dt.Rows[0]["account_status"].ToString().Trim() == "active")
                    //{
                    //    Label1.Attributes.Add("class", "badge badge-pill badge-success");
                    //}
                    //else if (dt.Rows[0]["account_status"].ToString().Trim() == "pending")
                    //{
                    //    Label1.Attributes.Add("class", "badge badge-pill badge-warning");
                    //}
                    //else if (dt.Rows[0]["account_status"].ToString().Trim() == "deactive")
                    //{
                    //    Label1.Attributes.Add("class", "badge badge-pill badge-danger");
                    //}
                    //else
                    //{
                    //    Label1.Attributes.Add("class", "badge badge-pill badge-info");
                    // }


                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");

                }
            }

            void getUserBookData()
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * from reservations_table where user_name='" + Session["username"].ToString() + "';", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();


                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");

                }
            }

            protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                try
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //Check your condition here
                        DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);
                        DateTime today = DateTime.Today;
                        if (today > dt)
                        {
                            e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["buyitems"];

            for(int i = 0; i<=dt.Rows.Count - 1; i++)
            {
                int sr;
                int srl;
                string qdata;
                string dtdata;
                sr = Convert.ToInt32(dt.Rows[i]["SerialNo"].ToString());
                TableCell cell = GridView1.Rows[e.RowIndex].Cells[0];
                qdata = cell.Text;
                dtdata = sr.ToString();
                srl = Convert.ToInt32(qdata);

                if (sr == srl)
                {
                    dt.Rows[i].Delete();
                    dt.AcceptChanges();
                    break;
                }
            }

            //setting serail no after deleting a row
            for(int i = 1; i<= dt.Rows.Count; i++)
            {
                dt.Rows[i - 1]["SerialNo"] = i;
                dt.AcceptChanges();
            }
            Session["buyitems"] = dt;
            Response.Redirect("UserProfile.aspx");
        }
        }
    } 