using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Customers, CustomerDto>(); });

            IMapper mapper = config.CreateMapper();
            var source = new Customers();
            var dest = mapper.Map<Customers, CustomerDto>(source);

        }
    }
}