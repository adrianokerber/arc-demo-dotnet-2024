using Microsoft.EntityFrameworkCore;

namespace ArcDemo2024.Hotel.Shared.UnitOfWork;

public interface IEfDbContextFactory<T> where T : DbContext
{
    Task<T> CriarAsync(string codigoTenant);
}