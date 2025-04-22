using System;
using System.Collections.Generic;
using System.Text;

namespace TestMobit.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        public DateTime Created { get; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
    }
}
