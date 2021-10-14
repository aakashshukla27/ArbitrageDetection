using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbitrageDetection.Helpers
{
    class DirectedCycle
    {
        private bool[] marked;
        private int[] edgeTo;
        private Stack<int> cycle;
        private bool[] onStack;

        public DirectedCycle(EdgeWeightedDigraph G)
        {
            onStack = new bool[G.Vertex()];
            edgeTo = new int[G.Vertex()];
            marked = new bool[G.Vertex()];

            for (int i = 0; i < G.Vertex(); i++)
            {
                if (!marked[i])
                {
                    DFS(G, i);
                }
            }
        }

        private void DFS(EdgeWeightedDigraph G, int v)
        {
            onStack[v] = true;
            marked[v] = true;
            foreach (DirectedEdge e in G.AdjacencyList(v))
            {
                int w = e.To();
                if (this.hasCycle()) return;
                else if (!marked[w])
                {
                    edgeTo[w] = v;
                    DFS(G, w);
                }
                else if (onStack[w])
                {
                    cycle = new Stack<int>();
                    for (int i = v; i != w; i = edgeTo[i])
                    {
                        cycle.Push(i);
                    }
                    cycle.Push(w);
                    cycle.Push(v);
                }
            }
            onStack[v] = false;
        }

        public bool hasCycle() => cycle != null;
        public Stack<int> Cycle() => cycle;
    }
}
