﻿using System.Data.Entity;
using RestaurantReviews.Data.EfLibrary.Entities;
using SqlProviderServices = System.Data.Entity.SqlServer.SqlProviderServices;

namespace RestaurantReviews.Data.EfLibrary.Context
{
    public class RestaurantReviewsContext : DbContext
    {
        public RestaurantReviewsContext() : this("DefaultConnection")
        {
            
        }

        public RestaurantReviewsContext(string connectionStringName) : base(connectionStringName)
        {

        }

        public IDbSet<UserDBO> Users { get; set; }
        public IDbSet<StateDBO> States { get; set; }
        public IDbSet<RestaurantDBO> Restaurants { get; set; }
        public IDbSet<ReviewDBO> Reviews { get; set; }
    }
}
