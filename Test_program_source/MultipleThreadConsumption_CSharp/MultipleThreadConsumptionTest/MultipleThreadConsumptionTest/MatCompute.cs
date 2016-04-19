using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultipleThreadConsumptionTest
{
    class MatCompute //: BaseThread
    {
        private int sizeTable;
        private int nbRepeat;
        private int minmax;
        private int threadNumber;

        public MatCompute(int pThreadNumber, int pSizeTable, int pNbRepeat, int pMinmax) 
        {
            this.threadNumber = pThreadNumber;
            this.sizeTable = pSizeTable;
            this.nbRepeat = pNbRepeat;
            this.minmax = pMinmax;
        }


        public void RunThread()
        {

            Console.WriteLine("Start Thread: " + threadNumber);
            // On initialise la matrice
            int[,] matrix = new int[sizeTable, sizeTable];

            Random r = new Random();

            // on remplit la matrice de valeurs aleatoires
            for (int i = 0; i < sizeTable - 1; i++)
            {
                for (int j = 0; j < sizeTable - 1; j++)
                {
                    matrix[i, j] = (int)(r.NextDouble() * 10);
                }
            }

            // On process un bete calcul dans la matrice
            int sumMatrix = 0;
            int minmaxCounter = 0;
            Boolean plus = true;

            for (int c = 0; c < nbRepeat - 1; c++)
            {
                for (int i = 0; i < sizeTable - 1; i++)
                {
                    for (int k = 0; k < sizeTable - 1; k++)
                    {
                        sumMatrix = ((plus) ? sumMatrix + matrix[i, k] : sumMatrix - matrix[i, k]);
                        plus = !plus;

                        if (minmax > 0)
                        {
                            minmaxCounter++;
                            if (minmaxCounter % minmax == 0)
                            {
                                Program.minMax(sumMatrix);
                            }
                        }
                    }
                }
            }

            Program.matComputedDone();
        }
    }
}
