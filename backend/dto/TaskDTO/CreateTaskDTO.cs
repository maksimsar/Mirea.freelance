namespace Mirea.freelance.backend.dto.TaskDTO;

public class CreateTaskDto
{
    public int    OrderId           { get; set; }
    public int    AssigneeStudentId { get; set; }
    public string Title             { get; set; } = string.Empty;
    public string Description       { get; set; } = string.Empty;
}