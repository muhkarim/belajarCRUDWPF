﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace belajar_crud_wpf.Model
{
    [Table("Tb_M_Supplier")] // anotasi nama tabel
    public class Supplier
    {
        [Key]
        public int Id {get; set;}
        public string Name { get; set; }
        public string Address { get; set; }

        public Supplier()
        {
            
        }

        public Supplier(String name, String address)
        {
            this.Name = name;
            this.Address = address;
        }
    }
}
