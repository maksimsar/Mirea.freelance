namespace Mirea.freelance.backend.dto.TaskDTO;
using DomainTaskStatus = Mirea.freelance.backend.models.TaskStatus;

public class ChangeStatusDto
{
    public DomainTaskStatus NewStatus { get; set; }
}
