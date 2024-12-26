using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace book_inven.Models;

public partial class BooksInvenContext : DbContext
{
    public BooksInvenContext()
    {
    }

    public BooksInvenContext(DbContextOptions<BooksInvenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Memberaction> Memberactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=books_inven;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Bid).HasName("PK__book__DE90ADE79FAF1007");

            entity.ToTable("book");

            entity.Property(e => e.Bid).HasColumnName("bid");
            entity.Property(e => e.Bprice).HasColumnName("bprice");
            entity.Property(e => e.Btitle)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("btitle");
            entity.Property(e => e.Btype)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("btype");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Mid).HasName("PK__members__DF5032ECB2FD6DAB");

            entity.ToTable("members");

            entity.Property(e => e.Mid).HasColumnName("mid");
            entity.Property(e => e.Maddress)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("maddress");
            entity.Property(e => e.Mname)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("mname");
            entity.Property(e => e.Mphone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mphone");
        });

        modelBuilder.Entity<Memberaction>(entity =>
        {
            entity.HasKey(e => e.Aid).HasName("PK__memberac__DE508E2EC83D9E23");

            entity.ToTable("memberactions");

            entity.Property(e => e.Aid).HasColumnName("aid");
            entity.Property(e => e.Bid).HasColumnName("bid");
            entity.Property(e => e.BorrowDate).HasColumnName("borrow_date");
            entity.Property(e => e.Mid).HasColumnName("mid");
            entity.Property(e => e.ReturnDate).HasColumnName("return_date");

            entity.HasOne(d => d.BidNavigation).WithMany(p => p.Memberactions)
                .HasForeignKey(d => d.Bid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__memberactio__bid__3B75D760");

            entity.HasOne(d => d.MidNavigation).WithMany(p => p.Memberactions)
                .HasForeignKey(d => d.Mid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__memberactio__mid__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
