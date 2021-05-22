using culebrita.Clases.Bi_Cola;
using culebrita.Clases.ColaCircular;
using culebrita.Clases.ColaConLista;
using culebrita.Clases.ColaLineal;
using culebrita.Classes_Structure.Auxiliar_Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace culebrita
{
    class Program
    {
        static void Main()
        {
            Menu();

            /*var punteo = 0;
            var velocidad = 100;    //modificar estos valores y ver qué pasa
                                    //Modifica la velocidad de movimiento
            
            var posiciónComida = Point.Empty;
            var tamañoPantalla = new Size(60, 20);
            var culebrita = new Queue<Point>();
            var longitudCulebra = 3; //modificar estos valores y ver qué pasa
                                    //Modifica la longitud inicial

            var posiciónActual = new Point(0, 9); //modificar estos valores y ver qué pasa
                                                //Modifica la posicion de la culebra al iniciar

            culebrita.Enqueue(posiciónActual);
            var dirección = Direction.Derecha; //modificar estos valores y ver qué pasa
                                                //Modifica la direccion de la culebrita

            DibujaPantalla(tamañoPantalla);
            MuestraPunteo(punteo);

            while (MoverLaCulebrita(culebrita, posiciónActual, longitudCulebra, tamañoPantalla))
            {
                Thread.Sleep(velocidad);
                dirección = ObtieneDireccion(dirección);
                posiciónActual = ObtieneSiguienteDireccion(dirección, posiciónActual);

                if (posiciónActual.Equals(posiciónComida))      //Se activa cuando come la comida
                {
                    posiciónComida = Point.Empty;
                    longitudCulebra++; //modificar estos valores y ver qué pasa
                                        //R//Cambia la longitud de la culebrita
                    
            punteo += 10; //modificar estos valores y ver qué pasa
                                 //R//Cambia el punteo acumulativo
                    MuestraPunteo(punteo);
                }

                if (posiciónComida == Point.Empty) //entender qué hace esta linea 
                                                    //Valida si la posición de la comida tiene valores de 0 tanto en x como en Y
                {
                    posiciónComida = MostrarComida(tamañoPantalla, culebrita);
                }
            }

            Console.ResetColor();
            Console.SetCursorPosition(tamañoPantalla.Width / 2 - 4, tamañoPantalla.Height / 2);
            Console.Write("Haz perdido");
            Thread.Sleep(2000);
            Console.ReadKey();*/


        }

        //Genera el menu para que el jugador elija que estructura usara
        static void Menu()
        {
                //inicializamos controlador del juego
        GameHandler SnakeHandler = new GameHandler();

       
            Console.ForegroundColor = ConsoleColor.DarkRed;
            int Option;
            Console.WriteLine("WELCOME TO SNAKE WARS\n\n\n");
            Console.WriteLine("Please choose a data structure\n ");

            Console.WriteLine("0. Bi-Cola\n");
            Console.WriteLine("1.Cola-Lineal\n");
            Console.WriteLine("2.Cola-Con-Lista\n ");
            Console.WriteLine("3. Cola-Circular\n");

            Option = int.Parse(Console.ReadLine());

            switch (Option) {
                
                case 0: SnakeHandler.option1(); break;
                    
                case 1: SnakeHandler.option2(); break;
                    
                case 2: SnakeHandler.option3(); break;
                    
                case 3: SnakeHandler.option4(); break;
                case 4: return;
            }
        }
      
    }//end class
}




