using AutoMapper;
using Wasleh.Domain.Entities;
using Wasleh.Dtos.Incoming;
using Wasleh.Dtos.Outgoing;

namespace Wasleh.Services.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RequestQuestionDto, Question>();
        CreateMap<Question, ResponseQuestionDto>();
        
        CreateMap<Answer, ResponseAnswerDto>();
        CreateMap<RequestAnswerDto, Answer>();

        CreateMap<Lecture, ResponseLectureDto>();
        CreateMap<RequestLectureDto, Lecture>();
    }
}
