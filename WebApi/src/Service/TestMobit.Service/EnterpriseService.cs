using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestMobit.Domain.Entities;
using TestMobit.Domain.Interfaces.Repositories.Database;
using TestMobit.Domain.Interfaces.Services;

namespace TestMobit.Service
{
    public class EnterpriseService : IEnterpriseService
    {
        private readonly IEnterpriseRepository _enterpriseRepository;
        public EnterpriseService(IEnterpriseRepository enterpriseRepository)
        {
            _enterpriseRepository = enterpriseRepository;
        }

        public async Task Add(EnterpriseEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
                throw new ArgumentNullException(nameof(entity));

            var existingEnterprise = await this.GetByName(entity.Name);

            if(existingEnterprise != null)
                throw new ApplicationException("Enterprise already exists");

            await _enterpriseRepository.Add(entity);
        }

        public async Task<EnterpriseEntity> GetByName(string name) => await Task.FromResult(_enterpriseRepository.FindOne(x => x.Name == name));
    }
}
