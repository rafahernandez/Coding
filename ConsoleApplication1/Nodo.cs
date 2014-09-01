using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Nodo
    {
        public Puzzle tablero {get; set;} 
        public Nodo padre {get; set;}
        //Guarda el calculo de heuristica de este tablero
        public int heuristica { get; set; }

        public Nodo(Puzzle p)
        {
            tablero = p;
            padre = null;
            calculaValor();
        }

        public Nodo(Puzzle p, Nodo n)
        {
            tablero = p;
            padre = n;
            calculaValor();
        }

        public void calculaValor()
        {

            //TODO: Generalizar heuristicas

            //Distancia Manhattan
            //http://en.wikipedia.org/wiki/Taxicab_geometry           
            int sumaParcial = 0;
            for (int i = 0; i < 9; i++)
            {
                int valor = tablero.getValorPos(i);
                if (valor != 0)
                {
                    int tX = (valor - 1) / 3;
                    int tY = (valor - 1) % 3;
                    int dx = i / 3 - tX;
                    int dy = i % 3 - tY;
                    sumaParcial += Math.Abs(dx) + Math.Abs(dy);
                }
            }
            //Distancia de Hamming (Valores fuera de lugar)
            //http://en.wikipedia.org/wiki/Hamming_distance
            int[] meta = { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
            for (int j = 0; j < 9; j++)
            {
                if (tablero.getValorPos(j) != meta[j])
                {
                    sumaParcial++;
                }
            }
            heuristica = sumaParcial;
        }

        public override string ToString()
        {
            return ConvertStringArrayToStringJoin(tablero.tablero);
        }

        static string ConvertStringArrayToStringJoin(int[] array)
        {
            //
            // Use string Join to concatenate the string elements.
            //
            string result = string.Join(",", array);
            return result;
        }
    }
}
