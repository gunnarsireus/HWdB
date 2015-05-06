using HWdB.Model;
using System.Data.Entity;

namespace HWdB.DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<LtbDataSet> LtbDataSets { get; set; }

    }
}
