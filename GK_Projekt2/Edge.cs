using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt2
{
	public class Edge
	{	
		public float yMax;
		public float xMin;
		public float step;
		public Edge next;

		public Edge(float yMax, float xMin, float step = 1)
		{			
			this.yMax = yMax;
			this.xMin = xMin;
			this.step = step;
		}
	}
}
