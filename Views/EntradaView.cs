using System;
using System.Threading;

namespace PokemonCiudad.Views
{
    public class EntradaView
    {
        public void MostrarBienvenida()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"
╔══════════════════════════════════════════════════════════════════════╗
║                                                                      ║
║                    🎮 ¡BIENVENIDO A POKÉMON CIUDAD! 🎮              ║
║                                                                      ║
║             ¡Prepárate para la aventura de tu vida!                 ║
║          Captura, entrena y lucha con tus Pokémon favoritos         ║
║                                                                      ║
║                     ⭐ ¡La aventura te espera! ⭐                    ║
║                                                                      ║
╚══════════════════════════════════════════════════════════════════════╝
            ");
            Console.ResetColor();
            
            MostrarArteAscii();
            Console.WriteLine("\nPresiona cualquier tecla para comenzar tu aventura...");
            Console.ReadKey();
        }

        public void MostrarMenuPrincipal()
        {
            Console.Clear();
            MostrarArteAscii();
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
╔══════════════════════════════════════════════════════════════════════╗
║                          MENÚ PRINCIPAL                             ║
╠══════════════════════════════════════════════════════════════════════╣
║                                                                      ║
║  🌲 1. Explorar el mundo                                            ║
║  🎒 2. Revisar mochila                                              ║
║  ⭐ 3. Ver Pokémon capturados                                       ║
║  🚪 4. Salir del juego                                              ║
║                                                                      ║
╚══════════════════════════════════════════════════════════════════════╝
            ");
            Console.ResetColor();
            Console.Write("Selecciona una opción: ");
        }

        public void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensaje);
            Console.ResetColor();
            Thread.Sleep(1200);
        }

        public void MostrarMensaje(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(mensaje);
            Console.ResetColor();
            Thread.Sleep(1000);
        }

        public void MostrarArteAscii()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"
             ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
            █░░░░░░░░░░░░░░░░POKÉMON CIUDAD░░░░░░░░░░░░░░░░█
            █▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄█
            ");
            Console.ResetColor();
        }

        public void MostrarPokemonCapturados(System.Collections.Generic.List<PokemonModels.Pokemon> pokemones)
        {
            Console.Clear();
            MostrarArteAscii();
            
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
╔══════════════════════════════════════════════════════════════════════╗
║                        🌟 POKÉMON CAPTURADOS 🌟                     ║
╚══════════════════════════════════════════════════════════════════════╝
            ");
            Console.ResetColor();
            
            if (pokemones.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(@"
                    📭 No tienes Pokémon aún 📭
                    
                ¡Sal a explorar y captura tu primer compañero!
                    🌲 → 🔍 → 🥎 → ⭐
                ");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"¡Tienes {pokemones.Count} Pokémon en tu equipo!\n");
                Console.ResetColor();
                
                int i = 1;
                foreach (var poke in pokemones)
                {
                    // Emoji basado en el tipo
                    string emoji = poke.Tipo switch
                    {
                        PokemonModels.TipoPokemon.Fuego => "🔥",
                        PokemonModels.TipoPokemon.Agua => "💧",
                        PokemonModels.TipoPokemon.Planta => "🌱",
                        PokemonModels.TipoPokemon.Electrico => "⚡",
                        PokemonModels.TipoPokemon.Roca => "🪨",
                        PokemonModels.TipoPokemon.Volador => "🪶",
                        _ => "✨"
                    };
                    
                    // Estado de salud visual
                    double porcentajeSalud = (double)poke.SaludActual / poke.SaludMaxima;
                    string barraVida = "";
                    int segmentos = (int)(porcentajeSalud * 10);
                    
                    Console.ForegroundColor = ConsoleColor.Green;
                    for (int j = 0; j < segmentos; j++) barraVida += "█";
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    for (int j = segmentos; j < 10; j++) barraVida += "░";
                    Console.ResetColor();
                    
                    Console.WriteLine($"{emoji} {i}. {poke.Nombre} (Apodo: {poke.Apodo})");
                    Console.WriteLine($"     Tipo: {poke.Tipo} | Salud: [{barraVida}] {poke.SaludActual}/{poke.SaludMaxima}");
                    Console.WriteLine();
                    i++;
                }
            }
        }
    }
}