using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
    {
        public void Configure(EntityTypeBuilder<TodoList> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.TodoItems)
                .WithOne(x => x.TodoList)
                .HasForeignKey(x => x.TodoListId);
        }
    }
}
