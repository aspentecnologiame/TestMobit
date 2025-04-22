using System;
using System.Collections.Generic;
using System.Text;

namespace TestMobit.Domain.Entities
{
    public class BranchEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public EnterpriseEntity? Enterprise { get; set; }
    }
}
