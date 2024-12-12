namespace BancoMundial.Models
{
    public abstract class ContaBancaria
    {
        public int Id { get; set; }
        public Cliente Titular { get; set; }
        public string NumeroConta { get; set; }
        public string Agencia { get; set; }
        public decimal Saldo { get; protected set; } = 0;
        public decimal TaxaSaque { get; set; }

        public abstract void Sacar(decimal valor);
        public abstract void Depositar(decimal valor);
        public abstract void Transferir(decimal valor, ContaBancaria contaDestino);

        public double ConsultarSaldo()
        {
            return (double)Saldo;
        }
    }
}