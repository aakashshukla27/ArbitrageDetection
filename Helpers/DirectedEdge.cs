using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbitrageDetection.Helpers
{
    class DirectedEdge
    {
        private readonly int v;
        private readonly int w;
        private readonly double weight;

        public DirectedEdge(int v, int w, double weight)
        {
            this.v = v;
            this.w = w;
            this.weight = weight;
        }

        public double Weight() => weight;
        public int From() => v;
        public int To() => w;

        public string EdgeToString() => ("%d-%d %.2f", v, w, weight).ToString();
    }
}
