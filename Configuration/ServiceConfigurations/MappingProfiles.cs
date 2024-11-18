using AutoMapper;
using GladLogsApi.Models.Dtos;
using GladLogsApi.Models.Dtos.Message;
using GladLogsApi.Models.Entities;

namespace GladLogsApi.Configuration.ServiceConfigurations
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Chat, ChatDto>().ReverseMap();
            CreateMap<Chat, CreateChatDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();

            CreateMap<Week, WeekDto>().ReverseMap();
            CreateMap<Week, CreateWeekDto>().ReverseMap();

            CreateMap<Message, MessageDto>().ReverseMap();
            CreateMap<Message, CreateMessageDto>().ReverseMap();
            CreateMap<Message, ShortMessageDto>().ReverseMap();
        }
    }
}
