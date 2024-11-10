using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt2
{
	public class Bernstein
	{

		public float GetBernsteinBaseFunction(int i, int n, float t)
		{
			return (float)(Factorial(n) / (Factorial(i) * Factorial(n - i)) * Math.Pow(t, i) * Math.Pow(1 - t, n - i));
		}
		private int Factorial(int n)
		{
			if (n == 0) return 1;
			else return n * Factorial(n - 1);
		}
	}

	public class PointPositionCalculator
	{
		Vector3[,] controlPoints;
		int n;
		int m;
		

		public PointPositionCalculator(Vector3[,] controlPoints, int n = 3, int m = 3)
		{
			this.controlPoints = controlPoints;
			this.n = n;
			this.m = m;
		}
		public Vector3 CalculatePointPosition(float u,float v)
		{
			Bernstein bernstein = new Bernstein();
			Vector3 result = new Vector3();
			for(int i = 0; i <= n; i++) 
			{
				for(int j = 0; j <= m; j++)
				{
					result += controlPoints[i,j] * bernstein.GetBernsteinBaseFunction(i,n, u) * bernstein.GetBernsteinBaseFunction(j, m, v);
				}
			}
			return result;
		}

		public Vector3 GetUNormalVector(float u, float v)
		{
			Bernstein bernstein = new Bernstein();
			Vector3 result = new Vector3();
			for (int i = 0; i <= n - 1; i++)
			{
				for (int j = 0; j <= m; j++)
				{
					result += (controlPoints[i + 1, j] - controlPoints[i, j]) * bernstein.GetBernsteinBaseFunction(i, n - 1, u)
						* bernstein.GetBernsteinBaseFunction(j, m, v);	
				}
			}
			return n * result;
		}

		public Vector3 GetVNormalVector(float u, float v)
		{
			Bernstein bernstein = new Bernstein();
			Vector3 result = new Vector3();
			for (int i = 0; i <= n; i++)
			{
				for (int j = 0; j <= m - 1; j++)
				{
					result += (controlPoints[i, j + 1] - controlPoints[i, j]) * bernstein.GetBernsteinBaseFunction(i, n, u)
						* bernstein.GetBernsteinBaseFunction(j, m - 1, v);
				}
			}
			return m * result;
		}

		public Vector3 GetNormalVector(float u, float v)
		{
			return Vector3.Cross(GetUNormalVector(u, v), GetVNormalVector(u, v));
		}
	}

	public static class Rotations 
	{
		
		public static float[,] GetZRotation(float phi)
		{
			float[,] result = new float[3, 3];
			result[0, 0] = (float)Math.Cos(phi);
			result[0, 1] = (float)-Math.Sin(phi);
			result[0, 2] = result[1,2] = result[2,0] = result[2,1] = 0;
			result[1, 0] = (float)Math.Sin(phi);
			result[1, 1] = (float)Math.Cos(phi);
			result[2, 2] = 1;
			return result;
		}

		public static float[,] GetXRotation(float phi)
		{
			float[,] result = new float[3, 3];
			result[0, 0] = 1;
			result[0,1] = result[0,2] = result[1,0] = result[2,0] = 0;
			result[1, 1] = (float)Math.Cos(phi);
			result[1, 2] = (float)-Math.Sin(phi);
			result[2, 1] = (float)Math.Sin(phi);
			result[2, 2] = (float)Math.Cos(phi);
			return result;
		}
	}

    public static class RotationsCalculator
    {
		public static Vector3 Rotate(Vector3 vector, float[,] rotation)
		{
			return new Vector3(vector.X * rotation[0, 0] + vector.Y * rotation[0, 1] + vector.Z * rotation[0, 2],
								vector.X * rotation[1, 0] + vector.Y * rotation[1, 1] + vector.Z * rotation[1, 2],
								vector.X * rotation[2, 0] + vector.Y * rotation[2, 1] + vector.Z * rotation[2, 2]);
		}

		public static Vector3 Rotate(Vector3 vector, float[,] ZRotation, float[,] XRotation)
		{
			return Rotate(Rotate(vector, XRotation), ZRotation);
		}
    }
}
