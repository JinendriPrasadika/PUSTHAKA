using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PUSTHAKA
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["role"] != null)
                {

                    if (Session["role"].Equals("user"))
                    {
                        LinkButton1.Visible = false; // user login link button
                        LinkButton2.Visible = false; // sign up link button

                        LinkButton3.Visible = true; // logout link button
                        LinkButton4.Visible = true; // searchbooks link button
                        LinkButton7.Visible = true; // hello user link button
                        LinkButton7.Text = "Hello " + Session["username"].ToString();


                        LinkButton6.Visible = true; // admin login link button
                        LinkButton11.Visible = false; // author management link button
                        LinkButton12.Visible = false; // supplier management link button
                        LinkButton13.Visible = false; // reservation management link button
                        LinkButton5.Visible = false; // publisher management link button
                        LinkButton8.Visible = false; // billing link button
                        LinkButton9.Visible = false; // book issuing link button
                    }
                    else if (Session["role"].Equals("admin"))
                    {
                        LinkButton1.Visible = false; // user login link button
                        LinkButton2.Visible = false; // sign up link button

                        LinkButton3.Visible = true; // logout link button
                        LinkButton4.Visible = true; // searchbooks link button
                        LinkButton7.Visible = true; // hello user link button
                        LinkButton7.Text = "Hello Admin";


                        LinkButton6.Visible = false; // admin login link button
                        LinkButton11.Visible = true; // author management link button
                        LinkButton12.Visible = true; // supplier management link button
                        LinkButton13.Visible = true; // reservation management link button
                        LinkButton5.Visible = true; // publisher management link button
                        LinkButton8.Visible = true; // book inventory link button
                        LinkButton9.Visible = true; // billing link button
                        LinkButton10.Visible = true; // user management link button
                    }
                }

                else {
                    LinkButton1.Visible = true; // user login link button
                    LinkButton2.Visible = true; // sign up link button

                    LinkButton3.Visible = false; // logout link button
                    LinkButton4.Visible = true; // searchbooks link button
                    LinkButton7.Visible = false; // hello user link button


                    LinkButton6.Visible = true; // admin login link button
                    LinkButton11.Visible = false; // author management link button
                    LinkButton12.Visible = false; // supplier management link button
                    LinkButton13.Visible = false; // reservation management link button
                    LinkButton5.Visible = false; // publisher management link button
                    LinkButton8.Visible = false; // book inventory link button
                    LinkButton9.Visible = false; // billing link button
                    LinkButton10.Visible = false; // user management link button
                }
                }
            catch (Exception ex)
            {

            }
        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminlogin.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("userlogin.aspx");
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            Response.Redirect("customermanagement.aspx");
        }

        //logout button
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            //String txt1 = null;
            Session["username"] = null;
            //Session["fullname"] = "";
            //String txt2 = null;
            Session["role"] = null;

            LinkButton1.Visible = true; // user login link button
            LinkButton2.Visible = true; // sign up link button

            LinkButton3.Visible = false; // logout link button
            LinkButton7.Visible = false; // hello user link button


            LinkButton6.Visible = true; // admin login link button
            LinkButton11.Visible = false; // author management link button
            LinkButton12.Visible = false; // publisher management link button
            LinkButton13.Visible = false; // reservation management link button
            LinkButton8.Visible = false; // book inventory link button
            LinkButton9.Visible = false; // billing link button
            LinkButton10.Visible = false; // user management link button
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            Response.Redirect("bookinventory.aspx");
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Response.Redirect("publishermanagement.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Response.Redirect("searchbooks.aspx");
        }

        protected void LinkButton13_Click(object sender, EventArgs e)
        {
            Response.Redirect("reservationmanagement.aspx");
        }
    }
}