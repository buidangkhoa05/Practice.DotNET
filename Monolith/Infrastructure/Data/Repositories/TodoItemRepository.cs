using Domain.Entities;
using Domain.TodoLists.Repository;
using Infrastructure.Data.Common;
using System.Data.Entity;

namespace Infrastructure.Data.Repositories
{
    public class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
