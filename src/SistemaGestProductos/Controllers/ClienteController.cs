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
    public class ClienteController : Controller
    {
        private readonly SystemAppContext _context;
        public ClienteController(SystemAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
           var clientes = await _context.Clientes.OrderBy(x => x.Nome)
                .Where(x => x.Estado == "1").AsNoTracking().ToListAsync();
            return View(clientes);
        }

        [HttpGet]
        public async Task<IActionResult> CreateOuEdit(int? id)
        {
            if (id.HasValue)
            {
                var clientes = await _context.Clientes.FindAsync(id);
                if (clientes == null)
                {
                    TempData["mensagem"] = MensagemModel.Serializar
                               ("Cliente não encontrado.", TipoMensagem.Erro);
                    return RedirectToAction(nameof(Index));
                }
                return View(clientes);
            }
            return View(new Cliente());
        }

        private bool ClienteExstente(int id)
        {
            return _context.Clientes.Any(x => x.Id == id);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOuEdit(int? id, [FromForm] Cliente Cliente)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue)//Verificar se tem id para alterar
                {
                    if (ClienteExstente(id.Value))
                    {
                        _context.Clientes.Update(Cliente);
  //_context.Entry(Cliente).Property(x => x.Senha).IsModified = false;
                        _context.Entry(Cliente).Property(x => x.DataRegisto).IsModified = false;
                        if (await _context.SaveChangesAsync() > 0)
                        {
                            TempData["mensagem"] = MensagemModel.Serializar
                                ("Cliente alterado com sucesso.");
                        }
                        else
                        {
                            TempData["mensagem"] = MensagemModel.Serializar
                                ("Erro ao alterar o Cliente.", TipoMensagem.Erro);
                        }
                    }
                    else
                    {
                        TempData["mensagem"] = MensagemModel.Serializar
                            ("Cliente não encontrado.", TipoMensagem.Erro);
                    }
                }
                if (id < 1)//Verificar se o id é menor que 1 para guardar
                {
                    _context.Clientes.Add(Cliente);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        TempData["mensagem"] = MensagemModel.Serializar
                           ("Cliente guardado com sucesso.");
                    }
                    else
                    {
                        TempData["mensagem"] = MensagemModel.Serializar
                           ("Erro ao guardar o Cliente.", TipoMensagem.Erro);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(Cliente);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Cliente não informado.",
                    TipoMensagem.Erro);
                return RedirectToAction(nameof(Index));
            }

            var Cliente = await _context.Clientes.FindAsync(id);
            if (Cliente == null)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Cliente não encontrado.",
                   TipoMensagem.Erro);
                return RedirectToAction(nameof(Index));
            }
            return View(Cliente);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var Cliente = await _context.Clientes.FindAsync(id);
            if (Cliente != null)
            {
                _context.Clientes.Update(Cliente);
                Cliente.Estado = "0";

                if (await _context.SaveChangesAsync() > 0)
                    TempData["mensagem"] = MensagemModel.Serializar
                        ("Cliente eliminado com sucesso.");
                else
                    TempData["mensagem"] = MensagemModel.Serializar
                       ("Não foi possível eliminar o Cliente.", TipoMensagem.Erro);
            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar
                       ("Cliente não foi encontrado.", TipoMensagem.Erro);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
