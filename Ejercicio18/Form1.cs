using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio18
{
    public partial class Form1 : Form
    {
        private string[] numerosLineas;
        private int cantidadLineas;
        int[][] matrizValores;
        int[][] copiaMatrizOriginal;
        public Form1()
        {
            InitializeComponent();
            cantidadLineas = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            txtRutaFinalTomada.Text = string.Empty;
            txtSumaMaxima.Text = string.Empty;
            txtCantidadTotalRutas.Text = string.Empty;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox1.Text = openFileDialog1.FileName;
                    string nuevaLinea = string.Empty;

                    System.IO.StreamReader contenidoArchivo = new System.IO.StreamReader(openFileDialog1.FileName);

                    //Se cuenta la cantidad de lineas que tiene el archivo para establecer la dimension del arreglo
                    while ((nuevaLinea = contenidoArchivo.ReadLine()) != null)
                        cantidadLineas++;
                    contenidoArchivo.BaseStream.Seek(0, SeekOrigin.Begin);

                    //Establesco el tamaño de la matriz de los valores
                    //================================================
                    matrizValores = new int[cantidadLineas][];
                    copiaMatrizOriginal = new int[cantidadLineas][];
                    for (int i = 0; i < cantidadLineas; i++)
                    {
                        matrizValores[i] = new int[cantidadLineas];
                        copiaMatrizOriginal[i] = new int[cantidadLineas];
                    }
                    //================================================              

                    int x = 0;
                    while ((nuevaLinea = contenidoArchivo.ReadLine()) != null)
                    {
                        numerosLineas = nuevaLinea.Split(' ');
                        for (int y = 0; y < numerosLineas.Length; y++)
                        {
                            matrizValores[x][y] = int.Parse(numerosLineas[y]);
                            copiaMatrizOriginal[x][y] = int.Parse(numerosLineas[y]);
                        }
                        x++;
                    }
                    contenidoArchivo.Close();

                    //Dibujar la matriz en pantalla
                    string lineaMatriz = string.Empty;
                    StringBuilder sbLineaMatriz = new StringBuilder();
                    int contadosEspacios = matrizValores.Length * 2 - 2;
                    for (int i = 0; i < matrizValores.Length; i++)
                    {
                        lineaMatriz = string.Empty;

                        for (int j = 0; j < matrizValores[i].Length; j++)
                        {
                            if (matrizValores[i][j] != 0)
                            {
                                if (matrizValores[i][j] < 10)
                                {
                                    lineaMatriz += "0" + matrizValores[i][j].ToString() + " ";
                                }
                                else
                                {
                                    lineaMatriz += matrizValores[i][j].ToString() + " ";
                                }

                            }
                            else
                            {
                                //lineaMatriz += "00 ";
                            }
                        }
                        sbLineaMatriz.AppendLine(string.Empty.PadLeft(contadosEspacios, ' ') + lineaMatriz);
                        contadosEspacios -= 2;
                    }

                    richTextBox1.Text = sbLineaMatriz.ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[][] matrizPiramidal = this.matrizValores;
            List<string> listaActualTomada = new List<string>();
            List<string> copiaListaActualTomada = new List<string>();
            List<string> listaRutaFinalTomada = new List<string>();
            StringBuilder historialRutas = new StringBuilder();

            //Se establece la cantidad total de rutas posibles dada la dimención de la matriz.
            //Que es como bien se explica en el problema 67 2 elevado a la cantidad de filas de la piramide.
            long cantidadTotalRutasPosibles = (long)Math.Pow(2, matrizPiramidal.Length - 1);

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
                    sumaActual = matrizPiramidal[0][0];
                    listaActualTomada = new List<string>();
                    listaActualTomada.Add("#" + i.ToString() + " -> ");
                    listaActualTomada.Add(matrizPiramidal[0][0].ToString());
                    xAux = 0;
                    for (int j = 0; j < matrizPiramidal.Length - 1; j++)
                    {
                        xAux = xAux + (i >> j & 1);
                        sumaActual += matrizPiramidal[j + 1][xAux];
                        //Guardo cada elemento de la ruta tomada para mostrar la ruta valida al final
                        listaActualTomada.Add(matrizPiramidal[j + 1][xAux].ToString());
                    }


                    string rutaActual = string.Empty;
                    for (int h = 0; h < listaActualTomada.Count(); h++)
                    {
                        if (h < listaActualTomada.Count() - 1)
                        {
                            rutaActual += listaActualTomada[h].ToString() + " - ";
                        }
                        else
                        {
                            rutaActual += listaActualTomada[h].ToString();
                        }
                    }

                    historialRutas.AppendLine(rutaActual);

                    if (sumaActual > sumaRutaMaxima)
                    {
                        sumaRutaMaxima = sumaActual;
                        listaRutaFinalTomada = listaActualTomada;
                    }
                }
                string ruta = string.Empty;
                for (int i = 0; i < listaRutaFinalTomada.Count(); i++)
                {
                    if (i < listaRutaFinalTomada.Count() - 1)
                    {
                        ruta += listaRutaFinalTomada[i].ToString() + " - ";
                    }
                    else
                    {
                        ruta += listaRutaFinalTomada[i].ToString();
                    }

                }
                //================================
                txtRutaFinalTomada.Text = ruta;
                txtSumaMaxima.Text = sumaRutaMaxima.ToString();
                richTextBox2.Text = historialRutas.ToString();
                txtCantidadTotalRutas.Text = cantidadTotalRutasPosibles.ToString();

            }
            else //Solución mas rapida para cuando es una piramide muy grande, y se utiliza un algoritmo mas eficiente.
            {
                int cantidadNivelesPiramide = this.matrizValores.Length;
                txtRutaFinalTomada.Text = "Ruta única -> ";
                historialRutas.AppendLine("Historial de sumas acumuladas:");
                //Se comienza a iterar de abajo hacia arriba para ir acortando los niveles de la piramide 
                //Con la suma mayor de los dos hijos con el padre inmediato superior.
                for (int x = cantidadNivelesPiramide - 2; x >= 0; x--)
                {
                    for (int y = 0; y <= x; y++)
                    {
                        this.matrizValores[x][y] += Math.Max(this.matrizValores[x + 1][y], this.matrizValores[x + 1][y + 1]);
                        
                        listaActualTomada = new List<string>();
                        listaActualTomada.Add(Math.Max(copiaMatrizOriginal[x + 1][y], copiaMatrizOriginal[x + 1][y + 1]).ToString() + " - ");

                        copiaListaActualTomada = new List<string>();
                        copiaListaActualTomada.Add(Math.Max(this.matrizValores[x + 1][y], this.matrizValores[x + 1][y + 1]).ToString() + " - ");
                    }
                    string rutaActual = string.Empty;
                    string historialSumaRutaActual = string.Empty;

                    for (int h = 0; h < listaActualTomada.Count(); h++)
                    {
                        if (h < listaActualTomada.Count() - 1)
                        {
                            rutaActual += listaActualTomada[h].ToString() + " - ";
                            historialSumaRutaActual += copiaListaActualTomada[h].ToString() + " - ";
                        }
                        else
                        {
                            rutaActual += listaActualTomada[h].ToString();
                            historialSumaRutaActual += copiaListaActualTomada[h].ToString();
                        }
                    }
                    txtRutaFinalTomada.Text += rutaActual;
                    historialRutas.AppendLine(historialSumaRutaActual);
                }

                //================================               
                txtSumaMaxima.Text = this.matrizValores[0][0].ToString();
                richTextBox2.Text = historialRutas.ToString();
                txtCantidadTotalRutas.Text = Math.Pow(2, matrizPiramidal.Length - 1).ToString();
            }
        }
    }
}
