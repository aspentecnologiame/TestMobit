using System;
using Hangfire;
using System.Threading.Tasks;
using TestMobit.Domain.Entities;
using TestMobit.Domain.Interfaces.Repositories.Database;
using TestMobit.Domain.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;
using TestMobit.Domain.Enums;
using TestMobit.Service.Hubs;

namespace TestMobit.Service
{
    public class EnterpriseService : IEnterpriseService
    {
        private readonly IHubContext<EnterpriseHub> _enterpriseHub;
        private readonly IEnterpriseRepository _enterpriseRepository;
        public EnterpriseService(IEnterpriseRepository enterpriseRepository, IHubContext<EnterpriseHub> enterpriseHub)
        {
            _enterpriseRepository = enterpriseRepository;
            _enterpriseHub = enterpriseHub;
        }

        public async Task Run(EnterpriseEntity enterpriseEntity, SystemFlow flow)
        {
            try
            {
                var method = typeof(EnterpriseService).GetMethod(SystemFlow.Save.ToString()) ?? throw new NotImplementedException($"Method {SystemFlow.Save} not implemented!");
                method.Invoke(this, [enterpriseEntity]);
                await _enterpriseHub.Clients.All.SendAsync("EnterpriseData", enterpriseEntity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task Add(EnterpriseEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
                throw new ArgumentNullException(nameof(entity));

            var existingEnterprise = await this.GetByName(entity.Name);

            if (existingEnterprise != null)
                throw new ApplicationException("Enterprise already exists");

            entity.Id = Guid.NewGuid();
            await Task.FromResult(BackgroundJob.Schedule(() => this.Run(entity, SystemFlow.Save), TimeSpan.FromSeconds(1)));
        }

        public async Task Save(EnterpriseEntity enterpriseEntity) => await _enterpriseRepository.Add(enterpriseEntity);

        public async Task<EnterpriseEntity> GetByName(string name) => await Task.FromResult(_enterpriseRepository.FindOne(x => x.Name == name));
    }
}
