namespace BancoMundial.Models
{
    public class PessoaFisica : Cliente
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Idade { get; private set; }
        public string FaixaEtaria { get; set; } // Removido o private set;
        public decimal Renda { get; set; }

        public void AtualizarIdadeEFaixaEtaria()
        {
            Idade = Auxiliar.CalcularIdade(DataNascimento);
            FaixaEtaria = Auxiliar.DeterminarFaixaEtaria(Idade);
        }
    }
}
