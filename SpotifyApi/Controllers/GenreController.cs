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
    public class GenreController : ControllerBase
    {
        public EfModel _efModel;

        public GenreController(EfModel efModel)
        {
            _efModel = efModel;
        }

        [HttpGet("/Genre")]
        public async Task<ActionResult<List<Genre>>> GetGenres()
        {
            return await _efModel.Genres
                .ToListAsync();
        }

        [HttpGet("/Genre/{id}/Music")]
        public async Task<ActionResult<List<Music>>> GetMusic(int id)
        {
            return await _efModel.Musics
                .Where(u => u.Genre.Any(u => u.Id == id)).ToListAsync();
        }


    }
}
