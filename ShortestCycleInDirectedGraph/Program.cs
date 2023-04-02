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
            List<int[]> successorsList = new()
            {
                new int[] { 1 },
                new int[] { },
                new int[] { 1, 3 },
                new int[] { 4 },
                new int[] { 0, 2 }
            };
            ShortestCycle(successorsList);
        }
    }
}