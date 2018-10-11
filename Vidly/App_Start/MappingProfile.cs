using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //var config = new MapperConfiguration(cfg => { cfg.CreateMap<Customers, CustomerDto>(); });

            //IMapper mapper = config.CreateMapper();
            //var source = new Customers();
            //var dest = mapper.Map<Customers, CustomerDto>(source);
            // Domain to Dto
            Mapper.CreateMap<Customers, CustomerDto>();
            Mapper.CreateMap<Movies, MovieDto>();

            // Dto to Domain
            Mapper.CreateMap<CustomerDto, Customers>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<MovieDto, Movies>()
                .ForMember(c => c.Id, opt => opt.Ignore());

        }
    }
}