using System.Linq;
using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    public class CommandsProfile : Profile
    {
        private readonly IMapper _mapper;

        public CommandsProfile(IMapper mapper)
        {
            _mapper = mapper;
        }

        public CommandsProfile()
        {
            // <source, target>
            CreateMap<Command, CommandReturnDto>()
                .ForMember(dest => dest.Instructions, opt => 
                    opt.MapFrom(src => src.Instructions.Description))
                .ForMember(dest => dest.PlatformId, opt =>
                    opt.MapFrom(src => src.Platform.Id))
                .ForMember(dest => dest.PlatformName, opt =>
                    opt.MapFrom(src => src.Platform.Name));

            CreateMap<CommandCreateDto, Platform>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.PlatformId))
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => src.PlatformName));

            CreateMap<CommandCreateDto, Command>()
                .ForMember(dest => dest.Instructions, opt => 
                    opt.MapFrom(src => new Instruction{ Description=src.Instructions }))
                .ForPath(dest => dest.Platform, opt =>
                    opt.MapFrom(src => _mapper.Map<CommandCreateDto, Platform>(src))) // try forMember to see if it ALSO works
                .ForMember(dest => dest.PlatformId, opt =>
                    opt.MapFrom(src => src.PlatformId));
                    
            // CreateMap<CommandUpdateDto, Command>()
            //     .ForMember(dest => dest.Instructions.Description, opt => 
            //         opt.MapFrom(src => src.Instructions))
            //     .ForMember(dest => dest.PlatformId, opt =>
            //         opt.MapFrom(src => src.PlatformId))
            //     .ForMember(dest => dest.Platform.Name, opt =>
            //         opt.MapFrom(src => src.PlatformName ));

            CreateMap<Platform, PlatformReturnDto>()
                .ForMember(dest => dest.Name, opt => 
                    opt.MapFrom(src => src.Name));

        }
    }
}


