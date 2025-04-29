using System;

namespace Buscaminas
{

    public class EstadoCelda
    {
        public bool TieneMina { get; set; }
        public int MinasAdyacentes { get; set; }
        
        public EstadoCelda(bool tieneMina, int minasAdyacentes)
        {
            TieneMina = tieneMina;
            MinasAdyacentes = minasAdyacentes;
        }
    }
}