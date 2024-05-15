using Cargodi.DataAccess.Data;
using Cargodi.DataAccess.Entities;
using Cargodi.DataAccess.Interfaces;
using Cargodi.DataAccess.Interfaces.Common;

namespace Cargodi.DataAccess.Repositories.Common;

public class AddressRepository : RepositoryBase<Address, long>, IAddressRepository
{
    public AddressRepository(DatabaseContext db) : base(db)
    {
    }

}