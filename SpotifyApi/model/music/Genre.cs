using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpotifyApi.model.music
{
    public class Genre
    {
        [Key] public int Id { get; set; }
        [Required] public string GenreTitle { get; set; }
        [Required] public string GenreIcon { get; set; }
        [JsonIgnore] public List<Music> Musics { get; set; }
    }
}
