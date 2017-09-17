namespace IndependentLineDrawer
{
    partial class MapView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMapView = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelMapView
            // 
            this.panelMapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMapView.Location = new System.Drawing.Point(0, 0);
            this.panelMapView.Name = "panelMapView";
            this.panelMapView.Size = new System.Drawing.Size(284, 262);
            this.panelMapView.TabIndex = 0;
            this.panelMapView.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMapView_Paint);
            this.panelMapView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMapView_MouseDown);
            // 
            // MapView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.panelMapView);
            this.Name = "MapView";
            this.Text = "MapView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMapView;
    }
}

