using TaskStatus = Mirea.freelance.backend.models.TaskStatus;   

namespace Mirea.freelance.backend.dto.TaskDTO;

public record TaskListItemDto(
    int        Id,
    string     Title,
    string     Description,
    TaskStatus Status,         
    int        AssigneeStudentId,
    string     AssigneeName
);
