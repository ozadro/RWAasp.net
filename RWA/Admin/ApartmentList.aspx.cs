using Admin.Repositories;
using System;
using System.Web;

namespace Admin
{
    public partial class ApartmentList : System.Web.UI.Page
    {
        private readonly ApartmentRepository _apartmentRepository;
        private readonly StatusRepository _statusRepository;
        private readonly CityRepository _cityRepository;
        private readonly OrderRepository _orderRepository;
        public ApartmentList()
        {
            _apartmentRepository = new ApartmentRepository();
            _statusRepository = new StatusRepository();
            _cityRepository = new CityRepository();
            _orderRepository = new OrderRepository();

        }

        private void FormToCookie()
        {
            HttpCookie cookie = Request.Cookies["ApartmentList"];
            if (cookie == null)
            {
                cookie = new HttpCookie("ApartmentList");
            }
            cookie["Status"] = ddlStatus.SelectedValue;
            cookie["City"] = ddlCity.SelectedValue;
            cookie["Order"] = ddlOrder.SelectedValue;

            cookie.Expires = DateTime.Now.AddMinutes(30);

            Response.SetCookie(cookie);

        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebindApartments();
            FormToCookie();
        }
        protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebindApartments();
            FormToCookie();
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebindApartments();
            FormToCookie();
        }

        private void RebindApartments()
        {
            var status = int.Parse(ddlStatus.SelectedValue);
            var cityId = int.Parse(ddlCity.SelectedValue);
            var order = int.Parse(ddlOrder.SelectedValue);
            gvListaApartmana.DataSource =
            _apartmentRepository.GetApartments(status, cityId, order);
            gvListaApartmana.DataBind();
        }

        private void CookieToForm()
        {
            HttpCookie cookie = Request.Cookies["ApartmentList"];

            if (cookie != null)
            {
                if (cookie["Status"] != null)
                {
                    cookie["Status"] = ddlStatus.SelectedValue;
                }
                if (cookie["City"] != null)
                {
                    cookie["City"] = ddlCity.SelectedValue;
                }
                if (cookie["Order"] != null)
                {
                    cookie["Order"] = ddlOrder.SelectedValue;
                }
            }
        }



        protected void lbApartmentEditor_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApartmentEditor.aspx");
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlStatus.DataSource = _statusRepository.GetStatuses();
                ddlStatus.DataBind();
                ddlCity.DataSource = _cityRepository.GetCities();
                ddlCity.DataBind();
                ddlOrder.DataSource = _orderRepository.GetOrders();
                ddlOrder.DataBind();

                CookieToForm();

                RebindApartments();
            }

        }


    }
}