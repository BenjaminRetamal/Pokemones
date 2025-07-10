using System;
using PokemonCiudad.Controllers;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("¡Bienvenido! Ingresa tu nombre de entrenador: ");
        string nombre = Console.ReadLine();
        var juego = new JuegoController(nombre);
        juego.IniciarJuego();
    }
}