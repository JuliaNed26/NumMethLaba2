using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChisMetLaba2
{
    internal class YakobiMethod
    {
        public EquationsSystem equatSystem;
        public List<double[]> Xiterations { get; private set; }

        public YakobiMethod(EquationsSystem _equatSystem)
        {
            equatSystem = _equatSystem;
            Xiterations = new List<double[]>();
            Check();
        }

        private void Check()
        {
            for(int i = 0; i < equatSystem.Size; i++)
            {
                double sumNonDiag = 0;

                for (int k = 0; k < equatSystem.Size; k++)
                {
                    if (i != k)
                    {
                        sumNonDiag += Math.Abs(equatSystem.matrix[i, k]);
                    }
                }
                if(Math.Abs(equatSystem.matrix[i,i]) < sumNonDiag)
                {
                    throw new Exception("Метод не збігається");
                }
            }
        }
        public double[] GetResolution(double accuracy)
        {
            Xiterations.Add(new double[equatSystem.Size]);
            while(true)
            {
                double[] curIteration = new double[equatSystem.Size];
                for (int i = 0; i < equatSystem.Size; i++)
                {
                    double curX = equatSystem.constants[i] / equatSystem.matrix[i, i];
                    for(int j = 0; j < i; j++)
                    {
                        curX -= (equatSystem.matrix[i, j] * Xiterations[Xiterations.Count() - 1][j])/ equatSystem.matrix[i,i];
                    }
                    for(int j = i + 1; j < equatSystem.Size; j++)
                    {
                        curX -= (equatSystem.matrix[i, j] * Xiterations[Xiterations.Count() - 1][j]) / equatSystem.matrix[i, i];
                    }
                    curIteration[i] = curX;
                }
                Xiterations.Add(curIteration);
                double maxDifference = Math.Abs(Xiterations.Last()[0] - Xiterations[Xiterations.Count - 2][0]);
                for(int i = 1; i < equatSystem.Size; i++)
                {
                    double curDifference = Math.Abs(Xiterations.Last()[i] - Xiterations[Xiterations.Count - 2][i]);
                    maxDifference = curDifference > maxDifference ? curDifference : maxDifference;
                }
                if(maxDifference <= accuracy)
                {
                    break;
                }
            }

            return Xiterations.Last();
        }
    }
}
