using AutoMapper;
using Luftborn.Application.Features.Departments;
using Luftborn.Application.Features.Departments.Commands.AddDepartment;
using Luftborn.Application.Features.Departments.Commands.UpdateDepartment;
using Luftborn.Application.Features.Users.Commands.RegisterUser;
using Luftborn.Domain.Entities;
using Luftborn.Domain.IdentityEntities;

namespace Luftborn.Application.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, RegisterUserCommand>().ReverseMap();
            CreateMap<AddDepartmentCommand, Department>().ReverseMap();
            CreateMap<UpdateDepartmentCommand, Department>().ReverseMap();
            CreateMap<UpdateDepartmentCommand, DepartmentResponse>().ReverseMap();
            CreateMap<Department, DepartmentResponse>().ReverseMap();

        }
    }
}
