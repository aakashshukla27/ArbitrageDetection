using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbitrageDetection.Helpers
{
    class BellmanFordSP
    {
        private double[] distTo;
        private DirectedEdge[] edgeTo;
        private bool[] onQ;
        private Queue<int> queue;
        private int cost;
        private Stack<int> cycle;

        public BellmanFordSP(EdgeWeightedDigraph G, int s)
        {
            distTo = new double[G.Vertex()];
            edgeTo = new DirectedEdge[G.Vertex()];
            onQ = new bool[G.Vertex()];
            queue = new Queue<int>();
            for (int v = 0; v < G.Vertex(); v++)
                distTo[v] = Double.PositiveInfinity;
            distTo[s] = 0.0;
            queue.Enqueue(s);
            onQ[s] = true;
            while (!queue.Any() && !this.hasNegativeCycle())
            {
                int v = queue.Dequeue();
                onQ[v] = false;
                Relax(G, v);
            }
        }

        private void Relax(EdgeWeightedDigraph G, int v)
        {
            foreach (DirectedEdge e in G.AdjacencyList(v))
            {
                int w = e.To();
                if (distTo[w] > distTo[v] + e.Weight())
                {
                    distTo[w] = distTo[v] + e.Weight();
                    edgeTo[w] = e;
                    if (!onQ[w])
                    {
                        queue.Enqueue(w);
                        onQ[w] = true;
                    }
                }
                if (cost++ % G.Vertex() == 0)
                {
                    FindNegativeCycle();
                }
            }
        }

        private void FindNegativeCycle()
        {
            int V = edgeTo.Length;
            EdgeWeightedDigraph spt = new EdgeWeightedDigraph(V);
            for (int v = 0; v < V; v++)
                if (edgeTo[v] != null)
                    spt.AddEdge(edgeTo[v]);
            DirectedCycle cf = new DirectedCycle(spt);
            cycle = cf.Cycle();
        }
        public bool hasNegativeCycle() => cycle != null;
        public Stack<int> negativeCycle() => cycle;

        public double DistTo(int v) => distTo[v];

        public bool hasPathTo(int v) => distTo[v] < double.PositiveInfinity;
        public Stack<DirectedEdge> PathTo(int v)
        {
            if (!hasPathTo(v)) return null;
            Stack<DirectedEdge> path = new Stack<DirectedEdge>();
            for (DirectedEdge e = edgeTo[v]; e != null; e = edgeTo[e.From()])
                path.Push(e);
            return path;
        }
    }
}
