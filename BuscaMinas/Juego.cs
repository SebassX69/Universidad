using System;

namespace Buscaminas
{
    // Implementación del patrón Singleton
    public class Juego
    {
        // Instancia única (Singleton)
        private static Juego? _instancia;
        
        // Propiedades del juego
        private Tablero? _tablero;
        private GeneradorMinas _generadorMinas;
        private bool _juegoTerminado;
        private bool _victoria;
        
        // Propiedad para acceder al tablero actual
        public Tablero? TableroActual => _tablero;
        
        // Constructor privado (parte del patrón Singleton)
        private Juego()
        {
            _generadorMinas = new GeneradorMinas();
            _juegoTerminado = false;
            _victoria = false;
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
        
        // Inicializar el juego con tamaño y número de minas
        public void IniciarJuego(int tamano, int numMinas)
        {
            // Crear un nuevo tablero
            _tablero = new Tablero(tamano);
            _juegoTerminado = false;
            _victoria = false;
            
            // Generar y colocar las minas en el tablero
            _generadorMinas.GenerarMinas(_tablero, numMinas);
            
            // Calcular números para las celdas adyacentes a minas
            CalcularNumerosAdyacentes();
        }
        
        // Finalizar el juego
        public void FinalizarJuego(bool victoria)
        {
            _juegoTerminado = true;
            _victoria = victoria;
            
            if (victoria)
            {
                Console.WriteLine("¡Has ganado! Todas las celdas sin minas han sido reveladas.");
            }
            else
            {
                Console.WriteLine("¡Has perdido! Has revelado una celda con mina.");
            }
        }
        
        // Verificar si todas las celdas sin minas han sido reveladas (victoria)
        public bool VerificarVictoria()
        {
            if (_tablero == null) return false;
            
            for (int fila = 0; fila < _tablero.Tamano; fila++)
            {
                for (int columna = 0; columna < _tablero.Tamano; columna++)
                {
                    Celda celda = _tablero.ObtenerCelda(fila, columna);
                    
                    // Si hay una celda que no tiene mina y no está revelada, aún no ha ganado
                    if (!celda.TieneMina && !celda.EstaRevelada)
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }
        
        // Revelar celdas vecinas recursivamente (cuando se revela una celda con 0 minas adyacentes)
        public void RevelarCeldasVecinas(int fila, int columna)
        {
            if (_tablero == null) return;
            
            // Revisar las 8 celdas adyacentes
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    // Saltarse la celda actual
                    if (i == 0 && j == 0)
                        continue;
                    
                    int filaAdyacente = fila + i;
                    int columnaAdyacente = columna + j;
                    
                    // Verificar que la celda adyacente está dentro del tablero
                    if (filaAdyacente >= 0 && filaAdyacente < _tablero.Tamano &&
                        columnaAdyacente >= 0 && columnaAdyacente < _tablero.Tamano)
                    {
                        Celda celdaAdyacente = _tablero.ObtenerCelda(filaAdyacente, columnaAdyacente);
                        
                        // Si la celda no está revelada y no está marcada
                        if (!celdaAdyacente.EstaRevelada && !celdaAdyacente.EstaMarcada)
                        {
                            // Revelar la celda
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
        
        // Calcular números para las celdas adyacentes a minas
        private void CalcularNumerosAdyacentes()
        {
            if (_tablero == null) return;
            
            for (int fila = 0; fila < _tablero.Tamano; fila++)
            {
                for (int columna = 0; columna < _tablero.Tamano; columna++)
                {
                    // Si la celda actual tiene una mina, no necesitamos calcular
                    if (_tablero.ObtenerCelda(fila, columna).TieneMina)
                        continue;
                    
                    int contadorMinas = 0;
                    
                    // Revisar las 8 celdas adyacentes
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            // Saltarse la celda actual
                            if (i == 0 && j == 0)
                                continue;
                            
                            int filaAdyacente = fila + i;
                            int columnaAdyacente = columna + j;
                            
                            // Verificar que la celda adyacente está dentro del tablero
                            if (filaAdyacente >= 0 && filaAdyacente < _tablero.Tamano &&
                                columnaAdyacente >= 0 && columnaAdyacente < _tablero.Tamano)
                            {
                                if (_tablero.ObtenerCelda(filaAdyacente, columnaAdyacente).TieneMina)
                                    contadorMinas++;
                            }
                        }
                    }
                    
                    // Asignar el número de minas adyacentes a la celda
                    _tablero.ObtenerCelda(fila, columna).MinasAdyacentes = contadorMinas;
                }
            }
        }
        
        // Método para contar las minas en el tablero
        public int ContarMinas()
        {
            if (_tablero == null) return 0;
            
            int contador = 0;
            for (int fila = 0; fila < _tablero.Tamano; fila++)
            {
                for (int columna = 0; columna < _tablero.Tamano; columna++)
                {
                    if (_tablero.ObtenerCelda(fila, columna).TieneMina)
                        contador++;
                }
            }
            
            return contador;
        }
        
        // Método para contar las celdas reveladas
        public int ContarCeldasReveladas()
        {
            if (_tablero == null) return 0;
            
            int contador = 0;
            for (int fila = 0; fila < _tablero.Tamano; fila++)
            {
                for (int columna = 0; columna < _tablero.Tamano; columna++)
                {
                    if (_tablero.ObtenerCelda(fila, columna).EstaRevelada)
                        contador++;
                }
            }
            
            return contador;
        }
        
        // Método para contar las celdas marcadas
        public int ContarCeldasMarcadas()
        {
            if (_tablero == null) return 0;
            
            int contador = 0;
            for (int fila = 0; fila < _tablero.Tamano; fila++)
            {
                for (int columna = 0; columna < _tablero.Tamano; columna++)
                {
                    if (_tablero.ObtenerCelda(fila, columna).EstaMarcada)
                        contador++;
                }
            }
            
            return contador;
        }
        
        // Mostrar el tablero en la consola
        public void MostrarTablero(bool mostrarMinas = false)
        {
            if (_tablero == null) return;
            
            Console.WriteLine("\nTablero de Buscaminas:");
            
            // Mostrar números de columna con mejor alineación
            Console.Write("  ");
            for (int i = 0; i < _tablero.Tamano; i++)
            {
                Console.Write($"  {i} ");
            }
            Console.WriteLine();
            
            for (int fila = 0; fila < _tablero.Tamano; fila++)
            {
                // Mostrar número de fila
                Console.Write($"{fila} ");
                
                for (int columna = 0; columna < _tablero.Tamano; columna++)
                {
                    Celda celda = _tablero.ObtenerCelda(fila, columna);
                    
                    // Mostrar la celda según su estado
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
                        Console.Write("[ ] "); // Celda vacía revelada
                    }
                }
                Console.WriteLine();
            }
        }
    }
}