using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultipleThreadConsumptionTest
{
    class Program
    {

        public static int sizeTable;
        public static int nbThreads;
        public static int nbRepeatComputings;

        public static int minmax;
        private static int min = int.MaxValue;
        private static int max = int.MinValue;

        public static int done;
        private static DateTime startTime;
        private static DateTime endTime;

        static void Main(string[] args)
        {

            sizeTable = int.Parse(args[0]); // 2000;
            nbThreads = int.Parse(args[1]); // 10;
            nbRepeatComputings = int.Parse(args[2]); //20;
            minmax = int.Parse(args[3]); //10;

            Console.WriteLine(""
                + "Lancement des calculs en matrice (" + sizeTable + "x" + sizeTable + ")"
                + " sur " + nbThreads + " threads..."
                + " avec un appel de conccurrence tous les " + minmax + " oppération(s)");

            Console.WriteLine("Appuyez sur enter pour lancer le traitement");
            Console.ReadKey();

            Task[] taskList = new Task[nbThreads];

            startTime = DateTime.Now; // (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;

            for (int i = 0; i < nbThreads; i++)
            {
                int ni = new int();
                ni = i+1;
                taskList[i] = Task.Factory.StartNew(() => new MatCompute(ni, sizeTable, nbRepeatComputings, nbThreads).RunThread());
            }

            Task.WaitAll(taskList);
            Thread.Sleep(100);
            Console.WriteLine("All threads complete");



        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void minMax(int number)
        {
            if (number > max)
            {
                max = number;
            }
            if (number < min)
            {
                min = number;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void matComputedDone()
        {
            done++;

            Console.WriteLine("# threads ok : " + done);

            if (done == nbThreads)
            {
                endTime = DateTime.Now;
                if (minmax > 0)
                {
                    Console.WriteLine("Fin des calculs");
                    Console.WriteLine("Min: " + min + ", Max: " + max);
                }
                double timeSeconds = (endTime - startTime).TotalSeconds;
                Console.WriteLine("Durée des calculs: " + (endTime - startTime) + " " + timeSeconds);
            }
        }
    }
}
