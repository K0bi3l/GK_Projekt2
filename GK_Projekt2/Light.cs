using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt2
{

	/// <summary>
	/// do poprawy to, żeby światło nie cofało się do początkowej pozycji
	/// </summary>
	public class Light
	{
		public Vector3 L;
		public LightMover lightMover;

		public Light()
		{
			L = new Vector3((float)(Math.Sqrt(0.375)), (float)(Math.Sqrt(0.375)), 0.5f);
			lightMover = new LightMover(1, 1, L.Z, 0.01f);
		}

		public void MoveLight(float z)
		{
			L = lightMover.MovePosition(z);			 
		}

		public void UpdatePosition()
		{
			L = lightMover.UpdatePosition().direction;
		}
		
		public double CalculateLightIntensity(float ll, float l0, Vector3 N)
		{
			Vector3 V = new Vector3(0, 0, 1);
			Vector3 R = Vector3.Normalize(2 * N * Vector3.Dot(N, L) - L);
			N = Vector3.Normalize(N);
			double ret = Form1.kd * Form1.ks * ll *l0 * (Math.Max(Vector3.Dot(N, L), 0) + Form1.ks*ll* l0 * Math.Pow(Math.Max(Vector3.Dot(R, V), 0), Form1.m));
			if (ret >= 0 && ret <= 1) return ret;
			else if(ret < 0) return 0;
			else return 1;
		}
	}

	public class LightMover
	{
		private float angle = 0; // Kąt startowy (w radianach)
		private float speed; // Prędkość kątowa
		private float a, b; // Półosie elipsy
		private float z; // Stała wysokość (lub oscylacyjna funkcja, jeśli wymagana)

		public Vector3 L = new Vector3((float)(Math.Sqrt(0.375)), (float)(Math.Sqrt(0.375)), 0.5f);

		public LightMover(float a, float b, float z, float speed)
		{
			this.a = a;
			this.b = b;
			this.z = z;
			this.speed = speed;
		}
		public Vector3 MovePosition(float z)
		{
			float scale = MathF.Sqrt((1 - z * z) / (L.X * L.X + L.Y * L.Y));

			// Nowe wartości X i Y
			float newX = a * scale;
			float newY = b * scale;

			// Zwracamy nowy, zaktualizowany wektor normalny
			L = new Vector3(newX, newY, z);
			return new Vector3(newX, newY, z);
		}
		public (Vector3 position, Vector3 direction) UpdatePosition()
		{
			// Aktualizacja kąta
			angle += speed;

			// Obliczenie pozycji na elipsie
			float x = L.X * MathF.Cos(speed);
			float y = L.Y * MathF.Sin(speed);
			Vector3 position = new Vector3(x, y, z);

			// Obliczenie stycznej do elipsy
			float dx = -a * MathF.Sin(angle);
			float dy = b * MathF.Cos(angle);
			Vector3 tangent = new Vector3(dx, dy, 0);

			// Wektor kierunku prostopadły do stycznej (normalny do trajektorii)
			Vector3 direction = Vector3.Normalize(Vector3.Cross(tangent, Vector3.UnitZ));

			return (position, direction);
		}
	}
}
