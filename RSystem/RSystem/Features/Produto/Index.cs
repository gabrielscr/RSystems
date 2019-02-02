using MediatR;
using RSystem.Infrastructure;
using RSystem.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RochaSystem.Features.Produto
{
    public class Index
    {
        public class Query : IRequest<ProdutoDto[]>
        {

        }

        public class ProdutoDto
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public double Valor { get; set; }

            public string Marca { get; set; }

            public UnidadeMedidaEnum UnidadeMedida { get; set; }

            public int Quantidade { get; set; }

            public int QuantidadeProdutosCadastrados {get;set;}

            public double Volume { get; set; }

            public bool Ativo { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, ProdutoDto[]>
        {
            private readonly AdminContext _adminContext;
            private readonly IMediator _mediator;

            public QueryHandler(AdminContext adminContext, IMediator mediator)
            {
                _adminContext = adminContext;
                _mediator = mediator;
            }

            public async Task<ProdutoDto[]> Handle(Query message, CancellationToken cancellationToken)
            {
                var totalProdutos = _adminContext.Set<RSystem.Domain.Produto>().Select(p => p.Quantidade).Count();

                var consulta = await _adminContext
                    .Set<RSystem.Domain.Produto>()
                    .AsNoTracking()
                    .OrderBy(m => m.Id)
                    .Select(m => new ProdutoDto
                    {
                        Id = m.Id,
                        Descricao = m.Descricao,
                        Valor = m.Valor,
                        Marca = m.Marca.Descricao,
                        UnidadeMedida = m.UnidadeMedida,
                        Quantidade = m.Quantidade,
                        Volume = m.Volume,
                        Ativo = m.Ativo,
                    })
                    .ToArrayAsync();

                return consulta;
            }
        }
    }
}
