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
    }
}
