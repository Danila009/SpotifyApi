using Microsoft.EntityFrameworkCore;
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

              modelBuilder.Entity<Music>()
                .HasMany(m => m.Autors).WithMany(u => u.Musics);

            modelBuilder.Entity<Autor>()
                .HasMany(m => m.Musics).WithMany(u => u.Autors);

        }

        public EfModel(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Music> Musics { get; set; }
        public virtual DbSet<Autor> Autors { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
    }
}
