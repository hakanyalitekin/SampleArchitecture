using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SampleArchitecture.Api.Models;
using SampleArchitecture.Api.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IContactService _contactService;

        public ContactController(IConfiguration configuration, IContactService contactService)
        {
            _configuration = configuration;
            _contactService = contactService;
        }
        [HttpGet]
        public string Get()
        {
            return _configuration["ReadMe"].ToString();
        }

        [HttpGet("id")]
        public ContactDTO GetContactById(int id)
        {
            return _contactService.GetContactById(id);
        }

        [HttpPost]
        public ContactDTO CreateContact (ContactDTO contact)
        {
            //FluentValidation'u test etmek için ekledik.
            return contact;
        }
    }
}
