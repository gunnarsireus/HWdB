using HWdB.Model;
using System.Configuration;
using System.Data.Entity;

namespace HWdB.DataAccess
{
    public class DataContext : DbContext
    {
        readonly static string Cs = ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;
        public DataContext()
            //: base(@"Data Source=(LocalDb)\v11.0;AttachDBFilename=|DataDirectory|\HWdB.mdf;Integrated Security=true;MultipleActiveResultSets=True")
            : base(Cs)
        {
            //http://www.entityframeworktutorial.net/code-first/database-initialization-strategy-in-code-first.aspx
            Database.SetInitializer<DataContext>(new DropCreateDatabaseIfModelChanges<DataContext>());
            // Find out the connection string being used
            //Debug.Write(Database.Connection.ConnectionString);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<LtbDataSet> LtbDataSets { get; set; }
        public DbSet<HwNumber> HwNumbers { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }

    }
}
