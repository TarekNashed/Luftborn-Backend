namespace Luftborn.Api.Dtos.Departments
{
    public class DepartmentDto
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public bool isActive { get; set; } = false;
    }
}
