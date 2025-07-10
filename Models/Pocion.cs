namespace PokemonModels
{
    public enum TipoPocion
    {
        Pocion,      // Cura 20 PS
        SuperPocion, // Cura 50 PS
        HiperPocion  // Cura 200 PS
    }

    public class Pocion
    {
        public TipoPocion Tipo { get; set; }
        public int CantidadCuracion { get; set; }

        public Pocion(TipoPocion tipo)
        {
            Tipo = tipo;
            CantidadCuracion = tipo switch
            {
                TipoPocion.Pocion => 20,
                TipoPocion.SuperPocion => 50,
                TipoPocion.HiperPocion => 200,
                _ => 0
            };
        }
    }
}