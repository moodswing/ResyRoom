﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResyRoom.Models
{
    public static class Constantes
    {
        public static readonly Comuna ComunaSinSeleccion = new Comuna { Descripcion = "Seleccione Región", IdComuna = 0 };
        public static readonly Region RegionSinSeleccion = new Region { Descripcion = "Seleccione ...", IdRegion = 0 };
    }
}