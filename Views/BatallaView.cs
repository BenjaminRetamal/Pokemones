using System;
using System.Threading;
using PokemonModels;

namespace PokemonCiudad.Views
{
    public class BatallaView
    {
        public void MostrarInicioBatalla(Pokemon miPokemon, Pokemon enemigo)
        {
            Console.Clear();
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️
                        ¡BATALLA POKÉMON!
⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️⚔️
            ");
            Console.ResetColor();
            
            MostrarArteBatalla(miPokemon, enemigo);
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"¡{miPokemon.Apodo} ({miPokemon.Tipo}) VS {enemigo.Nombre} ({enemigo.Tipo})!");
            Console.ResetColor();
            
            Thread.Sleep(2000);
        }

        public void MostrarAtaque(string atacante, string ataque)
        {
            Console.Write($"{atacante} usa {ataque}");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(300);
                Console.Write(".");
            }
            Console.WriteLine();
        }

        public void MostrarEfectividad(double efectividad)
        {
            if (efectividad > 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("¡Es súper efectivo!");
            }
            else if (efectividad < 1)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("No es muy efectivo...");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Es efectivo.");
            }
            Console.ResetColor();
            Thread.Sleep(700);
        }

        public void MostrarEstado(Pokemon jugador, Pokemon enemigo)
        {
            Console.Clear();
            
            // Encabezado de batalla
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");
            Console.ResetColor();
            
            // Estado del enemigo (arriba)
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"🔥 ENEMIGO: {enemigo.Nombre}");
            MostrarBarraVida(enemigo, ConsoleColor.Red);
            
            Console.WriteLine();
            
            // Arte de batalla simplificado
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                     ⚔️  BATALLA  ⚔️");
            Console.ResetColor();
            
            Console.WriteLine();
            
            // Estado del jugador (abajo)
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"💙 TU POKÉMON: {jugador.Apodo}");
            MostrarBarraVida(jugador, ConsoleColor.Blue);
            
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.WriteLine();
        }

        private void MostrarBarraVida(Pokemon pokemon, ConsoleColor color)
        {
            double porcentaje = (double)pokemon.SaludActual / pokemon.SaludMaxima;
            int segmentos = (int)(porcentaje * 20); // Barra de 20 caracteres
            
            Console.Write("HP: [");
            
            // Parte llena de la barra
            Console.ForegroundColor = porcentaje > 0.5 ? ConsoleColor.Green : 
                                      porcentaje > 0.2 ? ConsoleColor.Yellow : ConsoleColor.Red;
            for (int i = 0; i < segmentos; i++)
            {
                Console.Write("█");
            }
            
            // Parte vacía de la barra
            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (int i = segmentos; i < 20; i++)
            {
                Console.Write("░");
            }
            
            Console.ForegroundColor = color;
            Console.WriteLine($"] {pokemon.SaludActual}/{pokemon.SaludMaxima}");
            Console.ResetColor();
        }

        public void MostrarOpcionHuida(bool exito)
        {
            if (exito)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("¡Has huido exitosamente!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("¡No pudiste huir!");
            }
            Console.ResetColor();
            Thread.Sleep(1000);
        }

        public void MostrarFinBatalla(bool gano)
        {
            if (gano)
                Console.WriteLine("¡Ganaste la batalla!");
            else
                Console.WriteLine("¡Has perdido la batalla!");
            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey();
        }

        private void MostrarArteBatalla(Pokemon miPokemon, Pokemon enemigo)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($@"
   {miPokemon.Apodo}        VS        {enemigo.Nombre}
   (づ｡◕‿‿◕｡)づ           (ง'̀-'́)ง
            ");
            Console.ResetColor();
        }
    }
}