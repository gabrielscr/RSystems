﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tempus.Utils.FluentValidation;

namespace RochaSystem.Features.Marca
{

    [Route("Marca")]
    public class MarcaController : Controller
    {
        private readonly IMediator _mediator;

        public MarcaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("Index")]
        public async Task<IActionResult> Index(Index.Query request)
        {
            var model = await _mediator.Send(request);

            return View(model);
        }

        [Route("Inserir")]
        public async Task<IActionResult> Inserir(InserirEditar.Query request)
        {
            var model = await _mediator.Send(request);

            return View(nameof(InserirEditar), model);
        }

        [HttpPost]
        [Route("Inserir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inserir(InserirEditar.Command request)
        {
            try
            {
                await _mediator.Send(request);

                return RedirectToAction("Index", new { cache = DateTime.Now.Ticks });
            }
            catch (Exception ex)
            {
                if (!ModelState.AdicionarErrosDeValidacao(ex))
                    throw;
            }

            return View(nameof(InserirEditar), request);
        }

        [Route("Editar/{id:int}")]
        public async Task<IActionResult> Editar(InserirEditar.Query request)
        {
            var model = await _mediator.Send(request);

            return View(nameof(InserirEditar), model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Editar/{id:int}")]
        public async Task<IActionResult> Editar(InserirEditar.Command request)
        {
            try
            {
                await _mediator.Send(request);

                return RedirectToAction("Index", new { cache = DateTime.Now.Ticks });
            }
            catch (Exception ex)
            {
                if (!ModelState.AdicionarErrosDeValidacao(ex))
                    throw;
            }

            return View(nameof(InserirEditar), request);
        }

        [HttpPost]
        [Route("Excluir/{id:int}")]
        public async Task Excluir(Excluir.Command request)
        {
            await _mediator.Send(request);
        }
    }
}
