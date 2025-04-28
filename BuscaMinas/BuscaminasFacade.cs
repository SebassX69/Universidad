using System;

namespace Buscaminas
{
    // Clase que implementa el patrón Facade para simplificar la interacción con el juego
    public class BuscaminasFacade
    {
        // Referencia al juego (Singleton)
        private Juego _juego;
        
        // Constructor
        public BuscaminasFacade()
        {
            _juego = Juego.ObtenerInstancia();
        }
        
        // Método para iniciar un nuevo juego
        public void NuevoJuego(int tamano, int numMinas)
        {
            _juego.IniciarJuego(tamano, numMinas);
            Console.WriteLine($"Nuevo juego iniciado: Tablero {tamano}x{tamano} con {numMinas} minas.");
        }
        
        // Método para revelar una celda
        public bool RevelarCelda(int fila, int columna)
        {
            if (_juego.TableroActual == null)
                throw new InvalidOperationException("El juego no ha sido inicializado.");
            
            Celda celda = _juego.TableroActual.ObtenerCelda(fila, columna);
            
            // Verificar si ya está revelada o marcada
            if (celda.EstaRevelada || celda.EstaMarcada)
                return false;
            
            // Marcar como revelada
            celda.EstaRevelada = true;
            
            // Verificar si tocó una mina
            if (celda.TieneMina)
            {
                _juego.FinalizarJuego(false);
                return true;
            }
            
            // Si la celda no tiene minas adyacentes, revelar las celdas vecinas (recursivamente)
            if (celda.MinasAdyacentes == 0)
            {
                _juego.RevelarCeldasVecinas(fila, columna);
            }
            
            // Verificar si ganó el juego
            if (_juego.VerificarVictoria())
            {
                _juego.FinalizarJuego(true);
            }
            
            return true;
        }
        
        // Método para marcar/desmarcar una celda
        public bool MarcarCelda(int fila, int columna)
        {
            if (_juego.TableroActual == null)
                throw new InvalidOperationException("El juego no ha sido inicializado.");
            
            Celda celda = _juego.TableroActual.ObtenerCelda(fila, columna);
            
            // Verificar si ya está revelada
            if (celda.EstaRevelada)
                return false;
            
            // Cambiar el estado de marcado
            celda.EstaMarcada = !celda.EstaMarcada;
            
            return true;
        }
        
        // Método para mostrar el tablero actual
        public void MostrarTablero(bool mostrarMinas = false)
        {
            _juego.MostrarTablero(mostrarMinas);
        }
        
        // Método para obtener una estadística rápida del tablero actual
        public (int tamano, int numMinas, int celdasReveladas, int celdasMarcadas) ObtenerEstadisticas()
        {
            if (_juego.TableroActual == null)
                throw new InvalidOperationException("El juego no ha sido inicializado.");
            
            int tamano = _juego.TableroActual.Tamano;
            int numMinas = _juego.ContarMinas();
            int celdasReveladas = _juego.ContarCeldasReveladas();
            int celdasMarcadas = _juego.ContarCeldasMarcadas();
            
            return (tamano, numMinas, celdasReveladas, celdasMarcadas);
        }
    }
}