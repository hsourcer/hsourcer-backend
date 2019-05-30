using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HSourcer.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HSourcer.Persistence
{

    //because primary key is int type
    public class UserStore : UserStore<User, IdentityRole<int>, DbContext, int>
    {
        public UserStore(DbContext context) : base(context)
        {
        }

    }
    public class RoleStore : RoleStore<IdentityRole<int>, DbContext, int>
    {
        public RoleStore(DbContext context) : base(context)
        {
        }

    }
}
