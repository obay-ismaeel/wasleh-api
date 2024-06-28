using AutoMapper;
using Wasleh.Domain.Entities;
using Wasleh.Dtos;

namespace Wasleh.Services.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<QuestionRequestDto, Question>();
    }
}
