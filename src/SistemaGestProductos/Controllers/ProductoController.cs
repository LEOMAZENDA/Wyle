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
    public class ProductoController : Controller
    {
        private readonly SystemAppContext _context;
        public ProductoController(SystemAppContext context)
        {
            _context = context;
        }
        // GET: Producto
        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.OrderBy(x => x.Nome).Include(x => x.Categoria)
                .AsNoTracking().ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> CreateOuEdit(int? id)
        {
            var categorias = _context.Categorias.OrderBy(x => x.Nome).AsNoTracking().ToList();// trazer as categoria do inner join
            var categoriasSelectList = new SelectList(categorias, nameof(Categoria.Id), nameof(Categoria.Nome));

            ViewBag.Categorias = categoriasSelectList;

            if (id.HasValue)
            {
                var producto = await _context.Productos.FindAsync(id);
                if (producto == null)
                {
                    return NotFound();
                }
                return View(producto);
            }
            return View(new Producto());
        }

        private bool ProductoExstente(int id)
        {
            return _context.Productos.Any(x => x.Id == id);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOuEdit(int? id, [FromForm] Producto producto)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue)//Verificar se tem id para alterar
                {
                    if (ProductoExstente(id.Value))
                    {
                        _context.Productos.Update(producto);
                        _context.Entry(producto).Property(x => x.DataRegisto).IsModified = false;
                        if (await _context.SaveChangesAsync() > 0)
                        {
                            TempData["mensagem"] = MensagemModel.Serializar
                                ("Producto alterado com sucesso.");
                        }
                        else
                        {
                            TempData["mensagem"] = MensagemModel.Serializar
                                ("Erro ao alterar o producto.", TipoMensagem.Erro);
                        }
                    }
                    else
                    {
                        TempData["mensagem"] = MensagemModel.Serializar
                            ("Producto não encontrado.", TipoMensagem.Erro);
                    }
                }
                if (id < 1)//Verificar se o id é menor que 1 para guardar
                {
                    _context.Productos.Add(producto);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        TempData["mensagem"] = MensagemModel.Serializar
                           ("Producto guardado com sucesso.");
                    }
                    else
                    {
                        TempData["mensagem"] = MensagemModel.Serializar
                           ("Erro ao guardar o producto.", TipoMensagem.Erro);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(producto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Producto não informado.",
                    TipoMensagem.Erro);
                return RedirectToAction(nameof(Index));
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Producto não encontrado.",
                   TipoMensagem.Erro);
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                if (await _context.SaveChangesAsync() > 0)
                    TempData["mensagem"] = MensagemModel.Serializar
                        ("Producto eliminado com sucesso.");
                else
                    TempData["mensagem"] = MensagemModel.Serializar
                       ("Não foi possível eliminar o producto.", TipoMensagem.Erro);
            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar
                       ("Producto não foi encontrado.", TipoMensagem.Erro);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
