namespace Espresso.Domain.Infrastructure
{
    public interface IEntity<TKey, TEntity>
        where TKey : struct
        where TEntity : class, IEntity<TKey, TEntity>
    {
        public TKey Id { get; }
    }
}
