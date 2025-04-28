using System;

namespace Buscaminas
{
    // Clase para implementar el patrón Flyweight que contiene el estado compartido de las celdas
    // Este objeto será compartido entre múltiples celdas que tienen las mismas propiedades intrínsecas
    public class EstadoCelda
    {
        // Propiedades intrínsecas (compartidas)
        public bool TieneMina { get; set; }
        public int MinasAdyacentes { get; set; }
        
        // Constructor
        public EstadoCelda(bool tieneMina, int minasAdyacentes)
        {
            TieneMina = tieneMina;
            MinasAdyacentes = minasAdyacentes;
        }
    }
}