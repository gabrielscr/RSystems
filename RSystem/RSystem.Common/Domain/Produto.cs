using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSystem.Common.Domain
{
    public class Produto
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public double Valor { get; set; }

        public int MarcaId { get; set; }

        public Marca Marca { get; set; }

        public bool Ativo { get; set; }

        public int Quantidade { get; set; }

        public double Volume { get; set; }

        public string Imagem { get; set; }

        public string DescricaoDetalhada { get; set; }

        public UnidadeMedidaEnum UnidadeMedida { get; set; }
    }
}
