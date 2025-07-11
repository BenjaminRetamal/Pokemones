using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PokemonModels;
using PokemonCiudad.Views;

namespace PokemonCiudad.Controllers
{
    public class JuegoController
    {
        private Entrenador entrenador;
        private object lockObj = new object();
        private EntradaView entradaView;
        private BatallaView batallaView;
        private Random random;

        public JuegoController(string nombreEntrenador)
        {
            entrenador = new Entrenador(nombreEntrenador);
            entrenador.AgregarPocion(new Pocion(TipoPocion.Pocion));
            entrenador.AgregarPocion(new Pocion(TipoPocion.SuperPocion));
            entradaView = new EntradaView();
            batallaView = new BatallaView();
            random = new Random();
        }

        public void IniciarJuego()
        {
            entradaView.MostrarBienvenida();
            bool salir = false;
            
            while (!salir)
            {
                entradaView.MostrarMenuPrincipal();
                string[] opcionesValidas = { "1", "2", "3", "4" };
                string opcion = ObtenerEntradaValida("Selecciona una opción: ", opcionesValidas);

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
                        MostrarDespedida();
                        salir = true;
                        break;
                }
            }
        }

        private void MostrarDespedida()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
╔══════════════════════════════════════════════════════════════════════╗
║                          ¡GRACIAS POR JUGAR!                        ║
║                                                                      ║
║                       🎮 POKÉMON CIUDAD 🎮                          ║
║                                                                      ║
║                      ¡Hasta la próxima aventura!                    ║
╚══════════════════════════════════════════════════════════════════════╝
            ");
            Console.ResetColor();
            Thread.Sleep(2000);
        }

        private void Explorar()
        {
            Console.Clear();
            MostrarArteExploracion();
            
            var pokemonesSalvajes = new List<Pokemon>
            {
                new Pokemon("Charmander", TipoPokemon.Fuego, 100, new List<Ataque> { 
                    new Ataque("Ascuas", 40, TipoPokemon.Fuego),
                    new Ataque("Arañazo", 30, TipoPokemon.Normal)
                }),
                new Pokemon("Squirtle", TipoPokemon.Agua, 100, new List<Ataque> { 
                    new Ataque("Pistola Agua", 40, TipoPokemon.Agua),
                    new Ataque("Placaje", 35, TipoPokemon.Normal)
                }),
                new Pokemon("Bulbasaur", TipoPokemon.Planta, 100, new List<Ataque> { 
                    new Ataque("Látigo Cepa", 45, TipoPokemon.Planta),
                    new Ataque("Tackle", 35, TipoPokemon.Normal)
                }),
                new Pokemon("Pikachu", TipoPokemon.Electrico, 100, new List<Ataque> { 
                    new Ataque("Impactrueno", 40, TipoPokemon.Electrico),
                    new Ataque("Ataque Rápido", 30, TipoPokemon.Normal)
                }),
                new Pokemon("Geodude", TipoPokemon.Roca, 100, new List<Ataque> { 
                    new Ataque("Lanzarrocas", 35, TipoPokemon.Roca),
                    new Ataque("Puño", 40, TipoPokemon.Lucha)
                }),
                new Pokemon("Zubat", TipoPokemon.Volador, 100, new List<Ataque> { 
                    new Ataque("Supersónico", 25, TipoPokemon.Normal),
                    new Ataque("Ataque Aéreo", 35, TipoPokemon.Volador)
                })
            };

            var pokemonSalvaje = pokemonesSalvajes[random.Next(pokemonesSalvajes.Count)];
            MostrarAnimacionEncuentro(pokemonSalvaje);

            MostrarMenuEncuentro();
            string[] opcionesValidas = { "1", "2", "3" };
            string opcion = ObtenerEntradaValida("Elige una opción: ", opcionesValidas);

            switch (opcion)
            {
                case "1":
                    IniciarBatalla(pokemonSalvaje);
                    break;
                case "2":
                    IntentarCaptura(pokemonSalvaje);
                    break;
                case "3":
                    MostrarHuida();
                    break;
            }
        }

        private void MostrarMochila()
        {
            Console.Clear();
            MostrarArteMochila();
            
            if (entrenador.Pociones.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Tu mochila está vacía. ¡Encuentra más pociones explorando!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("═══ CONTENIDO DE LA MOCHILA ═══");
                Console.ResetColor();
                
                for (int i = 0; i < entrenador.Pociones.Count; i++)
                {
                    var pocion = entrenador.Pociones[i];
                    Console.WriteLine($"🧪 {i + 1}. {pocion.Tipo} (Cura {pocion.CantidadCuracion} PS)");
                }
                
                if (ObtenerConfirmacion("\n¿Deseas usar una poción?"))
                {
                    if (entrenador.Pokemones.Count == 0)
                    {
                        entradaView.MostrarError("No tienes Pokémon para curar.");
                    }
                    else
                    {
                        UsarPocion();
                    }
                }
            }
            Console.WriteLine("\nPresiona una tecla para volver al menú.");
            Console.ReadKey();
        }

        private void MostrarArteMochila()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"
