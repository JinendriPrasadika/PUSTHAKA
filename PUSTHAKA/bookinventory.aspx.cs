using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PUSTHAKA
{
    public partial class bookinventory : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        static string global_filepath;
        static int global_actual_stock, global_current_stock, global_issued_books;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillAuthorPublisherValues();
            }

            GridView1.DataBind();
        }

        

        // go button click
        protected void Button4_Click(object sender, EventArgs e)
        {
            getBookByID();
        }


        // update button click
        protected void Button3_Click(object sender, EventArgs e)
        {
            updateBookByID();
        }
        // delete button click
        protected void Button2_Click(object sender, EventArgs e)
        {
            deleteBookByID();
        }
        // add button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkIfBookExists())
            {
                Response.Write("<script>alert('Book Already Exists, try some other Book ID');</script>");
            }
            else
            {
                addNewBook();
            }
        }



        // user defined functions

        void deleteBookByID()
        {
            if (checkIfBookExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE from book_table WHERE BookId='" + TextBox1.Text.Trim() + "'", con);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Book Deleted Successfully');</script>");

                    GridView1.DataBind();

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }

            }
            else
            {
                Response.Write("<script>alert('Invalid Member ID');</script>");
            }
        }

        void updateBookByID()
        {

            if (checkIfBookExists())
            {
                try
                {

                    int actualStock = Convert.ToInt32(TextBox4.Text.Trim());
                    int currentStock = Convert.ToInt32(TextBox5.Text.Trim());

                    if (global_actual_stock == actualStock)
                    {

                    }
                    else
                    {
                        if (actualStock < global_issued_books)
                        {
                            Response.Write("<script>alert('Actual Stock value cannot be less than the Issued books');</script>");
                            return;


                        }
                        else
                        {
                            currentStock = actualStock - global_issued_books;
                            TextBox5.Text = "" + currentStock;
                        }
                    }

                    string genres = "";
                    foreach (int i in ListBox1.GetSelectedIndices())
                    {
                        genres = genres + ListBox1.Items[i] + ",";
                    }
                    genres = genres.Remove(genres.Length - 1);

                    string filepath = "~/bookinventory/logo";
                    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    if (filename == "" || filename == null)
                    {
                        filepath = global_filepath;

                    }
                    else
                    {
                        FileUpload1.SaveAs(Server.MapPath("bookinventory/" + filename));
                        filepath = "~/bookinventory/" + filename;
                    }

                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("UPDATE book_table set BookName=@BookName, Isbn=@Isbn genre=@genre, Author=@Author, PublisherName=@PublisherName, publishDate=@publishDate, edition=@edition, sellingPrice=@sellingPrice, bookDescription=@bookDescription, actualStock=@actualStock, currentStock=@currentStock, bookImageLink=@bookImageLink where BookId='" + TextBox1.Text.Trim() + "'", con);

                    cmd.Parameters.AddWithValue("@BookName", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Isbn", TextBox8.Text.Trim());
                    cmd.Parameters.AddWithValue("@genre", genres);
                    cmd.Parameters.AddWithValue("@Author", DropDownList3.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@PublisherName", DropDownList2.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@publishDate", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@edition", TextBox9.Text.Trim());
                    cmd.Parameters.AddWithValue("@sellingPrice", TextBox10.Text.Trim());
                    cmd.Parameters.AddWithValue("@bookDescription", TextBox6.Text.Trim());
                    cmd.Parameters.AddWithValue("@actualStock", actualStock.ToString());
                    cmd.Parameters.AddWithValue("@currentStock", currentStock.ToString());
                    cmd.Parameters.AddWithValue("@bookImageLink", filepath);


                    cmd.ExecuteNonQuery();
                    con.Close();
                    GridView1.DataBind();
                    Response.Write("<script>alert('Book Updated Successfully');</script>");


                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Book ID');</script>");
            }
        }


        void getBookByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from book_table WHERE BookId='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0]["BookName"].ToString();
                    TextBox8.Text = dt.Rows[0]["Isbn"].ToString();
                    TextBox3.Text = dt.Rows[0]["publishDate"].ToString();
                    TextBox9.Text = dt.Rows[0]["edition"].ToString();
                    TextBox10.Text = dt.Rows[0]["sellingPrice"].ToString().Trim();
                    TextBox4.Text = dt.Rows[0]["actualStock"].ToString().Trim();
                    TextBox5.Text = dt.Rows[0]["currentStock"].ToString().Trim();
                    TextBox6.Text = dt.Rows[0]["bookDescription"].ToString();
                    TextBox7.Text = "" + (Convert.ToInt32(dt.Rows[0]["actualStock"].ToString()) - Convert.ToInt32(dt.Rows[0]["currentStock"].ToString()));

                    DropDownList2.SelectedValue = dt.Rows[0]["PublisherName"].ToString().Trim();
                    DropDownList3.SelectedValue = dt.Rows[0]["Author"].ToString().Trim();

                    ListBox1.ClearSelection();
                    string[] genre = dt.Rows[0]["genre"].ToString().Trim().Split(',');
                    for (int i = 0; i < genre.Length; i++)
                    {
                        for (int j = 0; j < ListBox1.Items.Count; j++)
                        {
                            if (ListBox1.Items[j].ToString() == genre[i])
                            {
                                ListBox1.Items[j].Selected = true;

                            }
                        }
                    }

                    global_actual_stock = Convert.ToInt32(dt.Rows[0]["actualStock"].ToString().Trim());
                    global_current_stock = Convert.ToInt32(dt.Rows[0]["currentStock"].ToString().Trim());
                    global_issued_books = global_actual_stock - global_current_stock;
                    global_filepath = dt.Rows[0]["bookImageLink"].ToString();

                }
                else
                {
                    Response.Write("<script>alert('Invalid Book ID');</script>");
                }

            }
            catch (Exception ex)
            {

            }
        }

        void fillAuthorPublisherValues()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT authorName from author_table;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DropDownList3.DataSource = dt;
                DropDownList3.DataValueField = "authorName";
                DropDownList3.DataBind();

                cmd = new SqlCommand("SELECT publisherName from publisher_table;", con);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                DropDownList2.DataSource = dt;
                DropDownList2.DataValueField = "publisherName";
                DropDownList2.DataBind();

            }
            catch (Exception ex)
            {

            }
        }

        bool checkIfBookExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from book_table where BookId='" + TextBox1.Text.Trim() + "' OR BookName='" + TextBox2.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        void addNewBook()
        {
            try
            {
                string genres = "";
                foreach (int i in ListBox1.GetSelectedIndices())
                {
                    genres = genres + ListBox1.Items[i] + ",";
                }
                
                genres = genres.Remove(genres.Length - 1);

                string filepath = "~/bookinventory/logo.jpg";
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("bookinventory/" + filename));
                filepath = "~/bookinventory/" + filename;


                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO book_table(BookId,Isbn,BookName,genre,Author,PublisherName,publishDate,edition,sellingPrice,bookDescription,actualStock,bookImageLink) values(@BookId,@Isbn,@BookName,@genre,@Author,@PublisherName,@publishDate,@edition,@sellingPrice,@bookDescription,@actualStock,@bookImageLink)", con);

                cmd.Parameters.AddWithValue("@BookId", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@Isbn", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@BookName", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@genre", genres);
                cmd.Parameters.AddWithValue("@Author", DropDownList3.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@PublisherName", DropDownList2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publishDate", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@edition", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@sellingPrice", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@bookDescription", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@actualStock", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@bookImageLink", filepath);

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Book added successfully.');</script>");
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

    }
}