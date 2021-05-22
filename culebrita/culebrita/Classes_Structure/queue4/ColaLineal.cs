using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace culebrita.Clases.ColaLineal
{
    class ColaLineal
    {

        public int fin;
        private static int MAXIMAQ = 90000;
        protected int frente;
        int tam;
        protected Object[] listaCola;


        public ColaLineal()
        {
            frente = 0;
            fin = -1;
            listaCola = new Object[MAXIMAQ];
        }


        public void insertar(Object elemento)
        {
            if (!Cola_Llena())
            {
                listaCola[++fin] = elemento;
                tam++;

            }
            else
            {
                throw new Exception("Overflow de la cola");
            }
        }

        //retorna si esta vacia
        public bool Cola_Vacia()
        {
            return frente > fin;
        }
        //detecta cola si esta llena
        public bool Cola_Llena()
        {
            if (fin == MAXIMAQ - 1)
            {
                return true;
            }
            return false;
        }

        //quitar elementos
        public Object quitar()
        {
            if (!Cola_Vacia())
            {
                tam--;
                return listaCola[frente++];
                //      
            }
            else
            {
                throw new Exception("Cola Vacia");
            }
        }

        //limpiamos la cola
        public void LimpiarCola()
        {
            frente = 0;
            fin = -1;
        }

        //acceso a la cola
        public Object FrenteCola()
        {
            if (!Cola_Vacia())
            {
                return listaCola[frente++];
            }
            else
            {
                throw new Exception("Cola Vacia");
            }
        }

        public Object finalCola()
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
                i++;
            }
            return (cont != 0) ? true : false;
        }

    }//end class
}
