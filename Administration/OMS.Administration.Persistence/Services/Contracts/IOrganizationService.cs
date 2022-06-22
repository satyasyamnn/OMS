using OMS.Administration.Domain.Entities;
using System.Threading.Tasks;

namespace OMS.Administration.Infrasturcture.Services.Contracts
{
    public interface IOrganizationService
    {
        Task SaveOrganizationAsync(Organization organization);
    }
}
