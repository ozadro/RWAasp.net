using Admin.Models;
using Admin.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin
{
    public partial class ApartmentEditor : System.Web.UI.Page
    {
        private readonly StatusRepository _statusRepository;
        private readonly CityRepository _cityRepository;
        private readonly ApartmentOwnerRepository _apartmentOwnerRepository;
        private readonly TagRepository _tagRepository;
        private readonly ApartmentRepository _apartmentRepository;

        private readonly string _picPath = "/Content/Pictures/";

        public ApartmentEditor()
        {
            _statusRepository = new StatusRepository();
            _cityRepository = new CityRepository();
            _apartmentOwnerRepository = new ApartmentOwnerRepository();
            _tagRepository = new TagRepository();
            _apartmentRepository = new ApartmentRepository();

         

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string qryStrId = Request.QueryString["Id"];
                int? id = null;
                if (!string.IsNullOrEmpty(qryStrId))
                {
                
                    id = Convert.ToInt32(qryStrId);

                }
                    
                if (id.HasValue)
                {
                    var dbApartment = _apartmentRepository.GetApartment(id.Value);
                    dbApartment.Tags = _apartmentRepository.GetApartmentTags(id.Value);
                    //dbApartment.ApartmentPictures = _apartmentRepository.GetApartmentPictures(id.Value);

                    //for (int i = 0; i < dbApartment.ApartmentPictures.Count; i++)
                    //{
                    //    dbApartment.ApartmentPictures[i].Path =
                    //    Path.Combine(_picPath, dbApartment.ApartmentPictures[i].Path);
                    //}

                    SetExistingApartment(dbApartment);
                }

            


                RebindApartmentOwners();
                RebindCities();
                RebindStatuses();
                RebindTags();
            }
        }



        private List<string> SaveUploadedImagesToDisk()
        {
            var files = new List<string>();
            if (uplImages.HasFiles)
            {
                var uplImagesRoot = Server.MapPath(_picPath);
                if (!Directory.Exists(uplImagesRoot))
                    Directory.CreateDirectory(uplImagesRoot);
                foreach (HttpPostedFile uploadedFile in uplImages.PostedFiles)
                {
                    var uplImagePath = Path.Combine(uplImagesRoot, uploadedFile.FileName);
                    uploadedFile.SaveAs(uplImagePath);
                    files.Add(uploadedFile.FileName);
                }
            }
            return files;
        }

        private List<Models.ApartmentPicture> GetRepeaterPictures()
        {

            var repApartmentPicturesItems = repApartmentPictures.Items;
            var pictures = new List<Models.ApartmentPicture>();
            foreach (RepeaterItem item in repApartmentPicturesItems)
            {
                var pic = new Models.ApartmentPicture();
                pic.Id = int.Parse((item.FindControl("hidApartmentPictureId") as HiddenField).Value);
                pic.Name = (item.FindControl("txtApartmentPicture") as TextBox).Text;
                pic.Path = (item.FindControl("imgApartmentPicture") as Image).ImageUrl;
                pic.IsRepresentative = (item.FindControl("cbIsRepresentative") as CheckBox).Checked;
                pic.DoDelete = (item.FindControl("cbDelete") as CheckBox).Checked;
                pictures.Add(pic);
            }
            return pictures;
        }



        private void SetExistingApartment(Apartment apartment)
        {
            ddlStatus.SelectedValue = apartment.StatusId.ToString();
            ddlApartmentOwner.SelectedValue = apartment.OwnerId.ToString();
            tbName.Text = apartment.Name;
            tbAddress.Text = apartment.Address;
            ddlCity.SelectedValue = apartment.CityId.ToString();
            tbPrice.Text = apartment.Price.ToString();
            tbMaxAdults.Text = apartment.MaxAdults?.ToString();
            tbMaxChildren.Text = apartment.MaxChildren?.ToString();
            tbTotalRooms.Text = apartment.TotalRooms?.ToString();
            tbBeachDistance.Text = apartment.BeachDistance?.ToString();
            repTags.DataSource = apartment.Tags;
            repTags.DataBind();
            repApartmentPictures.DataSource = apartment.ApartmentPictures;
            repApartmentPictures.DataBind();
        }

        private Apartment GetFormApartment()
        {
            int statusId = int.Parse(ddlStatus.SelectedValue);
            int? cityId = int.Parse(ddlCity.SelectedValue);
            if (cityId == 0)
            {
                cityId = null;
            } 
            int ownerId = int.Parse(ddlApartmentOwner.SelectedValue);
            decimal price =  decimal.Parse(tbPrice.Text);
            int? maxAdults = null;
            if (!string.IsNullOrEmpty(tbMaxAdults.Text))
                maxAdults = int.Parse(tbMaxAdults.Text);
            int? maxChildren = null;
            if (!string.IsNullOrEmpty(tbMaxChildren.Text))
                maxChildren = int.Parse(tbMaxChildren.Text);
            int? totalRooms = null;
            if (!string.IsNullOrEmpty(tbTotalRooms.Text))
                totalRooms = int.Parse(tbTotalRooms.Text);
            int? beachDistance = null;
            if (!string.IsNullOrEmpty(tbBeachDistance.Text))
                beachDistance = int.Parse(tbBeachDistance.Text);
            return
            new Apartment
            {
                // Id kreira baza
                Guid = Guid.NewGuid(),
                // CreatedAt kreira baza
                // DeletedAt mora biti prazan
                OwnerId = ownerId,
                TypeId = ownerId+1,
                StatusId = statusId,
                CityId = cityId,
                Address = tbAddress.Text,
                Name = tbName.Text,
                Price = price,
                MaxAdults = maxAdults,
                MaxChildren = maxChildren,
                TotalRooms = totalRooms,
                BeachDistance = beachDistance,
                Tags = GetRepeaterTags(),
                ApartmentPictures = GetRepeaterPictures()
                
            };
        }

        private void RebindTags()
        {
            ddlTags.DataSource = _tagRepository.GetTags();
            ddlTags.DataBind();
        }

        private void RebindApartmentOwners()
        {
            ddlApartmentOwner.DataSource = _apartmentOwnerRepository.GetApartmentOwners();
            ddlApartmentOwner.DataBind();
        }
        private void RebindCities()
        {
            ddlCity.DataSource = _cityRepository.GetCities();
            ddlCity.DataBind();
        }
        private void RebindStatuses()
        {
            
            ddlStatus.DataSource = _statusRepository.GetStatuses();
            ddlStatus.DataBind();
        }

        private List<Tag> GetRepeaterTags()
        {

            var repTagsItems = repTags.Items;
            var tags = new List<Tag>();
            foreach (RepeaterItem item in repTagsItems)
            {
                var tag = new Tag
                {
                    Id = int.Parse((item.FindControl("hidTagId") as HiddenField).Value),
                    Name = (item.FindControl("hidTagId") as Label).Text
                };
                tags.Add(tag);
            }


            return tags;
        }

        protected void lblSave_Click(object sender, EventArgs e)
        {
            var files = SaveUploadedImagesToDisk();
            var apartmentPictures =
            files.Select(x => new ApartmentPicture
            {
                Path = x,
                Name = Path.GetFileNameWithoutExtension(x),
                IsRepresentative = false
            }).ToList();
            var isNewApartment = (Request.QueryString["id"] == null);
            if (isNewApartment)
            {
                var apartment = GetFormApartment();
                //apartment.ApartmentPictures = apartmentPictures;
                _apartmentRepository.CreateApartment(apartment);
                Response.Redirect($"ApartmentList.aspx");
            }
            else
            {
                var apartment = GetFormApartment();
                apartment.Id = int.Parse(Request.QueryString["id"]);
                //apartment.ApartmentPictures.AddRange(apartmentPictures);
                _apartmentRepository.UpdateApartment(apartment);
                Response.Redirect($"ApartmentEditor.aspx?{Request.QueryString}");
            }
            Response.Redirect($"ApartmentList.aspx");
        }

        protected void lblReturn_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("ApartmentList.aspx");
        }


        private Tag GetSelectedTag()
        {

            var selectedValue = ddlTags.SelectedItem.Value;
            var newTag = new Tag
            {
                Id = int.Parse(selectedValue),
                Name = ddlTags.SelectedItem.Text
                   
            };

            


            return newTag;
        }


        protected void btnDeleteTag_Click(object sender, EventArgs e)
        {
            var tags = GetRepeaterTags();
            // Nije lako naći pravu kontrolu
            var lbSender = (LinkButton)sender;
            var parentItem = (RepeaterItem)lbSender.Parent;
            var hidTagId = (HiddenField)parentItem.FindControl("hidTagId");
            var tagId = int.Parse(hidTagId.Value);
            var existingTag = tags.FirstOrDefault(x => x.Id == tagId);
            tags.Remove(existingTag);
            repTags.DataSource = tags;
            repTags.DataBind();
        }

        protected void ddlTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tags = GetRepeaterTags(); // TODO
            var newTag = GetSelectedTag(); // TODO
            if (tags.Any(x => x.Id == newTag.Id))
                return;
            tags.Add(newTag);
            repTags.DataSource = tags;
            repTags.DataBind();
        }

    }
}