🎒🎒🎒🎒🎒🎒🎒🎒🎒🎒🎒🎒🎒
🎒        MOCHILA             🎒
🎒🎒🎒🎒🎒🎒🎒🎒🎒🎒🎒🎒🎒
            ");
            Console.ResetColor();
        }

        private void UsarPocion()
        {
            int numPocion = ObtenerNumeroValido("Elige el número de la poción: ", 1, entrenador.Pociones.Count);
            var pocion = entrenador.Pociones[numPocion - 1];
            
            Console.WriteLine("Elige el número del Pokémon a curar:");
            for (int i = 0; i < entrenador.Pokemones.Count; i++)
            {
                var poke = entrenador.Pokemones[i];
                string estadoSalud = GetEstadoSalud(poke);
                Console.WriteLine($"{i + 1}. {poke.Apodo} ({poke.SaludActual}/{poke.SaludMaxima}) {estadoSalud}");
            }
            
            int numPoke = ObtenerNumeroValido("Selecciona el Pokémon: ", 1, entrenador.Pokemones.Count);
            var pokemon = entrenador.Pokemones[numPoke - 1];
            
            if (pokemon.SaludActual >= pokemon.SaludMaxima)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"⚠️ {pokemon.Apodo} ya tiene toda su salud.");
                Console.ResetColor();
                return;
            }
            
            MostrarAnimacionCuracion(pokemon.Apodo, pocion.CantidadCuracion);
            entrenador.UsarPocion(pokemon, pocion.Tipo);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"¡{pokemon.Apodo} ha sido curado!");
            Console.ResetColor();
        }

        private string GetEstadoSalud(Pokemon pokemon)
        {
            double porcentaje = (double)pokemon.SaludActual / pokemon.SaludMaxima;
            return porcentaje switch
            {
                >= 0.8 => "💚",
                >= 0.5 => "💛",
                >= 0.2 => "🧡",
                > 0 => "❤️",
                _ => "💀"
            };
        }

        private void MostrarPokemones()
        {
            Console.Clear();
            entradaView.MostrarPokemonCapturados(entrenador.Pokemones);
            
            if (entrenador.Pokemones.Count > 0)
            {
                if (ObtenerConfirmacion("\n¿Deseas editar el apodo de algún Pokémon?"))
                {
                    EditarApodoPokemon();
                }
            }
        }

        private void EditarApodoPokemon()
        {
            int numPoke = ObtenerNumeroValido("Elige el número del Pokémon: ", 1, entrenador.Pokemones.Count);
            var pokemon = entrenador.Pokemones[numPoke - 1];
            
            string nuevoApodo = ObtenerTextoNoVacio($"Nuevo apodo para {pokemon.Nombre}: ", 20);
            
            pokemon.EditarApodo(nuevoApodo);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"¡Apodo actualizado! Ahora se llama {nuevoApodo}");
            Console.ResetColor();
            
            Console.WriteLine("Presiona una tecla para volver al menú.");
            Console.ReadKey();
        }

        private void IniciarBatalla(Pokemon enemigo)
        {
            if (entrenador.Pokemones.Count == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"
╔══════════════════════════════════════════════════════════════════════╗
║                    ¡NO TIENES POKÉMON!                              ║
║                                                                      ║
║           Necesitas capturar Pokémon antes de luchar.               ║
║              ¡Vuelve cuando tengas un compañero!                    ║
╚══════════════════════════════════════════════════════════════════════╝
                ");
                Console.ResetColor();
                Console.WriteLine("Presiona una tecla para volver al menú.");
                Console.ReadKey();
                return;
            }

            // Seleccionar Pokémon si tienes más de uno
            var miPokemon = SeleccionarPokemonParaBatalla();
            if (miPokemon == null) return;

            var batalla = new Batalla(miPokemon, enemigo);
            batallaView.MostrarInicioBatalla(miPokemon, enemigo);

            bool batallaEnCurso = true;
            while (batallaEnCurso && !miPokemon.EstaDebilitado() && !enemigo.EstaDebilitado())
            {
                // Turno del jugador
                batallaView.MostrarEstado(miPokemon, enemigo);
                var ataqueSeleccionado = SeleccionarAtaque(miPokemon);
                
                if (ataqueSeleccionado != null)
                {
                    batallaView.MostrarAtaque(miPokemon.Apodo, ataqueSeleccionado.Nombre);
                    batalla.Atacar(miPokemon, enemigo, ataqueSeleccionado);

                    if (enemigo.EstaDebilitado())
                    {
                        batallaView.MostrarFinBatalla(true);
                        OtorgarRecompensas();
                        batallaEnCurso = false;
                        break;
                    }

                    // Turno enemigo
                    Thread.Sleep(1000);
                    var ataqueEnemigo = enemigo.Ataques[random.Next(enemigo.Ataques.Count)];
                    batallaView.MostrarAtaque(enemigo.Nombre, ataqueEnemigo.Nombre);
                    batalla.Atacar(enemigo, miPokemon, ataqueEnemigo);

                    if (miPokemon.EstaDebilitado())
                    {
                        batallaView.MostrarFinBatalla(false);
                        batallaEnCurso = false;
                    }
                }
                else
                {
                    // El jugador eligió huir o opción inválida
                    batallaEnCurso = false;
                }
            }
        }

        private Pokemon? SeleccionarPokemonParaBatalla()
        {
            if (entrenador.Pokemones.Count == 1)
            {
                var unicoPokemon = entrenador.Pokemones[0];
                if (unicoPokemon.EstaDebilitado())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠️ Tu único Pokémon está debilitado. Necesitas curarlo primero.");
                    Console.ResetColor();
                    Console.WriteLine("Presiona una tecla para volver al menú.");
                    Console.ReadKey();
                    return null;
                }
                return unicoPokemon;
            }

            // Verificar si hay algún Pokémon disponible
            var pokemonDisponibles = entrenador.Pokemones.Where(p => !p.EstaDebilitado()).ToList();
            if (pokemonDisponibles.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("⚠️ Todos tus Pokémon están debilitados. Necesitas curarlos primero.");
                Console.ResetColor();
                Console.WriteLine("Presiona una tecla para volver al menú.");
                Console.ReadKey();
                return null;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("═══ SELECCIONA TU POKÉMON ═══");
            Console.ResetColor();

            for (int i = 0; i < entrenador.Pokemones.Count; i++)
            {
                var poke = entrenador.Pokemones[i];
                if (!poke.EstaDebilitado())
                {
                    string estado = GetEstadoSalud(poke);
                    Console.WriteLine($"{i + 1}. {poke.Apodo} - {poke.Tipo} {estado} ({poke.SaludActual}/{poke.SaludMaxima})");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{i + 1}. {poke.Apodo} - DEBILITADO 💀");
                    Console.ResetColor();
                }
            }

            int seleccion = ObtenerNumeroValido("\nElige tu Pokémon: ", 1, entrenador.Pokemones.Count);
            var pokemon = entrenador.Pokemones[seleccion - 1];
            
            if (pokemon.EstaDebilitado())
            {
                entradaView.MostrarError("Ese Pokémon está debilitado. Elige otro.");
                Thread.Sleep(1500);
                return SeleccionarPokemonParaBatalla();
            }
            
            return pokemon;
        }

        private Ataque? SeleccionarAtaque(Pokemon pokemon)
        {
            Console.WriteLine("\n🗡️ SELECCIONA UN ATAQUE:");
            for (int i = 0; i < pokemon.Ataques.Count; i++)
            {
                var ataque = pokemon.Ataques[i];
                Console.ForegroundColor = GetColorForType(ataque.Tipo);
                Console.WriteLine($"{i + 1}. {ataque.Nombre} - {ataque.Tipo} (Potencia: {ataque.Potencia})");
                Console.ResetColor();
            }
            Console.WriteLine($"{pokemon.Ataques.Count + 1}. 💨 Intentar huir");
            
            int maxOpciones = pokemon.Ataques.Count + 1;
            int eleccion = ObtenerNumeroValido("Tu elección: ", 1, maxOpciones);
            
            if (eleccion <= pokemon.Ataques.Count)
            {
                return pokemon.Ataques[eleccion - 1];
            }
            else
            {
                // Intentar huir
                if (random.Next(100) < 50) // 50% de probabilidad de éxito
                {
                    batallaView.MostrarOpcionHuida(true);
                    return null;
                }
                else
                {
                    batallaView.MostrarOpcionHuida(false);
                    return SeleccionarAtaque(pokemon); // Vuelve a elegir
                }
            }
        }

        private void OtorgarRecompensas()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n🎉 ¡VICTORIA! 🎉");
            Console.WriteLine("Recompensas obtenidas:");
            
            // Probabilidad de obtener poción
            if (random.Next(100) < 30) // 30% de probabilidad
            {
                var tipoPocion = random.Next(2) == 0 ? TipoPocion.Pocion : TipoPocion.SuperPocion;
                entrenador.AgregarPocion(new Pocion(tipoPocion));
                Console.WriteLine($"🧪 Has encontrado una {tipoPocion}!");
            }
            
            Console.ResetColor();
            Thread.Sleep(2000);
        }

        private void IntentarCaptura(Pokemon pokemonSalvaje)
        {
            Console.Clear();
            MostrarAnimacionCaptura();
            
            // Probabilidad fija del 50% para capturar
            bool capturaExitosa = random.Next(100) < 50;
            
            if (capturaExitosa)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(@"
🎉🎉🎉 ¡CAPTURA EXITOSA! 🎉🎉🎉

        ⭐ ¡Has capturado a " + pokemonSalvaje.Nombre + @"! ⭐
        
             🥎 ← Pokébola
        ");
                Console.ResetColor();
                
                AsignarApodo(pokemonSalvaje);
                entrenador.AgregarPokemon(pokemonSalvaje);
                
                // Pequeña recompensa por capturar
                if (random.Next(100) < 20) // 20% de probabilidad
                {
                    entrenador.AgregarPocion(new Pocion(TipoPocion.Pocion));
                    Console.WriteLine("🧪 ¡También has encontrado una Poción!");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"
💨💨💨 ¡SE HA ESCAPADO! 💨💨💨

        ¡" + pokemonSalvaje.Nombre + @" ha logrado huir!
        
          🥎💥  ← Pokébola rota
        ");
                Console.ResetColor();
                Thread.Sleep(1500);
            }
            
            Console.WriteLine("\nPresiona una tecla para volver al menú.");
            Console.ReadKey();
        }

        private void AsignarApodo(Pokemon pokemon)
        {
            if (ObtenerConfirmacion($"\n¿Quieres ponerle un apodo a {pokemon.Nombre}?"))
            {
                string apodo = ObtenerTextoNoVacio("Escribe el apodo: ", 20);
                pokemon.EditarApodo(apodo);
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"¡Perfecto! Ahora se llamará {apodo}");
                Console.ResetColor();
            }
        }

        // --- Animaciones y uso de hilos/lock (simples ejemplos) ---

        private void MostrarAnimacionEncuentro(Pokemon pokemon)
        {
            lock (lockObj)
            {
                Console.Clear();
                MostrarArteExploracion();
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("¡Un Pokémon salvaje aparece!");
                Console.ResetColor();
                
                Thread.Sleep(1000);
                
                MostrarArtePokemon(pokemon);
                
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"¡Es un {pokemon.Nombre} salvaje!");
                Console.WriteLine($"Tipo: {pokemon.Tipo} | Salud: {pokemon.SaludMaxima} PS");
                Console.ResetColor();
                Thread.Sleep(1500);
            }
        }

        private void MostrarArtePokemon(Pokemon pokemon)
        {
            Console.ForegroundColor = GetColorForType(pokemon.Tipo);
            
            switch (pokemon.Tipo)
            {
                case TipoPokemon.Fuego:
                    Console.WriteLine(@"
        🔥🔥🔥
       🦎  🔥
      🔥🔥🔥🔥
                    ");
                    break;
                case TipoPokemon.Agua:
                    Console.WriteLine(@"
        💧💧💧
       🐢  💧
      💧💧💧💧
                    ");
                    break;
                case TipoPokemon.Planta:
                    Console.WriteLine(@"
        🌱🌱🌱
       🦕  🌱
      🌱🌱🌱🌱
                    ");
                    break;
                case TipoPokemon.Electrico:
                    Console.WriteLine(@"
        ⚡⚡⚡
       🐭  ⚡
      ⚡⚡⚡⚡
                    ");
                    break;
                default:
                    Console.WriteLine(@"
        ✨✨✨
       👾  ✨
      ✨✨✨✨
                    ");
                    break;
            }
            Console.ResetColor();
        }

        private ConsoleColor GetColorForType(TipoPokemon tipo)
        {
            return tipo switch
            {
                TipoPokemon.Fuego => ConsoleColor.Red,
                TipoPokemon.Agua => ConsoleColor.Blue,
                TipoPokemon.Planta => ConsoleColor.Green,
                TipoPokemon.Electrico => ConsoleColor.Yellow,
                TipoPokemon.Roca => ConsoleColor.DarkYellow,
                TipoPokemon.Volador => ConsoleColor.Cyan,
                _ => ConsoleColor.White
            };
        }

        private void MostrarAnimacionAtaque(string atacante, string ataque)
        {
            lock (lockObj)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"⚔️ {atacante} usa {ataque}");
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(300);
                    Console.Write(" 💥");
                }
                Console.WriteLine();
                Console.ResetColor();
                Thread.Sleep(500);
            }
        }

        private void MostrarAnimacionCaptura()
        {
            lock (lockObj)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(@"
🎯 ¡LANZANDO POKÉBOLA! 🎯
        ");
                Console.ResetColor();
                
                Console.Write("🥎 Pokébola en el aire");
                for (int i = 0; i < 4; i++)
                {
                    Thread.Sleep(600);
                    Console.Write(".");
                }
                
                Console.WriteLine("\n💥 ¡IMPACTO!");
                Thread.Sleep(800);
                
                Console.Write("🥎 Temblando");
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(700);
                    Console.Write("...");
                }
                Console.WriteLine();
                Thread.Sleep(500);
            }
        }

        private void MostrarAnimacionCuracion(string nombre, int cantidad)
        {
            lock (lockObj)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(@"
✨ USANDO POCIÓN ✨
        ");
                Console.ResetColor();
                
                Console.Write($"💊 Curando a {nombre}");
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(350);
                    Console.Write(" ✨");
                }
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n💚 ¡{nombre} recuperó {cantidad} PS!");
                Console.ResetColor();
                Thread.Sleep(1000);
            }
        }

        private void MostrarArteExploracion()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
