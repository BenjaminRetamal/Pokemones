using System;

namespace PokemonModels
{
    public class Batalla
    {
        public Pokemon PokemonJugador { get; set; }
        public Pokemon PokemonEnemigo { get; set; }
        public bool EnCurso { get; private set; }

        public Batalla(Pokemon jugador, Pokemon enemigo)
        {
            PokemonJugador = jugador;
            PokemonEnemigo = enemigo;
            EnCurso = true;
        }

        public void Atacar(Pokemon atacante, Pokemon defensor, Ataque ataque)
        {
            int danio = CalcularDanio(ataque, atacante, defensor);
            defensor.SaludActual = Math.Max(defensor.SaludActual - danio, 0);
        }

        public int CalcularDanio(Ataque ataque, Pokemon atacante, Pokemon defensor)
        {
            double efectividad = ObtenerEfectividad(ataque.Tipo, defensor.Tipo);
            return (int)(ataque.Potencia * efectividad);
        }

        public double ObtenerEfectividad(TipoPokemon tipoAtaque, TipoPokemon tipoDefensor)
        {
            if ((tipoAtaque == TipoPokemon.Agua && tipoDefensor == TipoPokemon.Fuego) ||
                (tipoAtaque == TipoPokemon.Fuego && tipoDefensor == TipoPokemon.Planta) ||
                (tipoAtaque == TipoPokemon.Planta && tipoDefensor == TipoPokemon.Agua) ||
                (tipoAtaque == TipoPokemon.Electrico && tipoDefensor == TipoPokemon.Agua))
                return 2.0;

            if ((tipoAtaque == TipoPokemon.Fuego && tipoDefensor == TipoPokemon.Agua) ||
                (tipoAtaque == TipoPokemon.Planta && tipoDefensor == TipoPokemon.Fuego) ||
                (tipoAtaque == TipoPokemon.Agua && tipoDefensor == TipoPokemon.Planta))
                return 0.5;

            return 1.0;
        }
    }
}