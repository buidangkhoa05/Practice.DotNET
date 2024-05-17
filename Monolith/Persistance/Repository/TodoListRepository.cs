using Domain.Entity;
using Domain.Persistence.Repository;
using Persistence.Common;
using System.Data.Entity;

namespace Persistence.Repository
{
    public class TodoListRepository : GenericRepository<TodoList>, ITodoListRepository
    {
        public TodoListRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
