using CottageApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CottageAPI.Models
{
    public class CottageContext : DbContext
    {
         public CottageContext(DbContextOptions<CottageContext> options)
            : base(options)
        {
        }

        public DbSet<Cottage> CottageItems { get; set; }
    }
}
