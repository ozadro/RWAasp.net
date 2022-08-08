using Admin.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Admin.Repositories
{
    public class TagRepository
    {
        private readonly string _connectionString;

        public TagRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["rwadb"].ConnectionString;
        }


        public List<Tag> GetTags()
        {

            var ds = SqlHelper.ExecuteDataset(
                _connectionString,
                CommandType.StoredProcedure,
                "dbo.GetTags");

            var tagList = new List<Tag>
            {
                new Tag { Id = 0, Name = "(odabir taga)" }
            };
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var tag = new Tag
                {
                    Id = (int)row["ID"],
                    Name = row["Name"].ToString()
                };
                tagList.Add(tag);


            }


            return tagList;
        }



    }
}