using MediatR;
using RSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Tempus.Linq;

namespace RSystem.Features.Cidade
{
    public class Listar
    {

        public class Query : IRequest<CidadeDto[]>
        {
            public int[] Ids { get; set; }
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
                    .Set<RSystem.Domain.Cidade>()
                    .AsNoTracking()
                    
                    .Select(p => new CidadeDto
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        Estado = p.Estado.Nome

                    })
                    .OrderBy(p => p.Nome)
                    .AsQueryable();

                if (request.Ids.IsNotEmpty())
                {
                    consulta = consulta.Where(c => request.Ids.Contains(c.Id));
                }

                return await consulta.ToArrayAsync();
            }
        }
    }
}
