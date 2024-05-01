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
            entity.HasKey(e => e.Friendrowid).HasName("FRIENDS_PK");

            entity.ToTable("FRIENDS");

            entity.HasIndex(e => e.Friendid1, "IX_FRIENDS_FRIENDID1");

            entity.HasIndex(e => e.Friendid2, "IX_FRIENDS_FRIENDID2");

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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010564");

            entity.HasOne(d => d.Friendid2Navigation).WithMany(p => p.FriendFriendid2Navigations)
                .HasForeignKey(d => d.Friendid2)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010565");
        });

        modelBuilder.Entity<Include>(entity =>
        {
            entity.HasKey(e => e.Includerowid).HasName("INCLUDES_PK");

            entity.ToTable("INCLUDES");

            entity.HasIndex(e => e.Commentid, "IX_INCLUDES_COMMENTID");

            entity.HasIndex(e => e.Postid, "IX_INCLUDES_POSTID");

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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010588");

            entity.HasOne(d => d.Post).WithMany(p => p.Includes)
                .HasForeignKey(d => d.Postid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010587");
        });

        modelBuilder.Entity<Managegroup>(entity =>
        {
            entity.HasKey(e => e.Memberrowid).HasName("MANAGEGROUPS_PK");

            entity.ToTable("MANAGEGROUPS");

            entity.HasIndex(e => e.Groupid, "IX_MANAGEGROUPS_GROUPID");

            entity.HasIndex(e => e.Userid, "IX_MANAGEGROUPS_USERID");

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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010574");

            entity.HasOne(d => d.User).WithMany(p => p.Managegroups)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010573");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Messageid).HasName("SYS_C0010567");

            entity.ToTable("MESSAGES");

            entity.HasIndex(e => e.Receiverid, "IX_MESSAGES_RECEIVERID");

            entity.HasIndex(e => e.Senderid, "IX_MESSAGES_SENDERID");

            entity.Property(e => e.Messageid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010569");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.Senderid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010568");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Permissionid).HasName("SYS_C0010560");

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
            entity.HasKey(e => e.Photoid).HasName("SYS_C0010576");

            entity.ToTable("PHOTOS");

            entity.HasIndex(e => e.Userid, "IX_PHOTOS_USERID");

            entity.Property(e => e.Photoid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010577");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Postid).HasName("SYS_C0010579");

            entity.ToTable("POSTS");

            entity.HasIndex(e => e.Groupid, "IX_POSTS_GROUPID");

            entity.HasIndex(e => e.Photoid, "IX_POSTS_PHOTOID");

            entity.HasIndex(e => e.Userid, "IX_POSTS_USERID");

            entity.Property(e => e.Postid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010582");

            entity.HasOne(d => d.Photo).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Photoid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010581");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010580");
        });

        modelBuilder.Entity<Sitecomment>(entity =>
        {
            entity.HasKey(e => e.Commentid).HasName("SYS_C0010584");

            entity.ToTable("SITECOMMENTS");

            entity.HasIndex(e => e.Postid, "IX_SITECOMMENTS_POSTID");

            entity.HasIndex(e => e.Userid, "IX_SITECOMMENTS_USERID");

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
            entity.Property(e => e.Postid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("POSTID");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERID");

            entity.HasOne(d => d.Post).WithMany(p => p.Sitecomments)
                .HasForeignKey(d => d.Postid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010585");

            entity.HasOne(d => d.User).WithMany(p => p.Sitecomments)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010586");
        });

        modelBuilder.Entity<Sitegroup>(entity =>
        {
            entity.HasKey(e => e.Groupid).HasName("SYS_C0010571");

            entity.ToTable("SITEGROUPS");

            entity.HasIndex(e => e.Ownerid, "IX_SITEGROUPS_OWNERID");

            entity.Property(e => e.Groupid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010572");
        });

        modelBuilder.Entity<Siteuser>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("SYS_C0010562");

            entity.ToTable("SITEUSERS");

            entity.HasIndex(e => e.Permissionid, "IX_SITEUSERS_PERMISSIONID");

            entity.Property(e => e.Userid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERID");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.Permissionid)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("(1)")
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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C0010563");
        });
        modelBuilder.HasSequence("FELHASZNALO_SEQ");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
