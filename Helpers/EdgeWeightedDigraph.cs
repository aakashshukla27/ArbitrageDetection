using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbitrageDetection.Helpers
{
    class EdgeWeightedDigraph
    {
        private readonly int V; // number of vertices
        private int E; // number of edges
        private LinkedList<DirectedEdge>[] adj; // adjacency list

        public EdgeWeightedDigraph(int v)
        {
            V = v;
            E = 0;
            adj = new LinkedList<DirectedEdge>[v];
            for(int i=0; i<v; i++)
            {
                adj[i] = new LinkedList<DirectedEdge>();
            }
        }

        public int Vertex() => V;
        public int Edge() => E;

        public void AddEdge(DirectedEdge e)
        {
            adj[e.From()].AddLast(e);
            E++;
        }

        public LinkedList<DirectedEdge> AdjacencyList(int v) => adj[v];

        public LinkedList<DirectedEdge> Edges()
        {
            LinkedList<DirectedEdge> list = new LinkedList<DirectedEdge>();
            for (int i = 0; i < V; i++)
            {
                foreach (DirectedEdge e in AdjacencyList(i))
                {
                    list.AddLast(e);
                }
            }
            return list;
        }

    }
}
