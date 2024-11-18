using FastBitmapLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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

		public Mesh(List<Triangle> triangles, Light l) : this(triangles)
		{
			this.l = l;
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

		public void SetClassicNormalVectors(Vector3[,] controlPoints)
		{
			foreach(var t in triangles)
			{
				t.SetClassicNormalVector(controlPoints);
			}
		}

		public void SetNormalMapVectors()
		{	
			Parallel.ForEach(triangles, t => t.SetNormalMapVector());
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
			Vector3 v1 = this.v1.positionAfterRotation;
			Vector3 v2 =  this.v2.positionAfterRotation;
			Vector3 v3 =  this.v3.positionAfterRotation;

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
			NormalMapReader reader = new NormalMapReader();
			BarycentricCoordinatesSetter setter = new BarycentricCoordinatesSetter(this);
			Graphics g = Graphics.FromImage(Form1.bitmap);
			PictureBox pictureBox = Form1.pictureBox;
			g.TranslateTransform(pictureBox.Width / 2, pictureBox.Height / 2);
			
			// Znajdź minimalne i maksymalne wartości Y dla trójkąta
			int yMax = (int)Math.Max(Math.Round(v1.positionAfterRotation.Y), Math.Max(Math.Round(v2.positionAfterRotation.Y), Math.Round(v3.positionAfterRotation.Y)));
			int yMin = (int)Math.Min(Math.Round(v1.positionAfterRotation.Y), Math.Min(Math.Round(v2.positionAfterRotation.Y), Math.Round(v3.positionAfterRotation.Y)));

			// Utworzenie tablicy ET (Edge Table)
			Edge[] ET = new Edge[yMax - yMin + 1];
			
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
				if (AET.Count == 3)
				{
					xEnd = (int)(Math.Round(AET[2].xMin));
				}


				for (int i = xStart; i <= xEnd + 1; i++)
					{
						if (i + xOffset >= 0 && i + xOffset < pictureBox.Width &&
							y + yOffset >= 0 && y + yOffset < pictureBox.Height)
						{
							// Interpolacja wektora normalnego i współrzędnej Z
							(Vector3 interpolatedNormal, float interpolatedZ) = setter.Interpolate(new Vector3(i, y, 0));
							interpolatedNormal = Vector3.Normalize(interpolatedNormal);
						(float u, float v, float w) = setter.CalculateBarycentricCoordinates(new Vector3(i, y, interpolatedZ));
						float interpolatedU = Math.Clamp(u * v1.u + v * v2.u + w * v3.u,0,1);
						float interpolatedV = Math.Clamp(u * v1.v + v * v2.v + w * v3.v,0,1);
						
						if (Form1.isNormalMap)
						{

							// Pobierz wektor normalny z mapy normalnej w oparciu o interpolowane UV
							Vector3 normalMapNormal = reader.GetNormalVector(interpolatedU, interpolatedV);
							normalMapNormal = Vector3.Normalize(normalMapNormal);

							// Interpoluj tangensy i bitangensy
							Vector3 interpolatedPU = u * v1.tangentPUBeforeRotation + v * v2.tangentPUBeforeRotation + w * v3.tangentPUBeforeRotation;
							Vector3 interpolatedPV = u * v1.tangentPVBeforeRotation + v * v2.tangentPVBeforeRotation + w * v3.tangentPVBeforeRotation;

							// Normalizuj interpolowane tangensy i bitangensy
							interpolatedPU = Vector3.Normalize(interpolatedPU);
							interpolatedPV = Vector3.Normalize(interpolatedPV);

							// Skonstruuj macierz TBN (Tangens, Bitangens, Normalny)
							Matrix4x4 TBNMatrix = new Matrix4x4(
								interpolatedPU.X, interpolatedPV.X, interpolatedNormal.X, 0,
								interpolatedPU.Y, interpolatedPV.Y, interpolatedNormal.Y, 0,
								interpolatedPU.Z, interpolatedPV.Z, interpolatedNormal.Z, 0,
								0, 0, 0, 1
							);

							// Przekształć normalny wektor z przestrzeni tangensów do przestrzeni obiektowej
							Vector3 transformedNormal = Vector3.Transform(normalMapNormal, TBNMatrix);
							transformedNormal = Vector3.Normalize(transformedNormal);

							// Ustaw wynikowy interpolowany wektor normalny
							//interpolatedNormal = transformedNormal;
							interpolatedNormal = new Vector3(transformedNormal.X, -transformedNormal.Y, transformedNormal.Z);
						}

						Color col;
						Bitmap image = Form1.imageBitmap;

						if (!Form1.isTexture)
						{
							col = Form1.MeshColor;
						}						
						else
						{
							int texX = (int)(interpolatedU * (Form1.imageBitmap.Width - 1));
							int texY = (int)(interpolatedV * (Form1.imageBitmap.Height - 1));
							col = Form1.imageBitmap.GetPixel(texX, texY );
						}
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
						using (FastBitmap fb = Form1.bitmap.FastLock())
						{
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

		public Color GetTextureColor(int x, int y)
		{
			int minX = (int)Math.Min(v1.positionAfterRotation.X, Math.Min(v2.positionAfterRotation.X, v3.positionAfterRotation.X));
			int maxX = (int)Math.Max(v1.positionAfterRotation.X, Math.Max(v2.positionAfterRotation.X, v3.positionAfterRotation.X));
			int minY = (int)Math.Min(v1.positionAfterRotation.Y, Math.Min(v2.positionAfterRotation.Y, v3.positionAfterRotation.Y));
			int maxY = (int)Math.Max(v1.positionAfterRotation.Y, Math.Max(v2.positionAfterRotation.Y, v3.positionAfterRotation.Y));

			// Ograniczenie do granic oryginalnej bitmapy
			minX = Math.Clamp(minX, 0, Form1.bitmap.Width - 1);
			maxX = Math.Clamp(maxX, 0, Form1.bitmap.Width - 1);
			minY = Math.Clamp(minY, 0, Form1.bitmap.Height - 1);
			maxY = Math.Clamp(maxY, 0, Form1.bitmap.Height - 1);

			Vector2 uv0 = new Vector2(v1.u, v1.v);
			Vector2 uv1 = new Vector2(v2.u, v2.v);
			Vector2 uv2 = new Vector2(v3.u, v3.v);

			// Przypisanie współrzędnych trójkąta dla interpolacji barycentrycznej
			Vector2 a = new Vector2(v1.positionAfterRotation.X, v1.positionAfterRotation.Y);
			Vector2 b = new Vector2(v2.positionAfterRotation.X, v2.positionAfterRotation.Y);
			Vector2 c = new Vector2(v3.positionAfterRotation.X, v3.positionAfterRotation.Y);

			// Iteracja przez piksele w obszarze ograniczonym przez min i max

			Vector2 p = new Vector2(x, y);

			// Obliczanie współczynników barycentrycznych dla bieżącego punktu (x, y)
			float denominator = (b.Y - c.Y) * (a.X - c.X) + (c.X - b.X) * (a.Y - c.Y);
			float w0 = ((b.Y - c.Y) * (p.X - c.X) + (c.X - b.X) * (p.Y - c.Y)) / denominator;
			float w1 = ((c.Y - a.Y) * (p.X - c.X) + (a.X - c.X) * (p.Y - c.Y)) / denominator;
			float w2 = 1 - w0 - w1;

			// Sprawdzenie, czy punkt (x, y) znajduje się w obrębie trójkąta
			if (w0 >= 0 && w1 >= 0 && w2 >= 0)
			{
				// Interpolacja współrzędnych tekstury u i v na podstawie współczynników barycentrycznych
				float u = w0 * uv0.X + w1 * uv1.X + w2 * uv2.X;
				float v = w0 * uv0.Y + w1 * uv1.Y + w2 * uv2.Y;

				// Przeskalowanie u i v na współrzędne pikseli tekstury
				int texX = (int)(u * (Form1.imageBitmap.Width - 1));
				int texY = (int)(v * (Form1.imageBitmap.Height - 1));

				// Pobranie koloru z tekstury
				return Form1.imageBitmap.GetPixel(texX, texY);
			}

			return Color.Black;
		}

		public void SetClassicNormalVector(Vector3[,] controlPoints)
		{
			PointPositionCalculator calculator = new PointPositionCalculator(controlPoints);
			v1.SetClassicNormalVector(controlPoints, calculator);
			v2.SetClassicNormalVector(controlPoints, calculator);
			v3.SetClassicNormalVector(controlPoints, calculator);
		}

		public void SetNormalMapVector()
		{
			NormalMapReader reader = new NormalMapReader();
			v1.SetNormalMapVector(reader);
			v2.SetNormalMapVector(reader);
			v3.SetNormalMapVector(reader);
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

		public bool isNormalMap = false;

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

		}

		public void SetCoordinatesAfterRotation(Vector3 positionAfterRotation, Vector3 tangentPUAfterRotation, Vector3 tangentPVAfterRotation, Vector3 NAfterRotation)
		{
			this.positionAfterRotation = positionAfterRotation;
			this.tangentPUAfterRotation = tangentPUAfterRotation;
			this.tangentPVAfterRotation = tangentPVAfterRotation;
			this.NAfterRotation = NAfterRotation;
		}

		public void SetClassicNormalVector(Vector3[,] controlPoints, PointPositionCalculator calculator)
		{
			
			NBeforeRotation = calculator.GetNormalVector(u, v);
			NAfterRotation = RotationsCalculator.Rotate(NBeforeRotation, Rotations.GetZRotation(Form1.alpha), Rotations.GetXRotation(Form1.beta));
		}

		public void SetNormalMapVector(NormalMapReader reader)
		{
			this.isNormalMap = true;
			Vector3 PU = Vector3.Normalize(tangentPUAfterRotation);
			Vector3 PV = Vector3.Normalize(tangentPVAfterRotation);
			Vector3 N = Vector3.Normalize(NAfterRotation);

			Matrix4x4 M = new Matrix4x4(
				PU.X, PV.X, N.X, 0,
				PU.Y, PV.Y, N.Y, 0,
				PU.Z, PV.Z, N.Z, 0,
				0, 0, 0, 1
			);
			Vector3 NNormalMap = reader.GetNormalVector(u, v);
			NAfterRotation = Vector3.Normalize(Vector3.Transform(NNormalMap, M));
			
		}

	}

	public class NormalMapReader
	{
		

		public Vector3 GetNormalVector(float u, float v)
		{
			
			u = Math.Clamp(u, 0, 1);
			v = Math.Clamp(v, 0, 1);
			int height;
			int width;
			lock (Form1.normalMap)
			{
				 height = v == 1.0f ? Form1.normalMap.Height - 1 : Form1.normalMap.Height;
				 width = u == 1.0f ? Form1.normalMap.Width - 1 : Form1.normalMap.Width;
			}
			Color color = new Color();
			lock (Form1.normalMap)
			{
				color = Form1.normalMap.GetPixel((int)(u * width), (int)(v * height));
			}
			float nx = color.R / 255.0f * 2 - 1;
			float ny = color.G / 255.0f * 2 - 1;
			float nz = color.B / 255.0f * 2 - 1;
			return new Vector3(nx, ny, nz);
		}
	}

	public class BarycentricCoordinatesSetter
	{

		Triangle t;

		public BarycentricCoordinatesSetter(Triangle t)
		{
			this.t = t;
		}
		public (Vector3 interpolatedNormal, float interpolatedZ) Interpolate(Vector3 point)
		{
			// Oblicz współczynniki barycentryczne
			(float u, float v, float w) = CalculateBarycentricCoordinates(point);

			// Interpolacja wektora normalnego i współrzędnej Z
			Vector3 interpolatedNormal = u * t.v1.NAfterRotation + v * t.v2.NAfterRotation + w * t.v3.NAfterRotation;
			float interpolatedZ = u * t.v1.positionAfterRotation.Z + v * t.v2.positionAfterRotation.Z + w * t.v3.positionAfterRotation.Z;

			return (interpolatedNormal, interpolatedZ);
		}

		public (float u, float v, float w) CalculateBarycentricCoordinates(Vector3 p)
		{
			

			float[] coords = GetBaricentricCoordinates(new Point((int)Math.Round(p.X),(int)Math.Round(p.Y)),
				new Point((int)(t.v1.positionAfterRotation.X),(int)t.v1.positionAfterRotation.Y),
				new Point((int)t.v2.positionAfterRotation.X,(int)t.v2.positionAfterRotation.Y), 
				new Point((int)t.v3.positionAfterRotation.X,(int)t.v3.positionAfterRotation.Y));
			return (coords[0], coords[1], coords[2]);
		}


		public static float[] GetBaricentricCoordinates(Point p, Point a, Point b, Point c)
		{
			float[] coords = new float[3];
			float invertedS = (float)1 / GetDoubledSarea(a, b, c);

			coords[0] = GetDoubledSarea(p, b, c) * invertedS;
			coords[1] = GetDoubledSarea(a, p, c) * invertedS;
			coords[2] = GetDoubledSarea(a, b, p) * invertedS;
			return coords;
		}
		public static int GetDoubledSarea(Point p1, Point p2, Point p3)
		{
			return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
		}
	}

}
