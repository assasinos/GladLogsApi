using AutoMapper;
using GladLogsApi.Models.Dtos;
using GladLogsApi.Models.Entities;

namespace GladLogsApi.Configuration.ServiceConfigurations
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Chat, ChatDto>();
            CreateMap<Chat, CreateChatDto>();

            CreateMap<User, UserDto>();
            CreateMap<User, CreateUserDto>();

            CreateMap<Week, WeekDto>();
            CreateMap<Week, CreateWeekDto>();

            CreateMap<Message, MessageDto>();
            CreateMap<Message, CreateMessageDto>();
        }
    }
}
