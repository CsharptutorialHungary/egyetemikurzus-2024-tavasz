using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CommunitySite.CommunitySiteEntities;

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
            entity.Property(e => e.Friendid1)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("FRIENDID1");
            entity.Property(e => e.Friendid2)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("FRIENDID2");
            entity.Property(e => e.IsFriend)
                .HasPrecision(1)
                .HasColumnName("IS_FRIEND");

            entity.HasOne(d => d.Friendid1Navigation).WithMany()
                .HasForeignKey(d => d.Friendid1)
                .HasConstraintName("SYS_C0010186");

            entity.HasOne(d => d.Friendid2Navigation).WithMany()
                .HasForeignKey(d => d.Friendid2)
                .HasConstraintName("SYS_C0010187");
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
                .HasConstraintName("SYS_C0010210");

            entity.HasOne(d => d.Post).WithMany()
                .HasForeignKey(d => d.Postid)
                .HasConstraintName("SYS_C0010209");
        });

        modelBuilder.Entity<Managegroup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MANAGEGROUPS");

            entity.Property(e => e.Groupid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("GROUPID");
            entity.Property(e => e.JoinDate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("JOIN_DATE");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERID");

            entity.HasOne(d => d.Group).WithMany()
                .HasForeignKey(d => d.Groupid)
                .HasConstraintName("SYS_C0010196");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C0010195");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Messageid).HasName("SYS_C0010189");

            entity.ToTable("MESSAGES");

            entity.Property(e => e.Messageid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("MESSAGEID");
            entity.Property(e => e.MessageText)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("MESSAGE_TEXT");
            entity.Property(e => e.Receiverid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RECEIVERID");
            entity.Property(e => e.SendDate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SEND_DATE");
            entity.Property(e => e.Senderid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SENDERID");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.Receiverid)
                .HasConstraintName("SYS_C0010191");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.Senderid)
                .HasConstraintName("SYS_C0010190");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Permissionid).HasName("SYS_C0010182");

            entity.ToTable("PERMISSIONS");

            entity.Property(e => e.Permissionid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PERMISSIONID");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PERMISSION_NAME");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Photoid).HasName("SYS_C0010198");

            entity.ToTable("PHOTOS");

            entity.Property(e => e.Photoid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PHOTOID");
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
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERID");

            entity.HasOne(d => d.User).WithMany(p => p.Photos)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C0010199");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Postid).HasName("SYS_C0010201");

            entity.ToTable("POSTS");

            entity.Property(e => e.Postid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("POSTID");
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
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERID");

            entity.HasOne(d => d.Group).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Groupid)
                .HasConstraintName("SYS_C0010204");

            entity.HasOne(d => d.Photo).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Photoid)
                .HasConstraintName("SYS_C0010203");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C0010202");
        });

        modelBuilder.Entity<Sitecomment>(entity =>
        {
            entity.HasKey(e => e.Commentid).HasName("SYS_C0010206");

            entity.ToTable("SITECOMMENTS");

            entity.Property(e => e.Commentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("COMMENTID");
            entity.Property(e => e.CommentDate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COMMENT_DATE");
            entity.Property(e => e.CommentText)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("COMMENT_TEXT");
            entity.Property(e => e.Postid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("POSTID");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERID");

            entity.HasOne(d => d.Post).WithMany(p => p.Sitecomments)
                .HasForeignKey(d => d.Postid)
                .HasConstraintName("SYS_C0010207");

            entity.HasOne(d => d.User).WithMany(p => p.Sitecomments)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C0010208");
        });

        modelBuilder.Entity<Sitegroup>(entity =>
        {
            entity.HasKey(e => e.Groupid).HasName("SYS_C0010193");

            entity.ToTable("SITEGROUPS");

            entity.Property(e => e.Groupid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("GROUPID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Ownerid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("OWNERID");

            entity.HasOne(d => d.Owner).WithMany(p => p.Sitegroups)
                .HasForeignKey(d => d.Ownerid)
                .HasConstraintName("SYS_C0010194");
        });

        modelBuilder.Entity<Siteuser>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("SYS_C0010184");

            entity.ToTable("SITEUSERS");

            entity.Property(e => e.Userid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");
            entity.Property(e => e.BirthDay)
                .HasPrecision(2)
                .HasColumnName("BIRTH_DAY");
            entity.Property(e => e.BirthMonth)
                .HasPrecision(2)
                .HasColumnName("BIRTH_MONTH");
            entity.Property(e => e.BirthYear)
                .HasPrecision(4)
                .HasColumnName("BIRTH_YEAR");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
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
                .HasConstraintName("SYS_C0010185");
        });
        modelBuilder.HasSequence("FELHASZNALO_SEQ");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
