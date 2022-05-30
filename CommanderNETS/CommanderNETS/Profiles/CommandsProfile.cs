using AutoMapper;
using CommanderNETS.Dtos;
using CommanderNETS.Models;

namespace CommanderNETS.Profiles
{
    public class CommandsProfiles : Profile
    {
        public CommandsProfiles()
        {
            //Source -> Target
            CreateMap<Command, CommandReadDto>().ReverseMap();
            CreateMap<Command, CommandCreateDto>().ReverseMap();
            CreateMap<Command, CommandUpdateDto>().ReverseMap();
        }
    }
}
