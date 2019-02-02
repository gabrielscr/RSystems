using MediatR;
using RSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace RochaSystem.Features.Estoque
{
    public class Index
    {
        public class Query : IRequest<EstoqueDto[]>
        {
        }

        public class EstoqueDto
        {
            public int Id { get; set; }

            public string Descricao { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, EstoqueDto[]>
        {
            private readonly AdminContext _adminContext;
            private readonly IMediator _mediator;

            public QueryHandler(AdminContext adminContext, IMediator mediator)
            {
                _adminContext = adminContext;
                _mediator = mediator;
            }

            public async Task<EstoqueDto[]> Handle(Query message, CancellationToken cancellationToken)
            {
                var consulta = await _adminContext
                    .Set<RSystem.Domain.Estoque>()
                    .AsNoTracking()
                    .OrderBy(m => m.Id)
                    .Select(m => new EstoqueDto
                    {
                        Id = m.Id,
                        Descricao = m.Descricao
                    })
                    .ToArrayAsync();

                return consulta;
            }
        }
    }
}
