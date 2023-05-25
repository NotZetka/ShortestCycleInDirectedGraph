using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyApp
{
    internal class Program
    {
        public static void ShortestCycle(List<int[]> successorsList)
        {
            string result = null;
            int shortest = int.MaxValue;
            for (int i = 0; i < successorsList.Count; i++)
            {
                bool[] visited = new bool[successorsList.Count];
                visited[i] = true;
                List<int> cycle = new() { i };
                helper(successorsList[i], visited, cycle);
            }
            void helper(int[] successors, bool[] visited, List<int> cycle)
            {
                foreach (var successor in successors)
                {
                    if (visited[successor])
                    {
                        if (cycle.Count < shortest)
                        {
                            shortest = cycle.Count;
                            result = CycleToString(cycle);
                        }
                    }
                    else
                    {
                        cycle.Add(successor);
                        visited[successor] = true;
                        helper(successorsList[successor], visited, cycle);
                        cycle.Remove(successor);
                        visited[successor] = false;
                    }
                }
            }
            if(result != null)
            {
                Console.WriteLine(shortest);
                Console.WriteLine(result);
            }
        }
        public static string CycleToString(List<int> cycle)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(cycle[0]);
            for (int i = 1; i < cycle.Count; i++)
            {
                sb.Append("->");
                sb.Append(cycle[i]);
            }
            return sb.ToString();
        }
        static void Main(string[] args)
        {
            Console.Write("Witaj w programie znajdowania najkrótszego cyklu w grafie skierowanym!");
            List<int[]> successorsList = new List<int[]>();
            Console.Write("Podaj liczbe wierzcholkow: ");
            int size = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < size; i++)
            {
                Console.Write("Podaj numery wierzcholkow (oddzielone spacja) sasiadow wierzcholka " + i + ": ");
                String input = Console.ReadLine();

                int[] neighbours = string.IsNullOrWhiteSpace(input)
                    ? Array.Empty<int>()
                    : input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(int.Parse)
                           .Where(num => num < size)
                           .ToArray();

                successorsList.Add(neighbours);
            }
            
            List<int[]> successorsList2 = new()
            {
                new int[] { 1 },
                new int[] { },
                new int[] { 1, 3 },
                new int[] { 4 },
                new int[] { 0, 2 }
            };

            ShortestCycle(successorsList);
            Console.ReadKey();
        }
    }
}