using SpotifyApi.model.person;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpotifyApi.model.music
{
    public class Music
    {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Date { get; set; }
        [Required] public string Document { get; set; }
        [Required] public string WebIcon { get; set; }
        [JsonIgnore] public virtual List<Playlist> Playlists { get; set; } = new List<Playlist>();
        [JsonIgnore] public virtual List<Genre> Genre { get; set; } = new List<Genre>();
        [JsonIgnore] public virtual List<Autor> Autors { get; set; } = new List<Autor>();
    }
}
