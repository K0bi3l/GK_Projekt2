using FastBitmapLib;
using System.Drawing;
using System.Numerics; 
namespace GK_Projekt2
{
	public partial class Form1 : Form
	{
		List<Vector3> controlPoints;
		List<Vector3> controlPointsToPrint;
		Vector3[,] controlPointsArray;
		List<Vertex> vertices;
		public Bitmap bitmap;
		public Mesh mesh;

		public float alpha;
		public float beta;

		public static Vector3 controlVector = new Vector3(int.MinValue,int.MinValue, int.MinValue);

		public Form1()
		{
			mesh = new Mesh();
			vertices = new List<Vertex>();
			controlPoints = new List<Vector3>();
			controlPointsArray = new Vector3[4, 4];

			alpha = 0;
			beta = 0;

			ReadVerticesFromFile();
			this.Load += (s, e) => CreateBitmap(MyPictureBox.Width, MyPictureBox.Height);
			this.Load += (s, e) => DrawVertices();
			InitializeComponent();

		}

		public void ReadVerticesFromFile()
		{
			FileStream f = File.Open(Environment.CurrentDirectory + @"\Vertices.txt", FileMode.Open);
			StreamReader s = new StreamReader(f);
			PointPositionCalculator pointPositionCalculator = new PointPositionCalculator(controlPointsArray);
			string[] line;
			for(int i = 0 ; i < 4; i++)
			{ 
				for(int j = 0; j < 4; j++)
				{
					line = s.ReadLine().Split(' ');
					Vector3 v = new Vector3(float.Parse(line[0]), float.Parse(line[1]), float.Parse(line[2]));
					controlPointsArray[i, j] = v;
					controlPoints.Add(v);					
				}
			}
			float ax = controlPointsArray[0,0].X;
			float bx = controlPointsArray[0,3].X;
			float ay = controlPointsArray[0,0].Y;
			float by = controlPointsArray[3,0].Y;
			foreach(var v in controlPoints)
			{
				Vector3 UNormal = pointPositionCalculator.GetUNormalVector((v.X - ax)/bx, (v.Y - ay)/by);
				Vector3 VNormal = pointPositionCalculator.GetVNormalVector((v.X - ax) / bx, (v.Y - ay) / by);
				Vector3 N = Vector3.Cross(UNormal, VNormal);
				vertices.Add(new Vertex(v, UNormal, VNormal, N, (v.X - ax) / bx, (v.Y - ay) / by));
			}
			controlPointsToPrint = new List<Vector3>(controlPoints);
		}

		private void CreateBitmap(int width, int height)
		{
			// Zwalnianie poprzedniej bitmapy, jeœli istnieje
			if (bitmap != null)
				bitmap.Dispose();

			// Tworzenie nowej bitmapy dopasowanej do rozmiaru PictureBox
			bitmap = new Bitmap(width, height);

			using (Graphics g = Graphics.FromImage(bitmap))
			{
				g.Clear(Color.White); // Wype³nienie t³a bia³ym kolorem
									  //g.DrawEllipse(Pens.Red, 10, 10, width - 20, height - 20); // Przyk³adowe rysowanie
			}

			// Przypisanie bitmapy do PictureBox
			MyPictureBox.Image = bitmap;
			MyPictureBox.Invalidate();
		}


		private void FormSizeChanged(object sender, EventArgs e)
		{			

			CreateBitmap(MyPictureBox.Width, MyPictureBox.Height);
			DrawVertices();
		}

		public void DrawVertices()
		{
			Graphics g = Graphics.FromImage(bitmap);
			g.TranslateTransform(MyPictureBox.Width / 2, MyPictureBox.Height / 2);
			Pen pen = new Pen(Color.Black, 1);

			g.Clear(Color.White); // Przy ka¿dym rysowaniu rysujemy od nowa
			
			foreach(var v in controlPointsToPrint)
			{
				g.FillRectangle(Brushes.Black, v.X, v.Y, 5, 5);
				DrawLines(v, g, pen);
			}
			MyPictureBox.Invalidate();
		}
		

