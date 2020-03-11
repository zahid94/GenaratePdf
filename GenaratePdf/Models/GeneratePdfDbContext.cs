using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GenaratePdf.Models
{
    public class GeneratePdfDbContext:DbContext
    {
        public GeneratePdfDbContext():base("Db")
        {

        }
        public DbSet<Customer> Customers { get; set; }
    }
}