namespace PokemonModels
{
    public class Ataque
    {
        public string Nombre { get; set; }
        public int Potencia { get; set; }
        public TipoPokemon Tipo { get; set; }

        public Ataque(string nombre, int potencia, TipoPokemon tipo)
        {
            Nombre = nombre;
            Potencia = potencia;
            Tipo = tipo;
        }
    }
}