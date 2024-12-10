using System.Reflection;

namespace AdventOfCodeCore;

public class SolverFactory
{
    public static ISolver Create(int year, int day)
    {
        Type? solverType = Assembly.GetCallingAssembly().GetType($"AdventOfCode{year}.Day{day}.Solver");
        if (solverType == null)
        {
            throw new NotImplementedException();
        }

        ISolver? solver = solverType.InvokeMember("", BindingFlags.CreateInstance, null, null, [day]) as ISolver;
        if (solver == null)
        {
            throw new InvalidOperationException();
        }
        return solver;
    }
}