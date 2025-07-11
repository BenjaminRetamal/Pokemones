# 🎮 Pokemon Ciudad

Un juego de Pokémon desarrollado en C# que simula batallas por turnos en una experiencia de consola interactiva.

## 📝 Descripción

Pokemon Ciudad es un juego de rol por turnos donde juegas como un entrenador Pokémon. Puedes capturar Pokémon, entrenar con ellos, participar en batallas estratégicas y gestionar tu equipo. El juego incluye un sistema completo de tipos de Pokémon, ataques, efectividad y pociones de curación.

## ✨ Características

- 🔥 **Sistema de Tipos Completo**: 13 tipos diferentes de Pokémon (Agua, Fuego, Planta, Eléctrico, Normal, Tierra, Hielo, Lucha, Volador, Bicho, Fantasma, Dragón, Roca)
- ⚔️ **Batallas por Turnos**: Sistema de combate estratégico con cálculo de efectividad
- 🎯 **Sistema de Ataques**: Múltiples ataques con diferentes potencias y efectos
- 💊 **Sistema de Pociones**: Pociones normales, súper pociones e hiper pociones para curar
- 🏷️ **Personalización**: Cambia los apodos de tus Pokémon
- 🎨 **Arte ASCII**: Interfaz visual atractiva con arte ASCII
- 📊 **Gestión de Estadísticas**: Seguimiento de salud, ataques y estado de los Pokémon

## 🏗️ Arquitectura del Proyecto

El proyecto sigue una arquitectura MVC (Model-View-Controller) limpia:

```
PokemonCiudad/
├── Controllers/
│   └── juegoController.cs      # Lógica principal del juego
├── Models/
│   ├── Ataque.cs              # Modelo de ataques
│   ├── Batalla.cs             # Lógica de batallas
│   ├── Entrenador.cs          # Modelo del entrenador
│   ├── Pocion.cs              # Sistema de pociones
│   └── Pokemon.cs             # Modelo principal de Pokémon
├── Views/
│   ├── BatallaView.cs         # Interfaz de batallas
│   └── EntradaView.cs         # Interfaz de entrada y menús
└── Program.cs                 # Punto de entrada de la aplicación
```

## 🚀 Cómo Ejecutar

### Prerrequisitos
- .NET 8.0 o superior
- Visual Studio 2022, Visual Studio Code, o cualquier IDE compatible con .NET

### Pasos para ejecutar

1. **Clonar el repositorio**:
   ```bash
   git clone https://github.com/BenjaminRetamal/Pokemones.git
   cd Pokemones
   ```

2. **Compilar el proyecto**:
   ```bash
   dotnet build
   ```

3. **Ejecutar el juego**:
   ```bash
   dotnet run
   ```

## 🎮 Cómo Jugar

1. **Inicio**: Ingresa tu nombre de entrenador cuando se te solicite
2. **Menú Principal**: Navega por las opciones usando los números del menú:
   - Ver tus Pokémon capturados
   - Cambiar apodos de tus Pokémon
   - Iniciar una batalla
   - Salir del juego

3. **Batallas**: 
   - Elige tus ataques estratégicamente
   - Usa pociones para curar a tus Pokémon
   - Ten en cuenta la efectividad de tipos para máximo daño

4. **Estrategia**: Cada tipo tiene fortalezas y debilidades específicas contra otros tipos

## 🔧 Tecnologías Utilizadas

- **Lenguaje**: C# 12
- **Framework**: .NET 8.0
- **Paradigma**: Programación Orientada a Objetos
- **Patrón**: MVC (Model-View-Controller)

## 🎯 Sistema de Tipos y Efectividad

El juego incluye un sistema completo de efectividad de tipos:

- **Súper efectivo** (2x daño): Ataques muy efectivos contra el tipo rival
- **Efectivo** (1x daño): Daño normal
- **Poco efectivo** (0.5x daño): Ataques debilitados contra el tipo rival


## 📧 Contacto

**Desarrollador**: Benjamín Retamal  
**Email**: be.retamalr@gmail.com  
**GitHub**: [@BenjaminRetamal](https://github.com/BenjaminRetamal)

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

---

¡Disfruta entrenando Pokémon! 🎉
