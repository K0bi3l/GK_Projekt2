using FastBitmapLib;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
namespace GK_Projekt2
{
	public partial class Form1 : Form
	{
		public static Bitmap bitmap;
		ColorDialog colorDialog;
		public static Color LightColor;
		public static Color MeshColor;
		public static PictureBox pictureBox;
		public static Bitmap originalBitmap;

		List<Vector3> controlPoints;
		List<Vector3> controlPointsToPrint;
		Vector3[,] controlPointsArray;
		List<Vertex> vertices;
		public Mesh mesh;

		public float alpha;
		public float beta;
		public static float kd;
		public static float ks;
		public static int m;
		public static float z;

		public static Vector3 controlVector = new Vector3(int.MinValue, int.MinValue, int.MinValue);

		public delegate void MoveLight();
		public event MoveLight MoveLightEvent;

		public Form1()
		{
			kd = 0.5f;
			ks = 0.5f;
			m = 50;
			z = 0.5f;

			mesh = new Mesh();
			vertices = new List<Vertex>();
			controlPoints = new List<Vector3>();
			controlPointsArray = new Vector3[4, 4];

			colorDialog = new ColorDialog();
			LightColor = Color.FromArgb(255, 255, 255);
			MeshColor = Color.Yellow;

			alpha = 0;
			beta = 0;

			ReadVerticesFromFile();
			this.Load += (s, e) => CreateBitmap(MyPictureBox.Width, MyPictureBox.Height);
			this.Load += (s, e) => mesh = CreateMesh(TriangulationTrackBar.Value);
			this.Load += (s, e) => DrawMesh();
			this.Load += (s, e) => DrawVertices();
			//this.Load += (s, e) => FillMesh();
			InitializeComponent();
			pictureBox = MyPictureBox;
			MoveLightEvent += LightMove;
			Thread th = new Thread(ML);
			th.IsBackground = true;
			//th.Start();

		}

		public void LightMove()
		{
			Console.WriteLine("LightMove");
			Mutex mutex = new Mutex();
			mutex.WaitOne();
			mesh.l.UpdatePosition();
			mutex.ReleaseMutex();
			if (FillRadioButton.Checked)
			{
				FillMesh();
				MyPictureBox.Invalidate();
			}
		}

		public void ML()
		{
			for (; ; )
			{
				Thread.Sleep(1000);
				MoveLightEvent.Invoke();
			}

		}

		public void ReadVerticesFromFile()
		{
			FileStream f = File.Open(Environment.CurrentDirectory + @"\Vertices.txt", FileMode.Open);
			StreamReader s = new StreamReader(f);
			PointPositionCalculator pointPositionCalculator = new PointPositionCalculator(controlPointsArray);
			string[] line;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					line = s.ReadLine().Split(' ');
					Vector3 v = new Vector3(float.Parse(line[0]), float.Parse(line[1]), float.Parse(line[2]));
					controlPointsArray[i, j] = v;
					controlPoints.Add(v);
				}
			}
			float ax = controlPointsArray[0, 0].X;
			float bx = controlPointsArray[0, 3].X;
			float ay = controlPointsArray[0, 0].Y;
			float by = controlPointsArray[3, 0].Y;
			foreach (var v in controlPoints)
			{
				Vector3 UNormal = pointPositionCalculator.GetUNormalVector((v.X - ax) / bx, (v.Y - ay) / by);
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
			DrawMesh();
		}

		public void DrawVertices()
		{
			Graphics g = Graphics.FromImage(bitmap);
			g.TranslateTransform(MyPictureBox.Width / 2, MyPictureBox.Height / 2);
			Pen pen = new Pen(Color.Black, 1);

			//g.Clear(Color.White); // Przy ka¿dym rysowaniu rysujemy od nowa

			foreach (var v in controlPointsToPrint)
			{
				g.FillEllipse(Brushes.Red, v.X, v.Y, 5, 5);
				//DrawLines(v, g, pen);
			}
			MyPictureBox.Invalidate();
		}


		public void DrawLines(Vector3 v, Graphics g, Pen pen)
		{
			int index = controlPointsToPrint.IndexOf(v);
			if (index == 15) return;
			else if (index > 11)
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
			mesh = CreateMesh(TriangulationTrackBar.Value);
			DrawMesh();
			if (FillRadioButton.Checked) FillMesh();
			DrawVertices();
		}

		private void AlphaTrackBar_Scroll(object sender, EventArgs e)
		{
			alpha = AlphaTrackBar.Value;
			AlphaLabel.Text = $"Obrót o alpha = {AlphaTrackBar.Value} deg";

			RotateMesh();
			DrawMesh();
			if (FillRadioButton.Checked) FillMesh();
			DrawVertices();
		}

		private void BetaTrackBar_Scroll(object sender, EventArgs e)
		{
			beta = BetaTrackBar.Value;
			BetaLabel.Text = $"Obrót o beta = {BetaTrackBar.Value} deg";

			RotateMesh();
			DrawMesh();
			if (FillRadioButton.Checked) FillMesh();
			DrawVertices();
		}

		public void DrawMesh()
		{
			Graphics g = Graphics.FromImage(bitmap);			

			g.TranslateTransform(MyPictureBox.Width / 2, MyPictureBox.Height / 2);
			Pen pen = new Pen(Color.Black, 1);
			g.Clear(Color.White);
			foreach (var t in mesh.triangles)
			{
				t.DrawTriangle(g, pen);
			}
			MyPictureBox.Invalidate();
		}

