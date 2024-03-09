using AutoMapper.Configuration.Annotations;

namespace Luftborn.Application.Features.Departments
{
    public class DepartmentResponse
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public bool isActive { get; set; } = false;

    }
}
