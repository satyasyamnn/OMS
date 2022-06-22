using Microsoft.EntityFrameworkCore;
using OMS.Administration.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OMS.Administration.Infrasturcture.Persistence
{
    public interface IAdministrationDbContext
    {
        DbSet<Organization> Organizations { get; set; }
        DbSet<Contact> Contacts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
