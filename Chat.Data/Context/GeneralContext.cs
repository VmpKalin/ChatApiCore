using System;
using System.Collections.Generic;
using System.Text;
using Chat.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.Data.Context
{
    public class GeneralContext : DbContext
    {
        public GeneralContext(DbContextOptions<GeneralContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<MessageEntity> Messages { get; set; }

        public DbSet<RoomEntity> ChatRooms { get; set; }
    }
}
