using FastestDeliveryApi.database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpotifyApi.model.music;
using SpotifyApi.model.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [HttpGet("/Playlist/{id}/Music")]
        public async Task<ActionResult<List<Music>>> GetPlaylist(int id)
        {
            return await _efModel.Musics
                .Where(u => u.Playlists.Any(u => u.Id == id)).ToListAsync();
        }

        [Authorize]
        [HttpPost("/User/Playlist")]
        public async Task<ActionResult<Playlist>> PostPlaylistFavorite(int idPalylist)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            int id = Convert.ToInt32(identity.FindFirst("Id").Value);

            User user = await _efModel.Users
                .Include(u => u.FavoriteMusics)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound();

            Playlist music = await _efModel.Playlists.FindAsync(idPalylist);

            user.Playlists.Add(music);
            await _efModel.SaveChangesAsync();

            return CreatedAtAction(nameof(PostPlaylistFavorite
                ), new { id = user.Id }, user.Playlists);
        }
    }
}
