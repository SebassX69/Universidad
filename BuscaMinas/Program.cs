using System;

namespace Buscaminas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== BUSCAMINAS BETA 1.0 ===");
            Console.WriteLine("Desarrollado con patrones Singleton y Prototype");
            
            // Configuración del juego
            Console.Write("Ingrese el tamaño del tablero (ejemplo: 8 para un tablero 8x8): ");
            string? inputTamano = Console.ReadLine();
            int tamano = inputTamano != null ? int.Parse(inputTamano) : 8;
            
            Console.Write("Ingrese el número de minas: ");
            string? inputMinas = Console.ReadLine();
            int numMinas = inputMinas != null ? int.Parse(inputMinas) : 10;
            
            // Obtener la instancia única del juego (Singleton)
            Juego juego = Juego.ObtenerInstancia();
            
            // Inicializar el juego con el tamaño y número de minas especificados
            juego.IniciarJuego(tamano, numMinas);
            
            // Mostrar el tablero inicial
            juego.MostrarTablero();
            
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}