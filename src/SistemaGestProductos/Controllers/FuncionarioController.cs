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


namespace SistemaGestFuncionarios.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly SystemAppContext _context;
        public FuncionarioController(SystemAppContext context)
        {
            _context = context;
        }
        // GET: Funcionario
        public async Task<IActionResult> Index()
        {
            return View(await _context.Funcionarios.OrderBy(x => x.Nome)
                .Include(x => x.Funcao).Where(f => f.Estado =="1")
                .AsNoTracking().ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> CreateOuEdit(int? id)
        {
            var funcoes = _context.Funcoes.OrderBy(x => x.Nome).AsNoTracking().ToList();// trazer as funcoes do inner join
            var funcoesSelectList = new SelectList(funcoes, nameof(Funcao.Id), nameof(Funcao.Nome));

            ViewBag.Funcoes = funcoesSelectList;

            if (id.HasValue)
            {
                var funcionario = await _context.Funcionarios.FindAsync(id);
                if (funcionario == null)
                {
                    return NotFound();
                }
                return View(funcionario);
            }
            return View(new Funcionario());
        }

        private bool FuncionarioExstente(int id)
        {
            return _context.Funcionarios.Any(x => x.Id == id);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOuEdit(int? id, [FromForm] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue)//Verificar se tem id para alterar
                {
                    if (FuncionarioExstente(id.Value))
                    {
                        _context.Funcionarios.Update(funcionario);
                        _context.Entry(funcionario).Property(x => x.DataRegisto).IsModified = false;
                        _context.Entry(funcionario).Property(x => x.Senha).IsModified = false;
                        _context.Entry(funcionario).Property(x => x.Estado).IsModified = false;
                        if (await _context.SaveChangesAsync() > 0)
                        {
                            TempData["mensagem"] = MensagemModel.Serializar
                                ("Funcionario alterado com sucesso.");
                        }
                        else
                        {
                            TempData["mensagem"] = MensagemModel.Serializar
                                ("Erro ao alterar o funcionario.", TipoMensagem.Erro);
                        }
                    }
                    else
                    {
                        TempData["mensagem"] = MensagemModel.Serializar
                            ("Funcionario não encontrado.", TipoMensagem.Erro);
                    }
                }
                if (id < 1)//Verificar se o id é menor que 1 para guardar
                {
                    _context.Funcionarios.Add(funcionario);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        TempData["mensagem"] = MensagemModel.Serializar
                           ("Funcionario guardado com sucesso.");
                    }
                    else
                    {
                        TempData["mensagem"] = MensagemModel.Serializar
                           ("Erro ao guardar o funcionario.", TipoMensagem.Erro);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(funcionario);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Funcionario não informado.",
                    TipoMensagem.Erro);
                return RedirectToAction(nameof(Index));
            }

            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                TempData["mensagem"] = MensagemModel.Serializar("Funcionario não encontrado.",
                   TipoMensagem.Erro);
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario != null)
            {
                _context.Funcionarios.Update(funcionario);
                funcionario.Estado = "0";
                if (await _context.SaveChangesAsync() > 0)
                    TempData["mensagem"] = MensagemModel.Serializar
                        ("Funcionario eliminado com sucesso.");
                else
                    TempData["mensagem"] = MensagemModel.Serializar
                       ("Não foi possível eliminar o funcionario.", TipoMensagem.Erro);
            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar
                       ("Funcionario não foi encontrado.", TipoMensagem.Erro);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
