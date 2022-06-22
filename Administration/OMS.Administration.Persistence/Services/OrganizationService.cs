using Microsoft.Extensions.Logging;
using OMS.Administration.Domain.Entities;
using OMS.Administration.Infrasturcture.Persistence;
using OMS.Administration.Infrasturcture.Services.Contracts;
using System.Threading.Tasks;

namespace OMS.Administration.Infrasturcture.Services
{
    public class OrganizationService : IOrganizationService
    {
        private ILogger<OrganizationService> _logger;
        private IAdministrationDbContext _dbContext;
        public OrganizationService(ILogger<OrganizationService> logger, IAdministrationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public async Task SaveOrganizationAsync(Organization organization)
        {
            _logger.LogInformation("Saving organization");
            _dbContext.Organizations.Add(organization);
            await _dbContext.SaveChangesAsync();
        }
    }
}
