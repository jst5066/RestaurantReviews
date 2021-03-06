﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data.EfLibrary.Context;
using RestaurantReviews.Data.EfLibrary.Entities;
using RestaurantReviews.Data.Framework.RepoContracts;
using RestaurantReviews.Data.Models;

namespace RestaurantReviews.Data.EfLibrary.Respositories
{
    public class UserRepo : IUserRepo
    {
        private readonly RestaurantReviewsContext _context;

        public UserRepo(RestaurantReviewsContext context)
        {
            _context = context;
        }

        public User Get(long userId)
        {
            var user = _context
                .Users
                .Find(userId);

            if (user == null)
                return null;

            return new User
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password
            };
        }

        public async Task<List<User>> FindMatchingResults(string username = null)
        {
            var usersQuery = _context
                .Users
                .AsQueryable();

            if (!string.IsNullOrEmpty(username))
                usersQuery.Where(user => user.Username == username);

            var results = await usersQuery.ToListAsync();

            return results
                .Select(dbo => new User
                {
                    Id = dbo.Id,
                    Password = dbo.Password,
                    Username = dbo.Username
                })
                .ToList();
        }
    }
}
