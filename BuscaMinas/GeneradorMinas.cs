using System;

namespace Buscaminas
{
    // Clase que implementa el patrón Prototype para generar minas
    public class GeneradorMinas
    {
        private Random _random;
        
        // Constructor
        public GeneradorMinas()
        {
            _random = new Random();
        }
        
        // Generar minas y colocarlas en el tablero
        public void GenerarMinas(Tablero tablero, int numMinas)
        {
            // Validar que el número de minas no exceda el tamaño del tablero
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
            
            // Colocar las minas aleatoriamente
            int minasColocadas = 0;
            while (minasColocadas < numMinas)
            {
                int fila = _random.Next(0, tamanoTablero);
                int columna = _random.Next(0, tamanoTablero);
                
                // Verificar si ya hay una mina en esta posición
                if (!tablero.ObtenerCelda(fila, columna).TieneMina)
                {
                    // Usar el prototipo para crear una nueva mina con las coordenadas correctas
                    Celda nuevaMina = minaPrototipo.ClonarEnPosicion(fila, columna);
                    
                    // Como no podemos reemplazar la celda directamente, 
                    // usamos el método del tablero para colocar la mina
                    tablero.ColocarMina(fila, columna);
                    
                    minasColocadas++;
                }
            }
        }
    }
}