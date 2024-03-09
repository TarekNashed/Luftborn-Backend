using Luftborn.Application.Model;
using MediatR;

namespace Luftborn.Application.Features.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommand:IRequest<HandlerResponse<DepartmentResponse>>
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public bool isActive { get; set; }
    }
}
