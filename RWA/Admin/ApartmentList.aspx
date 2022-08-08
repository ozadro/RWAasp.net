<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApartmentList.aspx.cs" Inherits="Admin.ApartmentList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div>
 <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
 <asp:DropDownList ID="ddlStatus" runat="server" DataValueField="Id" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" DataTextField="Name" CssClass="form-control"></asp:DropDownList>
</div>
<div>
 <asp:Label ID="lblCity" runat="server" Text="Grad:"></asp:Label>
 <asp:DropDownList ID="ddlCity" runat="server" DataValueField="Id" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" DataTextField="Name" CssClass="form-control"></asp:DropDownList>
</div>
<div>
 <asp:Label ID="lblOrder" runat="server" Text="Sortiranje:"></asp:Label>
 <asp:DropDownList ID="ddlOrder" runat="server" OnSelectedIndexChanged="ddlOrder_SelectedIndexChanged" DataValueField="Id"  DataTextField="Name"
 CssClass="form-control"></asp:DropDownList>
</div>
<hr />
<div>
    <asp:GridView ID="gvListaApartmana" runat="server" CssClass="table table-striped table-condensed table-hover">
        <Columns>
 <asp:BoundField DataField="OwnerName" HeaderText="Vlasnik" />
 <asp:BoundField DataField="StatusName" HeaderText="Status" />
 <asp:BoundField DataField="CityName" HeaderText="Grad" />
 <asp:BoundField DataField="Address" HeaderText="Adresa" />
 <asp:BoundField DataField="Name" HeaderText="Naziv" />
 <asp:BoundField DataField="MaxAdults" HeaderText="Broj odraslih" ItemStyle-HorizontalAlign="Right" />
 <asp:BoundField DataField="MaxChildren" HeaderText="Broj djece" ItemStyle-HorizontalAlign="Right" />
 <asp:BoundField DataField="TotalRooms" HeaderText="Broj soba" ItemStyle-HorizontalAlign="Right" />
 <asp:BoundField DataField="BeachDistance" HeaderText="Udaljenost od plaže" ItemStyle-HorizontalAlign="Right" />
 <asp:BoundField DataField="Price" HeaderText="Cijena" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
 <asp:TemplateField HeaderText="">
 <ItemTemplate>
     <asp:HyperLink ID="hlEditor" runat="server" CssClass="btn btn-primary" Text="Uredi" NavigateUrl='<%# Eval("Id","ApartmentEditor.aspx?Id={0}") %>'/>
 </ItemTemplate>
 </asp:TemplateField>
            <asp:TemplateField HeaderText="">
 <ItemTemplate>
 
<asp:HyperLink ID="hlDelete"  runat="server" CssClass="btn btn-danger" Text="Briši" NavigateUrl='<%# Eval("Id", "ApartmentDelete.aspx?Id={0}") %>'></asp:HyperLink>

 </ItemTemplate>
 </asp:TemplateField>

 </Columns>
</asp:GridView>
     <div>
        <asp:LinkButton ID="lbApartmentEditor" runat="server" Text="Add Apartment" CssClass="btn btn-primary" OnClick="lbApartmentEditor_Click"></asp:LinkButton>
    </div>
</div> 
</asp:Content>
