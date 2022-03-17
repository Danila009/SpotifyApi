using FastestDeliveryApi.database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpotifyApi.model.history;
using SpotifyApi.model.user;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SpotifyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        public EfModel _efModel;

        public HistoryController(EfModel efModel)
        {
            _efModel = efModel;
        }

        [Authorize]
        [HttpGet("/History/Search")]
        public async Task<ActionResult<List<HistorySearch>>> GetHistory()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            int id = Convert.ToInt32(identity.FindFirst("Id").Value);

            User user = await _efModel.Users
                .Include(u => u.HistorySearch)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound();

            return user.HistorySearch;
        }

        [Authorize]
        [HttpPost("/History/Search")]
        public async Task<ActionResult<List<HistorySearch>>> PostHistory(HistorySearch historySearch)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            int id = Convert.ToInt32(identity.FindFirst("Id").Value);

            User user = await _efModel.Users
                .Include(u => u.HistorySearch)
                .FirstOrDefaultAsync(u => u.Id == id);

            user.HistorySearch.Add(historySearch);
            await _efModel.SaveChangesAsync();

            return CreatedAtAction(nameof(PostHistory
                ), new { id = user.Id }, user.HistorySearch);
        }

        [Authorize]
        [HttpDelete("/History/Search/{id}")]
        public async Task<ActionResult<List<HistorySearch>>> DeleteHistory(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            int idUser = Convert.ToInt32(identity.FindFirst("Id").Value);

            User user = await _efModel.Users
                .Include(u => u.HistorySearch)
                .FirstOrDefaultAsync(u => u.Id == idUser);

            HistorySearch historySearch = user.HistorySearch.Find(u => u.Id == id);

            user.HistorySearch.Remove(historySearch);
            await _efModel.SaveChangesAsync();

            return CreatedAtAction(nameof(DeleteHistory
                ), new { id = user.Id }, user.HistorySearch);
        }

    }
}
