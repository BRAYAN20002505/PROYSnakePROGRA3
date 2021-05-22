using culebrita.Clases.Bi_Cola;
using culebrita.Clases.ColaCircular;
using culebrita.Clases.ColaConLista;
using culebrita.Clases.ColaLineal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Linq;
using static culebrita.Program;

namespace culebrita.Classes_Structure.Auxiliar_Classes
{
    //este es un
    class GameHandler
    {
        internal enum Direction
        {
            Abajo, Izquierda, Derecha, Arriba
        }
        private static void DibujaPantalla(Size size)
        {
            Console.Title = "=>SNAKE WARS <=";
            Console.WindowHeight = size.Height + 2;//Size sirve para graficos
            Console.WindowWidth = size.Width + 2;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Black;
            for (int row = 0; row < size.Height; row++)
            {
                for (int col = 0; col < size.Width; col++)
                {
                    Console.SetCursorPosition(col + 1, row + 1);
                    Console.Write(" ");
                }
            }
        }



        private static void MuestraPunteo(int punteo)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(1, 0);
            Console.Write($"Score: {punteo.ToString("00000000")}");
        }




        private static Direction ObtieneDireccion(Direction direccionAcutal)
        {
            if (!Console.KeyAvailable) return direccionAcutal;

            var tecla = Console.ReadKey(true).Key;
            switch (tecla)
            {
                case ConsoleKey.DownArrow:
                    if (direccionAcutal != Direction.Arriba)
                        direccionAcutal = Direction.Abajo;
                    break;
                case ConsoleKey.LeftArrow:
                    if (direccionAcutal != Direction.Derecha)
                        direccionAcutal = Direction.Izquierda;
                    break;
                case ConsoleKey.RightArrow:
                    if (direccionAcutal != Direction.Izquierda)
                        direccionAcutal = Direction.Derecha;
                    break;
                case ConsoleKey.UpArrow:
                    if (direccionAcutal != Direction.Abajo)
                        direccionAcutal = Direction.Arriba;
                    break;
            }
            return direccionAcutal;
        }



        private static Point ObtieneSiguienteDireccion(Direction direction, Point currentPosition)
        {
            Point siguienteDireccion = new Point(currentPosition.X, currentPosition.Y);
            switch (direction)
            {
                case Direction.Arriba:
                    siguienteDireccion.Y--;
                    break;
                case Direction.Izquierda:
                    siguienteDireccion.X--;
                    break;
                case Direction.Abajo:
                    siguienteDireccion.Y++;
                    break;
                case Direction.Derecha:
                    siguienteDireccion.X++;
                    break;
            }
            return siguienteDireccion;
        }


