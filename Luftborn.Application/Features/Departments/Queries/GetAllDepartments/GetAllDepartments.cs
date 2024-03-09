using AutoMapper;
using Luftborn.Application.Contracts.Persistence;
using Luftborn.Application.Model;
using Luftborn.Domain.Entities;
using MediatR;

namespace Luftborn.Application.Features.Departments.Queries.GetAllDepartments
{
    public record GetAllDepartmentsQuery:IRequest<ListHandlerResponse<List<DepartmentResponse>>>;
    public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartmentsQuery, ListHandlerResponse<List<DepartmentResponse>>>
    {
        private readonly IAsyncRepository<Department> _repository;
        private readonly IMapper _mapper;

        public GetAllDepartmentsHandler(IAsyncRepository<Department> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ListHandlerResponse<List<DepartmentResponse>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _repository.GetAllAsync();
            var DepartmentsResponse = _mapper.Map<List<DepartmentResponse>>(departments);
            return GetResponse(DepartmentsResponse);
        }

        private static ListHandlerResponse<List<DepartmentResponse>> GetResponse(List<DepartmentResponse> departmentsResponse)
        {
            return new ListHandlerResponse<List<DepartmentResponse>>
            {
                Data = departmentsResponse,
                IsSuccess = true,
                TotalDataCount = departmentsResponse.Count
            };
        }
    }
}
