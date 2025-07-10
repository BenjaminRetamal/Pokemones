using System.Collections.Generic;

namespace PokemonModels
{
    public class Entrenador
    {
        public string Nombre { get; set; }
        public List<Pokemon> Pokemones { get; set; }
        public List<Pocion> Pociones { get; set; }

        public Entrenador(string nombre)
        {
            Nombre = nombre;
            Pokemones = new List<Pokemon>();
            Pociones = new List<Pocion>();
        }

        public void AgregarPokemon(Pokemon pokemon)
        {
            Pokemones.Add(pokemon);
        }

        public void AgregarPocion(Pocion pocion)
        {
            Pociones.Add(pocion);
        }

        public bool UsarPocion(Pokemon pokemon, TipoPocion tipoPocion)
        {
            var pocion = Pociones.Find(p => p.Tipo == tipoPocion);
            if (pocion != null)
            {
                pokemon.Curar(pocion.CantidadCuracion);
                Pociones.Remove(pocion);
                return true;
            }
            return false;
        }
    }
}