using Luftborn.Application.Model;
using MediatR;

namespace Luftborn.Application.Features.Departments.Commands.AddDepartment
{
    public class AddDepartmentCommand : IRequest<HandlerResponse<DepartmentResponse>>
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public bool isActive { get; set; }
    }
}
