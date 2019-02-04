using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tempus.Utils.FluentValidation;

namespace RochaSystem.Features.Produto
{

    [Route("Produto")]
    public class ProdutoController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHostingEnvironment hostingEnvironment;
        public ProdutoController(IMediator mediator, IHostingEnvironment environment)
        {
            hostingEnvironment = environment;
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
                if (request.Imagem != null)
        {
            var uniqueFileName = GetUniqueFileName(request.Imagem.FileName);
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
            var filePath = Path.Combine(uploads,uniqueFileName);
            request.Imagem.CopyTo(new FileStream(filePath, FileMode.Create)); 

            //to do : Save uniqueFileName  to your db table   
        }

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

        private string GetUniqueFileName(string fileName)
    {
        fileName = Path.GetFileName(fileName);
        return  Path.GetFileNameWithoutExtension(fileName)
                  + "_" 
                  + Guid.NewGuid().ToString().Substring(0, 4) 
                  + Path.GetExtension(fileName);
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

        [HttpDelete]
        [Route("Excluir/{id:int}")]
        public async Task Excluir(Excluir.Command request)
        {
            await _mediator.Send(request);

        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePath });
        }
    }
}
