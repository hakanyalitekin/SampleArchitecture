using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SampleArchitecture.Api.Models;
using SampleArchitecture.Api.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SampleArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IContactService _contactService;
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IConfiguration configuration, IContactService contactService, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _contactService = contactService;
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet("TestHttpClientBaseAddress")]
        public string TestHttpClientBaseAddress()
        {
            var client = _httpClientFactory.CreateClient("garantiApi"); // Startup içerisindeki isimlendirme ile aynı olmalı.

            return client.BaseAddress.ToString(); //Oluşup oluşmadığını test etmek için.
        }

        [HttpGet]
        public string Get()
        {
            return _configuration["ReadMe"].ToString();
        }

        [ResponseCache(Duration = 10)] //Saniye 
        [HttpGet("id")]
        public ContactDTO GetContactById(int id)
        {
            return _contactService.GetContactById(id);
        }

        [HttpPost]
        public ContactDTO CreateContact(ContactDTO contact)
        {
            //FluentValidation'u test etmek için ekledik.
            return contact;
        }
    }
}
