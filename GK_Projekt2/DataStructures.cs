using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt2
{
	
	public class Mesh
	{
		public List<Traingle> triangles;

		public Mesh(List<Traingle> triangles)
		{
			this.triangles = triangles;
		}
		public Mesh() { }
			
	}

	public class Traingle
	{
		public Vertex v1;
		public Vertex v2;
		public Vertex v3;

		public Traingle(Vertex v1, Vertex v2, Vertex v3)
		{
			this.v1 = v1;
			this.v2 = v2;
			this.v3 = v3;
		}
	}


	public class Vertex
	{
		public Vector3 positionBeforeRotation;
		public Vector3 tangentPUBeforeRotation;
		public Vector3 tangentPVBeforeRotation;
		public Vector3 NBeforeRotation;

		public Vector3 positionAfterRotation;
		public Vector3 tangentPUAfterRotation;
		public Vector3 tangentPVAfterRotation;
		public Vector3 NAfterRotation;

		public float u;
		public float v;

		public Vertex(Vector3 positionBeforeRotation, Vector3 tangentPUBeforeRotation, Vector3 tangentPVBeforeRotation, Vector3 NBeforeRotation, float u, float v)
		{
			this.positionBeforeRotation = positionBeforeRotation;
			this.tangentPUBeforeRotation = tangentPUBeforeRotation;
			this.tangentPVBeforeRotation = tangentPVBeforeRotation;
			this.NBeforeRotation = NBeforeRotation;
			this.u = u;
			this.v = v;
		}

		public void SetCoordinatesAfterRotation(Vector3 positionAfterRotation, Vector3 tangentPUAfterRotation, Vector3 tangentPVAfterRotation, Vector3 NAfterRotation)
		{
			this.positionAfterRotation = positionAfterRotation;
			this.tangentPUAfterRotation = tangentPUAfterRotation;
			this.tangentPVAfterRotation = tangentPVAfterRotation;
			this.NAfterRotation = NAfterRotation;
		}
	}
}
