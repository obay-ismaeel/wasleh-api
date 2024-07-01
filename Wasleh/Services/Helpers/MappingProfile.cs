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
        
        CreateMap<RequestAnswerDto, Answer>();
        CreateMap<Answer, ResponseAnswerDto>();

        CreateMap<RequestLectureDto, Lecture>();
        CreateMap<Lecture, ResponseLectureDto>();

        CreateMap<RequestUniversityDto, University>();
        CreateMap<University, ResponseUniversityDto>();

        CreateMap<RequestCourseDto, Course>();
        CreateMap<Course, ResponseCourseDto>();

        CreateMap<RequestFacultyDto, Faculty>();
        CreateMap<Faculty, ResponseFacultyDto>();

        CreateMap<RequestReplyDto, Reply>();
        CreateMap<Reply, ResponseReplyDto>();
    }
}
