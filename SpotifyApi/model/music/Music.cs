﻿using SpotifyApi.model.person;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.model.music
{
    public class Music
    {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Date { get; set; }
        [Required] public string Document { get; set; }
        [Required] public virtual Genre Genre { get; set; } = new Genre();
        [Required] public virtual List<Autor> Autors { get; set; } = new List<Autor>();
    }
}