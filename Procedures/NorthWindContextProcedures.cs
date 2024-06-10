using Microsoft.EntityFrameworkCore;

namespace northwind.MYSQL.Procedures
{
    /// <summary>
    /// DB Context class for storedprocedures. It will be inherit from DbContext class
    /// </summary>
    public class NorthWindContextProcedures : DbContext
    {
        /// <summary>
        /// DB Set for Cust_order_history stored procedure
        /// </summary>
        public virtual DbSet<CustomerOrderHistory> CustOrderHistories { get; set; } = null!;

        public NorthWindContextProcedures() { }

        public NorthWindContextProcedures(DbContextOptions<NorthWindContextProcedures> options)
            : base(options) { }

        /// <summary>
        /// In OnModelCreating Method we mapping entity properties with database response
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerOrderHistory>(entity =>
            {
                entity.HasNoKey();
                entity.Property(e => e.ProductName);
                entity.Property(e => e.Total);
            });
        }
    }
}
