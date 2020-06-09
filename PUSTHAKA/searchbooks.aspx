<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="searchbooks.aspx.cs" Inherits="PUSTHAKA.searchbooks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
        <script type="text/javascript">
            $(document).ready(function () {
                $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
            });
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="server">

    <div class="container">
            <div class="row">

                <div class="col-sm-12">
                    <center>
                        <h3>
                        Book List</h3>
                    </center>
                    <div class="row">
                        <div class="col-sm-12 col-md-12">
                            <asp:Panel class="alert alert-success" role="alert" ID="Panel1" runat="server" Visible="False">
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            </asp:Panel>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="card">
                            <div class="card-body">

                                <div class="row">
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PusthakaDBConnectionString %>" SelectCommand="SELECT * FROM [book_table]"></asp:SqlDataSource>
                                    <div class="col-lg-8">
                                        <div class="card">
                                            
                                        <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="BookId" DataSourceID="SqlDataSource1" Width="100%" OnSelectedIndexChanged="Gridview1_SelectedIndexChanged">
                                            <Columns>
                                                <asp:BoundField DataField="BookId" HeaderText="ID" ReadOnly="True" SortExpression="BookId">
                                                    <ControlStyle Font-Bold="True" />
                                                    <ItemStyle Font-Bold="True" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="container-fluid">
                                                            <div class="row">
                                                                <div class="col-lg-10">
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("BookName") %>' Font-Bold="True" Font-Size="X-Large"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            <span>Author - </span>
                                                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Text='<%# Eval("author") %>'></asp:Label>
                                                                            &nbsp;| <span><span>&nbsp;</span>Genre - </span>
                                                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Text='<%# Eval("genre") %>'></asp:Label>
                                                                            &nbsp;|
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            Publisher -
                                                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Text='<%# Eval("PublisherName") %>'></asp:Label>
                                                                            &nbsp;| Publish Date -
                                                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Text='<%# Eval("publishDate") %>'></asp:Label>
                                                                            &nbsp;| Edition -
                                                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Text='<%# Eval("edition") %>'></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            Cost -
                                                                            <asp:Label ID="Label9" runat="server" Font-Bold="True" Text='<%# Eval("sellingPrice") %>'></asp:Label>
                                                                            &nbsp;| Actual Stock -
                                                                            <asp:Label ID="Label10" runat="server" Font-Bold="True" Text='<%# Eval("actualStock") %>'></asp:Label>
                                                                           </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            Description -
                                                                            <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Smaller" Text='<%# Eval("bookDescription") %>'></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-2">
                                                                    <asp:Image class="img-fluid" ID="Image1" runat="server" ImageUrl='<%# Eval("bookImageLink") %>' />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:ButtonField Text="Add To Cart" CommandName="Select" HeaderText="Add To Cart" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            
                            <div class="col-lg-4">
                                <div class="card">
                                    <div class="card-header">
                                        Confirm Reservation
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="form-group form-inline">
                                                <label for ="BookId">Book Id</label>
                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group form-inline">
                                                <label for ="BookName">Book Name</label>
                                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group form-inline">
                                                <label for ="userID">User Id</label>
                                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group form-inline">
                                                <label for ="username">Username</label>
                                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group form-inline">
                                                <label for ="reserveDate">Reserve Date</label>
                                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group form-inline">
                                                <label for ="dueDate">Expire Date</label>
                                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:Button ID="Button1" runat="server" Text="Confirm Reservation" OnClick="Button1_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
             </div>
                <center>
                    <a href="home.aspx">
                        << Back to Home</a><span class="clearfix"></span>
                            <br />
                            <center>
            </div>
        </div>

</asp:Content>
