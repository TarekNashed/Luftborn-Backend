namespace Luftborn.Api.Dtos.Departments
{
    public class DepartmentResponseDto
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public bool isActive { get; set; }
    }
}
