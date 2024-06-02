using Domain.Common.PagedList;
using Domain.Common.Persistence;
using Domain.Entities;

namespace Domain.TodoLists.Repository
{
    public interface ITodoListRepository : IGenericRepository<TodoList>
    {
        public Task<IPagedList<TodoList>> SearchAsync(GetTodoListPagingQuery query);
    }
}
