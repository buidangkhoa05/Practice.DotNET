using Domain.Common.Entity.Interface;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common.Entity
{
    public class EntityBase : IAuditable, IDeletable, IIdentifiable
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}
