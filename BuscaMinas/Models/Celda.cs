using System;

namespace Buscaminas
{

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
                _estado = FabricaEstadoCelda.ObtenerInstancia().ObtenerEstadoCelda(value, MinasAdyacentes);
            }
        }
        
        public int MinasAdyacentes 
        { 
            get { return _estado.MinasAdyacentes; }
            set 
            { 
                _estado = FabricaEstadoCelda.ObtenerInstancia().ObtenerEstadoCelda(TieneMina, value);
            }
        }
        
        public Celda(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
            EstaRevelada = false;
            EstaMarcada = false;

            _estado = FabricaEstadoCelda.ObtenerInstancia().ObtenerEstadoCelda(false, 0);
        }
        
        // Implementación del método Clonar de la interfaz IPrototype
        public IPrototype Clonar()
        {
            Celda clon = new Celda(this.Fila, this.Columna);
            clon.EstaRevelada = this.EstaRevelada;
            clon.EstaMarcada = this.EstaMarcada;
            
            clon._estado = this._estado;
            
            return clon;
        }
        
        public Celda ClonarEnPosicion(int fila, int columna)
        {
            Celda clon = (Celda)this.Clonar();
            clon.Fila = fila;
            clon.Columna = columna;
            
            return clon;
        }
    }
}