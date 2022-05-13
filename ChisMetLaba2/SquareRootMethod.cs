using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChisMetLaba2
{
    internal class SquareRootMethod
    {
        public EquationsSystem equatSystem;
        public double[,] matrixD;
        public double[,] matrixS;
        private double[,] stdMatrix;
        public SquareRootMethod(EquationsSystem _equatSystem)
        {
            equatSystem = _equatSystem;
            matrixD = new double[equatSystem.Size, equatSystem.Size];
            matrixS = new double[equatSystem.Size, equatSystem.Size];
            FillMatrixsSAndD();
            stdMatrix = MatrixOperations.MultiplyMatrixs(MatrixOperations.GetTransp(matrixS), matrixD);
        }

        private void FillMatrixsSAndD()
        {
            for (int i = 0; i < equatSystem.Size; i++)
            {
                double curDiagEl = equatSystem.matrix[i, i];
                for (int k = 0; k < i; k++)
                {
                    curDiagEl -= matrixS[k, i] * matrixS[k, i] * matrixD[k, k];
                }
                matrixD[i, i] = Math.Sign(curDiagEl);
                matrixS[i, i] = Math.Sqrt(curDiagEl);

                for (int j = i + 1; j < equatSystem.Size; j++)
                {
                    double substractSum = 0;
                    for (int p = 0; p < i; p++)
                    {
                        substractSum += matrixS[p, i] * matrixD[p, p] * matrixS[p, j];
                    }
                    matrixS[i, j] = (equatSystem.matrix[i, j] - substractSum) / (matrixD[i, i] * matrixS[i, i]);
                }
            }
        }

        private double[] GetIntermediateResultY()
        {
            double[] y = new double[equatSystem.Size];
            for (int i = 0; i < equatSystem.Size; i++)
            {
                y[i] = equatSystem.constants[i];
                for (int k = 0; k < i - 1; k++)
                {
                    y[i] -= stdMatrix[i, k] * y[k];
                }
                y[i] /= stdMatrix[i, i];
            }
            return y;
        }

        public double[] GetResolution()
        {
            double[] x = new double[equatSystem.Size];
            double[] y = GetIntermediateResultY();

            for (int i = equatSystem.Size - 1; i >= 0; i--)
            {
                x[i] = y[i];
                for (int k = equatSystem.Size - 1; k > i; k--)
                {
                    x[i] -= x[k] * matrixS[i, k];
                }
                x[i] /= matrixS[i, i];
            }
            return x;
        }

        public double GetDeterminant()
        {
            double determinant = 1;
            for(int i = 0; i < equatSystem.Size; i++)
            {
                determinant *= matrixD[i, i] * matrixS[i, i] * matrixS[i, i];
            }
            return determinant;
        }

        private double[,] GetIntermediateMatrixY()
        {
            double[,] matrixY = new double[equatSystem.Size, equatSystem.Size];

            for (int i = 0; i < equatSystem.Size; i++)
            {
                for (int k = i; k < equatSystem.Size; k++)
                {
                    if (i == k)
                    {
                        matrixY[i, i] = 1.0 / stdMatrix[i, i];
                    }
                    else
                    {
                        double chis = 0;
                        for (int p = i; p < k; p++)
                        {
                            chis -= stdMatrix[k, p] * matrixY[p, i];
                        }
                        matrixY[k, i] = chis / stdMatrix[i, i];
                    }
                }
            }
            return matrixY;
        }

        public double[,] GetInverseMatrix()
        {
            double[,] inverseMatrix = new double[equatSystem.Size, equatSystem.Size];
            double[,] matrixY = GetIntermediateMatrixY();

            for (int i = 0; i < equatSystem.Size; i++)
            {
                for(int k = equatSystem.Size - 1; k >= 0; k--)
                {
                    double chis = matrixY[k, i];
                    for(int p = equatSystem.Size - 1; p > k; p--)
                    {
                        chis -= inverseMatrix[p, i] * matrixS[k, p];
                    }
                    inverseMatrix[k,i] = chis / matrixS[k, k];
                }
            }
            return inverseMatrix;
        }
    }
}