        //Bicola
        public  void option1()
        {
            var punteo = 0;
            var velocidad = 100; //modificar estos valores y ver qué pasa
            var posiciónComida = Point.Empty;
            var tamañoPantalla = new Size(60, 20);
            BiCola culebrita = new BiCola();
            var longitudCulebra = 3; //modificar estos valores y ver qué pasa
            var posiciónActual = new Point(0, 9); //modificar estos valores y ver qué pasa
            culebrita.Insertar(posiciónActual);
            var dirección = Direction.Arriba; //modificar estos valores y ver qué pasa

            DibujaPantalla(tamañoPantalla);
            MuestraPunteo(punteo);

            while (MoverLaCulebritaBiCola(culebrita, posiciónActual, longitudCulebra, tamañoPantalla))
            {
                Thread.Sleep(velocidad);
                dirección = ObtieneDireccion(dirección);
                posiciónActual = ObtieneSiguienteDireccion(dirección, posiciónActual);

                if (posiciónActual.Equals(posiciónComida))
                {
                    posiciónComida = Point.Empty;
                    longitudCulebra += 3; //modificar estos valores y ver qué pasa
                    punteo += 5; //modificar estos valores y ver qué pasa
                    MuestraPunteo(punteo);
                    velocidad -= 10;

                }

                if (posiciónComida == Point.Empty) //entender qué hace esta linea
                {
                    posiciónComida = MostrarComidaBicola(tamañoPantalla, culebrita);
                }
            }

            Console.ResetColor();
            Console.SetCursorPosition(tamañoPantalla.Width / 2 - 4, tamañoPantalla.Height / 2);
            Console.Beep(659, 125);
            Console.Write("Fin del Juego");
            Thread.Sleep(2000);
            Console.ReadKey();
        }
        //Mueve con Bicola
        private static bool MoverLaCulebritaBiCola(BiCola culebra, Point posiciónObjetivo,
            int longitudCulebra, Size screenSize)
        {
            var lastPoint = (Point)culebra.finalBicola();

            if (lastPoint.Equals(posiciónObjetivo)) return true;

            // if (culebra.ToString().Any(x => x.Equals(posiciónObjetivo))) return false;//
            if (culebra.Any(posiciónObjetivo)) return false;//

            if (posiciónObjetivo.X < 0 || posiciónObjetivo.X >= screenSize.Width
                    || posiciónObjetivo.Y < 0 || posiciónObjetivo.Y >= screenSize.Height)
            {
                return false;
            }

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(lastPoint.X + 1, lastPoint.Y + 1);
            Console.WriteLine(" ");

            culebra.Insertar(posiciónObjetivo);

            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(posiciónObjetivo.X + 1, posiciónObjetivo.Y + 1);
            Console.Write(" ");

            // Quitar cola
            if (culebra.numElementosBicoLa() > longitudCulebra)//
            {
                var removePoint = (Point)culebra.quitar();//
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(removePoint.X + 1, removePoint.Y + 1);
                Console.Write(" ");
            }
            return true;
        }
        //Comida Bicola
        private static Point MostrarComidaBicola(Size screenSize, BiCola culebra)
        {

            var lugarComida = Point.Empty;

            var cabezaCulebra = (Point)culebra.finalBicola();
            var coor = cabezaCulebra.X;

            var rnd = new Random();
            do
            {
                var xi = rnd.Next(0, screenSize.Width - 1);
                var yi = rnd.Next(0, screenSize.Height - 1);
                if (culebra.ToString().All(x => coor != xi || coor != yi)
                    && Math.Abs(xi - cabezaCulebra.X) + Math.Abs(yi - cabezaCulebra.Y) > 8)
                {
                    lugarComida = new Point(xi, yi);
                    Console.Beep(659, 125);

                }

            } while (lugarComida == Point.Empty);

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(lugarComida.X + 1, lugarComida.Y + 1);
            Console.Write(" ");

            return lugarComida;
        }

        //ColaLineal

        public  void option2()
        {
            var punteo = 0;
            var velocidad = 100; //modificar estos valores y ver qué pasa
            var posiciónComida = Point.Empty;
            var tamañoPantalla = new Size(60, 20);
            var culebrita = new ColaLineal();
            var longitudCulebra = 3; //modificar estos valores y ver qué pasa
            var posiciónActual = new Point(0, 9); //modificar estos valores y ver qué pasa
            culebrita.insertar(posiciónActual);
            var dirección = Direction.Arriba; //modificar estos valores y ver qué pasa

            DibujaPantalla(tamañoPantalla);
            MuestraPunteo(punteo);

            while (MoverLaCulebritaColaLineal(culebrita, posiciónActual, longitudCulebra, tamañoPantalla))
            {
                Thread.Sleep(velocidad);
                dirección = ObtieneDireccion(dirección);
                posiciónActual = ObtieneSiguienteDireccion(dirección, posiciónActual);

                if (posiciónActual.Equals(posiciónComida))
                {
                    posiciónComida = Point.Empty;
                    longitudCulebra++; //modificar estos valores y ver qué pasa
                    punteo += 5; //modificar estos valores y ver qué pasa
                    MuestraPunteo(punteo);
                    velocidad -= 5;
                }

                if (posiciónComida == Point.Empty) //entender qué hace esta linea
                {
                    posiciónComida = MostrarComidaColaLineal(tamañoPantalla, culebrita);
                }
            }


            Console.ResetColor();
            Console.SetCursorPosition(tamañoPantalla.Width / 2 - 4, tamañoPantalla.Height / 2);
            Console.Beep(659, 125);
            Console.Write("Fin del Juego");
            Thread.Sleep(2000);
            Console.ReadKey();

        }
        private static bool MoverLaCulebritaColaLineal(ColaLineal culebra, Point posiciónObjetivo,
            int longitudCulebra, Size screenSize)
        {
            var lastPoint = (Point)culebra.finalCola();
            int pausa = 0;
            if (lastPoint.Equals(posiciónObjetivo)) return true;

            if (culebra.ToString().Any(x => x.Equals(posiciónObjetivo))) return false;
            //if (culebra.Any(posiciónObjetivo)) return false;

            if (posiciónObjetivo.X < 0 || posiciónObjetivo.X >= screenSize.Width
                    || posiciónObjetivo.Y < 0 || posiciónObjetivo.Y >= screenSize.Height)
            {
                return false;
            }

            Console.BackgroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(lastPoint.X + 1, lastPoint.Y + 1);
            Console.WriteLine(" ");

            culebra.insertar(posiciónObjetivo);
            int pausa1 = 0;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(posiciónObjetivo.X + 1, posiciónObjetivo.Y + 1);
            Console.Write(" ");

            // Quitar cola
            if (culebra.tamaño() > longitudCulebra)
            {
                var removePoint = (Point)culebra.quitar();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(removePoint.X + 1, removePoint.Y + 1);
                Console.Write(" ");
            }
            return true;
        }
        private static Point MostrarComidaColaLineal(Size screenSize, ColaLineal culebra)
        {
            var lugarComida = Point.Empty;
            var cabezaCulebra = (Point)culebra.finalCola();
            var rnd = new Random();
            var Px = cabezaCulebra.X;
            var Py = cabezaCulebra.Y;
            do
            {
                var x = rnd.Next(0, screenSize.Width - 1);
                var y = rnd.Next(0, screenSize.Height - 1);
                if (culebra.ToString().All(p => Px != x || Py != y)
                    && Math.Abs(x - cabezaCulebra.X) + Math.Abs(y - cabezaCulebra.Y) > 8)
                {
                    lugarComida = new Point(x, y);
                    Console.Beep(659, 125);
                }

            } while (lugarComida == Point.Empty);

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(lugarComida.X + 1, lugarComida.Y + 1);
            Console.Write(" ");

            return lugarComida;
        }

