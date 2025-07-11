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
            // Súper efectivo
            if ((tipoAtaque == TipoPokemon.Agua && (tipoDefensor == TipoPokemon.Fuego || tipoDefensor == TipoPokemon.Roca || tipoDefensor == TipoPokemon.Tierra)) ||
                (tipoAtaque == TipoPokemon.Fuego && (tipoDefensor == TipoPokemon.Planta || tipoDefensor == TipoPokemon.Bicho || tipoDefensor == TipoPokemon.Hielo)) ||
                (tipoAtaque == TipoPokemon.Planta && (tipoDefensor == TipoPokemon.Agua || tipoDefensor == TipoPokemon.Tierra || tipoDefensor == TipoPokemon.Roca)) ||
                (tipoAtaque == TipoPokemon.Electrico && (tipoDefensor == TipoPokemon.Agua || tipoDefensor == TipoPokemon.Volador)) ||
                (tipoAtaque == TipoPokemon.Tierra && (tipoDefensor == TipoPokemon.Fuego || tipoDefensor == TipoPokemon.Electrico || tipoDefensor == TipoPokemon.Roca)) ||
                (tipoAtaque == TipoPokemon.Hielo && (tipoDefensor == TipoPokemon.Planta || tipoDefensor == TipoPokemon.Tierra || tipoDefensor == TipoPokemon.Volador || tipoDefensor == TipoPokemon.Dragon)) ||
                (tipoAtaque == TipoPokemon.Lucha && (tipoDefensor == TipoPokemon.Normal || tipoDefensor == TipoPokemon.Hielo || tipoDefensor == TipoPokemon.Roca)) ||
                (tipoAtaque == TipoPokemon.Volador && (tipoDefensor == TipoPokemon.Planta || tipoDefensor == TipoPokemon.Lucha || tipoDefensor == TipoPokemon.Bicho)) ||
                (tipoAtaque == TipoPokemon.Bicho && (tipoDefensor == TipoPokemon.Planta)))
                return 2.0;

            // No muy efectivo
            if ((tipoAtaque == TipoPokemon.Agua && (tipoDefensor == TipoPokemon.Agua || tipoDefensor == TipoPokemon.Planta)) ||
                (tipoAtaque == TipoPokemon.Fuego && (tipoDefensor == TipoPokemon.Fuego || tipoDefensor == TipoPokemon.Agua || tipoDefensor == TipoPokemon.Roca)) ||
                (tipoAtaque == TipoPokemon.Planta && (tipoDefensor == TipoPokemon.Fuego || tipoDefensor == TipoPokemon.Planta || tipoDefensor == TipoPokemon.Bicho || tipoDefensor == TipoPokemon.Volador)) ||
                (tipoAtaque == TipoPokemon.Electrico && (tipoDefensor == TipoPokemon.Electrico || tipoDefensor == TipoPokemon.Planta)) ||
                (tipoAtaque == TipoPokemon.Tierra && (tipoDefensor == TipoPokemon.Planta || tipoDefensor == TipoPokemon.Bicho)) ||
                (tipoAtaque == TipoPokemon.Hielo && (tipoDefensor == TipoPokemon.Fuego || tipoDefensor == TipoPokemon.Agua || tipoDefensor == TipoPokemon.Hielo)) ||
                (tipoAtaque == TipoPokemon.Lucha && (tipoDefensor == TipoPokemon.Volador || tipoDefensor == TipoPokemon.Bicho)) ||
                (tipoAtaque == TipoPokemon.Volador && (tipoDefensor == TipoPokemon.Electrico || tipoDefensor == TipoPokemon.Roca)) ||
                (tipoAtaque == TipoPokemon.Bicho && (tipoDefensor == TipoPokemon.Fuego || tipoDefensor == TipoPokemon.Lucha || tipoDefensor == TipoPokemon.Volador)))
                return 0.5;

            // Sin efecto
            if ((tipoAtaque == TipoPokemon.Electrico && tipoDefensor == TipoPokemon.Tierra) ||
                (tipoAtaque == TipoPokemon.Normal && tipoDefensor == TipoPokemon.Fantasma) ||
                (tipoAtaque == TipoPokemon.Lucha && tipoDefensor == TipoPokemon.Fantasma) ||
                (tipoAtaque == TipoPokemon.Tierra && tipoDefensor == TipoPokemon.Volador) ||
                (tipoAtaque == TipoPokemon.Fantasma && tipoDefensor == TipoPokemon.Normal))
                return 0.0;

            return 1.0;
        }
    }
}