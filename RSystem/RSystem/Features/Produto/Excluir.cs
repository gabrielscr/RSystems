using MediatR;
using Microsoft.EntityFrameworkCore;
using RSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tempus.Utils;

namespace RochaSystem.Features.Produto
{
    public class Excluir
    {
        public class Command : IRequest<int>
        {
            public int Id { get; set; }
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
                var item = await _adminContext.Set<RSystem.Domain.Marca>()
                    .FirstOrDefaultAsync(p => p.Id == message.Id);

                ChecarSe.Encontrou(item);

                _adminContext.Remove(item);

                return await _adminContext.SaveChangesAsync();
            }
        }
    }
}
