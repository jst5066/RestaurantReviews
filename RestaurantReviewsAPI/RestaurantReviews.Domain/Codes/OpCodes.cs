﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Domain.Codes
{
    public enum OpCodes
    {
        Success,
        InvalidOperation,
        ResourceNotFound,
        UnauthorizedOperation,
        InsufficientPermissions
    }
}
