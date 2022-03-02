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
        }

        public EfModel(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Music> Musics { get; set; }
        public virtual DbSet<Autor> Autors { get; set; }
    }
}
