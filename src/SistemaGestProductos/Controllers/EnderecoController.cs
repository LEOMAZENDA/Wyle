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
    public class EnderecoController : Controller
    {
        private readonly SystemAppContext _context;
        public EnderecoController(SystemAppContext context)
        {
            _context = context;
            
        }

        public async Task<IActionResult> Index(int? cid)
        {
            if (cid.HasValue)
            {
                var cliente = await _context.Clientes.FindAsync(cid);
                if (cliente != null)
                {
                    _context.Entry(cliente).Collection(c => c.Enderecos).Load();
                    ViewBag.Cliente = cliente;
                    return View(cliente.Enderecos);
                }
                else
                {
                    TempData["mensagem"] = MensagemModel.Serializar("O Cliente nao foi encontrado.",TipoMensagem.Erro);
                    return RedirectToAction("Index", "Cliente");
                }              
            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar("Só é possivel mostrar endereco e um cliente específico."
                    ,TipoMensagem.Erro);
                return RedirectToAction("Index", "Cliente");
            }            
        }
        
        [HttpGet]
        public async Task<IActionResult> CreateOuEdit(int? cid, int? eid)
        {
            if (cid.HasValue)
            {
                var cliente = await _context.Clientes.FindAsync(cid);
                if (cliente != null)
                {
                    ViewBag.Cliente = cliente;
                    if (eid.HasValue)
                    {
                        _context.Entry(cliente).Collection(c => c.Enderecos).Load();//Busca o cliente e seus endereços
                        var endereco = cliente.Enderecos.FirstOrDefault(e => e.IdEndereco == eid);//pegar o endereço do cliente cujo o id = eid 
                        if (endereco != null)
                        {
                            return View(endereco);
                        }
                        else
                        {
                            TempData["mensagem"] = MensagemModel.Serializar("Endereço nao encontrado.", TipoMensagem.Erro);
                            return RedirectToAction("Index", "Cliente");
                        }
                    }
                    else//Cadastrando  um novo endereço
                    {
                        return View(new Endereco());
                    }
                }
                else
                {
                    TempData["mensagem"] = MensagemModel.Serializar
                          ("Cliente não encontrado.", TipoMensagem.Erro);
                }
                return RedirectToAction("Index");
            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar("Nenhum proprietário de endereço foi informado."
                      , TipoMensagem.Erro);
                return RedirectToAction("Index", "Cliente");
            }
            
        }

        private bool EnderecoExstente(int cid, int eid)
        {
            return _context.Clientes.FirstOrDefault(c => c.Id == cid)
                .Enderecos.Any(e => e.IdEndereco == eid);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOuEdit([FromForm]int? idCliente, [FromForm] Endereco endereco)
        {
            if (idCliente.HasValue)
            {
                var cliente = await _context.Clientes.FindAsync(idCliente);
                ViewBag.Cliente = cliente;
                if (ModelState.IsValid)
                {
                    if (endereco.IdEndereco == 0) 
                        endereco.Selecionado = true;

                    if (endereco.IdEndereco > 0) //verificar para alterar 
                    {
                        if (endereco.Selecionado) 
                            cliente.Enderecos.ToList().ForEach(e => e.Selecionado = false);
                        if (EnderecoExstente(idCliente.Value, endereco.IdEndereco))
                        {
                            var enderecoActual = cliente.Enderecos
                                .FirstOrDefault(e => e.IdEndereco == endereco.IdEndereco);
                            _context.Entry(enderecoActual).CurrentValues.SetValues(endereco);
                            if (_context.Entry(enderecoActual).State == EntityState.Unchanged)
                            {
                                TempData["mensagem"] = MensagemModel.Serializar("Nenhum dado de endereço foi alterado.");
                            }
                            else
                            {
                                if (await _context.SaveChangesAsync() > 0)
                                {
                                    TempData["mensagem"] = MensagemModel.Serializar("Endereço alterado com sucesso.");
                                }
                                else
                                {
                                    TempData["mensagem"] = MensagemModel.Serializar("Erro ao alterar o endereço.", TipoMensagem.Erro);
                                }
                            }
                        }
                        else
                        {
                            TempData["mensagem"] = MensagemModel.Serializar("Endereço não encontrado.", TipoMensagem.Erro);
                        }
                    }                    
                    else // Inclusao de endereço                    
                    {
                        var idEndereco = endereco.IdEndereco > 0 ? cliente.Enderecos
                            .Max(e => e.IdEndereco) + 1 : 1;                        
                        endereco.IdEndereco = idEndereco;
                        _context.Clientes.FirstOrDefault(c => c.Id == idCliente).Enderecos.Add(endereco);
                        if (await _context.SaveChangesAsync() > 0)
                        {
                            TempData["mensagem"] = MensagemModel.Serializar("Endereço adicionado com sucesso.");
                        }
                        else
                        {
                            TempData["mensagem"] = MensagemModel.Serializar("Erro ao adcicionar o endereço.", TipoMensagem.Erro);
                        }
                    }
                    return RedirectToAction("Index", "Endereco", new { cid = idCliente });
                }
                else
                {
                    return View(endereco);
                }
            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar("Não foi informado nenhum cliente."
                    , TipoMensagem.Erro);
                return RedirectToAction("Index", "Cliente");
            }
        } 
       
        //[HttpGet]
        //public async Task<ActionResult> Delete(int? cid, int? eid)
        //{
        //    if (!cid.HasValue)
        //    {
        //        TempData["mensagem"] = MensagemModel.Serializar("cliente não informado.", TipoMensagem.Erro);
        //        return RedirectToAction("Index", "Cliente");
        //    }
        //    if (!eid.HasValue)
        //    {
        //        TempData["mensagem"] = MensagemModel.Serializar("Endereço não informado.", TipoMensagem.Erro);
        //        return RedirectToAction("Index", new { cid = cid });
        //    }
        //    var cliente = await _context.Clientes.FindAsync(cid);
        //    var endereco = cliente.Enderecos.FirstOrDefault(e => e.IdEndereco == eid);
        //    if (endereco == null)
        //    {
        //        TempData["mensagem"] = MensagemModel.Serializar("Endereço não encontrado.", TipoMensagem.Erro);
        //        return RedirectToAction("Index", new { cid = cid });
        //    }
        //    ViewBag.Cliente = cliente;
        //    return View(endereco);
        //}

        //[HttpPost]
        //public async Task<ActionResult> Delete (int IdCliente, int IdEndereco)
        //{
        //    var cliente = await _context.Clientes.FindAsync(IdCliente);
        //    var endereco = cliente.Enderecos.FirstOrDefault(e => e.IdEndereco == IdEndereco);
        //   if (endereco != null)
        //   {
        //        cliente.Enderecos.Remove(endereco);
        //        if (await _context.SaveChangesAsync() >0)
        //        { 
        //            TempData["mensagem"] = MensagemModel.Serializar("Endereço eliminado.");
        //            if (endereco.Selecionado && cliente.Enderecos.Count() > 0)
        //            {
        //                cliente.Enderecos.FirstOrDefault().Selecionado = true;
        //                await _context.SaveChangesAsync();
        //            }
        //        }
        //        else
        //            TempData["mensagem"] = MensagemModel.Serializar("Endereço não encontrado.", TipoMensagem.Erro);
        //   }
        //   else
        //    {
        //        TempData["mensagem"] = MensagemModel.Serializar("Endereço não encontrado.", TipoMensagem.Erro);
        //    }
        //    return RedirectToAction("Index", new {cid = IdCliente });
        //}
    }
}
