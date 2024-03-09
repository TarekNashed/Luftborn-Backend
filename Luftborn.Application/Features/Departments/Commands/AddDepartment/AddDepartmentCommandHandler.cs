using AutoMapper;
using Luftborn.Application.Contracts.Persistence;
using Luftborn.Application.Model;
using Luftborn.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Luftborn.Application.Features.Departments.Commands.AddDepartment
{
    public class AddDepartmentCommandHandler : IRequestHandler<AddDepartmentCommand, HandlerResponse<DepartmentResponse>>
    {
        private readonly IAsyncRepository<Department> _repository;
        private readonly ILogger<AddDepartmentCommandHandler> _logger;
        private readonly IMapper _mapper;
        public AddDepartmentCommandHandler(IAsyncRepository<Department> repository, 
            ILogger<AddDepartmentCommandHandler> logger,IMapper mapper)
        {
            this._repository = repository;
            this._logger = logger;
            this._mapper = mapper;
        }
        public async Task<HandlerResponse<DepartmentResponse>> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            var departmentMapped = _mapper.Map<Department>(request);
            var result = await _repository.AddAsync(departmentMapped);
            return DepartmentResponseData(result);
        }

        private HandlerResponse<DepartmentResponse> DepartmentResponseData(Department result)
        {
            return new HandlerResponse<DepartmentResponse>()
            {
                Data = _mapper.Map<DepartmentResponse>(result),
                IsSuccess = true
            };
        }
    }
}
