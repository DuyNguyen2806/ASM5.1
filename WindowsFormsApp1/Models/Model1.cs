using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WindowsFormsApp1.Models
{
    public partial class Model1 : DbContext
    {
        
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<LoaiSach> LoaiSaches { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