		public void DrawLines(Vector3 v, Graphics g, Pen pen)
		{			
			int index = controlPointsToPrint.IndexOf(v);
			if (index == 15) return;
			else if(index > 11)
			{
				g.DrawLine(pen, new Point((int)v.X, (int)v.Y), new Point((int)controlPointsToPrint[index + 1].X, (int)controlPointsToPrint[index + 1].Y));				
			}
			else if (index % 4 == 3)
			{
				g.DrawLine(pen, new Point((int)v.X, (int)v.Y), new Point((int)controlPointsToPrint[index + 4].X, (int)controlPointsToPrint[index + 4].Y));				
			}
			else
			{
				g.DrawLine(pen, new Point((int)v.X, (int)v.Y), new Point((int)controlPointsToPrint[index + 1].X, (int)controlPointsToPrint[index + 1].Y));
				g.DrawLine(pen, new Point((int)v.X, (int)v.Y), new Point((int)controlPointsToPrint[index + 4].X, (int)controlPointsToPrint[index + 4].Y));
			}
		}


		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			
			int value = TriangulationTrackBar.Value;
			if (value % 2 == 1)
			{
				TriangulationTrackBar.Value = value - 1;
			}
			TriangulationLabel.Text = $"Dok³adnoœæ Triangulacji:{TriangulationTrackBar.Value}";

		}

		private void AlphaTrackBar_Scroll(object sender, EventArgs e)
		{
			alpha = AlphaTrackBar.Value;
			AlphaLabel.Text = $"Obrót o alpha = {AlphaTrackBar.Value} deg";

			RotateMeshX(alpha);
			DrawVertices();
		}

		private void BetaTrackBar_Scroll(object sender, EventArgs e)
		{
			beta = BetaTrackBar.Value;
			BetaLabel.Text = $"Obrót o beta = {BetaTrackBar.Value} deg";

			RotateMeshZ(beta);
			DrawVertices();
		}

		public void RotateMeshX(float alpha)
		{
			float phi = (float)(alpha * Math.PI / 180);
			float[,] rotation = Rotations.GetXRotation(phi);
			Rotate(rotation);
		}

		public void RotateMeshZ(float beta)
		{
			float phi = (float)(alpha * Math.PI / 180);
			float[,] rotation = Rotations.GetZRotation(phi);
			Rotate(rotation); 
			// rotacja do poprawy bo siê nie krêci
			/*foreach (Vertex v in vertices) 
			{
				Vector3 positionAfterRotation = RotationsCalculator.Rotate(v.positionAfterRotation, rotation);
				Vector3 tangentPUAfterRotation = RotationsCalculator.Rotate(v.tangentPUAfterRotation, rotation);
				Vector3 tangentPVAfterRotation = RotationsCalculator.Rotate(v.tangentPVAfterRotation, rotation);
				Vector3 NAfterRotation = RotationsCalculator.Rotate(v.NAfterRotation, rotation);
				v.SetCoordinatesAfterRotation(positionAfterRotation, tangentPUAfterRotation, tangentPVAfterRotation, NAfterRotation);
				int i = controlPoints.IndexOf(v.positionBeforeRotation);
				controlPointsToPrint.RemoveAt(i);
				controlPointsToPrint.Insert(i, CopyVector(v.positionAfterRotation));

			}*/
		}

		public void Rotate(float[,] rotation)
		{
			foreach (Vertex v in vertices)
			{
				Vector3 positionAfterRotation = RotationsCalculator.Rotate(v.positionBeforeRotation, rotation);
				Vector3 tangentPUAfterRotation = RotationsCalculator.Rotate(v.tangentPUBeforeRotation, rotation);
				Vector3 tangentPVAfterRotation = RotationsCalculator.Rotate(v.tangentPVBeforeRotation, rotation);
				Vector3 NAfterRotation = RotationsCalculator.Rotate(v.NBeforeRotation, rotation);
				v.SetCoordinatesAfterRotation(positionAfterRotation, tangentPUAfterRotation, tangentPVAfterRotation, NAfterRotation);
				int i = controlPoints.IndexOf(v.positionBeforeRotation);
				controlPointsToPrint.RemoveAt(i);
				controlPointsToPrint.Insert(i, CopyVector(v.positionAfterRotation));
				
			}
		}

		public Vector3 CopyVector(Vector3 v)
		{
			return new Vector3(v.X, v.Y, v.Z);
		}
	}
}
