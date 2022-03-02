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
    public class MusicController : ControllerBase
    {
        private EfModel _efModel;

        public MusicController(EfModel model)
        {
            _efModel = model;
        }

        [HttpGet("/Music")]
        public async Task<ActionResult<List<Music>>> GetMusics(
            int? idGnre, int? idPerson, string search
            )
        {
            if (idGnre != null)
                return await _efModel.Musics
                    .Include(u => u.Genre)
                    .Include(u => u.Autors)
                    .Where(u => u.Genre.Id == idGnre).ToListAsync();

            if (idPerson != null)
                return await _efModel.Musics
                    .Include(u => u.Genre)
                    .Include(u => u.Autors)
                    .Where(u => u.Autors.Any(u => u.Id == idPerson)).ToListAsync();

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
    }
}
