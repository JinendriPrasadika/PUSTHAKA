<%@ Page Title="" culture="en-SL" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="viewbooks.aspx.cs" Inherits="PUSTHAKA.viewbooks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="server">

    <table>

   </table>

    <asp:DataList ID="DataList1" runat="server" DataKeyField="BookId" DataSourceID="SqlDataSource1" Height="275px" Width="300px" RepeatColumns="4" RepeatDirection="Horizontal" OnItemCommand="DataList1_ItemCommand">
        <ItemTemplate>
             <table>
                 <tr>
                     <td style=" text-align:center ; background-color:#5f98f3">
                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("BookName") %>' Font-Bold="True" Font-Names="Open Sans Extrabold" ForeColor="White"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td>
                         <asp:Image ID="Image1" runat="server" BorderColor="#6600CC" BorderWidth="1px" Height="250px" Width="225px" ImageUrl='<%# Eval("bookImageLink") %>' />
                     </td>
                 </tr>
                 <tr>
                     <td style=" text-align:center ; background-color:#5f98f3">
                         <asp:Label ID="Label4" runat="server" Text='<%# Eval("author") %>' Font-Bold="true" Font-Names="Arial" ForeColor="White" style=" text-align:center"></asp:Label>
                    </td>
                 </tr>
                 <tr>
                     <td style=" text-align:center ; background-color:#5f98f3">
                         <asp:Label ID="Label3" runat="server" Text="Edition:" Font-Bold="true" Font-Names="Arial" ForeColor="White" style=" text-align:center" ></asp:Label>
                         <asp:Label ID="Label5" runat="server" Text='<%# Eval("edition") %>' Font-Bold="true" Font-Names="Arial" ForeColor="White" style=" text-align:center" ></asp:Label>
                       </td>
                 </tr>
                 <tr>
                     <td style=" text-align:center ; background-color:#5f98f3">
                         <asp:Label ID="Label6" runat="server" Text="Price: Rs" Font-Bold="true" Font-Names="Arial" ForeColor="White" style=" text-align:center"></asp:Label>
                         <asp:Label ID="Label7" runat="server" Text='<%# Eval("sellingPrice") %>' Font-Bold="true" Font-Names="Arial" ForeColor="White" style=" text-align:center"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td style=" text-align:center ; background-color:#5f98f3">
                         <asp:Label ID="Label2" runat="server" Text='<%# Eval("actualStock") %>' Font-Bold="true" Font-Names="Arial" ForeColor="White" style=" text-align:center"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td style=" text-align:center">
                         <asp:ImageButton ID="ImageButton1" runat="server" Height="40px" ImageUrl="~/Image/cartnew.jpg" width="80px" CommandArgument='<%# ("BookId") %>' CommandName="AddToCart" OnClick="ImageButton1_Click" />
                     </td>
                 </tr>
             </table>
             <br>
             <br></br>
             <br>
             <br></br>
             </br>
           </br>
        </ItemTemplate>

    </asp:DataList>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PusthakaDBConnectionString %>" OnSelecting="SqlDataSource1_Selecting" SelectCommand="SELECT [BookName], [Author], [edition], [sellingPrice], [actualStock], [bookImageLink], [BookId] FROM [book_table]"></asp:SqlDataSource>
  
</asp:Content>
