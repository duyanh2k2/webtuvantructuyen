using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BaiTapLonWNC.Models;

public partial class BaiTapLonWebContext : DbContext
{
    public BaiTapLonWebContext()
    {
    }

    public BaiTapLonWebContext(DbContextOptions<BaiTapLonWebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBaiViet> TblBaiViets { get; set; }

    public virtual DbSet<TblChuDe> TblChuDes { get; set; }

    public virtual DbSet<TblComment> TblComments { get; set; }

    public virtual DbSet<TblLike> TblLikes { get; set; }

    public virtual DbSet<TblTrangCn> TblTrangCns { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-6HQB58S\\SQLEXPRESS;Initial Catalog=BaiTapLonWeb;Encrypt=False;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBaiViet>(entity =>
        {
            entity.HasKey(e => e.MaBv).HasName("PK__tblBaiVi__27247595B3292C85");

            entity.ToTable("tblBaiViet", tb =>
                {
                    tb.HasTrigger("deletetblBV");
                    tb.HasTrigger("upBaiViet");
                });

            entity.Property(e => e.MaBv).HasColumnName("MaBV");
            entity.Property(e => e.LinkAnh)
                .HasMaxLength(1000)
                .HasColumnName("linkAnh");
            entity.Property(e => e.MaTcn).HasColumnName("MaTCN");
            entity.Property(e => e.NoiDung).HasMaxLength(1000);
            entity.Property(e => e.TieuDe).HasMaxLength(256);
            entity.Property(e => e.VerifyKey)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.MaChuDeNavigation).WithMany(p => p.TblBaiViets)
                .HasForeignKey(d => d.MaChuDe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chude_BaiViet");

            entity.HasOne(d => d.MaTcnNavigation).WithMany(p => p.TblBaiViets)
                .HasForeignKey(d => d.MaTcn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_trangCN_BaiViet");
        });

        modelBuilder.Entity<TblChuDe>(entity =>
        {
            entity.HasKey(e => e.MaChuDe).HasName("PK__tblChuDe__358545115EE9B111");

            entity.ToTable("tblChuDe");

            entity.Property(e => e.TenChuDe).HasMaxLength(50);
        });

        modelBuilder.Entity<TblComment>(entity =>
        {
            entity.HasKey(e => e.MaCm).HasName("PK__tblComme__27258E0FB1E30630");

            entity.ToTable("tblComment", tb =>
                {
                    tb.HasTrigger("AddComment");
                    tb.HasTrigger("removeComment");
                });

            entity.Property(e => e.MaCm).HasColumnName("MaCM");
            entity.Property(e => e.MaBv).HasColumnName("MaBV");
            entity.Property(e => e.MaTcn).HasColumnName("MaTCN");
            entity.Property(e => e.NoiDung).HasMaxLength(1000);

            entity.HasOne(d => d.MaBvNavigation).WithMany(p => p.TblComments)
                .HasForeignKey(d => d.MaBv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_BaiViet");

            entity.HasOne(d => d.MaTcnNavigation).WithMany(p => p.TblComments)
                .HasForeignKey(d => d.MaTcn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_TCN");
        });

        modelBuilder.Entity<TblLike>(entity =>
        {
            entity.HasKey(e => e.MaLike).HasName("PK__tblLike__728CDDCA94FB31B2");

            entity.ToTable("tblLike", tb =>
                {
                    tb.HasTrigger("AddLike");
                    tb.HasTrigger("removeLike");
                });

            entity.Property(e => e.Icon)
                .HasMaxLength(1000)
                .HasColumnName("icon");
            entity.Property(e => e.MaBv).HasColumnName("MaBV");
            entity.Property(e => e.MaTcn).HasColumnName("MaTCN");

            entity.HasOne(d => d.MaBvNavigation).WithMany(p => p.TblLikes)
                .HasForeignKey(d => d.MaBv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Like_BaiViet");

            entity.HasOne(d => d.MaTcnNavigation).WithMany(p => p.TblLikes)
                .HasForeignKey(d => d.MaTcn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Like_TCN");
        });

        modelBuilder.Entity<TblTrangCn>(entity =>
        {
            entity.HasKey(e => e.MaTcn).HasName("PK__tblTrang__314995E078CD2237");

            entity.ToTable("tblTrangCN");

            entity.Property(e => e.MaTcn).HasColumnName("MaTCN");
            entity.Property(e => e.MaUs).HasColumnName("MaUS");
            entity.Property(e => e.NameTcn)
                .HasMaxLength(100)
                .HasColumnName("NameTCN");

            entity.HasOne(d => d.MaUsNavigation).WithMany(p => p.TblTrangCns)
                .HasForeignKey(d => d.MaUs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_trangCN");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.MaUs).HasName("PK__tblUser__272518D84A0EAC8D");

            entity.ToTable("tblUser");

            entity.HasIndex(e => e.UserName, "UQ__tblUser__C9F284569F85B860").IsUnique();

            entity.Property(e => e.MaUs).HasColumnName("MaUS");
            entity.Property(e => e.Pass)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
