using System.Data.Entity;
using Ubs.Equities.EntityFramework.Entities;

namespace Ubs.Equities.EntityFramework.DbContexts
{
    public interface IFoundDbContext
    {
        #region Properties

        IDbSet<Found> Founds { get; }

        IDbSet<Stock> Stocks { get; }

        #endregion
        #region Public methods

        int SaveChanges();

        #endregion
    }
}