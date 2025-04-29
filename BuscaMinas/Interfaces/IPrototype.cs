using System;

namespace Buscaminas
{
    public interface IPrototype
    {
        IPrototype Clonar();
    }
}