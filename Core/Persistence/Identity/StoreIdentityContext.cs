global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Identity
{
    public class StoreIdentityContext : IdentityDbContext<User>
    {
        public StoreIdentityContext(DbContextOptions<StoreIdentityContext> options) : base(options) 
        { // options w ctor fady 3shan da ely hay3ml instance mn El Identity Db Package
          // Beyakhod el instance w el object w el initalize 3amtn ely 3amlo fel app settings w el service container 


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // base.on model creating bet3ml configuration ll type ely ana 3aml leha inhert  3shan types gahza
           // w kman el base.OnModelCreating(builder) lw 3andy configurations 3andy hazwedha bas lw ma3ndesh malhash lazma aktebha heya already gahza
          
            base.OnModelCreating(builder);
            builder.Entity<Address>().ToTable("Addresses");
        }


    }
}
