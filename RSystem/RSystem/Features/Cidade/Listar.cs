using MediatR;
using RSystem.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RSystem.Common.Domain;
using Microsoft.EntityFrameworkCore;

namespace RochaSystem.Features.Cidade
{
    public class Listar
    {

        public class Query : IRequest<CidadeDto[]>
        {

        }

        public class CidadeDto
        {
            public int Id { get; set; }

            public string Nome { get; set; }

            public string Estado { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, CidadeDto[]>
        {
            private readonly IMediator _mediator;
            readonly AdminContext _adminContext;

            public QueryHandler(AdminContext adminContext, IMediator mediator)
            {
                _adminContext = adminContext;
                _mediator = mediator;
            }

            public async Task<CidadeDto[]> Handle(Query request, CancellationToken cancellationToken)
            {
                var consulta = _adminContext
                    .Set<RSystem.Common.Domain.Cidade>()
                    .AsNoTracking()
                    
                    .Select(p => new CidadeDto
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        Estado = p.Estado.Nome

                    })
                    .OrderBy(p => p.Nome)
                    .AsQueryable();

                return await consulta.ToArrayAsync();
            }
        }
    }
}
