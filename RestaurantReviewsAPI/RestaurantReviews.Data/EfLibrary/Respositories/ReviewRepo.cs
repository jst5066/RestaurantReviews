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
    public class ReviewRepo : IReviewRepo
    {
        private readonly RestaurantReviewsContext _context;

        public ReviewRepo(RestaurantReviewsContext context)
        {
            _context = context;
        }

        public void Add(long restaurantId, long authorId, Review review)
        {
            var restaurant = _context
                .Restaurants
                .Find(restaurantId);

            var author = _context
                .Users
                .Find(authorId);

            _context
                .Reviews
                .Add(new ReviewDBO
                {
                    Restaurant = restaurant,
                    Author = author,
                    Stars = review.Stars,
                    Comments = review.Comments
                });
        }

        public async Task<List<Review>> FindMatchingResults(long restaurantId = -1, long authorId = -1)
        {
            var query = _context
                .Reviews
                .Include(review => review.Restaurant)
                .Include(review => review.Author)
                .AsQueryable();

            if (restaurantId > -1)
                query = query.Where(review => review.Restaurant.Id == restaurantId);
            if (authorId > -1)
                query = query.Where(review => review.Author.Id == authorId);

            var results = await query
                .ToListAsync();
            return results
                .Select(review => new Review
                {
                    Id = review.Id,
                    Stars = review.Stars,
                    Comments = review.Comments,
                    AuthorUsername = review.Author.Username,
                    RestaurantId = review.Restaurant.Id
                })
                .ToList();
        }

        public Review Get(long reviewId)
        {
            var reviewDBO = _context
                .Reviews
                .Find(reviewId);

            return new Review
            {
                Id = reviewDBO.Id,
                Stars = reviewDBO.Stars,
                Comments = reviewDBO.Comments,
                AuthorUsername = reviewDBO.Author.Username
            };
        }

        public void Remove(long reviewId)
        {
            var reviewDBO = _context
                .Reviews
                .Find(reviewId);

            if (reviewDBO == null)
                return;

            _context
                .Reviews
                .Remove(reviewDBO);
        }
    }
}
