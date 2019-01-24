using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BestPath
{
    class Program
    {
        static void Main(string[] args)
        {
            // Triangle will contain all nodes of given triangle.
            List<List<Node>> triangle = new List<List<Node>>();

            // All nodes are read using triangle txt file and filled into triangle object
            string filePath = Directory.GetCurrentDirectory() + "//" + "triangle.txt";
            var lines = File.ReadLines(filePath);
            foreach (var line in lines)
            {
                triangle.Add(CreateNodeList(line));
            }
            
            triangle = UpdateNodeWeights(triangle);
            FindBestPath(triangle);
            Console.ReadLine();
        }

        // Calculates weights of all nodes of triangle according to their position
        // The weight of nodes corresponds the sum of values of nodes under them
        static List<List<Node>> UpdateNodeWeights(List<List<Node>> triangle)
        {
            for (int i = triangle.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < triangle[i].Count - 1; j++)
                {
                    var leftSubNodeWeight = triangle[i][j].Weight;
                    var rightSubNodeWeight = triangle[i][j + 1].Weight;
                    triangle[i - 1][j].Weight += (leftSubNodeWeight > rightSubNodeWeight ? leftSubNodeWeight : rightSubNodeWeight);
                }
            }

            return triangle;
        }

        // Finds the best path of triangle according to weight of nodes
        // And writes to console
        static void FindBestPath(List<List<Node>> triangle)
        {
            int sum = 0;
            int j = 0;
            Console.WriteLine("Best way of maximum sum:");
            Console.Write(triangle[0][0].Value.ToString() + "->");
            sum += triangle[0][0].Value;

            for (int i = 0; i < triangle.Count - 1; i++)
            {
                var subNode1 = triangle[i + 1][j];
                var subNode2 = triangle[i + 1][j + 1];

                if (subNode1.Weight > subNode2.Weight)
                {
                    Console.Write(subNode1.Value + "->");
                    sum += subNode1.Value;
                }
                else
                {
                    Console.Write(subNode2.Value + "->");
                    sum += subNode2.Value;
                    j++;
                }
            }
            Console.WriteLine("Maximum Sum:" + sum);
        }
        

        // Splits given line of triangle.txt by empty character and fills nodes to node list 
        static List<Node> CreateNodeList(string nodes)
        {
            var points = nodes.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            List<Node> nodeList = new List<Node>();
            foreach (var point in points)
            {
                nodeList.Add(new Node { Value = Convert.ToInt32(point), Weight = Convert.ToInt32(point) });
            }

            return nodeList;
        }

        // Model class of the node
        public class Node
        {
            public int Value { get; set; }

            public int Weight { get; set; }
        }
    }
}
