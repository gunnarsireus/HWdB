using HWdB.Model;
using System.Data.Entity;

namespace HWdB.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base(@"Data Source=(LocalDb)\v11.0;AttachDBFilename=|DataDirectory|\HWdB.mdf;Integrated Security=true;MultipleActiveResultSets=True")
        {
            // Find out the connection string being used
            //Debug.Write(Database.Connection.ConnectionString);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<LtbDataSet> LtbDataSets { get; set; }

    }
}
