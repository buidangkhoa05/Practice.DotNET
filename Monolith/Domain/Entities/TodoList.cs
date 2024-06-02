using Domain.Common.Entities;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class TodoList : EntityBase
    {
        [Required]
        public string Title { get; set; } = null!;
        public bool IsCompleted { get; set; } = false;
        public PiorityLevel PiorityLevel { get; set; } = PiorityLevel.Medium;
        public virtual ICollection<TodoItem> TodoItems { get; set; } = null!; // Navigation property
    }
}
