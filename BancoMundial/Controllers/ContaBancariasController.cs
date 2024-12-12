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
    public class ContaBancariasController : Controller
    {
        private readonly BancoMundialContext _context;

        public ContaBancariasController(BancoMundialContext context)
        {
            _context = context;
        }

        // GET: ContaBancarias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contas.ToListAsync());
        }

        // GET: ContaBancarias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaBancaria = await _context.Contas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contaBancaria == null)
            {
                return NotFound();
            }

            return View(contaBancaria);
        }

        // GET: ContaBancarias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContaBancarias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumeroConta,Agencia,Saldo,TaxaSaque")] ContaBancaria contaBancaria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contaBancaria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contaBancaria);
        }

        // GET: ContaBancarias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaBancaria = await _context.Contas.FindAsync(id);
            if (contaBancaria == null)
            {
                return NotFound();
            }
            return View(contaBancaria);
        }

        // POST: ContaBancarias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumeroConta,Agencia,Saldo,TaxaSaque")] ContaBancaria contaBancaria)
        {
            if (id != contaBancaria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contaBancaria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContaBancariaExists(contaBancaria.Id))
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
            return View(contaBancaria);
        }

        // GET: ContaBancarias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaBancaria = await _context.Contas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contaBancaria == null)
            {
                return NotFound();
            }

            return View(contaBancaria);
        }

        // POST: ContaBancarias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contaBancaria = await _context.Contas.FindAsync(id);
            if (contaBancaria != null)
            {
                _context.Contas.Remove(contaBancaria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContaBancariaExists(int id)
        {
            return _context.Contas.Any(e => e.Id == id);
        }
    }
}
