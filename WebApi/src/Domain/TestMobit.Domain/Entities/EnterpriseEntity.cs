using System;
using System.Collections.Generic;
using System.Text;

namespace TestMobit.Domain.Entities
{
    public class EnterpriseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<BranchEntity>? Branches { get; set; }
    }
}
