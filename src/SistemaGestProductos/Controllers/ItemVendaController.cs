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
    public class ItemVendaController : Controller
    {
        private readonly SystemAppContext _context;
        public ItemVendaController(SystemAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? ved)
        {
            if (ved.HasValue)
            {
                if (_context.Vendas.Any(v => v.IdVenda == ved))
                {
                    var venda = await _context.Vendas
                        .Include(v => v.Cliente)
                        .Include(v => v.ItensVenda.OrderBy(i => i.Producto.Nome))
                        .ThenInclude(i => i.Producto)
                        .FirstOrDefaultAsync(v => v.IdVenda == ved);

                    ViewBag.Venda = venda;
                    return View(venda.ItensVenda);
                }
                TempData["mensagem"] = MensagemModel.Serializar("Venda não encontrada.", TipoMensagem.Erro);
                return RedirectToAction("Index", "Cliente");
            }
            TempData["mensagem"] = MensagemModel.Serializar("Venda não informada.", TipoMensagem.Erro);
            return RedirectToAction("Index", "Cliente");
        }
 
        [HttpGet]
        public async Task<IActionResult> CreateOuEdit(int? ved, int? prod )
        {
            if (ved.HasValue)
            {
                if (_context.Vendas.Any(v => v.IdVenda == ved))
                {
                    var productos = _context.Productos.OrderBy(x => x.Nome)
                        .Select(p => new { p.Id, NomeProco = $"{p.Nome} ({p.Preco:C})"})
                        .AsNoTracking().ToList();
                    var productosSelectList = new SelectList(productos, "Id", "NomeProco");// para mostrar numa combo box
                    ViewBag.Productos = productosSelectList;

                    if (prod.HasValue && ItemVendaExstente(ved.Value, prod.Value))//Verificar alteração de uma venda existente
                    {
                        var itemVenda = await _context.ItemsVendas
                            .Include(i => i.Producto)
                            .FirstOrDefaultAsync(i => i.IdVenda == ved && i.IdProducto == prod);
                        return View(itemVenda);
                    }
                    else // adicionando novo item
                    {
                        return View(new ItemVenda()
                        { IdVenda = ved.Value, ValorUnitario = 0, Quantidade = 1 });
                    }
                }               
                TempData["mensagem"] = MensagemModel.Serializar("Venda não encontrada.", TipoMensagem.Erro);
                return RedirectToAction("Index", "Cliente");
            }
            TempData["mensagem"] = MensagemModel.Serializar("Venda não informada.", TipoMensagem.Erro);
            return RedirectToAction("Index", "Cliente");
        }

        private bool ItemVendaExstente(int ved, int prod)
        {
            return _context.ItemsVendas.Any(x => x.IdVenda == ved && x.IdProducto == prod);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOuEdit([FromForm] ItemVenda itemVenda)
        {
            if (ModelState.IsValid)
            {
                if (itemVenda.IdVenda > 0)
                {
                    var producto = await _context.Productos.FindAsync(itemVenda.IdProducto);
                    itemVenda.ValorUnitario = producto.Preco;
                    if (ItemVendaExstente(itemVenda.IdVenda, itemVenda.IdProducto)) //Verificar para alterar
                    {
                        _context.ItemsVendas.Update(itemVenda);
                        if (await _context.SaveChangesAsync() > 0)                      
                            TempData["mensagem"] = MensagemModel.Serializar("Producto da Venda " +
                                "alterado com sucesso.");                       
                        else                        
                            TempData["mensagem"] = MensagemModel.Serializar("Erro ao alterar " +
                                  "producto da Venda.", TipoMensagem.Erro);                        
                    }
                    else //Inclusão
                    {
                        _context.ItemsVendas.Add(itemVenda);
                        if (await _context.SaveChangesAsync() > 0)                        
                            TempData["mensagem"] = MensagemModel.Serializar("Producto adicionado na Venda " +
                                " com sucesso.");                       
                        else                  
                            TempData["mensagem"] = MensagemModel.Serializar("Erro ao adicionar " +
                                  "producto na Venda.", TipoMensagem.Erro);                      
                    }
                    var venda = await _context.Vendas.FindAsync(itemVenda.IdVenda);
                    venda.ValorTotal = _context.ItemsVendas
                        .Where(i => i.IdVenda == itemVenda.IdVenda)
                        .Sum(i => i.ValorUnitario * i.Quantidade);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { ved = itemVenda.IdVenda });
                }
                else
                {
                    TempData["mensagem"] = MensagemModel.Serializar("Venda não informada.", TipoMensagem.Erro);
                    return RedirectToAction("Index", "Cliente");
                }
            }
            else
            {
                var productos = _context.Productos.OrderBy(x => x.Nome)
                       .Select(p => new { p.Id, NomeProco = $"{p.Nome} ({p.Preco:C})" })
                       .AsNoTracking().ToList();
                var productosSelectList = new SelectList(productos, "Id", "NomeProco");// para mostrar numa combo box
                ViewBag.Productos = productosSelectList;   
                
                return View(itemVenda);
            }
        }
    }
}

