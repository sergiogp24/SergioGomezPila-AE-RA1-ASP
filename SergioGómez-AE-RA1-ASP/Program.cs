using System;
using System.Collections.Generic;

// Clase abstracta base
public abstract class Contenido
{
    public string titulo { get; set; }
    private double minutos;
    public double Minutos
    {
        get { return minutos; }
        set { minutos = value < 0 ? 0.0 : value; }
    }

    public string ResolucionPantalla { get; private set; }

    public Contenido(string titulo, double minutos)
    {
        this.titulo = titulo;
        Minutos = minutos;
    }

    public abstract double ObtenerTasaCompresion();
    public abstract string Describir();
    public abstract string Reproducir();

    public override string ToString()
    {
        return $"Título: {titulo}, minutos: {Minutos}";
    }

    // Clase hija abstracta
    public abstract class ContenidoVisual : Contenido
    {
        protected string ResolucionPantalla { get; set; }

        public ContenidoVisual(string titulo, double minutos, string resolucionPantalla)
            : base(titulo, minutos)
        {
            ResolucionPantalla = resolucionPantalla;
        }

        public override double ObtenerTasaCompresion()
        {
            return ResolucionPantalla == "2160p" ? 0.6 : 0.9;
        }

        public override string ToString()
        {
            return base.ToString() + $" | Resolución: {ResolucionPantalla}";
        }

        public override string Describir()
        {
            return $"{titulo} ({Minutos} minutos)";
        }
    }

    // Clase hija abstracta auditiva
    public abstract class ContenidoAuditivo : Contenido
    {
        protected double Bitrate { get; set; }

        public ContenidoAuditivo(string titulo, double minutos, double bitrate)
            : base(titulo, minutos)
        {
            Bitrate = bitrate;
        }

        public override double ObtenerTasaCompresion()
        {
            return 1.0 - (Bitrate / 500);
        }

        public override string ToString()
        {
            return base.ToString() + $" | Bitrate: {Bitrate}";
        }

        public override string Describir()
        {
            return $"{titulo} ({Minutos} minutos)";
        }
    }
}

// Hereda de ContenidoVisual
class Pelicula : Contenido.ContenidoVisual
{
    private string Director { get; set; }

    public Pelicula(string titulo, double minutos, string resolucionPantalla, string director)
        : base(titulo, minutos, resolucionPantalla)
    {
        Director = director;
    }

    public override string Describir()
    {
        return $"Pelicula: {base.Describir()}";
    }

    public override string Reproducir()
    {
        return $"[REPRODUCIENDO PELÍCULA]\nTítulo: {titulo}\nDirector: {Director}\nResolución: {ResolucionPantalla}\nTasa de compresión: {ObtenerTasaCompresion()}\nDuración: {Minutos} minutos\n[FIN DE REPRODUCCIÓN]";
    }
}

// Hereda de ContenidoAuditivo
class Canciones : Contenido.ContenidoAuditivo
{
    private string Licencia { get; set; }

    public Canciones(string titulo, double minutos, double bitrate, string licencia)
        : base(titulo, minutos, bitrate)
    {
        Licencia = licencia;
    }

    public override string Describir()
    {
        return $"Canción: {base.Describir()}";
    }

    public override string Reproducir()
    {
        return $"[REPRODUCIENDO CANCIÓN]\nTítulo: {titulo}\nBitrate: {Bitrate}\nLicencia: {Licencia}\nTasa de compresión: {ObtenerTasaCompresion()}\nDuración: {Minutos} minutos\n[FIN DE REPRODUCCIÓN]";
    }
}

// Programa principal
class Programa
{
    static void Main()
    {
        List<Contenido> contenido = new List<Contenido>
        {
            new Pelicula("Interstellar", 169, "2160p", "Christopher Nolan"),
            new Pelicula("Feast", 7, "480p", "Patrick Osborne"),
            new Canciones("Bohemian Rhapsody", 6, 320, "Protegida"),
            new Canciones("The House of the Rising Sun", 5, 120, "Libre")
        };

        while (true)
        {
            Console.WriteLine("\n=== MENÚ ===");
            Console.WriteLine("1. Ver Catálogo");
            Console.WriteLine("2. Reproducir un contenido del catálogo");
            Console.WriteLine("3. Salir");
            Console.Write("Selecciona una opción: ");

            if (!int.TryParse(Console.ReadLine(), out int opcion))
            {
                Console.WriteLine("Opción no válida. Intente de nuevo.");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("\n--- Catálogo ---");
                    for (int i = 0; i < contenido.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {contenido[i].Describir()}");
                    }
                    break;

                case 2:
                    Console.WriteLine("\n--- Selecciona un número del catálogo ---");
                    for (int i = 0; i < contenido.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {contenido[i].Describir()}");
                    }

                    Console.Write("Tu elección: ");
                    if (int.TryParse(Console.ReadLine(), out int seleccion) &&
                        seleccion > 0 && seleccion <= contenido.Count)
                    {
                        Console.WriteLine();
                        Console.WriteLine(contenido[seleccion - 1].Reproducir());
                    }
                    else
                    {
                        Console.WriteLine("Selección no válida.");
                    }
                    break;

                case 3:
                    Console.WriteLine("Gracias por usar la biblioteca digital. ¡Hasta luego!");
                    return;

                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }
}
