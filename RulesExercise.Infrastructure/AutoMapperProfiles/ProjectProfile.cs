using AutoMapper;
using RulesExercise.Application.Projects.Commands.PostProject;
using RulesExercise.Domain.Entities;
using static RulesExercise.Application.Models.ProjectsRequestDto;

namespace RulesExercise.Infrastructure.AutoMapperProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<PostProjectCommand, Project>()
                .ReverseMap();
            CreateMap<ProjectDto, PostProjectCommand>()
                .ForMember(it => it.CreatedAt, conf => conf.MapFrom(it =>
                DateTimeOffset.FromUnixTimeSeconds(it.CreatedAt)))
                .ForMember(it => it.ModifiedAt, conf => conf.MapFrom(it =>
                DateTimeOffset.FromUnixTimeSeconds(it.ModifiedAt)))
                .ReverseMap();

            CreateMap<ProjectDto, Project>()
                .ForMember(it => it.CreatedAt, conf => conf.MapFrom(it =>
                DateTimeOffset.FromUnixTimeSeconds(it.CreatedAt)))
                .ForMember(it => it.ModifiedAt, conf => conf.MapFrom(it =>
                DateTimeOffset.FromUnixTimeSeconds(it.ModifiedAt)))
                .ReverseMap();
        }
    }
}
