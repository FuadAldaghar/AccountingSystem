using AccountingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tables
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ReceiptVoucher> ReceiptVouchers { get; set; }
        public DbSet<PaymentVoucher> PaymentVouchers { get; set; }

        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<PurchaseInvoiceDetail> PurchaseInvoiceDetails { get; set; }

        public DbSet<SalesInvoice> SalesInvoices { get; set; }
        public DbSet<SalesInvoiceDetail> SalesInvoiceDetails { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Account
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(a => a.AccountId);

                entity.HasIndex(a => a.AccountNumber)
                      .IsUnique();

                entity.Property(a => a.AccountNumber)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(a => a.AccountName)
                      .HasMaxLength(200)
                      .IsRequired();

                entity.Property(a => a.AccountType)
                      .HasMaxLength(100)
                      .IsRequired();
            });

            // Item
            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(i => i.ItemId);

                entity.HasIndex(i => i.ItemNumber)
                      .IsUnique();

                entity.Property(i => i.ItemNumber)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(i => i.ItemName)
                      .HasMaxLength(200)
                      .IsRequired();

                entity.Property(i => i.Unit)
                      .HasMaxLength(50)
                      .IsRequired();
            });

            // Receipt Voucher
            modelBuilder.Entity<ReceiptVoucher>(entity =>
            {
                entity.HasKey(r => r.ReceiptVoucherId);

                entity.Property(r => r.Amount)
                      .HasColumnType("decimal(18,2)");

                entity.HasOne(r => r.Account)
                      .WithMany(a => a.ReceiptVouchers)
                      .HasForeignKey(r => r.AccountId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Payment Voucher
            modelBuilder.Entity<PaymentVoucher>(entity =>
            {
                entity.HasKey(p => p.PaymentVoucherId);

                entity.Property(p => p.Amount)
                      .HasColumnType("decimal(18,2)");

                entity.HasOne(p => p.Account)
                      .WithMany(a => a.PaymentVouchers)
                      .HasForeignKey(p => p.AccountId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Purchase Invoice
            modelBuilder.Entity<PurchaseInvoice>(entity =>
            {
                entity.HasKey(p => p.PurchaseInvoiceId);

                entity.HasOne(p => p.Account)
                      .WithMany(a => a.PurchaseInvoices)
                      .HasForeignKey(p => p.AccountId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(p => p.Details)
                      .WithOne(d => d.PurchaseInvoice)
                      .HasForeignKey(d => d.PurchaseInvoiceId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Purchase Invoice Detail
            modelBuilder.Entity<PurchaseInvoiceDetail>(entity =>
            {
                entity.HasKey(d => d.PurchaseInvoiceDetailId);

                entity.Property(d => d.Quantity)
                      .HasColumnType("decimal(18,2)");

                entity.Property(d => d.UnitPrice)
                      .HasColumnType("decimal(18,2)");

                entity.Property(d => d.Total)
                      .HasColumnType("decimal(18,2)")
                      .ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.Item)
                      .WithMany(i => i.PurchaseInvoiceDetails)
                      .HasForeignKey(d => d.ItemId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Sales Invoice
            modelBuilder.Entity<SalesInvoice>(entity =>
            {
                entity.HasKey(s => s.SalesInvoiceId);

                entity.HasOne(s => s.Account)
                      .WithMany(a => a.SalesInvoices)
                      .HasForeignKey(s => s.AccountId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(s => s.Details)
                      .WithOne(d => d.SalesInvoice)
                      .HasForeignKey(d => d.SalesInvoiceId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Sales Invoice Detail
            modelBuilder.Entity<SalesInvoiceDetail>(entity =>
            {
                entity.HasKey(d => d.SalesInvoiceDetailId);

                entity.Property(d => d.Quantity)
                      .HasColumnType("decimal(18,2)");

                entity.Property(d => d.UnitPrice)
                      .HasColumnType("decimal(18,2)");

                entity.Property(d => d.Total)
                      .HasColumnType("decimal(18,2)")
                      .ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.Item)
                      .WithMany(i => i.SalesInvoiceDetails)
                      .HasForeignKey(d => d.ItemId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.RoleId);

                entity.Property(r => r.RoleName)
                      .HasMaxLength(100)
                      .IsRequired();
            });

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);

                entity.Property(u => u.UserName)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(u => u.PasswordHash)
                      .HasMaxLength(500)
                      .IsRequired();

                entity.HasOne(u => u.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(u => u.RoleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}