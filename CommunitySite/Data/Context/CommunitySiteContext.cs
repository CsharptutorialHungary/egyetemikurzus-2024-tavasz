using System;
using System.Collections.Generic;
using CommunitySite.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace CommunitySite.Data.Context;

public partial class CommunitySiteContext : DbContext
{
    public CommunitySiteContext()
    {
    }

    public CommunitySiteContext(DbContextOptions<CommunitySiteContext> options)
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("SZABOKD");

        modelBuilder.Entity<Friend>(entity =>
        {
            entity.HasKey(e => e.Friendrowid).HasName("SYS_C0011119");

            entity.ToTable("FRIENDS");

            entity.Property(e => e.Friendrowid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("FRIENDROWID");
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
                .HasColumnType("NUMBER(38)")
                .HasColumnName("IS_FRIEND");

            entity.HasOne(d => d.Friendid1Navigation).WithMany(p => p.FriendFriendid1Navigations)
                .HasForeignKey(d => d.Friendid1)
                .HasConstraintName("SYS_C0011120");

            entity.HasOne(d => d.Friendid2Navigation).WithMany(p => p.FriendFriendid2Navigations)
                .HasForeignKey(d => d.Friendid2)
                .HasConstraintName("SYS_C0011121");
        });

        modelBuilder.Entity<Include>(entity =>
        {
            entity.HasKey(e => e.Includerowid).HasName("SYS_C0011147");

            entity.ToTable("INCLUDES");

            entity.Property(e => e.Includerowid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("INCLUDEROWID");
            entity.Property(e => e.Commentid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COMMENTID");
            entity.Property(e => e.Postid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("POSTID");

            entity.HasOne(d => d.Comment).WithMany(p => p.Includes)
                .HasForeignKey(d => d.Commentid)
                .HasConstraintName("SYS_C0011149");

            entity.HasOne(d => d.Post).WithMany(p => p.Includes)
                .HasForeignKey(d => d.Postid)
                .HasConstraintName("SYS_C0011148");
        });

        modelBuilder.Entity<Managegroup>(entity =>
        {
            entity.HasKey(e => e.Memberrowid).HasName("SYS_C0011131");

            entity.ToTable("MANAGEGROUPS");

            entity.Property(e => e.Memberrowid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("MEMBERROWID");
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

            entity.HasOne(d => d.Group).WithMany(p => p.Managegroups)
                .HasForeignKey(d => d.Groupid)
                .HasConstraintName("SYS_C0011133");

            entity.HasOne(d => d.User).WithMany(p => p.Managegroups)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C0011132");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Messageid).HasName("SYS_C0011123");

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
                .HasConstraintName("SYS_C0011125");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.Senderid)
                .HasConstraintName("SYS_C0011124");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Permissionid).HasName("SYS_C0011113");

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
            entity.HasKey(e => e.Photoid).HasName("SYS_C0011135");

            entity.ToTable("PHOTOS");

            entity.Property(e => e.Photoid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PHOTOID");
            entity.Property(e => e.PhotoInByte)
                .HasColumnType("BLOB")
                .HasColumnName("PHOTO_IN_BYTE");
            entity.Property(e => e.PhotoName)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("PHOTO_NAME");
            entity.Property(e => e.PhotoSize)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PHOTO_SIZE");
            entity.Property(e => e.PhotoType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PHOTO_TYPE");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERID");

            entity.HasOne(d => d.User).WithMany(p => p.Photos)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C0011136");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Postid).HasName("SYS_C0011138");

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
                .HasConstraintName("SYS_C0011141");

            entity.HasOne(d => d.Photo).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Photoid)
                .HasConstraintName("SYS_C0011140");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C0011139");
        });

        modelBuilder.Entity<Sitecomment>(entity =>
        {
            entity.HasKey(e => e.Commentid).HasName("SYS_C0011143");

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
                .HasConstraintName("SYS_C0011144");

            entity.HasOne(d => d.User).WithMany(p => p.Sitecomments)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C0011145");
        });

        modelBuilder.Entity<Sitegroup>(entity =>
        {
            entity.HasKey(e => e.Groupid).HasName("SYS_C0011128");

            entity.ToTable("SITEGROUPS");

            entity.Property(e => e.Groupid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("GROUPID");
            entity.Property(e => e.Grouptechnicalname)
                .HasMaxLength(40)
                .HasDefaultValueSql("(sys_guid()) ")
                .HasColumnName("GROUPTECHNICALNAME");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Ownerid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("OWNERID");

            entity.HasOne(d => d.Owner).WithMany(p => p.Sitegroups)
                .HasForeignKey(d => d.Ownerid)
                .HasConstraintName("SYS_C0011129");
        });

        modelBuilder.Entity<Siteuser>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("SYS_C0011116");

            entity.ToTable("SITEUSERS");

            entity.Property(e => e.Userid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.Permissionid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PERMISSIONID");
            entity.Property(e => e.School)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SCHOOL");
            entity.Property(e => e.SurName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("SUR_NAME");
            entity.Property(e => e.Userbirthdate)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("USERBIRTHDATE");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USERNAME");
            entity.Property(e => e.Usertechnicalname)
                .HasDefaultValueSql("(sys_guid()) ")
                .HasColumnName("USERTECHNICALNAME");
            entity.Property(e => e.Workplace)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("WORKPLACE");

            entity.HasOne(d => d.Permission).WithMany(p => p.Siteusers)
                .HasForeignKey(d => d.Permissionid)
                .HasConstraintName("SYS_C0011117");
        });
        modelBuilder.HasSequence("FELHASZNALO_SEQ");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
