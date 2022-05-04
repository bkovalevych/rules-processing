using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RulesExercise.Application.Projects.Commands.PostProject;
using RulesExercise.Application.Models;
using System.Text;

namespace RulesExercise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ProjectsController> _logger;
        private readonly ISender _sender;

        public ProjectsController(ILogger<ProjectsController> logger, IMapper mapper, ISender sender)
        {
            _mapper = mapper;
            _logger = logger;
            _sender = sender;
        }

        [HttpPost]
        public async Task<string> PostProjects([FromBody] ProjectsRequestDto projectsRequestDto)
        {
            var commands = _mapper.Map<List<PostProjectCommand>>(projectsRequestDto.Projects);
            var sb = new StringBuilder();
            foreach(var command in commands)
            {
                var result = await _sender.Send(command);
                sb.Append(command.Name)
                    .Append(' ')
                    .AppendLine(result.ToString());
            }

            return sb.ToString();
        }
    }
}