using FastestDeliveryApi.database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpotifyApi.model.music;
using SpotifyApi.model.person;
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
    public class MusicController : ControllerBase
    {
        private EfModel _efModel;

        public MusicController(EfModel model)
        {
            _efModel = model;
        }

        [HttpGet("/Music")]
        public async Task<ActionResult<List<Music>>> GetMusics(
            string search
            )
        {
            if (search != null)
                return await _efModel.Musics
                    .Include(u => u.Genre)
                    .Include(u => u.Autors)
                    .Where(u => u.Title.Contains(search)).ToListAsync();

            return await _efModel.Musics
                .Include(u => u.Autors)
                .Include(u => u.Genre)
                .ToListAsync();
        }

        [HttpGet("/Music/{id}")]
        public async Task<ActionResult<Music>> GetMusic(int id)
        {
            return await _efModel.Musics
                .Include(u => u.Autors)
                .Include(u => u.Genre)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        [HttpGet("/Music/{id}/Genre")]
        public async Task<ActionResult<List<Genre>>> GetMusicGenre(int id)
        {
            List<Genre> genre = await _efModel.Genres.Where(u => u.Musics
            .Any(u => u.Id == id)).ToListAsync();

            if (genre == null)
                return NotFound();

            return genre;
        }

        [HttpGet("/Music/{id}/Autor")]
        public async Task<ActionResult<List<Autor>>> GetMusicAutor(int id)
        {
            List<Autor> autor = await _efModel.Autors.Where(u => u.Musics
            .Any(u => u.Id == id)).ToListAsync();

            if (autor == null)
                return NotFound();

            return autor;
        }

        [Authorize]
        [HttpPost("/User/Favorite/Music")]
        public async Task<ActionResult<List<Music>>> PostMusicsFavorite(int idMudic)
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

            Music music = await _efModel.Musics.FindAsync(idMudic);

            user.FavoriteMusics.Add(music);
            await _efModel.SaveChangesAsync();

            return CreatedAtAction(nameof(PostMusicsFavorite
                ), new { id = user.Id }, user.FavoriteMusics);
        }
    }
}
