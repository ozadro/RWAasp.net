using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin
{
    public partial class Logon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected void lbLogin_Click(object sender, EventArgs e)
        {
            // FormsAuthentication.RedirectFromLoginPage() automatically generates
            // the forms authentication cookie!
            if (ValidateUser(txtUserName.Value, txtUserPass.Value))
                System.Web.Security.FormsAuthentication.RedirectFromLoginPage(txtUserName.Value, chkPersistCookie.Checked);
                
            else
                Response.Redirect("Logon.aspx", true);
                
        }
        private bool ValidateUser(string userName, string passWord)
        {


            if (userName == "123" && passWord == "123")
                return true;
            return false;
        }

    }
}