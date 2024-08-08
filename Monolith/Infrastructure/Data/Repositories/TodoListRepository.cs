using Domain.Common.PagedList;
using Domain.Entities;
using Domain.TodoLists.Repository;
using Infrastructure.Common.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Infrastructure.Data.Repositories
{
    public class TodoListRepository : GenericRepository<TodoList>, ITodoListRepository
    {
        public TodoListRepository(DbContext dbContext) : base(dbContext)
        {
        }


    }
}
