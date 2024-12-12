namespace BancoMundial.Models
{
    public abstract class Cliente
    {
        //public int NumeroDePessoas { get; set; }
        public int Id { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
