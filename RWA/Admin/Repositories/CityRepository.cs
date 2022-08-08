using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Admin.Repositories
{
    public class CityRepository
    {
        private readonly string _connectionString;
        public CityRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["rwadb"].ConnectionString;
        }
        public List<Admin.Models.City> GetCities()
        {
            var ds = SqlHelper.ExecuteDataset(
            _connectionString,
            CommandType.StoredProcedure,
            "dbo.GetCities");
            var cityList = new List<Admin.Models.City>();
            cityList.Add(new Admin.Models.City { Id = 0, Name = "(odabir grada)" });
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var city = new Admin.Models.City();
                city.Id = Convert.ToInt32(row["ID"]);
                city.Guid = Guid.Parse(row["Guid"].ToString());
                city.Name = row["Name"].ToString();
                cityList.Add(city);
            }
            return cityList;
        }
    }
}