using eBeerShop.Persistance.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace eBeerShop.Persistance.Facade
{
    public class DomainModelFacade: IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DomainModelFacade(DbContextOptions<DomainModelFacade> options) 
            : base(options)
        { }
    }
}
