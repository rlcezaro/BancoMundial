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
    public class ContaPoupancasController : Controller
    {
        private readonly BancoMundialContext _context;

        public ContaPoupancasController(BancoMundialContext context)
        {
            _context = context;
        }

        // GET: ContaPoupancas
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContasPoupanca.ToListAsync());
        }

        // GET: ContaPoupancas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaPoupanca = await _context.ContasPoupanca
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contaPoupanca == null)
            {
                return NotFound();
            }

            return View(contaPoupanca);
        }

        // GET: ContaPoupancas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContaPoupancas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumeroConta,Agencia,Saldo,TaxaSaque")] ContaPoupanca contaPoupanca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contaPoupanca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contaPoupanca);
        }

        // GET: ContaPoupancas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaPoupanca = await _context.ContasPoupanca.FindAsync(id);
            if (contaPoupanca == null)
            {
                return NotFound();
            }
            return View(contaPoupanca);
        }

        // POST: ContaPoupancas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumeroConta,Agencia,Saldo,TaxaSaque")] ContaPoupanca contaPoupanca)
        {
            if (id != contaPoupanca.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contaPoupanca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContaPoupancaExists(contaPoupanca.Id))
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
            return View(contaPoupanca);
        }

        // GET: ContaPoupancas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaPoupanca = await _context.ContasPoupanca
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contaPoupanca == null)
            {
                return NotFound();
            }

            return View(contaPoupanca);
        }

        // POST: ContaPoupancas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contaPoupanca = await _context.ContasPoupanca.FindAsync(id);
            if (contaPoupanca != null)
            {
                _context.ContasPoupanca.Remove(contaPoupanca);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContaPoupancaExists(int id)
        {
            return _context.ContasPoupanca.Any(e => e.Id == id);
        }
    }
}
