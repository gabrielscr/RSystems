﻿namespace RSystem.Domain
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class Cidade
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int EstadoId { get; set; }

        public Estado Estado { get; set; }

    }
}
