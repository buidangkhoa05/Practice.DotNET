using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common.Entities
{
    public interface IEntityBase<TKey> : IAuditable<TKey>, IDeletable, IIdentifiable<TKey>
    {

    }

    public class EntityBase<TKey> : IEntityBase<TKey>
        where TKey : notnull
    {
        [Key]
        public virtual required TKey Id { get; set; } 
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public TKey? CreatedBy { get; set; } = default;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public TKey? UpdatedBy { get; set; } = default;
    }
}
