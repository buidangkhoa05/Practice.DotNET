using Domain.Common.PagedList;
using Domain.Common.Persistence;
using Domain.Common.Queries.Interface;
using Domain.Entities;
using Domain.TodoLists.Repository;

namespace Application.TodoLists.Queries.GetTodoList
{
    public class GetTodoListPagingQuery :
        IPagingQuery<TodoList>,
        IHasStringOrderBy
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; } = null;
    }

    internal class GetTodoListPagingQueryHandler : IPagingQueryHandler<GetTodoListPagingQuery, TodoList>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTodoListPagingQueryHandler(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IPagedList<TodoList>> HandleAsync(GetTodoListPagingQuery query, CancellationToken cancellationToken = default)
        {
            var todoLists = await _unitOfWork.Instance<ITodoListRepository>()
                .SearchAsync(query);

            return todoLists;
        }

    }

}
