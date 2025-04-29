using System;

namespace Buscaminas
{
    public class Tablero
    {
        private Celda[,] _celdas;
        
        public int Tamano { get; private set; }
        
        public Tablero(int tamano)
        {
            Tamano = tamano;
            _celdas = new Celda[tamano, tamano];
            
            for (int fila = 0; fila < tamano; fila++)
            {
                for (int columna = 0; columna < tamano; columna++)
                {
                    _celdas[fila, columna] = new Celda(fila, columna);
                }
            }
        }
        
        public Celda ObtenerCelda(int fila, int columna)
        {
            if (fila < 0 || fila >= Tamano || columna < 0 || columna >= Tamano)
            {
                throw new ArgumentOutOfRangeException("Posición fuera del tablero");
            }
            
            return _celdas[fila, columna];
        }
        
        public void ColocarMina(int fila, int columna)
        {
            ObtenerCelda(fila, columna).TieneMina = true;
        }
    }
}