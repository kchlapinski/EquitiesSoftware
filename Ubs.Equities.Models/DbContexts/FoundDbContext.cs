using System.Data.Entity;
using Ubs.Equities.EntityFramework.Entities;

namespace Ubs.Equities.EntityFramework.DbContexts
{
    public class FoundDbContext : DbContext, IFoundDbContext
    {
        #region Ctors

        public FoundDbContext() : base(Constants.DefaultConnection)
        {
        }

        #endregion
        #region IFoundDbContext Members

        public IDbSet<Found> Founds { get; set; }

        public IDbSet<Stock> Stocks { get; set; }

        #endregion
    }
}