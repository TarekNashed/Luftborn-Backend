using AutoMapper;
using Luftborn.Api.Dtos.Departments;
using Luftborn.Api.Dtos.HandleResponses;
using Luftborn.Application.Features.Departments;
using Luftborn.Application.Features.Departments.Commands.AddDepartment;
using Luftborn.Application.Features.Departments.Commands.DeleteDepartment;
using Luftborn.Application.Features.Departments.Commands.UpdateDepartment;
using Luftborn.Application.Features.Departments.Queries.GetAllDepartments;
using Luftborn.Application.Features.Departments.Queries.GetDepartmentById;
using Luftborn.Application.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luftborn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public DepartmentController(IMediator mediator, IMapper mapper, IConfiguration configuration)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        [HttpGet]
        [Route("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartment()
        {

            var request = new GetAllDepartmentsQuery();
            var result = await _mediator.Send(request);
            var resultDto = _mapper.Map<ListHandleResponseCommandDto<List<DepartmentDto>>>(result);
            if (resultDto.IsSuccess)
                return resultDto.TotalDataCount>0 ? Ok(resultDto) : NotFound(resultDto);

            resultDto.Message = "Something went worng";
            return BadRequest(resultDto);
        }

        [HttpGet]
        [Route("GetDepartmentById/{departmentId}")]
        public async Task<IActionResult> GetDepartmentById(int departmentId)
        {
            var request = new GetDepartmentByIdQuery(Id: departmentId);
            var result = await _mediator.Send(request);
            var resultDto = _mapper.Map<HandleResponseCommandDto<DepartmentDto>>(result);
            if (resultDto.IsSuccess)
                return resultDto.IsSuccess ? Ok(resultDto) : NotFound(resultDto);
            return BadRequest("Something went worng");
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddDepartment([FromBody] AddDepartmentDto addDepartment)
        {
            var addDepartmentData = _mapper.Map<AddDepartmentCommand>(addDepartment);
            var result = await _mediator.Send(addDepartmentData);
            var resultDto = _mapper.Map<HandleResponseCommandDto<DepartmentResponseDto>>(result);
            return (result.IsSuccess) ? Ok(resultDto) : BadRequest("Somthing went wrong");
        }
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentDto updateDepartmentDto)
        {
            var updateDepartmentData = _mapper.Map<UpdateDepartmentCommand>(updateDepartmentDto);
            var result = await _mediator.Send(updateDepartmentData);
            var resultDto = _mapper.Map<HandleResponseCommandDto<DepartmentResponseDto>>(result);
            return (result.IsSuccess) ? Ok(resultDto) : BadRequest("Somthing went wrong");
        }
        [HttpPost]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> DeleteDepartment(int Id)
        {
            var request = new DeleteDepartmentHandlerQuery(Id: Id);
            var result = await _mediator.Send(request);
            var resultDto = _mapper.Map<HandleResponseCommandDto<DepartmentResponseDto>>(result);
            return (result.IsSuccess) ? Ok(resultDto) : BadRequest("Somthing went wrong");
        }
    }
}
