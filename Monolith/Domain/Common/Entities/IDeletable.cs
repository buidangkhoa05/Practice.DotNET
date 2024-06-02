namespace Domain.Common.Entities
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
    }
}
