using System;

namespace Buscaminas
{
    // Interfaz para el patrón Prototype
    public interface IPrototype
    {
        // Método para clonar un objeto
        IPrototype Clonar();
    }
}