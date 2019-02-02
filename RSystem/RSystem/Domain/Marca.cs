using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RSystem.Domain
{
    public class Marca
    {

        public int Id { get; set; }

        public string Descricao { get; set; }

        public int EstoqueId { get; set; }

        public Estoque Estoque { get; set; }

    }
}
