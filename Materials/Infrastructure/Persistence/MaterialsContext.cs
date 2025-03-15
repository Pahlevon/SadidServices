using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SadidServices.Materials.Domain.Coils;
using SadidServices.Materials.Infrastructure.Persistence.Configurations;

namespace SadidServices.Materials.Infrastructure.Persistence
{
    //! This Class should Interact with Configurations
    public class MaterialsContext : DbContext
    {
        //? SQL Server Address => Connection String (UserId , Password, Server, Database)
        //? Developing SQL Server Address => Connection String (UserId , Password, Server, Database)
        // Injecting Option Pattern To Proive String For CatalogContext. DbContextOptions Is For EF Options.
        //First Using Constructor
        public MaterialsContext(DbContextOptions<MaterialsContext> options) : base(options)
        {
            // Now We Can Send With option to Parent.
            //Addding New Service into DI.

        }
        //? Coil Configuration , Other Entity

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //! OldWay
            //  //?modelBuilder.ApplyConfiguration(new CoilConfiguration());
            // modelBuilder.ApplyConfiguration(new PolyEthilenConfiguration());
            //! New-Way Using Reflection to Find Configuration From Assembly :
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        //? Coil
        //? Using LinQ
        //!  DbSet<> is a Combined Class from IEnumerable<> and IQueryable<>.
        public DbSet<Coil> Coils => Set<Coil>();

    }
}