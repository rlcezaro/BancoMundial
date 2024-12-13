using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BancoMundial.Data;
using BancoMundial.Models;

namespace BancoMundial.Controllers
{
    public class PessoaFisicasController : Controller
    {
        private readonly BancoMundialContext _context;

        public PessoaFisicasController(BancoMundialContext context)
        {
            _context = context;
        }

        // GET: PessoaFisicas
        public async Task<IActionResult> Index()
        {
            return View(await _context.PessoasFisicas.ToListAsync());
        }

        // GET: PessoaFisicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaFisica = await _context.PessoasFisicas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoaFisica == null)
            {
                return NotFound();
            }

            return View(pessoaFisica);
        }

        // GET: PessoaFisicas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PessoaFisicas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Sobrenome,RG,CPF,DataNascimento,Idade,FaixaEtaria,Renda,Endereco,Telefone,Email")] PessoaFisica pessoaFisica)
        {
            if (ModelState.IsValid)
            {
                pessoaFisica.AtualizarIdadeEFaixaEtaria();
                _context.Add(pessoaFisica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pessoaFisica);
        }

        // GET: PessoaFisicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaFisica = await _context.PessoasFisicas.FindAsync(id);
            if (pessoaFisica == null)
            {
                return NotFound();
            }
            return View(pessoaFisica);
        }

        // POST: PessoaFisicas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sobrenome,RG,CPF,DataNascimento,Idade,FaixaEtaria,Renda,Endereco,Telefone,Email")] PessoaFisica pessoaFisica)
        {
            if (id != pessoaFisica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pessoaFisica.AtualizarIdadeEFaixaEtaria();
                    _context.Update(pessoaFisica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaFisicaExists(pessoaFisica.Id))
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
            return View(pessoaFisica);
        }

        // GET: PessoaFisicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaFisica = await _context.PessoasFisicas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoaFisica == null)
            {
                return NotFound();
            }

            return View(pessoaFisica);
        }

        // POST: PessoaFisicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoaFisica = await _context.PessoasFisicas.FindAsync(id);
            if (pessoaFisica != null)
            {
                _context.PessoasFisicas.Remove(pessoaFisica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaFisicaExists(int id)
        {
            return _context.PessoasFisicas.Any(e => e.Id == id);
        }

        // Novo método para calcular idade e faixa etária
        [HttpGet]
        public IActionResult CalcularIdadeEFaixaEtaria(DateTime dataNascimento)
        {
            var idade = Auxiliar.CalcularIdade(dataNascimento);
            var faixaEtaria = Auxiliar.DeterminarFaixaEtaria(idade);
            return Json(new { idade, faixaEtaria });
        }
    }
}
