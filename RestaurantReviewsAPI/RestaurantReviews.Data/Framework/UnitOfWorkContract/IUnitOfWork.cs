﻿using System.Threading.Tasks;
using RestaurantReviews.Data.Framework.RepoContracts;

namespace RestaurantReviews.Data.Framework.UnitOfWorkContract
{
    public interface IUnitOfWork
    {
        IUserRepo UserRepo { get; }
        IStateRepo StateRepo { get; }
        IRestaurantRepo RestaurantRepo { get; }
        Task<int> CommitAsync();
    }
}
