using System;
using System.IO;
using Example.Domain.Models;
using Example.Infrastruct.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Example.Infrastruct.Data.Context
{
    public class CustomerContext : DbContext
    {
        public CustomerContext()
        {
            ChangeTracker.StateChanged += ChangeTracker_StateChanged;
            ChangeTracker.Tracked += ChangeTracker_Tracked;
        }

        private void ChangeTracker_Tracked(object sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityTrackedEventArgs e)
        {

        }

        private void ChangeTracker_StateChanged(object sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityStateChangedEventArgs e)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMap());

            base.OnModelCreating(modelBuilder);
        }
        /// <summary>
        /// 重写连接数据库
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 从 appsetting.json 中获取配置信息
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // 定义要使用的数据库
            //optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseSqlite("Data Source = ddd_demo.db");
            optionsBuilder.AddInterceptors(_commandInterceptor, _connectInterceptor, _transactionInterceptor);
        }

        private static readonly TaggedQueryCommandInterceptor _commandInterceptor = new TaggedQueryCommandInterceptor();
        private static readonly TaggedConnectionInterceptor _connectInterceptor = new TaggedConnectionInterceptor();
        private static readonly TaggedTransactionInterceptor _transactionInterceptor = new TaggedTransactionInterceptor();
    }
}
