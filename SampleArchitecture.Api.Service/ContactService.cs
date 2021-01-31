using AutoMapper;
using SampleArchitecture.Api.Data.Models;
using SampleArchitecture.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArchitecture.Api.Service
{
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;

        public ContactService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public ContactDTO GetContactById(int id)
        {
            Contact dbContact = GetDummyContact(); //ToDo:Veritabanından ilgili verilerin geldiğini varsayıyoruz. Şimdilik mock veri kullanacağız.

            //return _mapper.Map(dbContact, new ContactDTO()); //Farklı kullanım örneği

            return _mapper.Map<ContactDTO>(dbContact);
        }
        
        private Contact GetDummyContact() //Test data normalde olmaması gereken metot.
        {
            return new Contact() { Id = 1, FirstName = "Hakan", LastName = "Yalitekin"};
        }
    }
}
