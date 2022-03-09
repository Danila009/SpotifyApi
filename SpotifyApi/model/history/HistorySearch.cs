using System.ComponentModel.DataAnnotations;

namespace SpotifyApi.model.history
{
    public class HistorySearch
    {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; }
    }
}
