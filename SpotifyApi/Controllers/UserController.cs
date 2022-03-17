using FastestDeliveryApi.database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SpotifyApi.model.music;
using SpotifyApi.model.user;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Test2_API.Auth;

namespace SpotifyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private EfModel _efModel;

        public UserController(EfModel model)
        {
            _efModel = model;
        }

        [Authorize]
        [HttpGet("/User/Info")]
        public async Task<ActionResult<User>> GetUserInfo()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            int id = Convert.ToInt32(identity.FindFirst("Id").Value);

            User user = await _efModel.Users
                .Include(u => u.FavoriteMusics)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        [Authorize]
        [HttpGet("/User/Favorite/Music")]
        public async Task<ActionResult<List<Music>>> GetMusicUserFavotite()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            int id = Convert.ToInt32(identity.FindFirst("Id").Value);

            User user = await _efModel.Users
                .Include(u => u.FavoriteMusics)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user.FavoriteMusics;
        }

        [Authorize]
        [HttpDelete("/User/Favorite/Music/{id}")]
        public async Task<ActionResult<List<Music>>> DeleteMusicUserFavotite(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            int idUser = Convert.ToInt32(identity.FindFirst("Id").Value);

            User user = await _efModel.Users
                .Include(u => u.FavoriteMusics)
                .FirstOrDefaultAsync(u => u.Id == idUser);

            var favorite = await _efModel.Musics.FindAsync(id);

            user.FavoriteMusics.Remove(favorite);
            await _efModel.SaveChangesAsync();

            return CreatedAtAction(nameof(DeletePalylistUserFavotite
                ), new { id = user.Id }, user.FavoriteMusics);
        }

        [Authorize]
        [HttpGet("/User/Favorite/Music/{id}/Check")]
        public async Task<ActionResult<Boolean>> GetCheckMusicUserFavotite(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            int idUser = Convert.ToInt32(identity.FindFirst("Id").Value);

            User user = await _efModel.Users
                .Include(u => u.FavoriteMusics)
                .FirstOrDefaultAsync(u => u.Id == idUser);

            if (user.FavoriteMusics.Any(u => u.Id == id))
                return true;

            return false;
        }

        [Authorize]
        [HttpGet("/User/Playlist")]
        public async Task<ActionResult<List<Playlist>>> GetPalylistUserFavotite()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            int id = Convert.ToInt32(identity.FindFirst("Id").Value);

            User user = await _efModel.Users
                .Include(u => u.Playlists)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user.Playlists;
        }

        [Authorize]
        [HttpGet("/User/Playlist/{id}/Check")]
        public async Task<ActionResult<Boolean>> GetCheckPlaylist(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            int idUser = Convert.ToInt32(identity.FindFirst("Id").Value);

            User user = await _efModel.Users
                .Include(u => u.Playlists)
                .FirstOrDefaultAsync(u => u.Id == idUser);

            if (user.Playlists.Any(u => u.Id == id))
                return true;

            return false;
        }

        [Authorize]
        [HttpDelete("/User/Playlist/{id}")]
        public async Task<ActionResult<List<Playlist>>> DeletePalylistUserFavotite(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            int idUser = Convert.ToInt32(identity.FindFirst("Id").Value);

            User user = await _efModel.Users
                .Include(u => u.Playlists)
                .FirstOrDefaultAsync(u => u.Id == idUser);

            var favorite = await _efModel.Playlists.FindAsync(id);

            user.Playlists.Remove(favorite);
            await _efModel.SaveChangesAsync();

            return CreatedAtAction(nameof(DeletePalylistUserFavotite
                ), new { id = user.Id }, user.Playlists);
        }


        [HttpPost("/Registration")]
        public async Task<ActionResult<User>> PostSchol(User user)
        {
            if (_efModel.Users.Any(u => u.Email == user.Email))
                return BadRequest("Пользователь с таким email уже существует");

            if (user.Email.Length < 6 || user.Password.Length < 6)
                return BadRequest("Email должен состоять из 6 или больше символов \n" +
                    "Password должен состоять из 6 или больше символов \n" +
                   "FIO должен состоять из 6 или больше символов");

            if (!user.Email.Contains(".") || !user.Email.Contains("@"))
                return BadRequest("Некорректно введен Email");

            _efModel.Users.Add(user);
            await _efModel.SaveChangesAsync();

            return CreatedAtAction(nameof(PostSchol), new { id = user.Id }, user);
        }

        [HttpPost("/Authorization")]
        public ActionResult<object> Token(model.user.Authorization authorization)
        {
            var indentity = GetIdentity(authorization.Email, authorization.Password);

            if (indentity == null)
            {
                return BadRequest();
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    audience: TokenBaseOptions.AUDIENCE,
                    issuer: TokenBaseOptions.ISSUER,
                    notBefore: now,
                    claims: indentity.Claims,
                    expires: now.Add(TimeSpan.FromDays(TokenBaseOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(TokenBaseOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = indentity.Name,
            };

            return response;
        }

        [NonAction]
        public ClaimsIdentity GetIdentity(string email, string password)
        {
            User user = _efModel.Users.FirstOrDefault(x => x.Email == email && x.Password == password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                    new Claim("Id", user.Id.ToString())
                };

                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }
            return null;
        }
    }
}
