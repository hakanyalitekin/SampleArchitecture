using SampleArchitecture.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArchitecture.Api.Service
{
    public interface IContactService
    {
        public ContactDTO GetContactById(int id);
    }
}
