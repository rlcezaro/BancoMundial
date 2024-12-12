namespace BancoMundial.Models
{
    public class ContaPoupanca : ContaBancaria
    {
        public override void Sacar(decimal valor)
        {
            if (Saldo - valor >= 0)
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
