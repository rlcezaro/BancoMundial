namespace BancoMundial.Models
{
    public class ContaSalario : ContaBancaria
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
            throw new InvalidOperationException("Depósitos não são permitidos em Conta Salário.");
        }

        public override void Transferir(decimal valor, ContaBancaria contaDestino)
        {
            if (Titular == contaDestino.Titular)
            {
                Sacar(valor);
                contaDestino.Depositar(valor);
            }
            else
            {
                throw new InvalidOperationException("Transferência permitida apenas para contas do mesmo titular.");
            }
        }
    }
}
