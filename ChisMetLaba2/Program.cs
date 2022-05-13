using ChisMetLaba2;

int n = Convert.ToInt32(Console.ReadLine());
EquationsSystem equatSystem = new EquationsSystem(n);

equatSystem.PrintEquations();

SquareRootMethod sqrootMeth = new SquareRootMethod(equatSystem);
double[] result = sqrootMeth.GetResolution();

Console.WriteLine("Square root method resolution: ");
for(int i = 0; i < n; i++)
{
    Console.WriteLine($"x{i + 1} = " + result[i]);
}

Console.WriteLine("\n\nNevyazok: \n");
double[] nevyazok = equatSystem.GetNevyazok(result);
for (int i = 0; i < n; i++)
{
    Console.WriteLine($"x{i + 1} = " + nevyazok[i]);
}

Console.WriteLine("\n\nDeterminant: \n");
Console.WriteLine(sqrootMeth.GetDeterminant());

Console.WriteLine("\nInverse matrix: \n");
MatrixOperations.PrintMatrix(sqrootMeth.GetInverseMatrix());

try
{
    YakobiMethod yakMeth = new YakobiMethod(equatSystem);

    double[] result1 = yakMeth.GetResolution(0.0001);

    Console.WriteLine("\n\nYakobi method resolution: \n");
    for (int i = 0; i < n; i++)
    {
        Console.WriteLine($"x{i + 1} = " + result1[i]);
    }

    Console.WriteLine("\nCount of iterations: " + yakMeth.Xiterations.Count());

    Console.WriteLine("\nNevyazok: \n");
    double[] nevyazok1 = equatSystem.GetNevyazok(result1);
    for (int i = 0; i < n; i++)
    {
        Console.WriteLine($"x{i + 1} = " + nevyazok1[i]);
    }
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
