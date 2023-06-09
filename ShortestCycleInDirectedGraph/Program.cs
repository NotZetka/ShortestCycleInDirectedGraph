﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyApp
{
    internal class Program
    {
        public static void ShortestCycle(List<List<int>> successorsList)
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
            void helper(List<int> successors, bool[] visited, List<int> cycle)
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
            else
            {
                Console.WriteLine("Brak cyklu w grafie");
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
            sb.Append("->");
            sb.Append(cycle[0]);
            return sb.ToString();
        }
        
        public static void MainMenu()
        {
            Console.WriteLine("Wybierz opcję, wpisując odpowiadający jej numer: \n 1. Stwórz nowy graf \n 2. Załaduj przykładowy graf \n 3. Instrukcja \n 4. Wyjdź z programu ");
            var _continue = true;
            while (_continue)
            {
                var s = Convert.ToInt32(Console.ReadKey(true).KeyChar) - 48;
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
                        Console.Clear();
                        Manual();
                        break;
                    case 4:
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
            List<List<int>> successorsList2 = new()
            {
                new () { 1 },
                new () { },
                new () { 1, 3 },
                new () { 4 },
                new () { 0, 2 }
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
            try
            {
                var successorsList = new List<List<int>>();
                Console.Write("Podaj liczbę wierzchołków: ");
                int size = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < size; i++)
                {
                    Console.Write("Podaj numery następców wierzchołka " + i + " (oddzielone spacją): ");
                    String input = Console.ReadLine();
                    while (input.Contains(Convert.ToString(i)))
                    {
                        Console.WriteLine("Nie można tworzyć pętli, podaj wierzchołki ponownie.");
                        Console.Write("Podaj numery następców wierzchołka " + i + " (oddzielone spacją): ");
                        input = Console.ReadLine();
                    }

                    var neighbours = input == "" ? new List<int>() : input.Split(" ").Select(x => Convert.ToInt32(x)).ToList();
                    var didExceedLimit = neighbours.Where(x => x >= size).Count();
                    while (didExceedLimit > 0)
                    {
                        Console.WriteLine("Podano nie istniejący wierzchołek. Spróbuj ponownie.");
                        input = Console.ReadLine();
                        neighbours = input == "" ? new List<int>() : input.Split(" ").Select(x => Convert.ToInt32(x)).ToList();
                        didExceedLimit = neighbours.Where(x => x >= size).Count();
                    }

                    successorsList.Add(neighbours);
                }

                ShortestCycle(successorsList);
                Console.WriteLine("\nNaciśnij dowolny przycisk, aby wrócić do menu...");
                Console.ReadKey();
                Console.Clear();
                MainMenu();
            }
            catch(Exception e)
            {
                Console.Clear();
                Console.WriteLine("Błędne dane. Spróbuj jeszcze raz.");
                Console.WriteLine("\nNaciśnij dowolny przycisk...");
                Console.ReadKey();
                Console.Clear();
                NewGraph();
            }
        }

        public static void Manual()
        {
            Console.WriteLine("Instrukcja obsługi");
            Console.WriteLine("Tworzenie grafu");
            Console.WriteLine(" - Podajemy ilość wierzchołków w grafie");
            Console.WriteLine(" - Podajemy dla każdego wierzchołka indeksy wierzchołków z którymi chcemy stworzyć krawędź");
            Console.WriteLine(" - Sąsiadów wierzchołka podajemy kolejno po spacji");
            Console.WriteLine("Uwagi");
            Console.WriteLine(" - Nie można tworzyć pętli (krawędź sama z sobą)");
            Console.WriteLine(" - Wierzchołki indeksujemy od zera");
            Console.WriteLine(" - Ostatni wierzchołek ma zawsze indeks (ilość wierzchołków - 1)");
            Console.WriteLine(" - Jeżeli wierzchołek nie ma krawędzi nic nie podajemy wciskamy ENTER");
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