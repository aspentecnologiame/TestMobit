using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMobit.Domain.Entities;
using TestMobit.Domain.Interfaces.Repositories.Database;
using TestMobit.Domain.Interfaces.Repositories.Database.Base;
using TestMobit.Infra.Data.DatabaseRepository.Base;

namespace TestMobit.Infra.Data.DatabaseRepository
{
    public class EnterpriseRepository : DataBaseRepository<EnterpriseEntity>, IEnterpriseRepository, IRepository
    {
        public EnterpriseRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
