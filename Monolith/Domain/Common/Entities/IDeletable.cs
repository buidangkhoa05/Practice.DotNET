namespace Domain.Common.Entity
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
    }
}
