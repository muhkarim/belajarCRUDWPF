using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using belajar_crud_wpf.Model;

namespace belajar_crud_wpf.MyContext
{
    public class myContext : DbContext
    {
        public myContext(): base("BelajarCRUDWPF") { }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Role> Roles { get; set; }

    }
}
