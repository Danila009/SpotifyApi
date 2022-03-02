using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.model.music
{
    public class Playlist
    {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Icon { get; set; }
        public virtual List<Music> Musics { get; set; } = new List<Music>();
    }
}
