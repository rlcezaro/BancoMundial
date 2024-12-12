namespace BancoMundial.Models
{
    public class ContaCorrente : ContaBancaria
    {
        public enum TipoConta
        {
            SIMPLES,
            ESPECIAL
        }

        public TipoConta Tipo { get; set; }
        public decimal Limite { get; private set; }
        public decimal TaxaJuros { get; private set; }

        public ContaCorrente() { }

        public ContaCorrente(TipoConta tipo)
        {
            Tipo = tipo;
            if (tipo == TipoConta.SIMPLES)
            {
                TaxaJuros = 0.05m;
            }
            else
            {
                TaxaJuros = 0.02m;
            }
        }

        public void ConfigurarLimite(decimal renda)
        {
            if (Tipo == TipoConta.SIMPLES)
            {
                Limite = renda * 1.5m;
            }
            else
            {
                Limite = renda * 2.5m;
            }
        }

        public override void Sacar(decimal valor)
        {
            if (Saldo - valor >= -Limite)
            {
                Saldo -= valor;
            }
            else
            {
                throw new InvalidOperationException("Saldo insuficiente.");
            }
        }

        public override void Depositar(decimal valor)
        {
            Saldo += valor;
        }

        public override void Transferir(decimal valor, ContaBancaria contaDestino)
        {
            Sacar(valor);
            contaDestino.Depositar(valor);
        }
    }
}
