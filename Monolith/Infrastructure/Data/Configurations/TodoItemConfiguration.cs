using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.TodoList)
                .WithMany(x => x.TodoItems)
                .HasForeignKey(x => x.TodoListId);
        }
    }
}
