﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeamyMusic.Models
{
    public class Favorito
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdCancion { get; set; }
        public DateTime Fecha { get; set; }
        public Usuario Usuarios { get; set; }
        public Cancion Canciones { get; set; }

    }
}