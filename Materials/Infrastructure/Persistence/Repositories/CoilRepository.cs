using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SadidServices.Materials.Domain.Coils;
using SadidServices.Materials.Infrastructure.Persistence;

namespace SadidServices.Materials.Infrastructure.Persistance.Repositories
{
    public class CoilRepository : Repository<Coil,Guid> , ICoilRepository
    {
        public CoilRepository(CatalogContext context):base(context){   }
    }
}