﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.identity
{
    public class AppUser :IdentityUser
    {
        public string  DisplayName { get; set; }
        public  Address Addess {get; set; }
    }
}
