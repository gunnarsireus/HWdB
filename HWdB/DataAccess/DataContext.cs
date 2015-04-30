using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

using HWdB.Model;

namespace HWdB.DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

    }
}
