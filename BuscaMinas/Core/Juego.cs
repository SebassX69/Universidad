using System;

namespace Buscaminas
{
    // Implementación del patrón Singleton
    public class Juego
    {
        private static Juego? _instancia;
        
        private Tablero? _tablero;
        private GeneradorMinas _generadorMinas;
        
        public Tablero? TableroActual => _tablero;
        
        // Constructor privado (parte del patrón Singleton)
        private Juego()
        {
            _generadorMinas = new GeneradorMinas();
        }
        
        // Método para obtener la instancia única (Singleton)
        public static Juego ObtenerInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new Juego();
            }
            return _instancia;
        }

        public void IniciarJuego(int tamano, int numMinas)
        {
            _tablero = new Tablero(tamano);
            _generadorMinas.GenerarMinas(_tablero, numMinas);
            CalcularNumerosAdyacentes();
        }
        
        public void RevelarCeldasVecinas(int fila, int columna)
        {
            if (_tablero == null) return;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;
                    
                    int filaAdyacente = fila + i;
                    int columnaAdyacente = columna + j;
                    
                    if (filaAdyacente >= 0 && filaAdyacente < _tablero.Tamano &&
                        columnaAdyacente >= 0 && columnaAdyacente < _tablero.Tamano)
                    {
                        Celda celdaAdyacente = _tablero.ObtenerCelda(filaAdyacente, columnaAdyacente);
                        
                        if (!celdaAdyacente.EstaRevelada && !celdaAdyacente.EstaMarcada)
                        {
                            celdaAdyacente.EstaRevelada = true;
                            
                            // Si esta celda también tiene 0 minas adyacentes, continuar la recursión
                            if (celdaAdyacente.MinasAdyacentes == 0)
                            {
                                RevelarCeldasVecinas(filaAdyacente, columnaAdyacente);
                            }
                        }
                    }
                }
            }
        }
        
        private void CalcularNumerosAdyacentes()
        {
            if (_tablero == null) return;
            
            for (int fila = 0; fila < _tablero.Tamano; fila++)
            {
                for (int columna = 0; columna < _tablero.Tamano; columna++)
                {
                    if (_tablero.ObtenerCelda(fila, columna).TieneMina)
                        continue;
                    
                    int contadorMinas = 0;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (i == 0 && j == 0)
                                continue;
                            
                            int filaAdyacente = fila + i;
                            int columnaAdyacente = columna + j;

                            if (filaAdyacente >= 0 && filaAdyacente < _tablero.Tamano &&
                                columnaAdyacente >= 0 && columnaAdyacente < _tablero.Tamano)
                            {
                                if (_tablero.ObtenerCelda(filaAdyacente, columnaAdyacente).TieneMina)
                                    contadorMinas++;
                            }
                        }
                    }
                    
                    _tablero.ObtenerCelda(fila, columna).MinasAdyacentes = contadorMinas;
                }
            }
        }
        
        public void MostrarTablero(bool mostrarMinas = false)
        {
            if (_tablero == null) return;
            
            Console.WriteLine("\nTablero de Buscaminas:");
            
            Console.Write("  ");
            for (int i = 0; i < _tablero.Tamano; i++)
            {
                Console.Write($"  {i} ");
            }
            Console.WriteLine();
            
            for (int fila = 0; fila < _tablero.Tamano; fila++)
            {

                Console.Write($"{fila} ");
                
                for (int columna = 0; columna < _tablero.Tamano; columna++)
                {
                    Celda celda = _tablero.ObtenerCelda(fila, columna);
                    
                    if (celda.EstaMarcada)
                    {
                        Console.Write("[?] "); // Celda marcada
                    }
                    else if (!celda.EstaRevelada && !mostrarMinas)
                    {
                        Console.Write("[ ] "); // Celda no revelada
                    }
                    else if (celda.TieneMina)
                    {
                        Console.Write("[X] "); // Celda con mina
                    }
                    else if (celda.MinasAdyacentes > 0)
                    {
                        Console.Write($"[{celda.MinasAdyacentes}] "); // Celda con número
                    }
                    else
                    {
                        Console.Write("[.] "); // Celda vacía revelada
                    }
                }
                Console.WriteLine();
            }
        }
    }
}