		public void RotateMesh()
		{
			float phi = (float)(alpha * Math.PI / 180);
			float[,] ZRotation = Rotations.GetZRotation(phi);
			phi = (float)(beta * Math.PI / 180);
			float[,] XRotation = Rotations.GetXRotation(phi);
			Rotate(XRotation, ZRotation);
			mesh.Rotate(XRotation, ZRotation);
		}

		public void FillMesh()
		{
			mesh.Fill();
		}

		public void Rotate(float[,] XRotation, float[,] ZRotation)
		{
			foreach (var v in vertices)
			{
				Vector3 positionAfterRotation = RotationsCalculator.Rotate(v.positionBeforeRotation, XRotation, ZRotation);
				Vector3 tangentPUAfterRotation = RotationsCalculator.Rotate(v.tangentPUBeforeRotation, XRotation, ZRotation);
				Vector3 tangentPVAfterRotation = RotationsCalculator.Rotate(v.tangentPVBeforeRotation, XRotation, ZRotation);
				Vector3 NAfterRotation = RotationsCalculator.Rotate(v.NBeforeRotation, XRotation, ZRotation);
				v.SetCoordinatesAfterRotation(positionAfterRotation, tangentPUAfterRotation, tangentPVAfterRotation, NAfterRotation);
				int i = controlPoints.IndexOf(v.positionBeforeRotation);
				controlPointsToPrint.RemoveAt(i);
				controlPointsToPrint.Insert(i, CopyVector(v.positionAfterRotation));
			}
		}

		public static Vector3 CopyVector(Vector3 v)
		{
			return new Vector3(v.X, v.Y, v.Z);
		}

		public Mesh CreateMesh(int accuracy)
		{
			float step = 1.0f / (accuracy - 1);
			float Ypos = 0;
			float Xpos = 0;

			Vertex[,] points = new Vertex[accuracy, accuracy];
			PointPositionCalculator pointPositionCalculator = new PointPositionCalculator(controlPointsArray);

			//tworzenie punktów
			for (int i = 0; i < accuracy; i++)
			{

				for (int j = 0; j < accuracy; j++)
				{
					points[i, j] = new Vertex(pointPositionCalculator.CalculatePointPosition(Xpos, Ypos), pointPositionCalculator.GetUNormalVector(Xpos, Ypos)
						, pointPositionCalculator.GetVNormalVector(Xpos, Ypos), pointPositionCalculator.GetNormalVector(Xpos, Ypos), Xpos, Ypos);
					Ypos += step;
				}
				Ypos = 0;
				Xpos += step;
			}

			//tworzenie trójk¹tów
			List<Triangle> triangles = new List<Triangle>();
			for (int i = 0; i < accuracy - 1; i++)
			{
				for (int j = 0; j < accuracy - 1; j++)
				{
					triangles.Add(new Triangle(points[i, j], points[i + 1, j], points[i, j + 1]));
					triangles.Add(new Triangle(points[i + 1, j], points[i + 1, j + 1], points[i, j + 1]));
				}
			}
			return new Mesh(triangles);
		}



		private void wybierzKolorWype³nieniaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult result = colorDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				LightColor = colorDialog.Color;
			}
			if (FillRadioButton.Checked)
			{
				FillMesh();
				MyPictureBox.Invalidate();
			}

		}

		private void kdTrackBar_Scroll(object sender, EventArgs e)
		{
			kd = (float)(kdTrackBar.Value * 0.01);
			kdLabel.Text = $"Kd: {kd}";
			if (FillRadioButton.Checked)
			{
				FillMesh();
				MyPictureBox.Invalidate();
			}
		}

		private void ksTrackBar_Scroll(object sender, EventArgs e)
		{
			ks = (float)(ksTrackBar.Value * 0.01);
			ksLabel.Text = $"Ks: {ks}";
			if (FillRadioButton.Checked)
			{
				FillMesh();
				MyPictureBox.Invalidate();
			}
		}

		private void mTrackBar_Scroll(object sender, EventArgs e)
		{
			m = mTrackBar.Value;
			mLabel.Text = $"m: {m}";
			if (FillRadioButton.Checked)
			{
				FillMesh();
				MyPictureBox.Invalidate();
			}

		}

		private void zTrackBar_Scroll(object sender, EventArgs e)
		{

			z = (float)(zTrackBar.Value * 0.01);
			mesh.l.MoveLight(z);
			zLabel.Text = $"Z: {z}";
			if (FillRadioButton.Checked)
			{
				FillMesh();
				MyPictureBox.Invalidate();
			}

		}

		private void FillRadioButton_CheckedChanged(object sender, EventArgs e)
		{

			if (FillRadioButton.Checked)
			{
				FillMesh();
				MyPictureBox.Invalidate();
			}
		}

		private void MeshRadioButton_CheckedChanged(object sender, EventArgs e)
		{

			if (MeshRadioButton.Checked)
			{
				DrawMesh();
				DrawVertices();
				MyPictureBox.Invalidate();
			}
		}

		private void wybierzKolorObiektuToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult result = colorDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				MeshColor = colorDialog.Color;
			}
			if (FillRadioButton.Checked)
			{
				FillMesh();
				MyPictureBox.Invalidate();
			}
		}

		private void colorCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (colorCheckBox.Checked)
			{
				textureCheckBox.Checked = false;
			}
			else
			{
				textureCheckBox.Checked = true;
			}
		}

		private void textureCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if(textureCheckBox.Checked)
			{
			
				//originalBitmap = new Bitmap(Environment.CurrentDirectory + @"\bricks.png");
				colorCheckBox.Checked = false;
				//MyPictureBox.Invalidate();
			}
			else
			{
				colorCheckBox.Checked = true;
			}
			
		}
	}
}
