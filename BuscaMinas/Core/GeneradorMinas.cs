using System;

namespace Buscaminas
{
    // Clase que implementa el patrón Prototype para generar minas
    public class GeneradorMinas
    {
        private Random _random;
        public GeneradorMinas()
        {
            _random = new Random();
        }
        
        public void GenerarMinas(Tablero tablero, int numMinas)
        {
            int tamanoTablero = tablero.Tamano;
            int maxMinas = tamanoTablero * tamanoTablero;
            
            if (numMinas > maxMinas)
            {
                numMinas = maxMinas;
                Console.WriteLine($"El número de minas se ha limitado a {maxMinas}");
            }
            
            // Crear una mina prototipo base
            Celda minaPrototipo = new Celda(0, 0);
            minaPrototipo.TieneMina = true;
            
            int minasColocadas = 0;
            while (minasColocadas < numMinas)
            {
                int fila = _random.Next(0, tamanoTablero);
                int columna = _random.Next(0, tamanoTablero);
                if (!tablero.ObtenerCelda(fila, columna).TieneMina)
                {
                    Celda nuevaMina = minaPrototipo.ClonarEnPosicion(fila, columna);
                    tablero.ColocarMina(fila, columna);
                    
                    minasColocadas++;
                }
            }
        }
    }
}