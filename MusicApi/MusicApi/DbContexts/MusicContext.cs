using Microsoft.EntityFrameworkCore;
using MusicApi.Domain;
using MusicApi.Models;

namespace MusicApi.DbContexts
{
    public class MusicContext : DbContext
    {

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Info> Infos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        { 
           options.UseSqlite($"Data Source={Constants.DatabaseName}");
        }

    }
}
