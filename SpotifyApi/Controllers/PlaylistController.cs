using FastestDeliveryApi.database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpotifyApi.model.music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private EfModel _efModel;

        public PlaylistController(EfModel model)
        {
            _efModel = model;
        }

        [HttpGet("/Playlist")]
        public async Task<ActionResult<List<Playlist>>> GetPlaylists()
        {
            return await _efModel.Playlists.Include(u => u.Musics).ToListAsync();
        }

        [HttpGet("/Playlist/{id}")]
        public async Task<ActionResult<Playlist>> GetPlaylist(int id)
        {
            return await _efModel.Playlists
                .Include(u => u.Musics)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
