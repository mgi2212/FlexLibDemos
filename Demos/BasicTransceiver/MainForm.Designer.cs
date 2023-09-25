using System.Windows.Forms;

namespace BasicTransceiver
{
    partial class MainForm
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
            this.SpectrumPlot = new ScottPlot.FormsPlot();
            this.RadioPanel = new System.Windows.Forms.Panel();
            this.RadioPropGrid = new System.Windows.Forms.PropertyGrid();
            this.RadioPanelLabel = new System.Windows.Forms.Label();
            this.SlicePanel = new System.Windows.Forms.Panel();
            this.SlicePropGrid = new System.Windows.Forms.PropertyGrid();
            this.SlicePanelLabel = new System.Windows.Forms.Label();
            this.PanPanel = new System.Windows.Forms.Panel();
            this.PanPropGrid = new System.Windows.Forms.PropertyGrid();
            this.PanPanelLabel = new System.Windows.Forms.Label();
            this.ControPanel = new System.Windows.Forms.Panel();
            this.BandWidth = new System.Windows.Forms.TrackBar();
            this.Tune = new System.Windows.Forms.CheckBox();
            this.StartRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.RadioPanel.SuspendLayout();
            this.SlicePanel.SuspendLayout();
            this.PanPanel.SuspendLayout();
            this.ControPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BandWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // SpectrumPlot
            // 
            this.SpectrumPlot.BackColor = System.Drawing.Color.Transparent;
            this.SpectrumPlot.Dock = System.Windows.Forms.DockStyle.Top;
            this.SpectrumPlot.Location = new System.Drawing.Point(0, 48);
            this.SpectrumPlot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SpectrumPlot.Name = "SpectrumPlot";
            this.SpectrumPlot.Size = new System.Drawing.Size(1351, 522);
            this.SpectrumPlot.TabIndex = 0;
            // 
            // RadioPanel
            // 
            this.RadioPanel.Controls.Add(this.RadioPropGrid);
            this.RadioPanel.Controls.Add(this.RadioPanelLabel);
            this.RadioPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.RadioPanel.Location = new System.Drawing.Point(0, 570);
            this.RadioPanel.Name = "RadioPanel";
            this.RadioPanel.Size = new System.Drawing.Size(450, 360);
            this.RadioPanel.TabIndex = 5;
            // 
            // RadioPropGrid
            // 
            this.RadioPropGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadioPropGrid.Location = new System.Drawing.Point(0, 24);
            this.RadioPropGrid.Name = "RadioPropGrid";
            this.RadioPropGrid.Size = new System.Drawing.Size(450, 336);
            this.RadioPropGrid.TabIndex = 1;
            // 
            // RadioPanelLabel
            // 
            this.RadioPanelLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.RadioPanelLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.RadioPanelLabel.Location = new System.Drawing.Point(0, 0);
            this.RadioPanelLabel.Name = "RadioPanelLabel";
            this.RadioPanelLabel.Size = new System.Drawing.Size(450, 24);
            this.RadioPanelLabel.TabIndex = 0;
            this.RadioPanelLabel.Text = "Radio";
            this.RadioPanelLabel.Click += new System.EventHandler(this.RadioPanelLabel_Click);
            // 
            // SlicePanel
            // 
            this.SlicePanel.Controls.Add(this.SlicePropGrid);
            this.SlicePanel.Controls.Add(this.SlicePanelLabel);
            this.SlicePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.SlicePanel.Location = new System.Drawing.Point(900, 570);
            this.SlicePanel.Name = "SlicePanel";
            this.SlicePanel.Size = new System.Drawing.Size(450, 360);
            this.SlicePanel.TabIndex = 6;
            // 
            // SlicePropGrid
            // 
            this.SlicePropGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SlicePropGrid.Location = new System.Drawing.Point(0, 24);
            this.SlicePropGrid.Name = "SlicePropGrid";
            this.SlicePropGrid.Size = new System.Drawing.Size(450, 336);
            this.SlicePropGrid.TabIndex = 3;
            // 
            // SlicePanelLabel
            // 
            this.SlicePanelLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SlicePanelLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SlicePanelLabel.Location = new System.Drawing.Point(0, 0);
            this.SlicePanelLabel.Name = "SlicePanelLabel";
            this.SlicePanelLabel.Size = new System.Drawing.Size(450, 24);
            this.SlicePanelLabel.TabIndex = 2;
            this.SlicePanelLabel.Text = "Slice";
            this.SlicePanelLabel.Click += new System.EventHandler(this.SlicePanelLabel_Click);
            // 
            // PanPanel
            // 
            this.PanPanel.Controls.Add(this.PanPropGrid);
            this.PanPanel.Controls.Add(this.PanPanelLabel);
            this.PanPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanPanel.Location = new System.Drawing.Point(450, 570);
            this.PanPanel.Name = "PanPanel";
            this.PanPanel.Size = new System.Drawing.Size(450, 360);
            this.PanPanel.TabIndex = 7;
            // 
            // PanPropGrid
            // 
            this.PanPropGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanPropGrid.Location = new System.Drawing.Point(0, 24);
            this.PanPropGrid.Name = "PanPropGrid";
            this.PanPropGrid.Size = new System.Drawing.Size(450, 336);
            this.PanPropGrid.TabIndex = 2;
            // 
            // PanPanelLabel
            // 
            this.PanPanelLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanPanelLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PanPanelLabel.Location = new System.Drawing.Point(0, 0);
            this.PanPanelLabel.Name = "PanPanelLabel";
            this.PanPanelLabel.Size = new System.Drawing.Size(450, 24);
            this.PanPanelLabel.TabIndex = 1;
            this.PanPanelLabel.Text = "Panadapter";
            this.PanPanelLabel.Click += new System.EventHandler(this.PanadapterPanelLabel_Click);
            // 
            // ControPanel
            // 
            this.ControPanel.Controls.Add(this.label1);
            this.ControPanel.Controls.Add(this.BandWidth);
            this.ControPanel.Controls.Add(this.Tune);
            this.ControPanel.Controls.Add(this.StartRefresh);
            this.ControPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ControPanel.Location = new System.Drawing.Point(0, 0);
            this.ControPanel.Name = "ControPanel";
            this.ControPanel.Size = new System.Drawing.Size(1351, 48);
            this.ControPanel.TabIndex = 8;
            // 
            // BandWidth
            // 
            this.BandWidth.Location = new System.Drawing.Point(282, 12);
            this.BandWidth.Maximum = 1000;
            this.BandWidth.Minimum = 1;
            this.BandWidth.Name = "BandWidth";
            this.BandWidth.Size = new System.Drawing.Size(481, 45);
            this.BandWidth.TabIndex = 7;
            this.BandWidth.Value = 1;
            this.BandWidth.Scroll += new System.EventHandler(this.Bandwidth_Scroll);
            // 
            // Tune
            // 
            this.Tune.AutoSize = true;
            this.Tune.Location = new System.Drawing.Point(108, 16);
            this.Tune.Name = "Tune";
            this.Tune.Size = new System.Drawing.Size(76, 19);
            this.Tune.TabIndex = 6;
            this.Tune.Text = "TX (Tune)";
            this.Tune.UseVisualStyleBackColor = true;
            this.Tune.CheckedChanged += new System.EventHandler(this.Tune_CheckedChanged);
            // 
            // StartRefresh
            // 
            this.StartRefresh.Location = new System.Drawing.Point(6, 12);
            this.StartRefresh.Name = "StartRefresh";
            this.StartRefresh.Size = new System.Drawing.Size(75, 23);
            this.StartRefresh.TabIndex = 5;
            this.StartRefresh.Text = "Start";
            this.StartRefresh.UseVisualStyleBackColor = true;
            this.StartRefresh.Click += new System.EventHandler(this.StartRefresh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(212, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Bandwidth";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 930);
            this.Controls.Add(this.SlicePanel);
            this.Controls.Add(this.PanPanel);
            this.Controls.Add(this.RadioPanel);
            this.Controls.Add(this.SpectrumPlot);
            this.Controls.Add(this.ControPanel);
            this.Name = "MainForm";
            this.Text = "Basic Transceiver";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.RadioPanel.ResumeLayout(false);
            this.SlicePanel.ResumeLayout(false);
            this.PanPanel.ResumeLayout(false);
            this.ControPanel.ResumeLayout(false);
            this.ControPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BandWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ScottPlot.FormsPlot SpectrumPlot;
        private Panel RadioPanel;
        private Label RadioPanelLabel;
        private Panel SlicePanel;
        private PropertyGrid SlicePropGrid;
        private Label SlicePanelLabel;
        private Panel PanPanel;
        private PropertyGrid PanPropGrid;
        private Label PanPanelLabel;
        private PropertyGrid RadioPropGrid;
        private Panel ControPanel;
        private CheckBox Tune;
        private Button StartRefresh;
        private TrackBar BandWidth;
        private Label label1;
    }
}