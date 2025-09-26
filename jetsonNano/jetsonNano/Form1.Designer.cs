namespace RoadSimulation
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.PictureBox pictureBoxInput;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Panel panelRoad;
        private System.Windows.Forms.PictureBox pictureBoxCar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        private void InitializeComponent()
        {
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pictureBoxInput = new System.Windows.Forms.PictureBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.panelRoad = new System.Windows.Forms.Panel();
            this.pictureBoxCar = new System.Windows.Forms.PictureBox();
            this.labelBlocked = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInput)).BeginInit();
            this.panelRoad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCar)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Location = new System.Drawing.Point(17, 14);
            this.chart1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(714, 360);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // pictureBoxInput
            // 
            this.pictureBoxInput.BackColor = System.Drawing.Color.LightGray;
            this.pictureBoxInput.Location = new System.Drawing.Point(757, 14);
            this.pictureBoxInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxInput.Name = "pictureBoxInput";
            this.pictureBoxInput.Size = new System.Drawing.Size(429, 240);
            this.pictureBoxInput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxInput.TabIndex = 1;
            this.pictureBoxInput.TabStop = false;
            // 
            // lblResult
            // 
            this.lblResult.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblResult.ForeColor = System.Drawing.Color.Black;
            this.lblResult.Location = new System.Drawing.Point(757, 264);
            this.lblResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(429, 48);
            this.lblResult.TabIndex = 2;
            this.lblResult.Text = "예측 결과:";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelRoad
            // 
            this.panelRoad.BackColor = System.Drawing.Color.Gray;
            this.panelRoad.Controls.Add(this.pictureBoxCar);
            this.panelRoad.Controls.Add(this.labelBlocked);
            this.panelRoad.Location = new System.Drawing.Point(17, 396);
            this.panelRoad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelRoad.Name = "panelRoad";
            this.panelRoad.Size = new System.Drawing.Size(1169, 240);
            this.panelRoad.TabIndex = 3;
            // 
            // pictureBoxCar
            // 
            this.pictureBoxCar.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxCar.Location = new System.Drawing.Point(14, 84);
            this.pictureBoxCar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxCar.Name = "pictureBoxCar";
            this.pictureBoxCar.Size = new System.Drawing.Size(143, 72);
            this.pictureBoxCar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCar.TabIndex = 0;
            this.pictureBoxCar.TabStop = false;
            // 
            // labelBlocked
            // 
            this.labelBlocked.BackColor = System.Drawing.Color.Yellow;
            this.labelBlocked.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold);
            this.labelBlocked.ForeColor = System.Drawing.Color.Red;
            this.labelBlocked.Location = new System.Drawing.Point(429, 84);
            this.labelBlocked.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBlocked.Name = "labelBlocked";
            this.labelBlocked.Size = new System.Drawing.Size(286, 72);
            this.labelBlocked.TabIndex = 1;
            this.labelBlocked.Text = "🚧 BLOCKED";
            this.labelBlocked.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelBlocked.Visible = false;
 
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 660);
            this.Controls.Add(this.panelRoad);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.pictureBoxInput);
            this.Controls.Add(this.chart1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "🚗 도로 시뮬레이션";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInput)).EndInit();
            this.panelRoad.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelBlocked;
    }
}
