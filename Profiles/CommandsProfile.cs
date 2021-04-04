using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //Source to taget
            CreateMap<Command, CommandReadDto>();
            //Target to Source
            CreateMap<CommandCreateDto, Command>();
            //put request
            CreateMap<CommandUpdateDto, Command>();
            //Patch request
            CreateMap<Command, CommandUpdateDto>();
        }
    }
}