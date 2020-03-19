using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using belajar_crud_wpf.Model;

namespace belajar_crud_wpf.MyContext
{
    public class MyContext : DbContext
    {
        public MyContext(): base("BelajarCRUDWPF") { }
        public DbSet<Supplier> Suppliers { get; set; }


    }
}
