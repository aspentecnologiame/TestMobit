using System;
using TestMobit.Domain.Entities.Base;

namespace TestMobit.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
