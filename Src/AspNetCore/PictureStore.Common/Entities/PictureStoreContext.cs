using Microsoft.EntityFrameworkCore;
using System;

namespace PictureStore.Common.Entities
{
    public class PictureStoreContext : DbContext
    {
        //public PictureStoreContext()
        //{
        //}

        public PictureStoreContext(DbContextOptions<PictureStoreContext> options)
           : base(options)
        {
        }

        public DbSet<Picture> Pictures { get; set; }

        //        dotnet tool install --global dotnet-ef
        //dotnet add package Microsoft.EntityFrameworkCore.Design
        //dotnet ef migrations add InitialCreate
        //dotnet ef database update
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer();
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
