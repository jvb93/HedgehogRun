using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HedgehogRun.Entities;
using Microsoft.EntityFrameworkCore;

namespace HedgehogRun.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public ApplicationDbContext()
        { }

        public DbSet<HogLog> HogLogs { get; set; }
        public DbSet<Record> Records { get; set; }
    }
}
