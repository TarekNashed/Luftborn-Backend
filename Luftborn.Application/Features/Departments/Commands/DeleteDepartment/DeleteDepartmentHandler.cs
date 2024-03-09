using AutoMapper;
using Luftborn.Application.Contracts.Persistence;
using Luftborn.Application.Model;
using Luftborn.Domain.Entities;
using MediatR;

namespace Luftborn.Application.Features.Departments.Commands.DeleteDepartment
{
    public record DeleteDepartmentHandlerQuery(int Id):IRequest<HandlerResponse<DepartmentResponse>>;
    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentHandlerQuery, HandlerResponse<DepartmentResponse>>
    {
        private readonly IAsyncRepository<Department> _asyncRepository;
        private readonly IMapper _mapper;
        public DeleteDepartmentHandler(IAsyncRepository<Department> asyncRepository, IMapper mapper)
        {
            _asyncRepository = asyncRepository ?? throw new ArgumentNullException(nameof(asyncRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<HandlerResponse<DepartmentResponse>> Handle(DeleteDepartmentHandlerQuery request, CancellationToken cancellationToken)
        {
            var department =await _asyncRepository.GetByIdAsync(request.Id);
            department.isActive = false;
            await _asyncRepository.UpdateAsync(department);
            return DepartmentResponseData(department);
        }

        private HandlerResponse<DepartmentResponse> DepartmentResponseData(Department department)
        {
            return new HandlerResponse<DepartmentResponse>()
            {
                IsSuccess = true,
                Message = $"Department with Id {department.ID} and Name {department.Name} is removed successfully"
            };
        }
    }
}
