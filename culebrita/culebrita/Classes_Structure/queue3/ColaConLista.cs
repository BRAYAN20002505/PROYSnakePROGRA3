using culebrita.Clases.Auxiliares;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace culebrita.Clases.ColaConLista
{
    class ColaConLista
    {
        public Nodo primero;
        public Nodo ultimo;
        int tam;

        //constructor:: crear cola vacia
        public ColaConLista()
        {
            primero = ultimo = null;

            //tam = 0;

        }

        //verificar si esta vacia la cola

        public bool ColaVacia()
        {
            return (primero == null);
        }

        //insertarmos por el final de la cola
        public void Insertar(Object elemento)
        {
            Nodo nuevo;
            nuevo = new Nodo(elemento);

            if (ColaVacia())
            {
                primero = nuevo;
            }
            else
            {
                ultimo.siguiente = nuevo;
            }
            ultimo = nuevo;
            tam++;
        }

        ///quitar elemento
        ///
        public Object quitar()
        {
            Object aux;
            if (!ColaVacia())
            {
                aux = primero.elemento;
                primero = primero.siguiente;
                tam--;
            }
            else
            {
                throw new Exception("Error porque la cola esta vacía");
            }
            return aux;
        }

        //vaicar la cola o liberar todos los nodos

        public void borrarCola()
        {
            for (; primero != null;)
            {
                primero = primero.siguiente;
            }
        }

        //devolver el valor que esta el frenete de la cola

        public Object frenteCola()
        {
            if (ColaVacia())
            {
                throw new Exception("Error porque la cola esta vacía");
            }
            return (primero.elemento);
        }

        public Object finalCola()
        {
            if (ColaVacia())
            {
                throw new Exception("Error porque la cola esta vacía");
            }
            return (ultimo.elemento);
        }

        public Object finalcolaLista()
        {
            if (!ColaVacia())
            {
                return (ultimo.elemento);
            }
            else
            {
                throw new Exception("Cola vacia");
            }
        }

        public int tamaño()
        {
            return tam;
        }

        public int numElementosLista()
        {
            int n;
            Nodo a = primero;

            if (ColaVacia())
            {
                n = 0;
            }
            else
            {
                n = 1;
                while (a != ultimo)
                {
                    n++;
                    a = a.siguiente;

                }
            }

            return n;
        }

        public bool Any(Point dato)
        {
            int i = 0, cont = 0;

            Nodo aux = primero;
            bool flag;
            while (aux != null)
            {
                Point a = (Point)aux.elemento;
                flag = ((a.X == dato.X) && (a.Y == dato.Y));
                int z = (flag == true) ? cont++ : cont + 0;
                i++;
                aux = aux.siguiente;
            }
            return (cont != 0) ? true : false;
        }


    }
}
