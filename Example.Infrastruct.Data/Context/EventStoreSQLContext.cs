﻿using Example.Domain.Core.Events;
using Example.Infrastruct.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Example.Infrastruct.Data.Context
{
    public class EventStoreSQLContext : DbContext
    {
        public DbSet<StoredEvent> StoredEvents { get; set; }
        public EventStoreSQLContext(DbContextOptions<EventStoreSQLContext> options)
         : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //// 从 appsetting.json 中获取配置信息
            //var config = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //// 定义要使用的数据库
            //var serverVersion = new MySqlServerVersion(new Version(config["MysqlVersion"]));
            //optionsBuilder.UseMySql(config.GetConnectionString("Default"), serverVersion);
            ////optionsBuilder.UseSqlite("Data Source = ddd_demo.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredEventMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
