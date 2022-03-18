using SpotifyApi.model.user;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.model.person.user
{
    [Table("AutorUser")]
    public class AutorUser:User
    {
        public override string Role => "AutorUser";
        [Required] public virtual Autor Autor { get; set; } = new Autor();
    }
}
