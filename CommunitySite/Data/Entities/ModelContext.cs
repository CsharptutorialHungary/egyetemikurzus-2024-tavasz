using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CommunitySite.Data.Entities;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<Include> Includes { get; set; }

    public virtual DbSet<Managegroup> Managegroups { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Sitecomment> Sitecomments { get; set; }

    public virtual DbSet<Sitegroup> Sitegroups { get; set; }

    public virtual DbSet<Siteuser> Siteusers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("User Id=SZABOKD;Password=20020310;Data Source=localhost:1521/xe;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("SZABOKD");

        modelBuilder.Entity<Friend>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("FRIENDS");

            entity.Property(e => e.FriendStartDate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FRIEND_START_DATE");
            entity.Property(e => e.Friendemail1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FRIENDEMAIL1");
            entity.Property(e => e.Friendemail2)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FRIENDEMAIL2");
            entity.Property(e => e.IsFriend)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("IS_FRIEND");

            entity.HasOne(d => d.Friendemail1Navigation).WithMany()
                .HasForeignKey(d => d.Friendemail1)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010500");

            entity.HasOne(d => d.Friendemail2Navigation).WithMany()
                .HasForeignKey(d => d.Friendemail2)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010501");
        });

        modelBuilder.Entity<Include>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("INCLUDES");

            entity.Property(e => e.Commentid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COMMENTID");
            entity.Property(e => e.Postid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("POSTID");

            entity.HasOne(d => d.Comment).WithMany()
                .HasForeignKey(d => d.Commentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010524");

            entity.HasOne(d => d.Post).WithMany()
                .HasForeignKey(d => d.Postid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010523");
        });

        modelBuilder.Entity<Managegroup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MANAGEGROUPS");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Groupid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("GROUPID");
            entity.Property(e => e.JoinDate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("JOIN_DATE");

            entity.HasOne(d => d.EmailNavigation).WithMany()
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010509");

            entity.HasOne(d => d.Group).WithMany()
                .HasForeignKey(d => d.Groupid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010510");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Messageid).HasName("SYS_C0010503");

            entity.ToTable("MESSAGES");

            entity.Property(e => e.Messageid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("MESSAGEID");
            entity.Property(e => e.MessageText)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("MESSAGE_TEXT");
            entity.Property(e => e.Receiveremail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RECEIVEREMAIL");
            entity.Property(e => e.SendDate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SEND_DATE");
            entity.Property(e => e.Senderemail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SENDEREMAIL");

            entity.HasOne(d => d.ReceiveremailNavigation).WithMany(p => p.MessageReceiveremailNavigations)
                .HasForeignKey(d => d.Receiveremail)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010505");

            entity.HasOne(d => d.SenderemailNavigation).WithMany(p => p.MessageSenderemailNavigations)
                .HasForeignKey(d => d.Senderemail)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010504");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Permissionid).HasName("SYS_C0010497");

            entity.ToTable("PERMISSIONS");

            entity.Property(e => e.Permissionid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PERMISSIONID");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PERMISSION_NAME");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Photoid).HasName("SYS_C0010512");

            entity.ToTable("PHOTOS");

            entity.Property(e => e.Photoid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PHOTOID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.PhotoSize)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PHOTO_SIZE");
            entity.Property(e => e.PhotoType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PHOTO_TYPE");
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("PHOTO_URL");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Photos)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010513");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Postid).HasName("SYS_C0010515");

            entity.ToTable("POSTS");

            entity.Property(e => e.Postid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("POSTID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Groupid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("GROUPID");
            entity.Property(e => e.Photoid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PHOTOID");
            entity.Property(e => e.PostDate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("POST_DATE");
            entity.Property(e => e.PostText)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("POST_TEXT");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010516");

            entity.HasOne(d => d.Group).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Groupid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010518");

            entity.HasOne(d => d.Photo).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Photoid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010517");
        });

        modelBuilder.Entity<Sitecomment>(entity =>
        {
            entity.HasKey(e => e.Commentid).HasName("SYS_C0010520");

            entity.ToTable("SITECOMMENTS");

            entity.Property(e => e.Commentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COMMENTID");
            entity.Property(e => e.CommentDate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COMMENT_DATE");
            entity.Property(e => e.CommentText)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("COMMENT_TEXT");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Postid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("POSTID");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Sitecomments)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010522");

            entity.HasOne(d => d.Post).WithMany(p => p.Sitecomments)
                .HasForeignKey(d => d.Postid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010521");
        });

        modelBuilder.Entity<Sitegroup>(entity =>
        {
            entity.HasKey(e => e.Groupid).HasName("SYS_C0010507");

            entity.ToTable("SITEGROUPS");

            entity.Property(e => e.Groupid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("GROUPID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Owneremail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OWNEREMAIL");

            entity.HasOne(d => d.OwneremailNavigation).WithMany(p => p.Sitegroups)
                .HasForeignKey(d => d.Owneremail)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010508");
        });

        modelBuilder.Entity<Siteuser>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("SYS_C0010498");

            entity.ToTable("SITEUSERS");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.BirthDay)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BIRTH_DAY");
            entity.Property(e => e.BirthMonth)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BIRTH_MONTH");
            entity.Property(e => e.BirthYear)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BIRTH_YEAR");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.Passwords)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PASSWORDS");
            entity.Property(e => e.Permissionid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PERMISSIONID");
            entity.Property(e => e.School)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SCHOOL");
            entity.Property(e => e.SurName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SUR_NAME");
            entity.Property(e => e.Workplace)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("WORKPLACE");

            entity.HasOne(d => d.Permission).WithMany(p => p.Siteusers)
                .HasForeignKey(d => d.Permissionid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010499");
        });
        modelBuilder.HasSequence("FELHASZNALO_SEQ");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
