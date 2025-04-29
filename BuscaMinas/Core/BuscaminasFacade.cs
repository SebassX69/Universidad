using System;

namespace Buscaminas
{
    // Clase que implementa el patrón Facade para simplificar la interacción con el juego
    public class BuscaminasFacade
    {
        private Juego _juego;
        
        public BuscaminasFacade()
        {
            _juego = Juego.ObtenerInstancia();
        }
        public void NuevoJuego(int tamano, int numMinas)
        {
            _juego.IniciarJuego(tamano, numMinas);
            Console.WriteLine($"Nuevo juego iniciado: Tablero {tamano}x{tamano} con {numMinas} minas.");
        }
        
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
            
            // Si la celda no tiene minas adyacentes, revelar las celdas vecinas (recursivamente)
            if (celda.MinasAdyacentes == 0)
            {
                _juego.RevelarCeldasVecinas(fila, columna);
            }
            
            return true;
        }
        

        public bool MarcarCelda(int fila, int columna)
        {
            if (_juego.TableroActual == null)
                throw new InvalidOperationException("El juego no ha sido inicializado.");
            Celda celda = _juego.TableroActual.ObtenerCelda(fila, columna);
            if (celda.EstaRevelada)
                return false;
            celda.EstaMarcada = !celda.EstaMarcada;
            
            return true;
        }
        public void MostrarTablero(bool mostrarMinas = false)
        {
            _juego.MostrarTablero(mostrarMinas);
        }
    }
}