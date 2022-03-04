using SpotifyApi.model.music;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpotifyApi.model.person
{
    public class Autor
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string PosterUrl { get; set; }
        [Required] public string Birthday { get; set; }
        [Required] public string Death { get; set; }
        [Required] public int Age { get; set; }
        public virtual List<Playlist> Playlists { get; set; } = new List<Playlist>();
        [JsonIgnore] public List<Music> Musics { get; set; }
    }
}