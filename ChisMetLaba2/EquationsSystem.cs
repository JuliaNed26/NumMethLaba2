using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChisMetLaba2
{
    internal class EquationsSystem
    {
        public int Size { get; private set; }
        public readonly double[,] matrix;
        public readonly double[] constants;
        public EquationsSystem(int size)
        {
            Size = size;
            matrix = new double[size, size];
            constants = new double[size];
            FillEquations();
        }

        public double[] GetNevyazok(double[] custResult)
        {
            double[] nevyazok = new double[custResult.Length];
            for (int i = 0; i < nevyazok.Length; i++)
            {
                for(int j = 0; j < nevyazok.Length; j++)
                {
                    nevyazok[i] += matrix[i, j] * custResult[j];
                }
                nevyazok[i] -= constants[i];
                nevyazok[i] = Math.Abs(nevyazok[i]);
            }
            return nevyazok;
        }

        private void FillEquations()
        {
            for (int i = 1; i <= Size; i++)
            {
                for (int j = 1; j <= Size; j++)
                {
                    if(i == j)
                    {
                        matrix[i - 1, j - 1] = Size + 10.0 + (double)i / Size + j / 10.0;
                    }
                    else
                    {
                        matrix[i - 1, j - 1] = (i + j) / (5.0 + Size);
                    }
                }
                constants[i - 1] = 3 * i * i - Size;
            }
        }

        public void PrintEquations()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    char curX = (char)('a' + j);
                    Console.Write(matrix[i, j].ToString() + (char)curX + (j == Size - 1 ? " = " : " + "));
                    if (j % 3 == 0 && j != 0)
                    {
                        Console.WriteLine();
                    }
                }
                Console.Write(constants[i] + "\n\n");
            }
        }
    }
}
