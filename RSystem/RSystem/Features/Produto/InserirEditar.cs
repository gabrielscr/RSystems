using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RSystem.Common.Domain;
using RSystem.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tempus.Utils;

namespace RochaSystem.Features.Produto
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

            public double Valor { get; set; }

            public int MarcaId { get; set; }

            public UnidadeMedidaEnum UnidadeMedida { get; set; }

            public int Quantidade { get; set; }

            public double Volume { get; set; }

            public bool Ativo { get; set; }

            public string Imagem { get; set; }

            public string DescricaoDetalhada { get; set; }

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
                        .Set<RSystem.Common.Domain.Produto>()
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
                        .Set<RSystem.Common.Domain.Produto>()
                        .AsNoTracking()
                        .Select(m => new Command
                        {
                            Id = m.Id,
                            Descricao = m.Descricao,
                            Valor = m.Valor,
                            MarcaId = m.MarcaId,
                            UnidadeMedida = m.UnidadeMedida,
                            Ativo = m.Ativo,
                            Quantidade = m.Quantidade,
                            Volume = m.Volume,
                            Imagem = m.Imagem,
                            DescricaoDetalhada = m.DescricaoDetalhada

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
                RSystem.Common.Domain.Produto produto;

                if (!message.Editando)
                {
                    produto = new RSystem.Common.Domain.Produto { };

                    await _adminContext.AddAsync(produto);
                }
                else
                {
                    produto = await _adminContext
                        .Set<RSystem.Common.Domain.Produto>()
                        .FirstOrDefaultAsync(m => m.Id == message.Id);

                    ChecarSe.Encontrou(produto);
                }

                produto.Id = message.Id.Value;
                produto.Descricao = message.Descricao;
                produto.Valor = message.Valor;
                produto.MarcaId = message.MarcaId;
                produto.UnidadeMedida = message.UnidadeMedida;
                produto.Ativo = message.Ativo;
                produto.Quantidade = message.Quantidade;
                produto.Volume = message.Volume;
                produto.Imagem = message.Imagem;
                produto.DescricaoDetalhada = message.DescricaoDetalhada;

                await _adminContext.SaveChangesAsync();

                return produto.Id;
            }
        }
    }
}
