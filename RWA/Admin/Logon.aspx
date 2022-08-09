<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="Admin.Logon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <link href="LoginStyle.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="LogonForm" runat="server">
    <h3>
Logon Page
</h3>
<table class="table">
 <tr>
 <td>
     <label class="em">Email:</label> 
     <input id="txtUserName" type="text" runat="server"/>

 </td>
 <td>
     <ASP:RequiredFieldValidator ControlToValidate="txtUserName"
 Display="Static" ErrorMessage="*" runat="server"
 ID="vUserName" />

 </td>
 </tr>
 <tr>
 <td>
     <label class="pwd">Password:</label> 
     <input id="txtUserPass" type="password" runat="server"/>

 </td>
 <td>
     <ASP:RequiredFieldValidator ControlToValidate="txtUserPass"
 Display="Static" ErrorMessage="*" runat="server"
 ID="vUserPass" />

 </td>
 </tr>
 <tr>
 <td>
     <label class="cookie">Persistent Cookie:</label> <ASP:CheckBox id="chkPersistCookie" runat="server" autopostback="false" />

 </td>
  <td></td>
 </tr>
</table>
        <br />
        <br />
        <br />
<asp:LinkButton ID="lbLogin" CssClass="btn-default" OnClick="lbLogin_Click" runat="server">Login</asp:LinkButton>
<asp:Label id="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />

</form>



</body>
</html>
