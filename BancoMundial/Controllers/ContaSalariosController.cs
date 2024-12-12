using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BancoMundial.Data;
using BancoMundial.Models;

namespace BancoMundial.Controllers
{
    public class ContaSalariosController : Controller
    {
        private readonly BancoMundialContext _context;

        public ContaSalariosController(BancoMundialContext context)
        {
            _context = context;
        }

        // GET: ContaSalarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContasSalario.ToListAsync());
        }

        // GET: ContaSalarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaSalario = await _context.ContasSalario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contaSalario == null)
            {
                return NotFound();
            }

            return View(contaSalario);
        }

        // GET: ContaSalarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContaSalarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumeroConta,Agencia,Saldo,TaxaSaque")] ContaSalario contaSalario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contaSalario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contaSalario);
        }

        // GET: ContaSalarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaSalario = await _context.ContasSalario.FindAsync(id);
            if (contaSalario == null)
            {
                return NotFound();
            }
            return View(contaSalario);
        }

        // POST: ContaSalarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumeroConta,Agencia,Saldo,TaxaSaque")] ContaSalario contaSalario)
        {
            if (id != contaSalario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contaSalario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContaSalarioExists(contaSalario.Id))
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
            return View(contaSalario);
        }

        // GET: ContaSalarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaSalario = await _context.ContasSalario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contaSalario == null)
            {
                return NotFound();
            }

            return View(contaSalario);
        }

        // POST: ContaSalarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contaSalario = await _context.ContasSalario.FindAsync(id);
            if (contaSalario != null)
            {
                _context.ContasSalario.Remove(contaSalario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContaSalarioExists(int id)
        {
            return _context.ContasSalario.Any(e => e.Id == id);
        }
    }
}
