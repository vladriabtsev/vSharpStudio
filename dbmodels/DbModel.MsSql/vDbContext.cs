using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using vSharpStudio.common;

namespace vPlugin.DbModel.MsSql
{
    public class vDbContext : DbContext
    {
        IServiceProvider service_provider;
        private string conn_string;
        public vDbContext(string conn_string, IServiceProvider service_provider, DbContextOptions options) : base(options)
        {
            this.service_provider = service_provider;
            this.conn_string = conn_string;
        }

        //public vDbContext(DbContextOptions options) : base(options) { }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //DbModelCreator creator = new DbModelCreator();
        //    //creator.Visit(diffModel, modelBuilder);
        //    //this.Model = creator.Model;
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
            .UseInternalServiceProvider(service_provider)
            //.ReplaceService<>
            .UseSqlServer(conn_string);
    }
}
