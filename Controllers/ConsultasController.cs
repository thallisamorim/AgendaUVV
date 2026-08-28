using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AgendaUVV.Data;
using AgendaUVV.Models;
using AgendaUVV.ViewModels;

namespace AgendaUVV.Controllers
{
    [Authorize]
    public class ConsultasController : Controller
    {
        private readonly AppDbContext _context;

        public ConsultasController(AppDbContext context)
        {
            _context = context;
        }

        private int UsuarioLogadoId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<IActionResult> Index()
        {
            var consultas = await _context.Consultas
                .Where(c => c.UsuarioId == UsuarioLogadoId)
                .OrderBy(c => c.DataHora)
                .ToListAsync();

            return View(consultas);
        }

        public async Task<IActionResult> Details(int id)
        {
            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == UsuarioLogadoId);

            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsultaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var consulta = new Consulta
            {
                Especialidade = model.Especialidade,
                DataHora = model.DataHora,
                Descricao = model.Descricao,
                UsuarioId = UsuarioLogadoId
            };

            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Consulta cadastrada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == UsuarioLogadoId);

            if (consulta == null)
            {
                return NotFound();
            }

            var model = new ConsultaViewModel
            {
                Id = consulta.Id,
                Especialidade = consulta.Especialidade,
                DataHora = consulta.DataHora,
                Descricao = consulta.Descricao
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ConsultaViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == UsuarioLogadoId);

            if (consulta == null)
            {
                return NotFound();
            }

            consulta.Especialidade = model.Especialidade;
            consulta.DataHora = model.DataHora;
            consulta.Descricao = model.Descricao;

            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Consulta atualizada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == UsuarioLogadoId);

            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == UsuarioLogadoId);

            if (consulta == null)
            {
                return NotFound();
            }

            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Consulta excluída com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}