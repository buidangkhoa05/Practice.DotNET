namespace Domain.Common.Entities
{
    public interface IIdentifiable<T>
    {
        public T Id { get; set; }
    }
}
