using System;
using System.Collections.Generic;

namespace PokemonModels
{
    public enum TipoPokemon
    {
        Agua, Fuego, Planta, Electrico, Normal
    }

    public class Pokemon
    {
        public string Nombre { get; set; }
        public string Apodo { get; set; }
        public TipoPokemon Tipo { get; set; }
        public int SaludMaxima { get; set; }
        public int SaludActual { get; set; }
        public List<Ataque> Ataques { get; set; }

        public Pokemon(string nombre, TipoPokemon tipo, int saludMaxima, List<Ataque> ataques)
        {
            Nombre = nombre;
            Apodo = nombre;
            Tipo = tipo;
            SaludMaxima = saludMaxima;
            SaludActual = saludMaxima;
            Ataques = ataques;
        }

        public void EditarApodo(string nuevoApodo)
        {
            Apodo = nuevoApodo;
        }

        public void Curar(int cantidad)
        {
            SaludActual = Math.Min(SaludActual + cantidad, SaludMaxima);
        }

        public bool EstaDebilitado()
        {
            return SaludActual <= 0;
        }
    }
}