using System;
using Graph.Services;

var graph = new Graph<int>();

graph.AddPeak(1);
graph.AddPeak(2);
graph.AddPeak(3);
graph.AddPeak(4);
graph.AddPeak(5);


graph.AddRids(1, 2);
graph.AddRids(1, 4);
graph.AddRids(2, 3);
graph.AddRids(4, 5);

for(int i = 1; i <= graph.Count; i++)
{
    Console.Write($"{i}:");
    foreach (var ii in graph.GetListPeaks(i))
    {
        Console.Write($"{ii.Index}\t");
    }
    Console.WriteLine();
}

Console.WriteLine(graph.GetRoad(2, 3));

var matrix = graph.GetConnectMatrix();