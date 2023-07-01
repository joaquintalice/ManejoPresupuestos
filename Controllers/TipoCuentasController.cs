using Dapper;
using ManejoPresupuestos2.Interfaces;
using ManejoPresupuestos2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuestos2.Controllers
{
    public class TipoCuentasController : Controller
    {
        private readonly IRepositorioTiposCuentas _repositorioTiposCuentas;

        public readonly string connectionString;

        public TipoCuentasController(IRepositorioTiposCuentas repositorioTiposCuentas)
        {
            _repositorioTiposCuentas = repositorioTiposCuentas;
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(TipoCuenta tipoCuenta)
        {
            if (!ModelState.IsValid)
            {
                return View(tipoCuenta);
            }

            tipoCuenta.UsuarioId = 1;


            var yaExisteTipoCuenta = await _repositorioTiposCuentas.Existe(tipoCuenta.Nombre, tipoCuenta.UsuarioId);

            if (yaExisteTipoCuenta)
            {
                ModelState.AddModelError(nameof(tipoCuenta.Nombre), $"El nombre {tipoCuenta.Nombre} ya existe.");

                return View(tipoCuenta);
            }


            await _repositorioTiposCuentas.Crear(tipoCuenta);

            return View();
        }


    }
}
