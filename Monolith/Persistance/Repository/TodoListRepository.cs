using Domain.Common.PagedList;
using Domain.Entities;
using Domain.TodoLists.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Persistence.Common;

namespace Persistence.Repository
{
    public class TodoListRepository : GenericRepository<TodoList>, ITodoListRepository
    {
        public TodoListRepository(DbContext dbContext) : base(dbContext)
        {
        }

      
    }
}
