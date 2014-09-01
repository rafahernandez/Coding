using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class HillClimbing
    {
        //Se utiliza list ya que se debe ordenar la agenda
        List<Nodo> agenda = new List<Nodo>();
        List<Nodo> visitados = new List<Nodo>();

        public HillClimbing(Puzzle raiz)
        {
            //Crea la agenda con el elemento raiz
            agenda.Add(new Nodo(raiz));
        }

         private int creaHijos(Nodo n)
         {
             int cuenta = 0;
             //Los hijos se generan en el orden de 
             //1-Arriba, 2-Abajo, 3-Derecha, 4-Izquierda
             for (int i = 1; i < 5; i++)
             {
                 Puzzle nuevo = n.tablero.mover(i);
                 if (nuevo != null)
                 {
                     //Revisar si el tablero ya se creo
                     //Agregarlo si no se ha creado antes                     
                     Nodo temp = new Nodo(nuevo, n);
                     if (!yaCreado(temp))
                     {
                         agenda.Add(temp);
                         cuenta++;
                     }
                     
                     
                 }
             }
             return cuenta;
         }

         public void buscaSolucion(Puzzle meta)
         {
             int NodosVisitados = 0;
             int NodosCreados = 1;
             bool encontrado = false;
             Nodo final = null;
             //Hasta que la agenda este vacía 
             while (agenda.Count != 0)
             {
                 NodosVisitados++;
                 //Imprime informaicon de proceso
                // Console.SetCursorPosition(0, 0);
                //Console.Write("Visitados " + NodosVisitados + " de " + NodosCreados);
                //System.Threading.Thread.Sleep(50);

                 Puzzle tempP = agenda.First().tablero;
                 //Console.Write(agenda.First().heuristica+"\n");
                 //tempP.imprime();
                 
                 //tempP.imprime();           //debug
                 //Si el primer elemento es la meta 
                 if (meta.igualA(agenda.First().tablero))
                 {
                     encontrado = true;
                     final = agenda.First(); 
                     agenda.RemoveAt(0);
                     //Entonces acaba
                     break;
                 }
                 //Si no
                 else
                 {
                     //Elimina el primer elemento
                     Nodo temp = agenda.First();
                     agenda.RemoveAt(0);
                     visitados.Add(temp);
                     //Y añade sus sucesores al final de la agenda 
                     NodosCreados += creaHijos(temp);
                     //Ordena los elementos de la agenda de menor a mayor
                     //Se trata de minimizar el valor de heuristica
                     agenda.Sort((x, y) => x.heuristica.CompareTo(y.heuristica));
                     //Eliminar los demas.
                     agenda.RemoveRange(1, agenda.Count - 1);
                     
                 }

             }
             if (encontrado)
             {
                 List<Nodo> pasos = traza(final);
                 Console.WriteLine("Se encontro una solucion en " + (pasos.Count - 1) + " pasos.");
                //foreach (Nodo p in pasos)
                //{ p.tablero.imprime(); }
             }
             else
             {
                 Console.WriteLine("No se encontro una solucion.");
             }
             Console.WriteLine("Nodos Creados : " + NodosCreados);
             Console.WriteLine("Nodos Visitados : " + NodosVisitados);

         }

         public List<Nodo> traza(Nodo hoja)
         {
             List<Nodo> antecesores = new List<Nodo>();
             antecesores.Add(hoja);
             Nodo aux = hoja;
             while (aux.padre != null)
             {
                 antecesores.Add(aux.padre);
                 aux = aux.padre;
             }
             antecesores.Reverse();
             return antecesores;
         }

         private bool yaCreado(Nodo n)
         {             
             bool ageval = ExisteEnLista(agenda,n);
             bool visval = ExisteEnLista(visitados,n);
             if (!(ageval || visval))
             {return false;}
             else { return true; }
            
         }

         private static bool ExisteEnLista(List<Nodo> l, Nodo n)
         {
             bool val=false;
             foreach (Nodo nl in l)
             {
                     for (int i = 0; i < 9; i++)
                     {
                         val = true; 
                         if (nl.tablero.getValorPos(i) != n.tablero.getValorPos(i))
                         {
                         val= false;
                         break;
                         }                         
                     }                       
                     if (val) { return true; } 
             }
             return false;
         }
       
        
    }
}
