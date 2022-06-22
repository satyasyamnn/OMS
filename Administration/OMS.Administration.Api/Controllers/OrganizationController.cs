using Microsoft.AspNetCore.Mvc;
using OMS.Administration.Domain.Entities;
using OMS.Administration.Infrasturcture.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> GetOrganization()
        {
            Organization organization = new Organization
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Organization" + Guid.NewGuid().ToString(),
                Email = "s@s.com",
                TaxIdenfier = Guid.NewGuid().ToString(),
            };
            organization.Contacts = new List<Contact>();
            Contact Owner = new Contact
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Satya SYAM",
                Email = "ss@s.com",
                PhoneNumber = "123-456-789",
                //ContactAddress = new Address
                //{
                //    Address1 = "a1",
                //    Address2 = "21",
                //    City = "123",
                //    Country = "123",
                //    LandMark = "234",
                //    PinCode = 123,
                //    State = "asdf",
                //    StreetName = "Asdf"
                //}
            };
            organization.Contacts.Add(Owner);
            await _organizationService.SaveOrganizationAsync(organization);
            return new OkResult();
        }
    }
}
