using System;
using System.Collections.Generic;

namespace Buscaminas
{
    // Clase fábrica para el patrón Flyweight que administra los objetos compartidos
    public class FabricaEstadoCelda
    {
        // Diccionario que almacena los estados compartidos existentes
        private Dictionary<string, EstadoCelda> _estadosCelda;
        
        // Instancia única (aplicando también el patrón Singleton para la fábrica)
        private static FabricaEstadoCelda? _instancia;
        
        // Constructor privado
        private FabricaEstadoCelda()
        {
            _estadosCelda = new Dictionary<string, EstadoCelda>();
        }
        
        // Método para obtener la instancia única
        public static FabricaEstadoCelda ObtenerInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new FabricaEstadoCelda();
            }
            return _instancia;
        }
        
        // Método para obtener un EstadoCelda (creándolo si no existe)
        public EstadoCelda ObtenerEstadoCelda(bool tieneMina, int minasAdyacentes)
        {
            // Crear una clave única para este estado
            string clave = $"{tieneMina}-{minasAdyacentes}";
            
            // Verificar si ya tenemos este estado
            if (!_estadosCelda.ContainsKey(clave))
            {
                // Si no existe, crear un nuevo estado
                _estadosCelda[clave] = new EstadoCelda(tieneMina, minasAdyacentes);
            }
            
            // Devolver el estado existente o recién creado
            return _estadosCelda[clave];
        }
        
        // Método para obtener la cantidad de estados únicos
        public int ObtenerCantidadEstados()
        {
            return _estadosCelda.Count;
        }
    }
}