namespace ArcDemo2024.Hotel.Shared.UnitOfWork;

public interface IUnitOfWork
{
    Task Commit(CancellationToken cancellationToken);
}