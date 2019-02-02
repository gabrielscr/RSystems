using MediatR;
using RSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace RochaSystem.Features.Marca
{
    public class Index
    {
        public class Query : IRequest<MarcaDto[]>
        {
        }

        public class MarcaDto
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Estoque { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, MarcaDto[]>
        {
            private readonly AdminContext _adminContext;
            private readonly IMediator _mediator;

            public QueryHandler(AdminContext adminContext, IMediator mediator)
            {
                _adminContext = adminContext;
                _mediator = mediator;
            }

            public async Task<MarcaDto[]> Handle(Query message, CancellationToken cancellationToken)
            {
                var consulta = await _adminContext
                    .Set<RSystem.Domain.Marca>()
                    .AsNoTracking()
                    .OrderBy(m => m.Id)
                    .Select(m => new MarcaDto
                    {
                        Id = m.Id,
                        Descricao = m.Descricao,
                        Estoque = m.Estoque.Descricao
                    })
                    .ToArrayAsync();

                return consulta;
            }
        }
    }
}
