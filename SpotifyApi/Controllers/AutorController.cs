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
    public class AutorController : ControllerBase
    {
        public EfModel _efModel;

        public AutorController(EfModel efModel)
        {
            _efModel = efModel;
        }

        [HttpGet("/Autor")]
        public async Task<ActionResult<List<Autor>>> GetPersons()
        {
            return await _efModel.Autors
                .Include(u => u.Musics)
                .Include(u => u.Playlists)
                .ToListAsync();
        }

        [HttpGet("/Autor/{id}")]
        public async Task<ActionResult<Autor>> GetPerson(int id)
        {
            return await _efModel.Autors
                .Include(u => u.Musics)
                .Include(u => u.Playlists)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        [HttpGet("/Autor/{id}/Music")]
        public async Task<ActionResult<List<Music>>> GetAutorMusic(int id)
        {
            return await _efModel.Musics
                .Where(u => u.Autors.Any(u => u.Id == id)).ToListAsync();
        }

        [Authorize(Roles = "AutorUser")]
        [HttpPost("/Autor/Music")]
        public async Task<ActionResult<List<Music>>> PostMusicAutor(Music music)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            int id = Convert.ToInt32(identity.FindFirst("Id").Value);

            Autor autor = await _efModel.Autors
                .Include(u => u.Playlists)   
                .Include(u => u.Musics)
                .FirstOrDefaultAsync(u => u.Id == id);

            autor.Musics.Add(music);
            await _efModel.SaveChangesAsync();

            return CreatedAtAction(nameof(PostMusicAutor
                ), new { id = autor.Id }, autor.Musics);
        }

        [Authorize(Roles = "AutorUser")]
        [HttpDelete("/Autor/Music/{id}")]
        public async Task<ActionResult<List<Music>>> DeleteMusicAutor(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            int idUser = Convert.ToInt32(identity.FindFirst("Id").Value);

            Autor autor = await _efModel.Autors
                .Include(u => u.Playlists)
                .Include(u => u.Musics)
                .FirstOrDefaultAsync(u => u.Id == idUser);

            Music music = await _efModel.Musics
                .Include(u => u.Playlists)
                .FirstOrDefaultAsync(u => u.Id == id);

            autor.Musics.Remove(music);
            await _efModel.SaveChangesAsync();

            return CreatedAtAction(nameof(PostMusicAutor
                ), new { id = autor.Id }, autor.Musics);
        }

        [Authorize(Roles = "AutorUser")]
        [HttpPost("/Autor/Playlist")]
        public async Task<ActionResult<List<Playlist>>> PostPlaylistAutor(Playlist playlist)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            int id = Convert.ToInt32(identity.FindFirst("Id").Value);

            Autor autor = await _efModel.Autors
                .Include(u => u.Playlists)
                .Include(u => u.Musics)
                .FirstOrDefaultAsync(u => u.Id == id);

            autor.Playlists.Add(playlist);
            await _efModel.SaveChangesAsync();

            return CreatedAtAction(nameof(PostPlaylistAutor
                ), new { id = autor.Id }, autor.Playlists);
        }

        [Authorize(Roles = "AutorUser")]
        [HttpDelete("/Autor/Playlist/{id}")]
        public async Task<ActionResult<List<Playlist>>> DeletePlaylistAutor(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            int idUser = Convert.ToInt32(identity.FindFirst("Id").Value);

            Playlist playlist = await _efModel.Playlists
                .Include(u => u.Musics)
                .FirstOrDefaultAsync(u => u.Id == id);

            _efModel.Playlists.Remove(playlist);
            await _efModel.SaveChangesAsync();

            return CreatedAtAction(nameof(PostPlaylistAutor
                ), new { id = playlist.Id }, playlist);
        }

        [Authorize(Roles = "AutorUser")]
        [HttpPost("/Autor/Playlist/{id}/Music")]
        public async Task<ActionResult<List<Playlist>>> PostPlaylistMusicAutor(int id, Music music)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            int idUser = Convert.ToInt32(identity.FindFirst("Id").Value);

            Autor autor = await _efModel.Autors
                .Include(u => u.Playlists)
                .Include(u => u.Musics)
                .FirstOrDefaultAsync(u => u.Id == idUser);

            Playlist playlistUser = await _efModel.Playlists
                .Include(u => u.Musics)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (playlistUser == null)
                return NotFound();

            playlistUser.Musics.Add(music);
            await _efModel.SaveChangesAsync();

            return CreatedAtAction(nameof(PostPlaylistAutor
                ), new { id = playlistUser.Id }, playlistUser.Musics);
        }

        [Authorize(Roles = "AutorUser")]
        [HttpDelete("/Autor/Playlist/{PlaylistId}/Music/{MusicId}")]
        public async Task<ActionResult<List<Playlist>>> DeletePlaylistMusicAutor(int PlaylistId, int MusicId)
        {
           
            Playlist playlistUser = await _efModel.Playlists
                .Include(u => u.Musics)
                .FirstOrDefaultAsync(u => u.Id == PlaylistId);

            Music musicUser = await _efModel.Musics
                .Include(u => u.Playlists)
                .FirstOrDefaultAsync(u => u.Id == MusicId);

            playlistUser.Musics.Remove(musicUser);
            await _efModel.SaveChangesAsync();

            return CreatedAtAction(nameof(DeletePlaylistMusicAutor
                ), new { id = musicUser.Id }, musicUser);
        }
    }
}
