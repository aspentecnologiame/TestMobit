using System;
using System.Collections.Generic;
using System.Text;
using TestMobit.Domain.Enums;

namespace TestMobit.Domain.Entities
{
    public class OperatorEntity
    {
        public Guid Id { get; set; }
        public string OperatorName { get; set; } = string.Empty;
        public ServiceType ServiceType { get; set; }
        public string SupportContact { get; set; } = string.Empty;
    }
}
