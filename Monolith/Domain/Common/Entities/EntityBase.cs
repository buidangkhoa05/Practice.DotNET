using System.ComponentModel.DataAnnotations;

namespace Domain.Common.Entities
{
    public interface IEntityBase : IAuditable, IDeletable, IIdentifiable
    {

    }

    public class EntityBase : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public int UpdatedBy { get; set; }
    }
}
