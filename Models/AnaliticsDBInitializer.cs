using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CourseWorkDO.Models
{
    public class AnaliticsDBInitializer : DropCreateDatabaseAlways<AnaliticsContext>
    {
        protected override void Seed(AnaliticsContext context)
        {
            base.Seed(context);
        }
    }
}
