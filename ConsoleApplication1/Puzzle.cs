using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            {
                Puzzle inicio = new Puzzle(new int[] { 4, 2, 1, 7, 3, 5, 0, 8, 6 });      
                Puzzle meta = new Puzzle(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 0 });
                Stopwatch stopWatch = new Stopwatch();

                Console.WriteLine("Iniciando Breadth First Search: ");
                stopWatch.Start();
                BFS bfs = new BFS(inicio);
                bfs.buscaSolucion(meta);                
                stopWatch.Stop();
                long tsbfs = stopWatch.ElapsedMilliseconds;
                Console.WriteLine("RunTime BFS: " + tsbfs + " ms");

                Console.WriteLine("Iniciando Best First: ");
                stopWatch.Reset();
                stopWatch.Start();
                BestFirst bf = new BestFirst(inicio);
                bf.buscaSolucion(meta);
                stopWatch.Stop();
                long tsbf = stopWatch.ElapsedMilliseconds;
                Console.WriteLine("RunTime BF: " + tsbf +" ms");

                Console.WriteLine("Iniciando Hill Climbing: ");
                stopWatch.Reset();
                stopWatch.Start();
                HillClimbing hc = new HillClimbing(inicio);
                hc.buscaSolucion(meta);
                stopWatch.Stop();
                long hcbf = stopWatch.ElapsedMilliseconds;
                Console.WriteLine("RunTime HC: " + hcbf + " ms");                
               
                Console.ReadKey();
            }
        }
    }

    public class Puzzle
{
   // Esta clase solo sirve para tableros de 3x3
        public int[] tablero { get; set; } 
   
   //Crea un tablero vacio
    public Puzzle()
    {
        tablero = new int[] {0,0,0,
                             0,0,0,
                             0,0,0};
    }
    
    //Crea un tablero con valores establecidos
    public Puzzle(int[] tab)
    {
        //TODO: revisar longitud de arreglo
        tablero = new int[] {tab[0],tab[1],tab[2],
                             tab[3],tab[4],tab[5],
                             tab[6],tab[7],tab[8]};
    }
    
    public bool igualA (Puzzle otro){
        bool aux=false;
        for(int i=0;i<9;i++){
            if (this.tablero[i]!=otro.tablero[i]){
                aux=false;
                break;
            }
            aux=true;
        }
        return aux;
    }
    public int getValorPos(int x)
    {
        return tablero[x];
    }
    //Mueve el puzzle a la siguiente posicion
    // 1-Arriba, 2-Abajo, 3-Derecha, 4-Izquierda
    // Se considera el movimiento del espacio vacio
    //+---+---+---+            +---+---+---+
    //|     1   2 |  (Abajo)   | 3   1   2 |
    //| 3   4   5 |   ----->   |     4   5 |
    //| 6   7   8 |            | 6   7   8 |
    //+---+---+---+            +---+---+---+
    public Puzzle mover(int direccion){
        //Buscar la posicion vacia
        int pos = Array.IndexOf(tablero, 0);
        int[] auxTab = (int[]) tablero.Clone();
        switch (direccion){
            case 1: //Ariba
                if (pos==0 || pos==1 || pos==2) {return null;}
                //intercambiar posiciones
                else {
                    auxTab[pos]=auxTab[pos-3];
                    auxTab[pos-3]=0;
                    return new Puzzle(auxTab);
                }                
            case 2: //Abajo
                if (pos==6 || pos==7 || pos==8) {return null;}
                //intercambiar posiciones
                else {
                    auxTab[pos]=auxTab[pos+3];
                    auxTab[pos+3]=0;
                    return new Puzzle(auxTab);
                }
            case 3: //Derecha
                if (pos==2 || pos==5 || pos==8) {return null;}
                //intercambiar posiciones
                else {
                    auxTab[pos]=auxTab[pos+1];
                    auxTab[pos+1]=0;
                    return new Puzzle(auxTab);
                }
            case 4: //Izquierda
                if (pos==0 || pos==3 || pos==6) {return null;}
                //intercambiar posiciones
                else {
                    auxTab[pos]=auxTab[pos-1];
                    auxTab[pos-1]=0;
                    return new Puzzle(auxTab);
                }
            default: return null;
        }
    }
    
    public void imprime(){
        for(int i=0; i<3;i++){
            for(int j=0; j<3;j++){
			if(tablero[j+(i*3)]==0)
			{ Console.Write("  "); }
			else 
            {
                Console.Write(tablero[j+(i*3)]+" "); }
            }
            Console.WriteLine("\n");
        }
       Console.WriteLine("\n");        
    }
}
}
