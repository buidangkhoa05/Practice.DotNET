using Domain.Common.Persistence;
using Domain.Entities;

namespace Domain.TodoLists.Repository
{
    public interface ITodoItemRepository : IGenericRepository<TodoItem>
    {
    }
}
