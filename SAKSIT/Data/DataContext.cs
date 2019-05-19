using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAKSIT.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Models.customer> customer { get; set; }
        public DbSet<Models.product> product { get; set; }
        public DbSet<Models.order_list> order_list { get; set; }
        public DbSet<Models.order_detail> order_detail { get; set; }
    }
}
