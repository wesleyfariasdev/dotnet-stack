using HeroDotNet.Data.HeroContext;
using HeroDotNet.Domain.IRepository;
using Microsoft.EntityFrameworkCore.Storage;

namespace HeroDotNet.Data.Repository;

internal class UnitOfWork(HeroContextDb dbContext,
                          IDbContextTransaction dbContextTransaction) : IUnitOfWork
{
    public async Task BeginTransactionAsync() =>
           await dbContext.Database.BeginTransactionAsync();

    public async Task CommitAsync()
    {
        try
        {
            await dbContext.SaveChangesAsync();
            if (dbContextTransaction != null)
                await dbContextTransaction.CommitAsync();
        }
        finally
        {
            if (dbContextTransaction != null)
                await dbContextTransaction.DisposeAsync();
        }
    }

    public void Dispose()
    {
        dbContextTransaction?.Dispose();
        dbContext.Dispose();
    }

    public async Task RollbackAsync()
    {
        if (dbContextTransaction != null)
        {
            await dbContextTransaction.RollbackAsync();
            await dbContextTransaction.DisposeAsync();
        }
    }
}
