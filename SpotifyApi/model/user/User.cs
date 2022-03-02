using SpotifyApi.model.music;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.model.user
{
    public class User
    {
        [Key] public int Id { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        public virtual List<Music> FavoriteMusics { get; set; } = new List<Music>();
        public virtual List<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
