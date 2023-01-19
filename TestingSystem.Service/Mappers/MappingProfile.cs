using AutoMapper;
using TestingSystem.Domain.Entities.Attachments;
using TestingSystem.Domain.Entities.Courses;
using TestingSystem.Domain.Entities.Quizes;
using TestingSystem.Domain.Entities.Users;
using TestingSystem.Service.DTOs.Attachments;
using TestingSystem.Service.DTOs.Courses;
using TestingSystem.Service.DTOs.Quizes;
using TestingSystem.Service.DTOs.Users;

namespace TestingSystem.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // user
            CreateMap<User, UserForCreationDTO>().ReverseMap();
            CreateMap<User, UserForViewDTO>().ReverseMap();
            CreateMap<User, UserForUpdateDTO>().ReverseMap();

            // course
            CreateMap<Course, CourseForViewDTO>().ReverseMap();
            CreateMap<Course, CourseForCreationDTO>().ReverseMap();

            // quiz

            CreateMap<Quiz, QuizForCreationDTO>().ReverseMap();
            CreateMap<Quiz, QuizForViewDTO>().ReverseMap();

            // question
            CreateMap<Question, QuestionForCreationDTO>().ReverseMap();
            CreateMap<Question, QuestionForViewDTO>().ReverseMap();

            // answer
            CreateMap<Answer, AnswerForCreationDTO>().ReverseMap();
            CreateMap<Answer, AnswerForViewDTO>().ReverseMap();

            // quizresult
            CreateMap<QuizResult, QuizResultForCreationDTO>().ReverseMap();
            CreateMap<QuizResult, QuizResultForViewDTO>().ReverseMap();

            // attachment
            CreateMap<Attachment, AttachmentForCreationDTO>().ReverseMap();
            CreateMap<Attachment, AttachmentForViewDTO>().ReverseMap();

            // solved questions
            CreateMap<SolvedQuestion, SolvedQuestionForCreationDTO>().ReverseMap();
            CreateMap<SolvedQuestion, SolvedQuestionForViewDTO>().ReverseMap();
        }

    }
}
