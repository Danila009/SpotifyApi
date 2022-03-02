using FastestDeliveryApi.database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("/Person")]
        public async Task<ActionResult<List<Autor>>> GetPersons()
        {
            return await _efModel.Autors.ToListAsync();
        }

        [HttpGet("/Person/{id}")]
        public async Task<ActionResult<Autor>> GetPerson(int id)
        {
            return await _efModel.Autors.FindAsync(id);
        }
    }
}
