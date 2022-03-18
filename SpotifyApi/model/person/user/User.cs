using SpotifyApi.model.history;
using SpotifyApi.model.music;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpotifyApi.model.user
{
    [Table("BaseUser")]
    public class User
    {
        [Key] public int Id { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        public virtual string Role => "BaseUser";
        [JsonIgnore] public virtual List<HistorySearch> HistorySearch { get; set; }
        [JsonIgnore] public virtual List<Music> FavoriteMusics { get; set; } = new List<Music>();
        [JsonIgnore] public virtual List<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
