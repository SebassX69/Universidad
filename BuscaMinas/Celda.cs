using System;

namespace Buscaminas
{
    // Clase que representa una celda individual del tablero
    // Implementa la interfaz IPrototype para el patrón Prototype
    public class Celda : IPrototype
    {
        // Propiedades de la celda
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool TieneMina { get; set; }
        public int MinasAdyacentes { get; set; }
        public bool EstaRevelada { get; set; }
        public bool EstaMarcada { get; set; }
        
        // Constructor de la celda
        public Celda(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
            TieneMina = false;
            MinasAdyacentes = 0;
            EstaRevelada = false;
            EstaMarcada = false;
        }
        
        // Implementación del método Clonar de la interfaz IPrototype
        public IPrototype Clonar()
        {
            Celda clon = new Celda(this.Fila, this.Columna);
            clon.TieneMina = this.TieneMina;
            clon.MinasAdyacentes = this.MinasAdyacentes;
            clon.EstaRevelada = this.EstaRevelada;
            clon.EstaMarcada = this.EstaMarcada;
            
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