using Domain.Common.Entity;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
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
