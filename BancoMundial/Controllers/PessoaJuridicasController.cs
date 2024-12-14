using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BancoMundial.Data;
using BancoMundial.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BancoMundial.Controllers
{
    public class PessoaJuridicasController : Controller
    {
        private readonly BancoMundialContext _context;

        public PessoaJuridicasController(BancoMundialContext context)
        {
            _context = context;
        }

        // GET: PessoaJuridicas
        public async Task<IActionResult> Index()
        {
            var pessoasJuridicas = await _context.PessoasJuridicas
                .Include(p => p.Socios)
                .ToListAsync();
            return View(pessoasJuridicas);
        }

        // GET: PessoaJuridicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaJuridica = await _context.PessoasJuridicas
                .Include(p => p.Socios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoaJuridica == null)
            {
                return NotFound();
            }

            return View(pessoaJuridica);
        }

        // GET: PessoaJuridicas/Create
        public IActionResult Create()
        {
            ViewBag.Socios = _context.PessoasFisicas.ToList();
            return View();
        }

        // POST: PessoaJuridicas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CNPJ,RazaoSocial,NomeFantasia,InscricaoEstadual,DataAbertura,Faturamento,Id,Endereco,Telefone,Email")] PessoaJuridica pessoaJuridica, int[] sociosIds)
        {
            if (ModelState.IsValid)
            {
                pessoaJuridica.Socios = _context.PessoasFisicas.Where(p => sociosIds.Contains(p.Id)).ToList();
                pessoaJuridica.AtualizarIdade();
                _context.Add(pessoaJuridica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Socios = _context.PessoasFisicas.ToList();
            return View(pessoaJuridica);
        }

        // GET: PessoaJuridicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaJuridica = await _context.PessoasJuridicas.Include(p => p.Socios).FirstOrDefaultAsync(p => p.Id == id);
            if (pessoaJuridica == null)
            {
                return NotFound();
            }
            ViewBag.Socios = _context.PessoasFisicas.ToList();
            return View(pessoaJuridica);
        }

        // POST: PessoaJuridicas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CNPJ,RazaoSocial,NomeFantasia,InscricaoEstadual,DataAbertura,Faturamento,Id,Endereco,Telefone,Email")] PessoaJuridica pessoaJuridica, int[] sociosIds)
        {
            if (id != pessoaJuridica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingPessoaJuridica = await _context.PessoasJuridicas.Include(p => p.Socios).FirstOrDefaultAsync(p => p.Id == id);
                    if (existingPessoaJuridica == null)
                    {
                        return NotFound();
                    }

                    existingPessoaJuridica.CNPJ = pessoaJuridica.CNPJ;
                    existingPessoaJuridica.RazaoSocial = pessoaJuridica.RazaoSocial;
                    existingPessoaJuridica.NomeFantasia = pessoaJuridica.NomeFantasia;
                    existingPessoaJuridica.InscricaoEstadual = pessoaJuridica.InscricaoEstadual;
                    existingPessoaJuridica.DataAbertura = pessoaJuridica.DataAbertura;
                    existingPessoaJuridica.Faturamento = pessoaJuridica.Faturamento;
                    existingPessoaJuridica.Endereco = pessoaJuridica.Endereco;
                    existingPessoaJuridica.Telefone = pessoaJuridica.Telefone;
                    existingPessoaJuridica.Email = pessoaJuridica.Email;
                    existingPessoaJuridica.Socios = _context.PessoasFisicas.Where(p => sociosIds.Contains(p.Id)).ToList();
                    existingPessoaJuridica.AtualizarIdade();

                    _context.Update(existingPessoaJuridica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaJuridicaExists(pessoaJuridica.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewBag.Socios = _context.PessoasFisicas.ToList();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Socios = _context.PessoasFisicas.ToList();
            return View(pessoaJuridica);
        }

        // GET: PessoaJuridicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaJuridica = await _context.PessoasJuridicas
                .Include(p => p.Socios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoaJuridica == null)
            {
                return NotFound();
            }

            return View(pessoaJuridica);
        }

        // POST: PessoaJuridicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoaJuridica = await _context.PessoasJuridicas.FindAsync(id);
            if (pessoaJuridica != null)
            {
                _context.PessoasJuridicas.Remove(pessoaJuridica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaJuridicaExists(int id)
        {
            return _context.PessoasJuridicas.Any(e => e.Id == id);
        }

        // Método para calcular idade
        [HttpGet]
        public IActionResult CalcularIdade(DateTime dataAbertura)
        {
            var idade = Auxiliar.CalcularIdade(dataAbertura);
            return Json(new { idade });
        }
    }
}