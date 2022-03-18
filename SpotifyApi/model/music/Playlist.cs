using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpotifyApi.model.music
{
    public class Playlist
    {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Icon { get; set; }
        [JsonIgnore] public List<Music> Musics { get; set; } 
        public Playlist()
        {
            Musics = new List<Music>();
        }
    }
}
