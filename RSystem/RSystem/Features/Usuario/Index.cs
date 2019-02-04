using MediatR;
using RSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace RSystem.Features.Usuario
{
    public class Index
    {

        public class Query : IRequest<UsuarioDto[]>
        {
        }

        public class UsuarioDto
        {

            public int Id { get; set; }

            public string Login { get; set; }

            public string Nome { get; set; }

            public string Avatar { get; set; }

            public string Email { get; set; }

            public string Senha { get; set; }

        }

        public class QueryHandler : IRequestHandler<Query, UsuarioDto[]>
        {
            readonly AdminContext _adminContext;
            readonly IMediator _mediator;

            public QueryHandler(AdminContext adminContext, IMediator mediator)
            {
                _adminContext = adminContext;
                _mediator = mediator;

            }

            public async Task<UsuarioDto[]> Handle(Query request, CancellationToken cancellationToken)
            {
                var usuarios = await _adminContext
                    .Set<Domain.Usuario>()
                    .AsNoTracking()
                    .Select(s => new UsuarioDto
                    {
                        Id = s.Id,
                        Login = s.Login,
                        Nome = s.Nome,
                        Email = s.Email,
                        Senha = s.Senha

                    }).ToArrayAsync();

                return usuarios;

            }
        }

    }
}
