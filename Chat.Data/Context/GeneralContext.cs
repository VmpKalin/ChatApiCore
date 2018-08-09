using System;
using System.Collections.Generic;
using System.Text;
using Chat.Data.Models.Entities;
using Chat.Data.Models.Entities.LikeModels;
using Chat.Data.Models.Entities.UserModels;
using Microsoft.EntityFrameworkCore;

namespace Chat.Data.Context
{
    public class GeneralContext : DbContext
    {
        public GeneralContext(DbContextOptions<GeneralContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            //model.Entity<UserEntity>(x =>
            //    x.HasMany(y => y.Posts)
            //     .WithOne(o => o.UserEntity)
            //     .HasForeignKey(k => k.UserId));

            model.Entity<PostEntity>(x => x.HasOne(y => y.LikeInfo).WithOne(y => y.Module));

            //model.Entity<LikeBinding<PostEntity>>(x =>
            //    x.HasOne(y => y.LikeEntity).WithMany(z => z.LikeBindings).HasForeignKey(k => k.LikeId));

            //model.Entity<LikeEntity<PostEntity>>(x =>
            //    x.HasMany(y => y.LikeBindings).WithOne(z => z.LikeEntity).HasForeignKey(k => k.LikeId));
        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<UserInfoEntity> UsersInfo { get; set; }

        public DbSet<MessageEntity> Messages { get; set; }

        public DbSet<RoomEntity> ChatRooms { get; set; }

        public DbSet<LikeEntity<PostEntity>> Likes { get; set; }

        public DbSet<LikeBinding> LikeBindings { get; set; }

        public DbSet<PostEntity> Posts { get; set; }

    }
}
