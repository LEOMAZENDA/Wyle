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
    public class VendaController : Controller
    {
        private readonly SystemAppContext _context;
        public VendaController(SystemAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? cid)
        {
            if (cid.HasValue)
            {
                var cliente = await _context.Clientes.FindAsync(cid);
                if (cliente != null)
                {
                    var vendas = await _context.Vendas
                        .Where(p => p.IdCliente == cid && p.Estado =="1")
                        .OrderByDescending(x => x.IdVenda)
                        .AsNoTracking().ToListAsync();

                    ViewBag.Cliente = cliente;
                    return View(vendas);
                }
                else
                {
                    TempData["mensagem"] = MensagemModel.Serializar ("Cliente não encontrado.", 
                        TipoMensagem.Erro);
                    return RedirectToAction("Index", "Cliente");
                }
            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar ("Cliente não Inforádo .",
                    TipoMensagem.Erro);
                return RedirectToAction("Index", "Cliente");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateOuEdit(int? cid)
        {
            if (cid.HasValue)
            {
                var cliente = await _context.Clientes.FindAsync(cid);
                if (cliente != null)
                {
                    _context.Entry(cliente).Collection(c => c.Vendas).Load();
                    Venda venda = null;
                    if (_context.Vendas.Any(p => p.IdCliente == cid && !p.DataVenda.HasValue))
                    {
                        venda = await _context.Vendas
                            .FirstOrDefaultAsync(p => p.IdCliente == cid && !p.DataVenda.HasValue);
                    }
                    else //Creiando Nova venda
                    {
                        venda = new Venda { IdCliente = cid.Value, ValorTotal = 0 };
                        cliente.Vendas.Add(venda);
                        await _context.SaveChangesAsync();
                    }
                    return RedirectToAction("Index", "ItemVenda", new { ved = venda.IdVenda });
                }
                TempData["mensagem"] = MensagemModel.Serializar("Cliente não encontrado", TipoMensagem.Erro);
                return RedirectToAction("Index", "Cliente");
            }
            TempData["mensagem"] = MensagemModel.Serializar("Cliente não informado", TipoMensagem.Erro);
            return RedirectToAction("Index", "Cliente");
        }
    }
}


//private bool VendaExstente(int id)
//{
//    return _context.Vendas.Any(x => x.IdVenda == id);
//}

//[HttpPost]
////[ValidateAntiForgeryToken]
//public async Task<IActionResult> CreateOuEdit(int? id, [FromForm] Venda venda)
//{
//    if (ModelState.IsValid)
//    {
//        if (id.HasValue)//Verificar se tem id para alterar
//        {
//            if (VendaExstente(id.Value))
//            {
//                _context.Vendas.Update(venda);
//                _context.Entry(venda).Property(x => x.DataVenda).IsModified = false;
//                if (await _context.SaveChangesAsync() > 0)
//                {
//                    TempData["mensagem"] = MensagemModel.Serializar
//                        ("Venda alterada com sucesso.");
//                }
//                else
//                {
//                    TempData["mensagem"] = MensagemModel.Serializar
//                        ("Erro ao alterar a venda.", TipoMensagem.Erro);
//                }
//            }
//            else
//            {
//                TempData["mensagem"] = MensagemModel.Serializar
//                    ("Venda não encontrada.", TipoMensagem.Erro);
//            }
//        }
//        if (id<1)//Verificar se o id é menor que 1 para guardar
//        {
//            _context.Vendas.Add(venda);
//            if (await _context.SaveChangesAsync() > 0)
//            {
//                TempData["mensagem"] = MensagemModel.Serializar
//                   ("Venda guardada com sucesso.");
//            }
//            else
//            {
//                TempData["mensagem"] = MensagemModel.Serializar
//                   ("Erro ao guardar a venda.", TipoMensagem.Erro);
//            }
//        }
//        return RedirectToAction(nameof(Index));
//    }
//    else
//    {
//        return View(venda);
//    }
//}

//public async Task<IActionResult> Delete(int? id)
//{
//    if (!id.HasValue)
//    {
//        TempData["mensagem"] = MensagemModel.Serializar("Venda não informada.", 
//            TipoMensagem.Erro);
//        return RedirectToAction(nameof(Index));
//    }

//    var venda = await _context.Vendas.FindAsync(id);
//    if (venda == null)
//    {
//        TempData["mensagem"] = MensagemModel.Serializar("Venda não encontrada.",
//           TipoMensagem.Erro);
//        return RedirectToAction(nameof(Index));
//    }
//    return View(venda);
//}

//[HttpPost]
//public async Task<ActionResult> Delete(int id)
//{
//    var venda = await _context.Vendas.FindAsync(id);
//    if (venda != null)
//    {
//        _context.Vendas.Remove(venda);
//        if (await _context.SaveChangesAsync() > 0)             
//            TempData["mensagem"] = MensagemModel.Serializar
//                ("Venda eliminada com sucesso.");
//        else
//            TempData["mensagem"] = MensagemModel.Serializar
//               ("Não foi possível eliminar a venda.",TipoMensagem.Erro);              
//    }
//    else
//    {
//        TempData["mensagem"] = MensagemModel.Serializar
//               ("Venda não foi encontrada.", TipoMensagem.Erro);
//    }
//    return RedirectToAction(nameof(Index));
//}
   