using AutoMapper;
using Luftborn.Application.Contracts.Persistence;
using Luftborn.Application.Model;
using Luftborn.Domain.Entities;
using MediatR;

namespace Luftborn.Application.Features.Departments.Queries.GetDepartmentById
{
    public record GetDepartmentByIdQuery(int Id):IRequest<HandlerResponse<DepartmentResponse>>;
    public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentByIdQuery, HandlerResponse<DepartmentResponse>>
    {
        private readonly IAsyncRepository<Department> _repository;
        private readonly IMapper _mapper;

        public GetDepartmentByIdHandler(IAsyncRepository<Department> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<HandlerResponse<DepartmentResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _repository.GetByIdAsync(request.Id);
            var departmentsResponse = _mapper.Map<DepartmentResponse>(department);
            return GetResponseById(departmentsResponse);
        }

        private static HandlerResponse<DepartmentResponse> GetResponseById(DepartmentResponse departmentResponse)
        {
            return new ListHandlerResponse<DepartmentResponse>
            {
                Data = departmentResponse,
                IsSuccess = true
            };
        }
    }
}
