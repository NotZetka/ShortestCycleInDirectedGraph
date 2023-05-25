using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

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
                Console.WriteLine("Najkórtszy cykl ma długość: "+shortest);
                Console.WriteLine("Kolejne wierzchołki w cyklu: "+result);
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
        
        public static void MainMenu()
        {
            Console.WriteLine("Wybierz opcję, wpisując odpowiadający jej numer: \n 1. Stwórz nowy graf \n 2. Załaduj przykładowy graf \n 3. Wyjdź z programu ");
            var s = Convert.ToInt32(Console.ReadKey(true).KeyChar)-48;
            var _continue = true;
            while (_continue)
            {
                _continue = false;
                switch (s)
                {
                    case 1:
                        Console.Clear();
                        NewGraph();
                        break;
                    case 2:
                        Console.Clear();
                        Example();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        _continue = true;
                        Console.WriteLine("Wybierz poprawny numer opcji.");
                        break;
                }
            }
        }

        public static void Example()
        {
            List<int[]> successorsList2 = new()
            {
                new int[] { 1 },
                new int[] { },
                new int[] { 1, 3 },
                new int[] { 4 },
                new int[] { 0, 2 }
            };

            Console.WriteLine("\nPrzykładowy graf: \n Liczba wierzchołków: 5");
            
            for (int i = 0; i < successorsList2.Count; i++)
            {
                Console.Write(" Następcy wierzchołka " + i + ":");
                foreach(int element in successorsList2[i])
                {
                    Console.Write(" "+element);
                }
                Console.WriteLine();
            }
            ShortestCycle(successorsList2);
            Console.WriteLine("\nNaciśnij dowolny przycisk, aby wrócić do menu...");
            Console.ReadKey();
            Console.Clear();
            MainMenu();
        }

        public static void NewGraph()
        {
            List<int[]> successorsList = new List<int[]>();
            Console.WriteLine("Podaj liczbę wierzchołków: ");
            int size = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < size; i++)
            {
                Console.Write("Podaj numery następców wierzchołka " + i + " (oddzielone spacją): ");
                String input = Console.ReadLine();

                int[] neighbours = string.IsNullOrWhiteSpace(input)
                    ? Array.Empty<int>()
                    : input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(int.Parse)
                           .Where(num => num < size)
                           .ToArray();

                successorsList.Add(neighbours);
            }

            ShortestCycle(successorsList);
            Console.WriteLine("\nNaciśnij dowolny przycisk, aby wrócić do menu...");
            Console.ReadKey();
            Console.Clear();
            MainMenu();

        }
        static void Main(string[] args)
        {
            ConsoleHelper.SetCurrentFont("Consolas", 25);
            Console.WriteLine("Witaj w programie znajdowania najkrótszego cyklu w grafie skierowanym!");
            MainMenu();
            Console.ReadKey();
        }
    }
}