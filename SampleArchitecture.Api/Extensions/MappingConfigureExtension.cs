using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SampleArchitecture.Api.Data.Models;
using SampleArchitecture.Api.Models;

namespace SampleArchitecture.Api.Extensions
{
    public static class MappingConfigureExtension
    {
        //Startup.cs bizden IServiceCollection bekleyecektir ondan sebep IServiceCollection dönüyoruz.
        public static IServiceCollection ConfigureMapping(this IServiceCollection service)
        {
            var mappingConfig = new MapperConfiguration(i => i.AddProfile(new AutoMapperMappingProfile())); //Aşağıda özelleştirdiğimiz profilimizi ekliyoruz.

            IMapper mapper = mappingConfig.CreateMapper(); // Özelleştirilmiş profilimizle beraber MappingConfiguration'umuzu create ediyoruz.

            service.AddSingleton(mapper); //Servisimize ekleyip return ile dönüyoruz.

            return service;
        }
    }

    public class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile()
        {
            CreateMap<Contact, ContactDTO>() //Contact'ı ContactDTO'ya çevir
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.FirstName + " " + z.LastName)) // Çeviriken nelere dikkat etmesi gerketiğini söylüyoruz
                //.ForMember(x=>x.Id, y=>y.MapFrom(z=>z.Id)) // Property isimlari aynıysa gerek yok.
                .ReverseMap();  //ContactDTO'yu da  Contact'a çevirebilme özelliği ekliyoruz.
        }
    }
}
