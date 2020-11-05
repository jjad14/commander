using System.Linq;
using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // <source, target>
            CreateMap<Command, CommandReturnDto>()
                .ForMember(dest => dest.Instructions, opt => 
                    opt.MapFrom(src => src.Instructions.Description))
                .ForMember(dest => dest.Platform, opt =>
                    opt.MapFrom(src => src.Platform.Name));

            CreateMap<CommandCreateDto, Command>()
                .ForMember(dest => dest.Instructions, opt => 
                    opt.MapFrom(src => new Instruction{ Description=src.Instructions}))
                .ForMember(dest => dest.Platform, opt =>
                    opt.MapFrom(src => new Platform{ Name=src.Platform }));
                    
            CreateMap<CommandUpdateDto, Command>()
                .ForMember(dest => dest.Instructions, opt => 
                    opt.MapFrom(src => new Instruction{Description=src.Instructions}))
                .ForMember(dest => dest.Platform, opt =>
                    opt.MapFrom(src => new Platform{ Id=src.PlatformId, Name=src.Platform }));

            CreateMap<Command, CommandUpdateDto>()
                .ForMember(dest => dest.Instructions, opt => 
                    opt.MapFrom(src => src.Instructions.Description))
                .ForMember(dest => dest.Platform, opt =>
                    opt.MapFrom(src => src.Platform.Name))
                .ForMember(dest => dest.PlatformId, opt =>
                    opt.MapFrom(src => src.PlatformId));

            CreateMap<Platform, PlatformReturnDto>()
                .ForMember(dest => dest.Name, opt => 
                    opt.MapFrom(src => src.Name));

        }
    }
}