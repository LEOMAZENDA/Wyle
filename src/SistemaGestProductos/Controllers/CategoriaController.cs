using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcessoDados.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Entidades;


namespace SistemaGestProductos.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly SystemAppContext _context;
        public CategoriaController(SystemAppContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var categorias = await _context.Categorias.Where(x => x.Estado == "1")
                .AsNoTracking().ToListAsync();
            return View(categorias);
        }
    
        [HttpGet]
        public async Task<IActionResult> CreateOuEdit(int? id)
        {
            if (id.HasValue)
            {
                var categoria = await _context.Categorias.FindAsync(id);
                if (categoria == null)
                {
                    return NotFound();
                }
                return View(categoria);
            }
            return View(new Categoria());
        }

        private bool CategoriaExstente(int id)
        {
            return _context.Categorias.Any(x => x.Id == id);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOuEdit(int? id, [FromForm] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue)//Verificar se tem id para alterar
                {
                    if (CategoriaExstente(id.Value))
                    {
                        _context.Categorias.Update(categoria);
                        _context.Entry(categoria).Property(x => x.DataRegisto).IsModified = false;
                        if (await _context.SaveChangesAsync() > 0)
                        {
                            TempData["mensagem"] = MensagemModel.Serializar
                                ("Categoria alterada com sucesso.");
                        }
                        else
                        {
                            TempData["mensagem"] = MensagemModel.Serializar
                                ("Erro ao alterar a categoria.", TipoMensagem.Erro);
                        }
                    }
                    else
                    {
                        TempData["mensagem"] = MensagemModel.Serializar
                            ("Categoria não encontrada.", TipoMensagem.Erro);
                    }
                }
                if (id<1)//Verificar se o id é menor que 1 para guardar
                {
                    _context.Categorias.Add(categoria);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        TempData["mensagem"] = MensagemModel.Serializar
                           ("Categoria guardada com sucesso.");
                    }
                    else
                    {
                        TempData["mensagem"] = MensagemModel.Serializar
                           ("Erro ao guardar a categoria.", TipoMensagem.Erro);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(categoria);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Categoria não informada.", 
                    TipoMensagem.Erro);
                return RedirectToAction(nameof(Index));
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Categoria não encontrada.",
                   TipoMensagem.Erro);
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Update(categoria);
                categoria.Estado = "0";
                if (await _context.SaveChangesAsync() > 0)             
                    TempData["mensagem"] = MensagemModel.Serializar
                        ("Categoria eliminada com sucesso.");
                else
                    TempData["mensagem"] = MensagemModel.Serializar
                       ("Não foi possível eliminar a categoria.",TipoMensagem.Erro);              
            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar
                       ("Categoria não foi encontrada.", TipoMensagem.Erro);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
