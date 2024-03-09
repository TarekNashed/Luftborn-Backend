using AutoMapper;
using Luftborn.Api.Dtos;
using Luftborn.Application.Features.Users.Commands.RegisterUser;
using Luftborn.Application.Features.Users.Queries.UserLogin;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Luftborn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterUserDto registerUserDto)
        {
            var register = _mapper.Map<RegisterUserCommand>(registerUserDto);
            register.RoleName = "user";
            var result = await _mediator.Send(register);
            var resultDto = _mapper.Map<AuthResponseDto>(result);
            return resultDto.IsSuccess ? Ok(resultDto) : BadRequest("Some properties are not valid");
        }

        [HttpPost]
        [Route(nameof(RegisterAdmin))]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserDto registerUserDto)
        {
            var register = _mapper.Map<RegisterUserCommand>(registerUserDto);
            register.RoleName = "admin";
            var result = await _mediator.Send(register);
            var resultDto = _mapper.Map<AuthResponseDto>(result);
            return resultDto.IsSuccess ? Ok(resultDto) : BadRequest("Some properties are not valid");
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var login = _mapper.Map<UserLoginQuery>(loginDto);
            var result = await _mediator.Send(login);
            var resultDto = _mapper.Map<AuthResponseDto>(result);
            return resultDto.IsSuccess ? Ok(resultDto) : BadRequest("Some properties are not valid");
        }
    }
}
