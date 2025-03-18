using System;

namespace Buscaminas
{
    // Clase que representa el tablero del juego
    public class Tablero
    {
        private Celda[,] _celdas;
        
        // Propiedad para obtener el tamaño del tablero
        public int Tamano { get; private set; }
        
        // Constructor del tablero
        public Tablero(int tamano)
        {
            Tamano = tamano;
            _celdas = new Celda[tamano, tamano];
            
            // Inicializar todas las celdas
            for (int fila = 0; fila < tamano; fila++)
            {
                for (int columna = 0; columna < tamano; columna++)
                {
                    _celdas[fila, columna] = new Celda(fila, columna);
                }
            }
        }
        
        // Método para obtener una celda específica
        public Celda ObtenerCelda(int fila, int columna)
        {
            if (fila < 0 || fila >= Tamano || columna < 0 || columna >= Tamano)
            {
                throw new ArgumentOutOfRangeException("Posición fuera del tablero");
            }
            
            return _celdas[fila, columna];
        }
        
        // Método para colocar una mina en una celda específica
        public void ColocarMina(int fila, int columna)
        {
            ObtenerCelda(fila, columna).TieneMina = true;
        }
    }
}