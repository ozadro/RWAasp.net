using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Repositories
{
    public class OrderRepository
    {
        public List<Models.Order> GetOrders()
        {
            return new List<Models.Order>
            {
                new Models.Order { Id = 0, Name = "(kriterij sortiranja)" },
                new Models.Order { Id = 1, Name = "BrojSoba" },
                new Models.Order { Id = 2, Name = "BrojOdraslih" },
                new Models.Order { Id = 3, Name = "BrojDjece" },
                new Models.Order { Id = 4, Name = "Cijena" },
            };
        }
    }
}