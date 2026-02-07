using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskDispatcherApi.Models;

public class TaskItem
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Task Title is Mandatory")]
    [StringLength(100, MinimumLength = 3)]
    public string Title {get; set; } = string.Empty;
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    public bool IsDispatched { get; set; }
    [RegularExpression("Low|Medium|High", ErrorMessage = "Priority must be Low, Medium, or High")]
    public string Priority { get; set; } = "Low";
}