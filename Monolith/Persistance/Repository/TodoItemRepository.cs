using Domain.Entities;
using Domain.TodoLists.Repository;
using Persistence.Common;
using System.Data.Entity;

namespace Persistence.Repository
{
    public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
