using Domain.Entity;
using Domain.Persistence.Repository;
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
