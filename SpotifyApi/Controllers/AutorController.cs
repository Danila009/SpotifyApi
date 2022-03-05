using FastestDeliveryApi.database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpotifyApi.model.music;
using SpotifyApi.model.person;
using System;
using System.Collections.Generic;
using System.Linq;
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
                .FirstOrDefaultAsync(u=> u.Id == id);
        }

        [HttpGet("/Autor/{id}/Music")]
        public async Task<ActionResult<List<Music>>> GetAutorMusic(int id)
        {
            return await _efModel.Musics.Where(u => u.AutorId == id).ToListAsync();
        }
    }
}
