using Microsoft.AspNetCore.Mvc;
using OMS.Administration.Infrasturcture.Services.Contracts;

namespace OMS.Administration.Api.Controllers
{
    [Route("api/v1/Organization")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private IOrganizationService _organizationService;
        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }
    }
}