        //ColaConLista
       public  void option3()
        {
            var punteo = 0;
            var velocidad = 100; //modificar estos valores y ver qué pasa
            var posiciónComida = Point.Empty;
            var tamañoPantalla = new Size(60, 20);
            var culebrita = new ColaConLista();
            var longitudCulebra = 3; //modificar estos valores y ver qué pasa
            var posiciónActual = new Point(0, 9); //modificar estos valores y ver qué pasa
            culebrita.Insertar(posiciónActual);
            var dirección = Direction.Arriba; //modificar estos valores y ver qué pasa

            DibujaPantalla(tamañoPantalla);
            MuestraPunteo(punteo);

            while (MoverLaCulebritaColaConLista(culebrita, posiciónActual, longitudCulebra, tamañoPantalla))
            {
                Thread.Sleep(velocidad);
                dirección = ObtieneDireccion(dirección);
                posiciónActual = ObtieneSiguienteDireccion(dirección, posiciónActual);

                if (posiciónActual.Equals(posiciónComida))
                {
                    posiciónComida = Point.Empty;
                    longitudCulebra++; //modificar estos valores y ver qué pasa
                    punteo += 5; //modificar estos valores y ver qué pasa
                    MuestraPunteo(punteo);
                    velocidad -= 10;

                }

                if (posiciónComida == Point.Empty) //entender qué hace esta linea
                {
                    posiciónComida = MostrarComidaColaConLista(tamañoPantalla, culebrita);
                }
            }

            Console.ResetColor();
            Console.SetCursorPosition(tamañoPantalla.Width / 2 - 4, tamañoPantalla.Height / 2);
            Console.Beep(659, 125);
            Console.Write("Fin del Juego");
            Thread.Sleep(2000);
            Console.ReadKey();

        }

        private static Point MostrarComidaColaConLista(Size screenSize, ColaConLista culebra)
        {
            var lugarComida = Point.Empty;
            var cabezaCulebra = (Point)culebra.finalCola();
            var rnd = new Random();

            var Px = cabezaCulebra.X;
            var Py = cabezaCulebra.Y;
            {
                var x = rnd.Next(0, screenSize.Width - 1);
                var y = rnd.Next(0, screenSize.Height - 1);
                if (culebra.ToString().Any(p => Px != x || Py != y)
                    && Math.Abs(x - cabezaCulebra.X) + Math.Abs(y - cabezaCulebra.Y) > 8)
                {
                    lugarComida = new Point(x, y);
                    Console.Beep(659, 125);
                }

            } while (lugarComida == Point.Empty) ;

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(lugarComida.X + 1, lugarComida.Y + 1);
            Console.Write(" ");

            return lugarComida;
        }

