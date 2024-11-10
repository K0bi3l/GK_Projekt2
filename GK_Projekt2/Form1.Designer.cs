namespace GK_Projekt2
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			MyPictureBox = new PictureBox();
			MeshRadioButton = new RadioButton();
			FillRadioButton = new RadioButton();
			TriangulationTrackBar = new TrackBar();
			TriangulationLabel = new Label();
			AlphaTrackBar = new TrackBar();
			BetaTrackBar = new TrackBar();
			AlphaLabel = new Label();
			BetaLabel = new Label();
			((System.ComponentModel.ISupportInitialize)MyPictureBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)TriangulationTrackBar).BeginInit();
			((System.ComponentModel.ISupportInitialize)AlphaTrackBar).BeginInit();
			((System.ComponentModel.ISupportInitialize)BetaTrackBar).BeginInit();
			SuspendLayout();
			// 
			// MyPictureBox
			// 
			MyPictureBox.Dock = DockStyle.Fill;
			MyPictureBox.Location = new Point(0, 0);
			MyPictureBox.Name = "MyPictureBox";
			MyPictureBox.Size = new Size(1478, 744);
			MyPictureBox.TabIndex = 0;
			MyPictureBox.TabStop = false;
			// 
			// MeshRadioButton
			// 
			MeshRadioButton.AutoSize = true;
			MeshRadioButton.BackColor = SystemColors.Window;
			MeshRadioButton.Location = new Point(0, 0);
			MeshRadioButton.Name = "MeshRadioButton";
			MeshRadioButton.Size = new Size(84, 29);
			MeshRadioButton.TabIndex = 1;
			MeshRadioButton.TabStop = true;
			MeshRadioButton.Text = "Siatka";
			MeshRadioButton.UseVisualStyleBackColor = false;
			// 
			// FillRadioButton
			// 
			FillRadioButton.AutoSize = true;
			FillRadioButton.BackColor = SystemColors.Window;
			FillRadioButton.Location = new Point(0, 35);
			FillRadioButton.Name = "FillRadioButton";
			FillRadioButton.Size = new Size(134, 29);
			FillRadioButton.TabIndex = 2;
			FillRadioButton.TabStop = true;
			FillRadioButton.Text = "Wypełnienie";
			FillRadioButton.UseVisualStyleBackColor = false;
			// 
			// TriangulationTrackBar
			// 
			TriangulationTrackBar.BackColor = SystemColors.Window;
			TriangulationTrackBar.LargeChange = 4;
			TriangulationTrackBar.Location = new Point(182, 0);
			TriangulationTrackBar.Maximum = 18;
			TriangulationTrackBar.Minimum = 2;
			TriangulationTrackBar.Name = "TriangulationTrackBar";
			TriangulationTrackBar.Size = new Size(312, 69);
			TriangulationTrackBar.SmallChange = 4;
			TriangulationTrackBar.TabIndex = 3;
			TriangulationTrackBar.TickFrequency = 2;
			TriangulationTrackBar.Value = 2;
			TriangulationTrackBar.Scroll += trackBar1_Scroll;
			// 
			// TriangulationLabel
			// 
			TriangulationLabel.AutoSize = true;
			TriangulationLabel.BackColor = SystemColors.Window;
			TriangulationLabel.Location = new Point(197, 37);
			TriangulationLabel.Name = "TriangulationLabel";
			TriangulationLabel.Size = new Size(217, 25);
			TriangulationLabel.TabIndex = 4;
			TriangulationLabel.Text = "Dokładność Triangulacji: 2";
			TriangulationLabel.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// AlphaTrackBar
			// 
			AlphaTrackBar.BackColor = SystemColors.Window;
			AlphaTrackBar.Location = new Point(531, 0);
			AlphaTrackBar.Maximum = 45;
			AlphaTrackBar.Name = "AlphaTrackBar";
			AlphaTrackBar.Size = new Size(269, 69);
			AlphaTrackBar.TabIndex = 5;
			AlphaTrackBar.Scroll += AlphaTrackBar_Scroll;
			// 
			// BetaTrackBar
			// 
			BetaTrackBar.BackColor = SystemColors.Window;
			BetaTrackBar.Location = new Point(866, 0);
			BetaTrackBar.Name = "BetaTrackBar";
			BetaTrackBar.Size = new Size(299, 69);
			BetaTrackBar.TabIndex = 6;
			BetaTrackBar.Scroll += BetaTrackBar_Scroll;
			// 
			// AlphaLabel
			// 
			AlphaLabel.AutoSize = true;
			AlphaLabel.BackColor = SystemColors.Window;
			AlphaLabel.Location = new Point(571, 35);
			AlphaLabel.Name = "AlphaLabel";
			AlphaLabel.Size = new Size(192, 25);
			AlphaLabel.TabIndex = 7;
			AlphaLabel.Text = "Obrót o alpha = 0 deg";
			// 
			// BetaLabel
			// 
			BetaLabel.AutoSize = true;
			BetaLabel.BackColor = SystemColors.Window;
			BetaLabel.Location = new Point(894, 37);
			BetaLabel.Name = "BetaLabel";
			BetaLabel.Size = new Size(184, 25);
			BetaLabel.TabIndex = 8;
			BetaLabel.Text = "Obrót o beta = 0 deg";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1478, 744);
			Controls.Add(BetaLabel);
			Controls.Add(AlphaLabel);
			Controls.Add(BetaTrackBar);
			Controls.Add(AlphaTrackBar);
			Controls.Add(TriangulationLabel);
			Controls.Add(TriangulationTrackBar);
			Controls.Add(FillRadioButton);
			Controls.Add(MeshRadioButton);
			Controls.Add(MyPictureBox);
			MinimumSize = new Size(1500, 800);
			Name = "Form1";
			Text = "Form1";
			SizeChanged += FormSizeChanged;
			((System.ComponentModel.ISupportInitialize)MyPictureBox).EndInit();
			((System.ComponentModel.ISupportInitialize)TriangulationTrackBar).EndInit();
			((System.ComponentModel.ISupportInitialize)AlphaTrackBar).EndInit();
			((System.ComponentModel.ISupportInitialize)BetaTrackBar).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private PictureBox MyPictureBox;
		private RadioButton MeshRadioButton;
		private RadioButton FillRadioButton;
		private TrackBar TriangulationTrackBar;
		private Label TriangulationLabel;
		private TrackBar AlphaTrackBar;
		private TrackBar BetaTrackBar;
		private Label AlphaLabel;
		private Label BetaLabel;
	}
}
