using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tempus.Utils;

namespace RochaSystem.Features.Marca
{
    public class InserirEditar
    {
        public class Query : IRequest<Command>
        {
            public int? Id { get; set; }

        }

        public class Command : IRequest<int>
        {
            public int? Id { get; set; }

            public string Descricao { get; set; }

            public int EstoqueId { get; set; }

            public bool Editando { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Command>
        {
            private readonly AdminContext _adminContext;
            private readonly IMediator _mediator;

            public QueryHandler(AdminContext adminContext, IMediator mediator)
            {
                _adminContext = adminContext;
                _mediator = mediator;
            }

            public async Task<Command> Handle(Query message, CancellationToken cancellationToken)
            {
                Command command;

                if (!message.Id.HasValue)
                {

                    var ultimoId = await _adminContext
                        .Set<RSystem.Domain.Marca>()
                        .Take(1)
                        .OrderByDescending(o => o.Id)
                        .Select(s => s.Id)
                        .FirstOrDefaultAsync();

                    command = new Command
                    {
                        Id = ultimoId + 1,
                        Editando = false
                    };
                }
                else
                {
                    command = await _adminContext
                        .Set<RSystem.Domain.Marca>()
                        .AsNoTracking()
                        .Select(m => new Command
                        {
                            Id = m.Id,
                            Descricao = m.Descricao,
                            EstoqueId = m.EstoqueId

                        })
                        .FirstOrDefaultAsync(m => m.Id == message.Id);

                    ChecarSe.Encontrou(command);

                    command.Editando = true;
                }

                return command;
            }
        }

        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly AdminContext _adminContext;

            public CommandHandler(AdminContext adminContext)
            {
                _adminContext = adminContext;
            }

            public async Task<int> Handle(Command message, CancellationToken cancellationToken)
            {
                RSystem.Domain.Marca marca;

                if (!message.Editando)
                {
                    marca = new RSystem.Domain.Marca { };

                    await _adminContext.AddAsync(marca);
                }
                else
                {
                    marca = await _adminContext
                        .Set<RSystem.Domain.Marca>()
                        .FirstOrDefaultAsync(m => m.Id == message.Id);

                    ChecarSe.Encontrou(marca);
                }

                marca.EstoqueId = message.EstoqueId;
                marca.Descricao = message.Descricao;
                marca.Id = message.Id.Value;

                await _adminContext.SaveChangesAsync();

                return marca.Id;
            }
        }
    }
}
