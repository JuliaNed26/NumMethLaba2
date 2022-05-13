using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChisMetLaba2
{
    internal static class MatrixOperations
    {
        public static double[,] GetTransp (double[,] matrix)
        {
            int matrixSize = (int)Math.Sqrt(matrix.Length);
            double[,] transpMatrix = new double[matrixSize, matrixSize];
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = i; j < matrixSize; j++)
                {
                    transpMatrix[i, j] = matrix[j, i];
                    transpMatrix[j, i] = matrix[i, j];
                }
            }
            return transpMatrix;
        }

        public static double[,] MultiplyMatrixs (double[,] matrix1, double[,] matrix2)
        {
            int matrixSize = (int)Math.Sqrt(matrix1.Length);
            double[,] result = new double[matrixSize, matrixSize];
            for (int i = 0; i < matrixSize; i++)//matrix1 line
            {
                for(int j = 0; j < matrixSize; j++)//matrix2 column
                {
                    double curVal = 0;

                    for(int k = 0; k < matrixSize; k++)//matrix1 column
                    {
                        curVal += matrix1[i, k] * matrix2[k, j];
                    }
                    result[i,j] = curVal;
                }
            }
            return result;
        }

        public static void PrintMatrix(double[,] matrix)
        {
            int matrixSize = (int)Math.Sqrt(matrix.Length);

            for (int i = 0; i < matrixSize; i++)
            {
                Console.Write("{ ");
                for (int j = 0; j < matrixSize; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                    if (j % 4 == 0 && j != 0)
                    {
                        Console.WriteLine('\n');
                    }
                }
                Console.Write(" }, \n\n");
            }
        }
    }
}
