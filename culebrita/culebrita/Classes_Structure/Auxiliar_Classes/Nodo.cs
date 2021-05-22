using System;
using System.Collections.Generic;
using System.Text;

namespace culebrita.Clases.Auxiliares
{
    class Nodo
    {
        public Object elemento;
        public Nodo siguiente;


        public Nodo(Object dato)
        {
            elemento = dato;
            siguiente = null;
        }


    }
}
