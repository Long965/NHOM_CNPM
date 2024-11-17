using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PayPalRepositories.Entities;

public partial class PaypaldemoContext : DbContext
{
    public PaypaldemoContext()
    {
    }

    public PaypaldemoContext(DbContextOptions<PaypaldemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TransactionPayPal> TransactionPayPals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TETETETE\\THUYTIEN;Database=Paypaldemo;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionPayPal>(entity =>
        {
            entity.HasKey(e => e.PaymentId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
