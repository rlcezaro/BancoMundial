namespace BancoMundial.Models
{
    public static class Auxiliar
    {
        public static int CalcularIdade(DateTime dataNascimento)
        {
            int idade = DateTime.Now.Year - dataNascimento.Year;
            if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
                idade--;

            return idade;
        }

        public static string DeterminarFaixaEtaria(int idade)
        {
            if (idade <= 11)
                return "criança";
            else if (idade <= 21)
                return "jovem";
            else if (idade <= 59)
                return "adulto";
            else
                return "idoso";
        }
    }
}
