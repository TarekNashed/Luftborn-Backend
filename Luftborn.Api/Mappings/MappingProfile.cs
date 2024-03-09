using Luftborn.Application.Features.Users.Commands.RegisterUser;
using Luftborn.Application.Features.Users.Queries.UserLogin;
using Luftborn.Application.Features.Users;
using Luftborn.Api.Dtos;
using AutoMapper;
using Luftborn.Application.Model;
using Luftborn.Application.Features.Departments.Commands.AddDepartment;
using Luftborn.Api.Dtos.Departments;
using Luftborn.Api.Dtos.HandleResponses;
using Luftborn.Application.Features.Departments;
using Luftborn.Application.Features.Departments.Commands.UpdateDepartment;

namespace Luftborn.Api.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            //User Identity
            CreateMap<RegisterUserCommand, RegisterUserDto>().ReverseMap();
            CreateMap<UserManagerResponse, AuthResponseDto>().ReverseMap();
            CreateMap<UserLoginQuery, LoginDto>().ReverseMap();

            // Get Departments
            CreateMap<ListHandleResponseCommandDto<List<DepartmentDto>>, ListHandlerResponse<List<DepartmentResponse>>>().ReverseMap();
            CreateMap<HandleResponseCommandDto<DepartmentDto>,HandlerResponse<DepartmentResponse>>().ReverseMap();
            CreateMap<DepartmentDto, DepartmentResponse>().ReverseMap();

            // Add Department.
            CreateMap<AddDepartmentCommand, AddDepartmentDto>().ReverseMap();
            CreateMap<DepartmentResponse, DepartmentResponseDto>().ReverseMap();
            CreateMap<HandlerResponse<DepartmentResponse>, HandleResponseCommandDto<DepartmentResponseDto>>().ReverseMap();

            // Update Department.
            CreateMap<UpdateDepartmentCommand, UpdateDepartmentDto>().ReverseMap();
        }
    }
}
