using AcessoDados.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestProductos.Controllers
{
    public class EstudantController : Controller
    {
        DynamicParameters param = new DynamicParameters();
        [HttpGet]
        public IActionResult Index()
        {
            return View(GlobalDapper.ReturnList<EStudenteModel>("uspEstudanteShowAll"));
        }
        [HttpGet]
        public IActionResult AddOrEdit(int id=0)
        {
            if (id==0)
            {
                return View();
            }
               
            else
            {
                param.Add("@IdEstudant", id);
                return View(GlobalDapper.ReturnList<EStudenteModel>("uspEstudanteShowById", param).FirstOrDefault<EStudenteModel>());
            }
        }
       
        [HttpPost]
        public IActionResult AddOrEdit(EStudenteModel studante)
        {           
            param.Add("@IdEstudant", studante.IdEstudant);
            param.Add("@Name", studante.Nome);
            param.Add("@Morada", studante.Morada);
            param.Add("@Telefone", studante.Telefone);
            param.Add("@DataNascimento", studante.DataNascimento);
            GlobalDapper.ExecuteWithoutReturn("uspEstudanteAddOrEdit",param);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            param.Add("@IdEstudant", id);
            GlobalDapper.ExecuteWithoutReturn("uspEstudanteDeleteById", param);
            return RedirectToAction("Index");
        }
    }
}
