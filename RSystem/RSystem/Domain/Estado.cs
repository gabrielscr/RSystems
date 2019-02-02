using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSystem.Domain
{
    public class Estado
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Uf { get; set; }

        public int PaisId { get; set; }

        public Pais Pais { get; set; }
    }
}
