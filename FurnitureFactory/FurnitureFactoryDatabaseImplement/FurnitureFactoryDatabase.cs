using FurnitureFactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace FurnitureFactoryDatabaseImplement
{
    class FurnitureFactoryDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                //optionsBuilder.UseSqlServer(@"Data Source=WIN-7QPLO386PS9;Initial Catalog=FurnitureFactoryDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-1ORTU77;Initial Catalog=FurnitureFactoryDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Cost> Costs { set; get; }
        public virtual DbSet<Employee> Employees { set; get; }
        public virtual DbSet<Furniture> Furnitures { set; get; }
        public virtual DbSet<Payment> Payments { set; get; }
        public virtual DbSet<Purchase> Purchases { set; get; }
        public virtual DbSet<PurchaseFurniture> PurchaseFurnitures { set; get; }
        public virtual DbSet<User> Users { set; get; }
    }
}
