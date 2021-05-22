using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace culebrita.Clases.ColaCircular
{
    class ColaCircular
    {
        public int fin;
        private static int MAXIAMQ = 90000;
        protected int frente;
        int tam;

        protected Object[] listaCola;

        //avanza a una posicion
        private int siguiente(int n)
        {
            return (n + 1) % MAXIAMQ;
        }

        //constructor
        public ColaCircular()
        {
            frente = 0;
            fin = MAXIAMQ - 1;
            listaCola = new Object[MAXIAMQ];
        }

        //validaciones
        public bool Cola_Vacia()
        {
            return frente == siguiente(fin);
        }
        //detecta cola si esta llena
        public bool Cola_Llena()
        {
            return fin == siguiente(siguiente(fin));
        }


        //operaciones modificacion de cola
        //inserta

        public void insertar(Object elemento)
        {
            if (!Cola_Llena())
            {
                fin = siguiente(fin);
                listaCola[fin] = elemento;
                tam++;
            }
            else
            {
                throw new Exception("Overflow de la cola");
            }
        }

        //quitar elemento

        public Object quitar()
        {
            if (!Cola_Vacia())
            {
                Object tm = listaCola[frente];
                frente = siguiente(frente);
                tam--;
                return tm;
            }
            else
            {
                throw new Exception("Cola Vacia");
            }
        }

        //limpiar cola

        public void BorrarCola()
        {
            frente = 0;
            fin = MAXIAMQ - 1;
            listaCola = new Object[MAXIAMQ];
        }


        //detener el valor de frente

        public Object frenteCola()
        {
            if (!Cola_Vacia())
            {
                return listaCola[frente];
            }
            else
            {
                throw new Exception("Cola Vacia");
            }
        }

        public Object FinaColaCircular()
        {

            if (!Cola_Vacia())
            {
                return listaCola[fin];
            }
            else
            {
                throw new Exception("Cola Vacia");
            }

        }

        public int tamaño()
        {
            return tam;
        }

        public bool Any(Point dato)
        {
            int i = 0, cont = 0;
            bool flag;
            while (i <= fin)
            {
                Point a = (Point)listaCola[i];
                flag = ((a.X == dato.X) && (a.Y == dato.Y));
                int z = (flag == true) ? cont++ : cont + 0;
            }
            return (cont != 0) ? true : false;
        }
    }
}
