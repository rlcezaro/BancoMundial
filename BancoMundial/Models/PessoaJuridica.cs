namespace BancoMundial.Models
{
    public class PessoaJuridica : Cliente
    {
        public List<PessoaFisica> Socios { get; set; } = new List<PessoaFisica>();
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string InscricaoEstadual { get; set; }
        public DateTime DataAbertura { get; set; }
        public decimal Faturamento { get; set; } // Removido o private set;
        public int Idade { get; private set; }

        public void AtualizarIdade()
        {
            Idade = Auxiliar.CalcularIdade(DataAbertura);
        }
    }
}