🌲🌲🌲 EXPLORANDO EL BOSQUE 🌲🌲🌲
     🌿 Buscando Pokémon salvajes... 🌿
        🦋    🐛    🕷️    🐾
            ");
            Console.ResetColor();
            Thread.Sleep(1000);
        }

        private void MostrarMenuEncuentro()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║            ¿QUÉ HARÁS?                ║");
            Console.WriteLine("╠═══════════════════════════════════════╣");
            Console.WriteLine("║  1. ⚔️  Luchar                        ║");
            Console.WriteLine("║  2. 🥎 Intentar capturar              ║");
            Console.WriteLine("║  3. 💨 Huir                           ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();
            Console.Write("Elige una opción: ");
        }

        private void MostrarHuida()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
💨💨💨 ¡HAS HUIDO CON ÉXITO! 💨💨💨
       
       🏃‍♂️💨    ←─ TÚ
                 
       🐾🐾🐾  ←─ RASTROS

¡Te has escapado sano y salvo!
            ");
            Console.ResetColor();
            Console.WriteLine("Presiona una tecla para volver al menú.");
            Console.ReadKey();
        }

        // Métodos de validación de datos
        private string ObtenerEntradaValida(string mensaje, string[] opcionesValidas)
        {
            string? entrada;
            do
            {
                Console.Write(mensaje);
                entrada = Console.ReadLine()?.Trim();
                
                if (string.IsNullOrWhiteSpace(entrada))
                {
                    entradaView.MostrarError("⚠️ La entrada no puede estar vacía. Intenta de nuevo.");
                    continue;
                }
                
                if (opcionesValidas.Contains(entrada))
                {
                    return entrada;
                }
                
                entradaView.MostrarError($"⚠️ Opción inválida. Las opciones válidas son: {string.Join(", ", opcionesValidas)}");
                
            } while (true);
        }
        
        private int ObtenerNumeroValido(string mensaje, int min, int max)
        {
            string? entrada;
            int numero;
            
            do
            {
                Console.Write(mensaje);
                entrada = Console.ReadLine()?.Trim();
                
                if (string.IsNullOrWhiteSpace(entrada))
                {
                    entradaView.MostrarError("⚠️ Debes ingresar un número. Intenta de nuevo.");
                    continue;
                }
                
                if (!int.TryParse(entrada, out numero))
                {
                    entradaView.MostrarError("⚠️ Debes ingresar un número válido. Intenta de nuevo.");
                    continue;
                }
                
                if (numero < min || numero > max)
                {
                    entradaView.MostrarError($"⚠️ El número debe estar entre {min} y {max}. Intenta de nuevo.");
                    continue;
                }
                
                return numero;
                
            } while (true);
        }
        
        private string ObtenerTextoNoVacio(string mensaje, int longitudMaxima = 50)
        {
            string? entrada;
            
            do
            {
                Console.Write(mensaje);
                entrada = Console.ReadLine()?.Trim();
                
                if (string.IsNullOrWhiteSpace(entrada))
                {
                    entradaView.MostrarError("⚠️ El texto no puede estar vacío. Intenta de nuevo.");
                    continue;
                }
                
                if (entrada.Length > longitudMaxima)
                {
                    entradaView.MostrarError($"⚠️ El texto no puede tener más de {longitudMaxima} caracteres. Intenta de nuevo.");
                    continue;
                }
                
                return entrada;
                
            } while (true);
        }

        private bool ObtenerConfirmacion(string mensaje)
        {
            string[] opcionesValidas = { "s", "si", "sí", "n", "no" };
            string respuesta = ObtenerEntradaValida(mensaje + " (s/n): ", opcionesValidas).ToLower();
            return respuesta == "s" || respuesta == "si" || respuesta == "sí";
        }
    }
}