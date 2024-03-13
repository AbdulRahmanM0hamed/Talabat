﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Services
{
    public interface IPaymentServices
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string backetId);
    }
}
