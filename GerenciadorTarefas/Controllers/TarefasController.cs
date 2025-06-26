using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciadorTarefas.Data;
using GerenciadorTarefas.Models;
using System.Threading.Tasks;

namespace GerenciadorTarefas.Controllers
{
    public class TarefasController : Controller
    {
        private readonly AppDbContext _context;

        public TarefasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Tarefas
        public async Task<IActionResult> Index()
        {
            var tarefas = await _context.Tarefas.ToListAsync();
            return View(tarefas);
        }

        // GET: /Tarefas/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Tarefas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _context.Tarefas.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Volta para a lista
            }
            return View(tarefa); // Se erro, permanece na tela de cadastro
        }

        // GET: /Tarefas/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return NotFound();
            return View(tarefa);
        }

        // POST: /Tarefas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tarefa tarefa)
        {
            if (id != tarefa.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(tarefa).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TarefaExists(tarefa.Id))
                        return NotFound();
                    else
                        throw;
                }
            }
            return View(tarefa);
        }

        // GET: /Tarefas/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return NotFound();
            return View(tarefa);
        }

        // POST: /Tarefas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TarefaExists(int id)
        {
            return await _context.Tarefas.AnyAsync(e => e.Id == id);
        }
    }
}
