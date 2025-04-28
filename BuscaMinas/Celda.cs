using System;

namespace Buscaminas
{
    // Clase que representa una celda individual del tablero
    // Implementa la interfaz IPrototype para el patrón Prototype
    // Ahora utiliza el patrón Flyweight para compartir el estado intrínseco
    public class Celda : IPrototype
    {
        // Propiedades extrínsecas (específicas de cada celda)
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool EstaRevelada { get; set; }
        public bool EstaMarcada { get; set; }
        
        // Estado compartido (Flyweight)
        private EstadoCelda _estado;
        
        // Propiedades de acceso al estado compartido
        public bool TieneMina 
        { 
            get { return _estado.TieneMina; }
            set 
            { 
                // Al cambiar esta propiedad, necesitamos actualizar el estado compartido
                _estado = FabricaEstadoCelda.ObtenerInstancia().ObtenerEstadoCelda(value, MinasAdyacentes);
            }
        }
        
        public int MinasAdyacentes 
        { 
            get { return _estado.MinasAdyacentes; }
            set 
            { 
                // Al cambiar esta propiedad, necesitamos actualizar el estado compartido
                _estado = FabricaEstadoCelda.ObtenerInstancia().ObtenerEstadoCelda(TieneMina, value);
            }
        }
        
        // Constructor de la celda
        public Celda(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
            EstaRevelada = false;
            EstaMarcada = false;
            
            // Obtener el estado inicial (sin mina, 0 minas adyacentes)
            _estado = FabricaEstadoCelda.ObtenerInstancia().ObtenerEstadoCelda(false, 0);
        }
        
        // Implementación del método Clonar de la interfaz IPrototype
        public IPrototype Clonar()
        {
            Celda clon = new Celda(this.Fila, this.Columna);
            clon.EstaRevelada = this.EstaRevelada;
            clon.EstaMarcada = this.EstaMarcada;
            
            // También clonamos el estado compartido (pero se sigue compartiendo el mismo objeto)
            clon._estado = this._estado;
            
            return clon;
        }
        
        // Método adicional que permite clonar con coordenadas específicas
        public Celda ClonarEnPosicion(int fila, int columna)
        {
            Celda clon = (Celda)this.Clonar();
            clon.Fila = fila;
            clon.Columna = columna;
            
            return clon;
        }
    }
}