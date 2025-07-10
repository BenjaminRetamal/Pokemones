using System;
using System.Collections.Generic;
using System.Threading;
using PokemonModels;

namespace PokemonCiudad.Controllers
{
    public class JuegoController
    {
        private Entrenador entrenador;
        private object lockObj = new object();

        public JuegoController(string nombreEntrenador)
        {
            entrenador = new Entrenador(nombreEntrenador);
            entrenador.AgregarPocion(new Pocion(TipoPocion.Pocion));
        }

        public void IniciarJuego()
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ PRINCIPAL ===");
                Console.WriteLine("1. Explorar");
                Console.WriteLine("2. Mochila");
                Console.WriteLine("3. Pokémon capturados");
                Console.WriteLine("4. Salir");
                Console.Write("Selecciona una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Explorar();
                        break;
                    case "2":
                        MostrarMochila();
                        break;
                    case "3":
                        MostrarPokemones();
                        break;
                    case "4":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Presiona una tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void Explorar()
        {
            Console.Clear();
            var pokemonesSalvajes = new List<Pokemon>
            {
                new Pokemon("Charmander", TipoPokemon.Fuego, 39, new List<Ataque> { new Ataque("Ascuas", 40, TipoPokemon.Fuego) }),
                new Pokemon("Squirtle", TipoPokemon.Agua, 44, new List<Ataque> { new Ataque("Pistola Agua", 40, TipoPokemon.Agua) }),
                new Pokemon("Bulbasaur", TipoPokemon.Planta, 45, new List<Ataque> { new Ataque("Látigo Cepa", 45, TipoPokemon.Planta) }),
                new Pokemon("Pikachu", TipoPokemon.Electrico, 35, new List<Ataque> { new Ataque("Impactrueno", 40, TipoPokemon.Electrico) })
            };

            Random rnd = new Random();
            var pokemonSalvaje = pokemonesSalvajes[rnd.Next(pokemonesSalvajes.Count)];

            MostrarAnimacionEncuentro(pokemonSalvaje.Nombre);

            Console.WriteLine("¿Qué quieres hacer?");
            Console.WriteLine("1. Luchar");
            Console.WriteLine("2. Intentar capturar");
            Console.WriteLine("3. Huir");
            Console.Write("Elige una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    IniciarBatalla(pokemonSalvaje);
                    break;
                case "2":
                    IntentarCaptura(pokemonSalvaje);
                    break;
                case "3":
                    Console.WriteLine("¡Has huido con éxito!");
                    Console.WriteLine("Presiona una tecla para volver al menú.");
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Opción no válida. Presiona una tecla para volver al menú.");
                    Console.ReadKey();
                    break;
            }
        }

        private void MostrarMochila()
        {
            Console.Clear();
            Console.WriteLine("=== MOCHILA ===");
            if (entrenador.Pociones.Count == 0)
            {
                Console.WriteLine("No tienes pociones.");
            }
            else
            {
                for (int i = 0; i < entrenador.Pociones.Count; i++)
                {
                    var pocion = entrenador.Pociones[i];
                    Console.WriteLine($"{i + 1}. {pocion.Tipo} (Cura {pocion.CantidadCuracion} PS)");
                }
                Console.WriteLine("¿Deseas usar una poción? (s/n): ");
                string usar = Console.ReadLine();
                if (usar.Trim().ToLower() == "s")
                {
                    if (entrenador.Pokemones.Count == 0)
                    {
                        Console.WriteLine("No tienes Pokémon para curar.");
                    }
                    else
                    {
                        Console.WriteLine("Elige el número de la poción:");
                        int numPocion;
                        if (int.TryParse(Console.ReadLine(), out numPocion) && numPocion > 0 && numPocion <= entrenador.Pociones.Count)
                        {
                            var pocion = entrenador.Pociones[numPocion - 1];
                            Console.WriteLine("Elige el número del Pokémon a curar:");
                            for (int i = 0; i < entrenador.Pokemones.Count; i++)
                            {
                                var poke = entrenador.Pokemones[i];
                                Console.WriteLine($"{i + 1}. {poke.Apodo} ({poke.SaludActual}/{poke.SaludMaxima})");
                            }
                            int numPoke;
                            if (int.TryParse(Console.ReadLine(), out numPoke) && numPoke > 0 && numPoke <= entrenador.Pokemones.Count)
                            {
                                var poke = entrenador.Pokemones[numPoke - 1];
                                MostrarAnimacionCuracion(poke.Apodo, pocion.CantidadCuracion);
                                entrenador.UsarPocion(poke, pocion.Tipo);
                                Console.WriteLine($"{poke.Apodo} ha sido curado.");
                            }
                            else
                            {
                                Console.WriteLine("Selección de Pokémon inválida.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Selección de poción inválida.");
                        }
                    }
                }
            }
            Console.WriteLine("Presiona una tecla para volver al menú.");
            Console.ReadKey();
        }

        private void MostrarPokemones()
        {
            Console.Clear();
            Console.WriteLine("=== POKÉMON CAPTURADOS ===");
            if (entrenador.Pokemones.Count == 0)
            {
                Console.WriteLine("No tienes Pokémon.");
            }
            else
            {
                for (int i = 0; i < entrenador.Pokemones.Count; i++)
                {
                    var poke = entrenador.Pokemones[i];
                    Console.WriteLine($"{i + 1}. {poke.Nombre} (Apodo: {poke.Apodo}) - Tipo: {poke.Tipo} - Salud: {poke.SaludActual}/{poke.SaludMaxima}");
                }
                Console.WriteLine("¿Deseas editar el apodo de algún Pokémon? (s/n): ");
                string editar = Console.ReadLine();
                if (editar.Trim().ToLower() == "s")
                {
                    Console.WriteLine("Elige el número del Pokémon:");
                    int numPoke;
                    if (int.TryParse(Console.ReadLine(), out numPoke) && numPoke > 0 && numPoke <= entrenador.Pokemones.Count)
                    {
                        var poke = entrenador.Pokemones[numPoke - 1];
                        Console.Write("Nuevo apodo: ");
                        string nuevoApodo = Console.ReadLine();
                        poke.EditarApodo(nuevoApodo);
                        Console.WriteLine("Apodo actualizado.");
                    }
                    else
                    {
                        Console.WriteLine("Selección inválida.");
                    }
                }
            }
            Console.WriteLine("Presiona una tecla para volver al menú.");
            Console.ReadKey();
        }

        private void IniciarBatalla(Pokemon enemigo)
        {
            Console.Clear();
            if (entrenador.Pokemones.Count == 0)
            {
                Console.WriteLine("No tienes Pokémon para luchar.");
                Console.WriteLine("Presiona una tecla para volver al menú.");
                Console.ReadKey();
                return;
            }

            var miPokemon = entrenador.Pokemones[0]; // Por simplicidad, usa el primero
            var batalla = new Batalla(miPokemon, enemigo);

            while (!miPokemon.EstaDebilitado() && !enemigo.EstaDebilitado())
            {
                Console.WriteLine($"{miPokemon.Apodo} ({miPokemon.SaludActual}/{miPokemon.SaludMaxima}) vs {enemigo.Nombre} ({enemigo.SaludActual}/{enemigo.SaludMaxima})");
                Console.WriteLine("Elige un ataque:");
                for (int i = 0; i < miPokemon.Ataques.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {miPokemon.Ataques[i].Nombre}");
                }
                int eleccion;
                if (int.TryParse(Console.ReadLine(), out eleccion) && eleccion > 0 && eleccion <= miPokemon.Ataques.Count)
                {
                    var ataque = miPokemon.Ataques[eleccion - 1];
                    MostrarAnimacionAtaque(miPokemon.Apodo, ataque.Nombre);
                    batalla.Atacar(miPokemon, enemigo, ataque);

                    if (enemigo.EstaDebilitado())
                    {
                        Console.WriteLine($"¡{enemigo.Nombre} se debilitó! Ganaste la batalla.");
                        break;
                    }

                    // Turno enemigo (ataque aleatorio)
                    var ataqueEnemigo = enemigo.Ataques[0];
                    MostrarAnimacionAtaque(enemigo.Nombre, ataqueEnemigo.Nombre);
                    batalla.Atacar(enemigo, miPokemon, ataqueEnemigo);

                    if (miPokemon.EstaDebilitado())
                    {
                        Console.WriteLine($"¡{miPokemon.Apodo} se debilitó! Has perdido la batalla.");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Selección inválida.");
                }
            }
            Console.WriteLine("Presiona una tecla para volver al menú.");
            Console.ReadKey();
        }

        private void IntentarCaptura(Pokemon pokemonSalvaje)
        {
            Console.Clear();
            MostrarAnimacionCaptura();
            Random rnd = new Random();
            int probabilidad = rnd.Next(100);
            if (probabilidad < 50)
            {
                Console.WriteLine($"¡Capturaste a {pokemonSalvaje.Nombre}!");
                Console.Write("¿Quieres ponerle un apodo? (s/n): ");
                string respuesta = Console.ReadLine();
                if (respuesta.Trim().ToLower() == "s")
                {
                    Console.Write("Escribe el apodo: ");
                    string apodo = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(apodo))
                    {
                        pokemonSalvaje.EditarApodo(apodo);
                    }
                }
                entrenador.AgregarPokemon(pokemonSalvaje);
            }
            else
            {
                Console.WriteLine($"¡{pokemonSalvaje.Nombre} escapó!");
            }
            Console.WriteLine("Presiona una tecla para volver al menú.");
            Console.ReadKey();
        }

        // --- Animaciones y uso de hilos/lock (simples ejemplos) ---

        private void MostrarAnimacionEncuentro(string nombre)
        {
            lock (lockObj)
            {
                Console.WriteLine("¡Un Pokémon salvaje aparece!");
                Console.WriteLine($@"
        (\__/)
        (o^.^) {nombre}
        z(_(\)
                ");
                Thread.Sleep(700);
            }
        }

        private void MostrarAnimacionAtaque(string atacante, string ataque)
        {
            lock (lockObj)
            {
                Console.Write($"{atacante} usa {ataque}");
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(300);
                    Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        private void MostrarAnimacionCaptura()
        {
            lock (lockObj)
            {
                Console.Write("¡Lanzando Pokébola");
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(400);
                    Console.Write(".");
                }
                Console.WriteLine("!");
            }
        }

        private void MostrarAnimacionCuracion(string nombre, int cantidad)
        {
            lock (lockObj)
            {
                Console.Write($"{nombre} está siendo curado");
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(350);
                    Console.Write(".");
                }
                Console.WriteLine($" +{cantidad} PS!");
            }
        }
    }
}