using Microsoft.EntityFrameworkCore;

namespace ArcDemo2024.Hotel.Shared.UnitOfWork;

public interface IEfDbContextAccessor<T> : IDisposable where T : DbContext
{
    void Register(T context);
    T Get();
    void Clear();
}