        private static bool MoverLaCulebritaColaConLista(ColaConLista culebra, Point posiciónObjetivo,
           int longitudCulebra, Size screenSize)
        {

            var lastPoint = (Point)culebra.finalcolaLista();

            int pausa = 0;
            if (lastPoint.Equals(posiciónObjetivo)) return true;

            if (culebra.ToString().Any(x => x.Equals(posiciónObjetivo))) return false;
            //if (culebra.Any(posiciónObjetivo)) return false;

            if (posiciónObjetivo.X < 0 || posiciónObjetivo.X >= screenSize.Width
                    || posiciónObjetivo.Y < 0 || posiciónObjetivo.Y >= screenSize.Height)
            {
                return false;
            }

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(lastPoint.X + 1, lastPoint.Y + 1);
            Console.WriteLine(" ");

            culebra.Insertar(posiciónObjetivo);
            int pausa1 = 0;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(posiciónObjetivo.X + 1, posiciónObjetivo.Y + 1);
            Console.Write(" ");

            // Quitar cola
            if (culebra.numElementosLista() > longitudCulebra)
            {
                var removePoint = (Point)culebra.quitar();

                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(removePoint.X + 1, removePoint.Y + 1);
                Console.Write(" ");
            }
            return true;
        }

        //ColaCircular

       public  void option4()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            var punteo = 0;
            var velocidad = 100; //modificar estos valores y ver qué pasa
            var posiciónComida = Point.Empty;
            var tamañoPantalla = new Size(60, 20);
            var culebrita = new ColaCircular();
            var longitudCulebra = 5; //modificar estos valores y ver qué pasa
            var posiciónActual = new Point(0, 9); //modificar estos valores y ver qué pasa
            culebrita.insertar(posiciónActual);
            var dirección = Direction.Arriba; //modificar estos valores y ver qué pasa

            DibujaPantalla(tamañoPantalla);
            MuestraPunteo(punteo);

            while (MoverLaCulebritaColaCircular(culebrita, posiciónActual, longitudCulebra, tamañoPantalla))
            {
                Thread.Sleep(velocidad);
                dirección = ObtieneDireccion(dirección);
                posiciónActual = ObtieneSiguienteDireccion(dirección, posiciónActual);

                if (posiciónActual.Equals(posiciónComida))
                {
                    posiciónComida = Point.Empty;
                    longitudCulebra += 3; //modificar estos valores y ver qué pasa
                    punteo += 5; //modificar estos valores y ver qué pasa
                    MuestraPunteo(punteo);
                    velocidad -= 5;
                }

                if (posiciónComida == Point.Empty) //entender qué hace esta linea
                {
                    posiciónComida = MostrarComidaColaCircular(tamañoPantalla, culebrita);
                }
            }

            Console.ResetColor();
            Console.SetCursorPosition(tamañoPantalla.Width / 2 - 4, tamañoPantalla.Height / 2);
            Console.Beep(659, 125);
            Console.Write("Fin del Juego");
            Console.Beep(650, 2);
            Thread.Sleep(2000);
            Console.ReadKey();

        }

        private static bool MoverLaCulebritaColaCircular(ColaCircular culebra, Point posiciónObjetivo,
            int longitudCulebra, Size screenSize)
        {
            var lastPoint = (Point)culebra.FinaColaCircular();//

            if (lastPoint.Equals(posiciónObjetivo)) return true;

            if (culebra.ToString().Any(x => x.Equals(posiciónObjetivo))) return false;//

            if (posiciónObjetivo.X < 0 || posiciónObjetivo.X >= screenSize.Width
                    || posiciónObjetivo.Y < 0 || posiciónObjetivo.Y >= screenSize.Height)
            {
                return false;
            }

            Console.BackgroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(lastPoint.X + 1, lastPoint.Y + 1);
            Console.WriteLine(" ");

            culebra.insertar(posiciónObjetivo);

            Console.BackgroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(posiciónObjetivo.X + 1, posiciónObjetivo.Y + 1);
            Console.Write(" ");

            // Quitar cola
            if (culebra.tamaño() > longitudCulebra)//
            {
                var removePoint = (Point)culebra.quitar();//
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(removePoint.X + 1, removePoint.Y + 1);
                Console.Write(" ");
            }
            return true;
        }

        private static Point MostrarComidaColaCircular(Size screenSize, ColaCircular culebra)
        {

            var lugarComida = Point.Empty;

            var cabezaCulebra = (Point)culebra.FinaColaCircular();//ca
            var coor = cabezaCulebra.X;//

            var rnd = new Random();
            do
            {
                var x = rnd.Next(0, screenSize.Width - 1);
                var y = rnd.Next(0, screenSize.Height - 1);
                if (culebra.ToString().All(p => coor != x || coor != y)//
                    && Math.Abs(x - cabezaCulebra.X) + Math.Abs(y - cabezaCulebra.Y) > 8)
                {
                    lugarComida = new Point(x, y);
                    Console.Beep(659, 125);

                }

            } while (lugarComida == Point.Empty);

            Console.BackgroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(lugarComida.X + 1, lugarComida.Y + 1);
            Console.Write(" ");

            return lugarComida;
        }

    }
}
