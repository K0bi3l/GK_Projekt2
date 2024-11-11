using FastBitmapLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GK_Projekt2
{
	
	public class Mesh
	{
		public List<Triangle> triangles;
		public Light l;

		public Mesh(List<Triangle> triangles)
		{
			this.triangles = triangles;
			l = new Light();
		}
		public Mesh() 
		{
			triangles = new List<Triangle>();
			l = new Light();
		}

		public void Rotate(float[,] XRotation, float[,] ZRotation)
		{
			foreach(var t in triangles)
			{
				t.Rotate(XRotation,ZRotation);
			}
		}

		public void Fill()
		{
			foreach(var t in triangles)
			{
				t.Fill(l);
			}
		}
			
	}

	public class Triangle
	{
		public Vertex v1;
		public Vertex v2;
		public Vertex v3;
	

		public Triangle(Vertex v1, Vertex v2, Vertex v3)
		{
			this.v1 = v1;
			this.v2 = v2;
			this.v3 = v3;
			
		}

		public void DrawTriangle(Graphics g,Pen pen)
		{
			//do usuniecia
			Vector3 v1 = this.v1.positionAfterRotation == Form1.controlVector ? this.v1.positionBeforeRotation : this.v1.positionAfterRotation;
			Vector3 v2 = this.v2.positionAfterRotation == Form1.controlVector ? this.v2.positionBeforeRotation : this.v2.positionAfterRotation;
			Vector3 v3 = this.v3.positionAfterRotation == Form1.controlVector ? this.v3.positionBeforeRotation : this.v3.positionAfterRotation;

			g.DrawLine(pen, new Point((int)v1.X,(int)v1.Y), new Point((int)v2.X,(int)v2.Y));
			g.DrawLine(pen, new Point((int)v2.X,(int)v2.Y), new Point((int)v3.X,(int)v3.Y));
			g.DrawLine(pen, new Point((int)v3.X,(int)v3.Y), new Point((int)v1.X,(int)v1.Y));
		}

		public void Rotate(float[,] XRotation, float[,] ZRotation)
		{
			v1.SetCoordinatesAfterRotation(RotationsCalculator.Rotate(v1.positionBeforeRotation, XRotation, ZRotation),
											RotationsCalculator.Rotate(v1.tangentPUBeforeRotation, XRotation, ZRotation),
											RotationsCalculator.Rotate(v1.tangentPVBeforeRotation, XRotation, ZRotation),
											RotationsCalculator.Rotate(v1.NBeforeRotation, XRotation, ZRotation));
			v2.SetCoordinatesAfterRotation(RotationsCalculator.Rotate(v2.positionBeforeRotation, XRotation, ZRotation),
											RotationsCalculator.Rotate(v2.tangentPUBeforeRotation, XRotation, ZRotation),
											RotationsCalculator.Rotate(v2.tangentPVBeforeRotation, XRotation, ZRotation),
											RotationsCalculator.Rotate(v2.NBeforeRotation, XRotation, ZRotation));
			v3.SetCoordinatesAfterRotation(RotationsCalculator.Rotate(v3.positionBeforeRotation, XRotation, ZRotation),
											RotationsCalculator.Rotate(v3.tangentPUBeforeRotation, XRotation, ZRotation),
											RotationsCalculator.Rotate(v3.tangentPVBeforeRotation, XRotation, ZRotation),
											RotationsCalculator.Rotate(v3.NBeforeRotation, XRotation, ZRotation));
		}

		public void Fill(Light l)
		{

			Graphics g = Graphics.FromImage(Form1.bitmap);
			PictureBox pictureBox = Form1.pictureBox;
			g.TranslateTransform(pictureBox.Width / 2, pictureBox.Height / 2);
			//Light l = new Light();
			// Znajdź minimalne i maksymalne wartości Y dla trójkąta
			int yMax = (int)Math.Max(Math.Round(v1.positionAfterRotation.Y), Math.Max(Math.Round(v2.positionAfterRotation.Y), Math.Round(v3.positionAfterRotation.Y)));
			int yMin = (int)Math.Min(Math.Round(v1.positionAfterRotation.Y), Math.Min(Math.Round(v2.positionAfterRotation.Y), Math.Round(v3.positionAfterRotation.Y)));

			// Utworzenie tablicy ET (Edge Table)
			Edge[] ET = new Edge[yMax - yMin + 1];

			int off = pictureBox.Width;
			int off2 = pictureBox.Height;
			int xOffset = pictureBox.Width / 2;
			int yOffset = pictureBox.Height / 2; // dziwne to odejmowanie

			// Dodanie krawędzi do ET
			Sort(v1.positionAfterRotation, v2.positionAfterRotation, ET, yMin);
			Sort(v2.positionAfterRotation, v3.positionAfterRotation, ET, yMin);
			Sort(v3.positionAfterRotation, v1.positionAfterRotation, ET, yMin);

			// Tworzenie AET (Active Edge Table)
			List<Edge> AET = new List<Edge>();
			int startY = yMin;

			for(int i = 0; i < ET.Length; i++)
			{
				if (ET[i] != null)
				{
					startY = i + yMin;
					break;
				}
			}

			int ETItemCount = 0;

			for(int i = 0; i < ET.Length; i++)
			{
				if (ET[i] != null)
				{
					int inc = 1;
					Edge e = ET[i];
					while (e.next != null)
					{
						inc++;
						e = e.next;						
					}
					ETItemCount += inc;
				}
			}
			int y = startY;
			while(AET.Count > 0 || ETItemCount > 0)
			{
				Edge e = ET[y - yMin];
				if (e != null)
				{
					while (e != null)
					{ 
						AET.Add(e);
						ETItemCount--;
						e = e.next;
					}
					ET[y - yMin] = null;
				}
				AET = AET.OrderBy(e => e.xMin).ToList();
				

				int xStart = (int)Math.Round(AET[0].xMin);
				int xEnd = (int)Math.Round(AET[1].xMin);

				using (FastBitmap fb = Form1.bitmap.FastLock())
				{
					for (int i = xStart; i <= xEnd; i++)
					{
						if (i + xOffset >= 0 && i + xOffset < pictureBox.Width &&
							y + yOffset >= 0 && y + yOffset < pictureBox.Height)
						{
							// Interpolacja wektora normalnego i współrzędnej Z
							(Vector3 interpolatedNormal, float interpolatedZ) = Interpolate(new Vector3(i, y, 0));
							interpolatedNormal = Vector3.Normalize(interpolatedNormal);
							Color col = Form1.MeshColor;
							float lo = col.R / 255.0f;
							int ll = Form1.LightColor.R / 255;							
							int R = Math.Min((int)(l.CalculateLightIntensity(lo, ll, interpolatedNormal) * 255), 255);

							lo = col.G / 255;
							ll = Form1.LightColor.G / 255;
							int G = Math.Min((int)(l.CalculateLightIntensity(lo, ll, interpolatedNormal) * 255), 255);

							lo = col.B / 255;
							ll = Form1.LightColor.B / 255;
							int B = Math.Min((int)(l.CalculateLightIntensity(lo, ll, interpolatedNormal) * 255), 255);

							Color color = Color.FromArgb(R, G, B);
							
							fb.SetPixel(i + xOffset, y + yOffset, color);
						}
					}
				}				
				

				AET.RemoveAll(e => e.yMax == y);

				y++;

				foreach (var edge in AET)
				{
					edge.xMin += edge.step;
				}
			}


		}

		public (Vector3 interpolatedNormal, float interpolatedZ) Interpolate(Vector3 point)
		{
			// Oblicz współczynniki barycentryczne
			(float u, float v, float w) = CalculateBarycentricCoordinates(point);

			// Interpolacja wektora normalnego i współrzędnej Z
			Vector3 interpolatedNormal = u * v1.NAfterRotation + v * v2.NAfterRotation + w * v3.NAfterRotation;
			float interpolatedZ = u * v1.positionAfterRotation.Z + v * v2.positionAfterRotation.Z + w * v3.positionAfterRotation.Z;

			return (interpolatedNormal, interpolatedZ);
		}

		public (float u, float v, float w) CalculateBarycentricCoordinates(Vector3 p)
		{
			// Oblicz wektory pomocnicze
			Vector3 v0 = this.v2.positionAfterRotation - this.v1.positionAfterRotation;
			Vector3 v1 = this.v3.positionAfterRotation - this.v1.positionAfterRotation;
			Vector3 v2 = p - this.v1.positionAfterRotation;

			// Oblicz skalarne produkty (dot products)
			float d00 = Vector3.Dot(v0, v0);
			float d01 = Vector3.Dot(v0, v1);
			float d11 = Vector3.Dot(v1, v1);
			float d20 = Vector3.Dot(v2, v0);
			float d21 = Vector3.Dot(v2, v1);

			// Oblicz współczynniki barycentryczne
			float denom = d00 * d11 - d01 * d01;
			float v = (d11 * d20 - d01 * d21) / denom;
			float w = (d00 * d21 - d01 * d20) / denom;
			float u = 1.0f - v - w;

			return (u, v, w);
		}

		public void Sort(Vector3 p1, Vector3 p2, Edge[] ET, int yMin)
		{
			if (Math.Round(p1.Y) == Math.Round(p2.Y)) return;

			int ymin = (int)Math.Min(Math.Round(p1.Y), Math.Round(p2.Y));
			int ymax = (int)Math.Max(Math.Round(p1.Y), Math.Round(p2.Y));
			int x = (int)(p1.Y < p2.Y ? p1.X : p2.X);
			//float dx = p1.X > p2.X ? (p1.X - p2.X) / (p1.Y - p2.Y) : (p2.X - p1.X) / (p2.Y - p1.Y); // chyba powinno być tak
			float dx = (p1.X - p2.X) / (p1.Y - p2.Y);
			if (ET[ymin - yMin] is null)
			{
				ET[ymin - yMin] = new Edge(ymax, x, dx);
			}
			else
			{
				Edge e = ET[ymin - yMin];
				while (e.next != null)
				{
					e = e.next;
				}
				e.next = new Edge(ymax, x, dx);
			}
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

			positionAfterRotation = Form1.CopyVector(positionBeforeRotation);
			tangentPUAfterRotation = Form1.CopyVector(tangentPUBeforeRotation);
			tangentPVAfterRotation = Form1.CopyVector(tangentPVBeforeRotation);
			NAfterRotation = Form1.CopyVector(NBeforeRotation);

			/*positionAfterRotation = Form1.controlVector;
			tangentPUAfterRotation = Form1.controlVector;
			tangentPVAfterRotation = Form1.controlVector;
			NAfterRotation = Form1.controlVector;*/


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
