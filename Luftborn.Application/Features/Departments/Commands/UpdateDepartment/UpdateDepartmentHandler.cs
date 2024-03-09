using AutoMapper;
using Luftborn.Application.Contracts.Persistence;
using Luftborn.Application.Model;
using Luftborn.Domain.Entities;
using MediatR;

namespace Luftborn.Application.Features.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand, HandlerResponse<DepartmentResponse>>
    {
        private readonly IAsyncRepository<Department> _asyncRepository;
        private readonly IMapper _mapper;
        public UpdateDepartmentHandler(IAsyncRepository<Department> asyncRepository, IMapper mapper)
        {
            _asyncRepository = asyncRepository ?? throw new ArgumentNullException(nameof(asyncRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<HandlerResponse<DepartmentResponse>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var departmentMapped = _mapper.Map<Department>(request);
            await _asyncRepository.UpdateAsync(departmentMapped);
            return DepartmentResponseData(request);
        }

        private HandlerResponse<DepartmentResponse> DepartmentResponseData(UpdateDepartmentCommand request)
        {
            return new HandlerResponse<DepartmentResponse>()
            {
                Data = _mapper.Map<DepartmentResponse>(request),
                IsSuccess = true
            };
        }
    }
}
