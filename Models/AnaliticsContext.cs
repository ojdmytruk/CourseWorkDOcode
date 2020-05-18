using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkDO.Models
{
    public class AnaliticsContext: DbContext
    {
        public DbSet<Analitics> AnaliticsTable { get; set; }

        //public AnaliticsContext()
        //{
        //    Database.SetInitializer<AnaliticsContext>(new AnaliticsDBInitializer());
        //}


        public AnaliticsContext(DbContextOptions<AnaliticsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
