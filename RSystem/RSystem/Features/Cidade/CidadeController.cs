using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSystem.Features.Cidade
{
    public class CidadeController : Controller
    {
        readonly IMediator _mediator;

    public CidadeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("Cidade")]
    [Route("Cidade/{uf}")]
    [HttpGet]
    public async Task<IActionResult> Get(Listar.Query request)
    {
        return Json(await _mediator.Send(request));
    }
}
}
