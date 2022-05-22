using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CitasPlatform.Data;
using CitasPlatform.Models;

namespace CitasPlatform.Controllers
{
    public class Cita_UsuarioController : Controller
    {
        private readonly CitasPlatformContext _context;

        public Cita_UsuarioController(CitasPlatformContext context)
        {
            _context = context;
        }

        // GET: Cita_Usuario
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cita_Usuario.ToListAsync());
        }

        // GET: Cita_Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita_Usuario = await _context.Cita_Usuario
                .FirstOrDefaultAsync(m => m.Cita_UsuarioId == id);
            if (cita_Usuario == null)
            {
                return NotFound();
            }

            return View(cita_Usuario);
        }

        // GET: Cita_Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cita_Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cita_UsuarioId,CitaId,UsuarioId")] Cita_Usuario cita_Usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cita_Usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cita_Usuario);
        }

        // GET: Cita_Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita_Usuario = await _context.Cita_Usuario.FindAsync(id);
            if (cita_Usuario == null)
            {
                return NotFound();
            }
            return View(cita_Usuario);
        }

        // POST: Cita_Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cita_UsuarioId,CitaId,UsuarioId")] Cita_Usuario cita_Usuario)
        {
            if (id != cita_Usuario.Cita_UsuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cita_Usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Cita_UsuarioExists(cita_Usuario.Cita_UsuarioId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cita_Usuario);
        }

        // GET: Cita_Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita_Usuario = await _context.Cita_Usuario
                .FirstOrDefaultAsync(m => m.Cita_UsuarioId == id);
            if (cita_Usuario == null)
            {
                return NotFound();
            }

            return View(cita_Usuario);
        }

        // POST: Cita_Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cita_Usuario = await _context.Cita_Usuario.FindAsync(id);
            _context.Cita_Usuario.Remove(cita_Usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Cita_UsuarioExists(int id)
        {
            return _context.Cita_Usuario.Any(e => e.Cita_UsuarioId == id);
        }
    }
}
