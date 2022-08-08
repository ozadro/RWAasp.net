<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApartmentDelete.aspx.cs" Inherits="Admin.ApartmentDelete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="form-group">
        <label>Vlasnik</label>
        <asp:Label ID="lblApartmentOwner" runat="server" CssClass="form-control"></asp:Label>
    </div>
    <div class="form-group">
        <label>Naziv</label>
        <asp:Label ID="lblName" runat="server" CssClass="form-control"></asp:Label>
    </div>
    <div class="form-group">
        <label>Adresa</label>
        <asp:Label ID="lblAddress" runat="server" CssClass="form-control"></asp:Label>
    </div>
    <div class="form-group">
        <label>Grad</label>
        <asp:Label ID="lblCity" runat="server" CssClass="form-control"></asp:Label>
    </div>
    <div>
        <asp:LinkButton  ID="lbConfirmDelete" runat="server" Text="Potvrdi brisanje"  OnClick="lbConfirmDelete_Click" CssClass="btn btn-danger" />
        <asp:LinkButton ID="lbBack" runat="server" OnClick="lbBack_Click" Text="Odustani" CssClass="btn btn-primary" />
    </div>

</asp:Content>
