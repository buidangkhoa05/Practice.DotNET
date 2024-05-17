using Domain.Common.Entity;

namespace Domain.Entity
{
    public class TodoItem : EntityBase
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsCompleted { get; set; }
        public int TodoListId { get; set; }
        public TodoList TodoList { get; set; } = null!; // Navigation property
    }
}
