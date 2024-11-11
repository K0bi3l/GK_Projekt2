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
			menuStrip1 = new MenuStrip();
			wybierzKolorWypełnieniaToolStripMenuItem = new ToolStripMenuItem();
			wybierzKolorObiektuToolStripMenuItem = new ToolStripMenuItem();
			kdTrackBar = new TrackBar();
			kdLabel = new Label();
			ksTrackBar = new TrackBar();
			ksLabel = new Label();
			mTrackBar = new TrackBar();
			mLabel = new Label();
			zTrackBar = new TrackBar();
			zLabel = new Label();
			colorCheckBox = new CheckBox();
			textureCheckBox = new CheckBox();
			((System.ComponentModel.ISupportInitialize)MyPictureBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)TriangulationTrackBar).BeginInit();
			((System.ComponentModel.ISupportInitialize)AlphaTrackBar).BeginInit();
			((System.ComponentModel.ISupportInitialize)BetaTrackBar).BeginInit();
			menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)kdTrackBar).BeginInit();
			((System.ComponentModel.ISupportInitialize)ksTrackBar).BeginInit();
			((System.ComponentModel.ISupportInitialize)mTrackBar).BeginInit();
			((System.ComponentModel.ISupportInitialize)zTrackBar).BeginInit();
			SuspendLayout();
			// 
			// MyPictureBox
			// 
			MyPictureBox.Dock = DockStyle.Fill;
			MyPictureBox.Location = new Point(0, 33);
			MyPictureBox.Name = "MyPictureBox";
			MyPictureBox.Size = new Size(1478, 711);
			MyPictureBox.TabIndex = 0;
			MyPictureBox.TabStop = false;
			// 
			// MeshRadioButton
			// 
			MeshRadioButton.AutoSize = true;
			MeshRadioButton.BackColor = SystemColors.Window;
			MeshRadioButton.Location = new Point(12, 40);
			MeshRadioButton.Name = "MeshRadioButton";
			MeshRadioButton.Size = new Size(84, 29);
			MeshRadioButton.TabIndex = 1;
			MeshRadioButton.TabStop = true;
			MeshRadioButton.Text = "Siatka";
			MeshRadioButton.UseVisualStyleBackColor = false;
			MeshRadioButton.CheckedChanged += MeshRadioButton_CheckedChanged;
			// 
			// FillRadioButton
			// 
			FillRadioButton.AutoSize = true;
			FillRadioButton.BackColor = SystemColors.Window;
			FillRadioButton.Location = new Point(12, 74);
			FillRadioButton.Name = "FillRadioButton";
			FillRadioButton.Size = new Size(134, 29);
			FillRadioButton.TabIndex = 2;
			FillRadioButton.TabStop = true;
			FillRadioButton.Text = "Wypełnienie";
			FillRadioButton.UseVisualStyleBackColor = false;
			FillRadioButton.CheckedChanged += FillRadioButton_CheckedChanged;
			// 
			// TriangulationTrackBar
			// 
			TriangulationTrackBar.BackColor = SystemColors.Window;
			TriangulationTrackBar.LargeChange = 4;
			TriangulationTrackBar.Location = new Point(180, 40);
			TriangulationTrackBar.Maximum = 72;
			TriangulationTrackBar.Minimum = 4;
			TriangulationTrackBar.Name = "TriangulationTrackBar";
			TriangulationTrackBar.Size = new Size(312, 69);
			TriangulationTrackBar.SmallChange = 4;
			TriangulationTrackBar.TabIndex = 3;
			TriangulationTrackBar.TickFrequency = 2;
			TriangulationTrackBar.Value = 4;
			TriangulationTrackBar.Scroll += trackBar1_Scroll;
			// 
			// TriangulationLabel
			// 
			TriangulationLabel.AutoSize = true;
			TriangulationLabel.BackColor = SystemColors.Window;
			TriangulationLabel.Location = new Point(213, 78);
			TriangulationLabel.Name = "TriangulationLabel";
			TriangulationLabel.Size = new Size(217, 25);
			TriangulationLabel.TabIndex = 4;
			TriangulationLabel.Text = "Dokładność Triangulacji: 4";
			TriangulationLabel.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// AlphaTrackBar
			// 
			AlphaTrackBar.BackColor = SystemColors.Window;
			AlphaTrackBar.Location = new Point(533, 40);
			AlphaTrackBar.Maximum = 45;
			AlphaTrackBar.Minimum = -45;
			AlphaTrackBar.Name = "AlphaTrackBar";
			AlphaTrackBar.Size = new Size(269, 69);
			AlphaTrackBar.TabIndex = 5;
			AlphaTrackBar.Scroll += AlphaTrackBar_Scroll;
			// 
			// BetaTrackBar
			// 
			BetaTrackBar.BackColor = SystemColors.Window;
			BetaTrackBar.Location = new Point(869, 40);
			BetaTrackBar.Name = "BetaTrackBar";
			BetaTrackBar.Size = new Size(299, 69);
			BetaTrackBar.TabIndex = 6;
			BetaTrackBar.Scroll += BetaTrackBar_Scroll;
			// 
			// AlphaLabel
			// 
			AlphaLabel.AutoSize = true;
			AlphaLabel.BackColor = SystemColors.Window;
			AlphaLabel.Location = new Point(576, 78);
			AlphaLabel.Name = "AlphaLabel";
			AlphaLabel.Size = new Size(192, 25);
			AlphaLabel.TabIndex = 7;
			AlphaLabel.Text = "Obrót o alpha = 0 deg";
			// 
			// BetaLabel
			// 
			BetaLabel.AutoSize = true;
			BetaLabel.BackColor = SystemColors.Window;
			BetaLabel.Location = new Point(934, 76);
			BetaLabel.Name = "BetaLabel";
			BetaLabel.Size = new Size(184, 25);
			BetaLabel.TabIndex = 8;
			BetaLabel.Text = "Obrót o beta = 0 deg";
			// 
			// menuStrip1
			// 
			menuStrip1.ImageScalingSize = new Size(24, 24);
			menuStrip1.Items.AddRange(new ToolStripItem[] { wybierzKolorWypełnieniaToolStripMenuItem, wybierzKolorObiektuToolStripMenuItem });
			menuStrip1.Location = new Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new Size(1478, 33);
			menuStrip1.TabIndex = 9;
			menuStrip1.Text = "menuStrip1";
			// 
			// wybierzKolorWypełnieniaToolStripMenuItem
			// 
			wybierzKolorWypełnieniaToolStripMenuItem.Name = "wybierzKolorWypełnieniaToolStripMenuItem";
			wybierzKolorWypełnieniaToolStripMenuItem.Size = new Size(197, 29);
			wybierzKolorWypełnieniaToolStripMenuItem.Text = "Wybierz kolor światła";
			wybierzKolorWypełnieniaToolStripMenuItem.Click += wybierzKolorWypełnieniaToolStripMenuItem_Click;
			// 
			// wybierzKolorObiektuToolStripMenuItem
			// 
			wybierzKolorObiektuToolStripMenuItem.Name = "wybierzKolorObiektuToolStripMenuItem";
			wybierzKolorObiektuToolStripMenuItem.Size = new Size(183, 29);
			wybierzKolorObiektuToolStripMenuItem.Text = "Wybierz kolor siatki";
			wybierzKolorObiektuToolStripMenuItem.Click += wybierzKolorObiektuToolStripMenuItem_Click;
			// 
			// kdTrackBar
			// 
			kdTrackBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			kdTrackBar.Location = new Point(1383, 36);
			kdTrackBar.Maximum = 100;
			kdTrackBar.Name = "kdTrackBar";
			kdTrackBar.Orientation = Orientation.Vertical;
			kdTrackBar.Size = new Size(69, 133);
			kdTrackBar.TabIndex = 10;
			kdTrackBar.Value = 50;
			kdTrackBar.Scroll += kdTrackBar_Scroll;
			// 
			// kdLabel
			// 
			kdLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			kdLabel.AutoSize = true;
			kdLabel.Location = new Point(1373, 162);
			kdLabel.Name = "kdLabel";
			kdLabel.Size = new Size(65, 25);
			kdLabel.TabIndex = 11;
			kdLabel.Text = "kd: 0.5";
			// 
			// ksTrackBar
			// 
			ksTrackBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			ksTrackBar.Location = new Point(1379, 190);
			ksTrackBar.Maximum = 100;
			ksTrackBar.Name = "ksTrackBar";
			ksTrackBar.Orientation = Orientation.Vertical;
			ksTrackBar.Size = new Size(69, 135);
			ksTrackBar.TabIndex = 12;
			ksTrackBar.Value = 50;
			ksTrackBar.Scroll += ksTrackBar_Scroll;
			// 
			// ksLabel
			// 
			ksLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			ksLabel.AutoSize = true;
			ksLabel.Location = new Point(1373, 328);
			ksLabel.Name = "ksLabel";
			ksLabel.Size = new Size(62, 25);
			ksLabel.TabIndex = 13;
			ksLabel.Text = "ks: 0.5";
			// 
			// mTrackBar
			// 
			mTrackBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			mTrackBar.Location = new Point(1373, 356);
			mTrackBar.Maximum = 100;
			mTrackBar.Minimum = 1;
			mTrackBar.Name = "mTrackBar";
			mTrackBar.Orientation = Orientation.Vertical;
			mTrackBar.Size = new Size(69, 156);
			mTrackBar.TabIndex = 14;
			mTrackBar.Value = 50;
			mTrackBar.Scroll += mTrackBar_Scroll;
			// 
			// mLabel
			// 
			mLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			mLabel.AutoSize = true;
			mLabel.Location = new Point(1379, 515);
			mLabel.Name = "mLabel";
			mLabel.Size = new Size(57, 25);
			mLabel.TabIndex = 15;
			mLabel.Text = "m: 50";
			// 
			// zTrackBar
			// 
			zTrackBar.Location = new Point(1373, 543);
			zTrackBar.Maximum = 95;
			zTrackBar.Minimum = -95;
			zTrackBar.Name = "zTrackBar";
			zTrackBar.Orientation = Orientation.Vertical;
			zTrackBar.Size = new Size(69, 156);
			zTrackBar.TabIndex = 16;
			zTrackBar.Value = 50;
			zTrackBar.Scroll += zTrackBar_Scroll;
			// 
			// zLabel
			// 
			zLabel.AutoSize = true;
			zLabel.Location = new Point(1373, 702);
			zLabel.Name = "zLabel";
			zLabel.Size = new Size(53, 25);
			zLabel.TabIndex = 17;
			zLabel.Text = "z: 0.5";
			// 
			// colorCheckBox
			// 
			colorCheckBox.AutoSize = true;
			colorCheckBox.Checked = true;
			colorCheckBox.CheckState = CheckState.Checked;
			colorCheckBox.Location = new Point(1197, 41);
			colorCheckBox.Name = "colorCheckBox";
			colorCheckBox.Size = new Size(129, 29);
			colorCheckBox.TabIndex = 18;
			colorCheckBox.Text = "Jeden kolor";
			colorCheckBox.UseVisualStyleBackColor = true;
			colorCheckBox.CheckedChanged += colorCheckBox_CheckedChanged;
			// 
			// textureCheckBox
			// 
			textureCheckBox.AutoSize = true;
			textureCheckBox.Location = new Point(1197, 72);
			textureCheckBox.Name = "textureCheckBox";
			textureCheckBox.Size = new Size(102, 29);
			textureCheckBox.TabIndex = 19;
			textureCheckBox.Text = "Tekstura";
			textureCheckBox.UseVisualStyleBackColor = true;
			textureCheckBox.CheckedChanged += textureCheckBox_CheckedChanged;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.Window;
			ClientSize = new Size(1478, 744);
			Controls.Add(textureCheckBox);
			Controls.Add(colorCheckBox);
			Controls.Add(zLabel);
			Controls.Add(zTrackBar);
			Controls.Add(mLabel);
			Controls.Add(mTrackBar);
			Controls.Add(ksLabel);
			Controls.Add(ksTrackBar);
			Controls.Add(kdLabel);
			Controls.Add(kdTrackBar);
			Controls.Add(BetaLabel);
			Controls.Add(AlphaLabel);
			Controls.Add(BetaTrackBar);
			Controls.Add(AlphaTrackBar);
			Controls.Add(TriangulationLabel);
			Controls.Add(TriangulationTrackBar);
			Controls.Add(FillRadioButton);
			Controls.Add(MeshRadioButton);
			Controls.Add(MyPictureBox);
			Controls.Add(menuStrip1);
			MainMenuStrip = menuStrip1;
			MinimumSize = new Size(1500, 800);
			Name = "Form1";
			Text = "Form1";
			SizeChanged += FormSizeChanged;
			((System.ComponentModel.ISupportInitialize)MyPictureBox).EndInit();
			((System.ComponentModel.ISupportInitialize)TriangulationTrackBar).EndInit();
			((System.ComponentModel.ISupportInitialize)AlphaTrackBar).EndInit();
			((System.ComponentModel.ISupportInitialize)BetaTrackBar).EndInit();
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)kdTrackBar).EndInit();
			((System.ComponentModel.ISupportInitialize)ksTrackBar).EndInit();
			((System.ComponentModel.ISupportInitialize)mTrackBar).EndInit();
			((System.ComponentModel.ISupportInitialize)zTrackBar).EndInit();
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
		private MenuStrip menuStrip1;
		private ToolStripMenuItem wybierzKolorWypełnieniaToolStripMenuItem;
		private TrackBar kdTrackBar;
		private Label kdLabel;
		private TrackBar ksTrackBar;
		private Label ksLabel;
		private TrackBar mTrackBar;
		private Label mLabel;
		private TrackBar zTrackBar;
		private Label zLabel;
		private ToolStripMenuItem wybierzKolorObiektuToolStripMenuItem;
		private CheckBox colorCheckBox;
		private CheckBox textureCheckBox;
	}
}
