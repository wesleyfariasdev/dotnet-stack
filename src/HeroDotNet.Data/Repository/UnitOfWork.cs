using HeroDotNet.Data.HeroContext;
using HeroDotNet.Domain.IRepository;
using Microsoft.EntityFrameworkCore.Storage;

namespace HeroDotNet.Data.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly HeroContextDb _dbContext;
    private IDbContextTransaction? _dbContextTransaction;

    public UnitOfWork(HeroContextDb dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task BeginTransactionAsync()
    {
        _dbContextTransaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
            if (_dbContextTransaction != null)
                await _dbContextTransaction.CommitAsync();
        }
        finally
        {
            if (_dbContextTransaction != null)
            {
                await _dbContextTransaction.DisposeAsync();
                _dbContextTransaction = null;
            }
        }
    }

    public async Task RollbackAsync()
    {
        if (_dbContextTransaction != null)
        {
            await _dbContextTransaction.RollbackAsync();
            await _dbContextTransaction.DisposeAsync();
            _dbContextTransaction = null;
        }
    }

    public void Dispose()
    {
        _dbContextTransaction?.Dispose();
        _dbContext.Dispose();
    }
}
