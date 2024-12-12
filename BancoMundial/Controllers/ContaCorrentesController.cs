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
    public class ContaCorrentesController : Controller
    {
        private readonly BancoMundialContext _context;

        public ContaCorrentesController(BancoMundialContext context)
        {
            _context = context;
        }

        // GET: ContaCorrentes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContasCorrente.ToListAsync());
        }

        // GET: ContaCorrentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaCorrente = await _context.ContasCorrente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contaCorrente == null)
            {
                return NotFound();
            }

            return View(contaCorrente);
        }

        // GET: ContaCorrentes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContaCorrentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tipo,Limite,TaxaJuros,Id,NumeroConta,Agencia,Saldo,TaxaSaque")] ContaCorrente contaCorrente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contaCorrente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contaCorrente);
        }

        // GET: ContaCorrentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaCorrente = await _context.ContasCorrente.FindAsync(id);
            if (contaCorrente == null)
            {
                return NotFound();
            }
            return View(contaCorrente);
        }

        // POST: ContaCorrentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Tipo,Limite,TaxaJuros,Id,NumeroConta,Agencia,Saldo,TaxaSaque")] ContaCorrente contaCorrente)
        {
            if (id != contaCorrente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contaCorrente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContaCorrenteExists(contaCorrente.Id))
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
            return View(contaCorrente);
        }

        // GET: ContaCorrentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaCorrente = await _context.ContasCorrente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contaCorrente == null)
            {
                return NotFound();
            }

            return View(contaCorrente);
        }

        // POST: ContaCorrentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contaCorrente = await _context.ContasCorrente.FindAsync(id);
            if (contaCorrente != null)
            {
                _context.ContasCorrente.Remove(contaCorrente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContaCorrenteExists(int id)
        {
            return _context.ContasCorrente.Any(e => e.Id == id);
        }
    }
}
