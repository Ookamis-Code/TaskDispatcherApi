using System.ComponentModel.DataAnnotations;

namespace TaskDispatcherApi.DTOs;

public class TaskCreatDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [RegularExpression("Low|Medium|High")]
    public string Priority { get; set; } = "Low";
}