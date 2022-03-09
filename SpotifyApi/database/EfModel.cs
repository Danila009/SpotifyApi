using Microsoft.EntityFrameworkCore;
using SpotifyApi.model.history;
using SpotifyApi.model.music;
using SpotifyApi.model.person;
using SpotifyApi.model.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastestDeliveryApi.database
{
    public class EfModel : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

              modelBuilder.Entity<Music>()
                  .HasMany(m => m.Genre).WithMany(u => u.Musics);

            modelBuilder.Entity<Genre>()
                .HasMany(m => m.Musics).WithMany(u => u.Genre);

              modelBuilder.Entity<Music>()
                .HasMany(m => m.Autors).WithMany(u => u.Musics);

            modelBuilder.Entity<Autor>()
                .HasMany(m => m.Musics).WithMany(u => u.Autors);

            modelBuilder.Entity<Playlist>()
                .HasMany(m => m.Musics).WithMany(u => u.Playlists);

            modelBuilder.Entity<Music>()
                .HasMany(M => M.Playlists).WithMany(u => u.Musics);

        }

        public EfModel(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Music> Musics { get; set; }
        public virtual DbSet<Autor> Autors { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<HistorySearch> HistorySearches { get; set; }
    }
}
