using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio18ConsolaNameSpace
{
    class Program
    {
        private static string[] numerosLineas;
        private static int cantidadLineas;
        private static int[][] matrizValores;
        private static string nuevaLinea;

        static void Main(string[] args)
        {
            cantidadLineas = 0;
            string fileName = "E:\\Problema stackbuilder\\problema18.txt";
            System.IO.StreamReader contenidoArchivo = new System.IO.StreamReader(fileName);

            //Se cuenta la cantidad de lineas que tiene el archivo para establecer la dimension del arreglo
            while ((nuevaLinea = contenidoArchivo.ReadLine()) != null)
                cantidadLineas++;
            contenidoArchivo.BaseStream.Seek(0, SeekOrigin.Begin);

            //Establesco el tamaño de la matriz de los valores
            //================================================
            matrizValores = new int[cantidadLineas][];
            for (int i = 0; i < cantidadLineas; i++)
            {
                matrizValores[i] = new int[cantidadLineas];
            }
            //================================================              

            int x = 0;
            while ((nuevaLinea = contenidoArchivo.ReadLine()) != null)
            {
                numerosLineas = nuevaLinea.Split(' ');
                for (int y = 0; y < numerosLineas.Length; y++)
                {
                    matrizValores[x][y] = int.Parse(numerosLineas[y]);
                }
                x++;
            }
            contenidoArchivo.Close();

            //Comienza la algoritmizacion
            //============================================================
            
            //Se establece la cantidad total de rutas posibles dada la dimención de la matriz.
            //Que es como bien se explica en el problema 67 2 elevado a la cantidad de filas de la piramide.
            long cantidadTotalRutasPosibles = (long)Math.Pow(2, matrizValores.Length - 1);

            //Solución rapida para cuando es uan piramide no muy grande
            if (cantidadTotalRutasPosibles > 0)
            {
                int sumaRutaMaxima = 0;
                int sumaActual;
                //Variable que va a contener el valor del dezplazamiento de bits que va a indicar la decision a tomar
                int xAux;

                for (int i = 0; i <= cantidadTotalRutasPosibles; i++)
                {
                    //Obtengo el primer valor de la matriz para a partir de ahi comenzar las posibles rutas
                    //y comparar el resultado final
                    sumaActual = matrizValores[0][0];
                    xAux = 0;
                    for (int j = 0; j < matrizValores.Length - 1; j++)
                    {
                        xAux = xAux + (i >> j & 1);//Se ejecuta un desplazamiento bitwise y luego un AND con 1 para que siempre quede 1 o 0 y poder moverme en dos posiciones del sub-arbol en la matriz 
                        sumaActual += matrizValores[j + 1][xAux];                        
                    }

                    if (sumaActual > sumaRutaMaxima)
                    {
                        sumaRutaMaxima = sumaActual;
                    }
                }
                Console.WriteLine("La suma de la ruta máxima es: " + sumaRutaMaxima.ToString());
                Console.ReadLine();

            }
            else //Solución mas rapida para cuando es una piramide muy grande, y se utiliza un algoritmo mas eficiente.
            {
                int cantidadNivelesPiramide = matrizValores.Length;                
                //Se comienza a iterar de abajo hacia arriba para ir acortando los niveles de la piramide 
                //Con la suma mayor de los dos hijos con el padre inmediato superior.
                for (int k = cantidadNivelesPiramide - 2; k >= 0; k--)
                {
                    for (int y = 0; y <= k; y++)
                    {
                        matrizValores[k][y] += Math.Max(matrizValores[k + 1][y], matrizValores[k + 1][y + 1]);                       
                    }                    
                }
                //================================               
                Console.WriteLine("La suma de la ruta máxima es: " + matrizValores[0][0].ToString());
                Console.ReadLine();
            }
        }
    }
}
