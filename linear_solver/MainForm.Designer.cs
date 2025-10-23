using System;
using System.Drawing;
using System.Windows.Forms;

namespace linear_solver
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            panelTop = new Panel();
            lblTitle = new Label();
            radioSystem = new RadioButton();
            radioMatrix = new RadioButton();
            panelInput = new Panel();
            lblInput = new Label();
            txtEquations = new TextBox();
            dgvMatrix = new DataGridView();
            btnConvertSystemToMatrix = new Button();
            lblAlgorithm = new Label();
            comboAlgorithm = new ComboBox();
            btnSolve = new Button();
            rtbOutput = new RichTextBox();
            txtFinalOutput = new TextBox();
            panelBottom = new Panel();
            btnResetOutput = new Button();
            btnResetAll = new Button();
            panelTop.SuspendLayout();
            panelInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMatrix).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.Peru;
            panelTop.Controls.Add(lblTitle);
            panelTop.Controls.Add(radioSystem);
            panelTop.Controls.Add(radioMatrix);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1200, 80);
            panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(308, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Linear System Solver";
            // 
            // radioSystem
            // 
            radioSystem.AutoSize = true;
            radioSystem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            radioSystem.ForeColor = Color.White;
            radioSystem.Location = new Point(600, 30);
            radioSystem.Name = "radioSystem";
            radioSystem.Size = new Size(137, 27);
            radioSystem.TabIndex = 1;
            radioSystem.Text = "System Input";
            radioSystem.CheckedChanged += radioSystem_CheckedChanged;
            // 
            // radioMatrix
            // 
            radioMatrix.AutoSize = true;
            radioMatrix.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            radioMatrix.ForeColor = Color.White;
            radioMatrix.Location = new Point(750, 30);
            radioMatrix.Name = "radioMatrix";
            radioMatrix.Size = new Size(132, 27);
            radioMatrix.TabIndex = 2;
            radioMatrix.Text = "Matrix Input";
            radioMatrix.CheckedChanged += radioMatrix_CheckedChanged;
            // 
            // panelInput
            // 
            panelInput.BackColor = Color.Beige;
            panelInput.Controls.Add(lblInput);
            panelInput.Controls.Add(txtEquations);
            panelInput.Controls.Add(dgvMatrix);
            panelInput.Controls.Add(btnConvertSystemToMatrix);
            panelInput.Controls.Add(lblAlgorithm);
            panelInput.Controls.Add(comboAlgorithm);
            panelInput.Controls.Add(btnSolve);
            panelInput.Controls.Add(rtbOutput);
            panelInput.Controls.Add(txtFinalOutput);
            panelInput.Location = new Point(20, 100);
            panelInput.Name = "panelInput";
            panelInput.Size = new Size(1150, 502);
            panelInput.TabIndex = 1;
            // 
            // lblInput
            // 
            lblInput.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblInput.ForeColor = Color.Brown;
            lblInput.Location = new Point(20, 20);
            lblInput.Name = "lblInput";
            lblInput.Size = new Size(275, 23);
            lblInput.TabIndex = 0;
            lblInput.Text = "Enter your system or matrix:";
            // 
            // txtEquations
            // 
            txtEquations.BackColor = Color.Moccasin;
            txtEquations.Location = new Point(20, 50);
            txtEquations.Multiline = true;
            txtEquations.Name = "txtEquations";
            txtEquations.ScrollBars = ScrollBars.Vertical;
            txtEquations.Size = new Size(450, 150);
            txtEquations.TabIndex = 1;
            // 
            // dgvMatrix
            // 
            dgvMatrix.AllowUserToOrderColumns = true;
            dgvMatrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMatrix.BackgroundColor = Color.Moccasin;
            dgvMatrix.ColumnHeadersHeight = 29;
            dgvMatrix.Location = new Point(20, 220);
            dgvMatrix.Name = "dgvMatrix";
            dgvMatrix.RowHeadersWidth = 51;
            dgvMatrix.Size = new Size(450, 150);
            dgvMatrix.TabIndex = 2;
            // 
            // btnConvertSystemToMatrix
            // 
            btnConvertSystemToMatrix.BackColor = Color.SaddleBrown;
            btnConvertSystemToMatrix.ForeColor = Color.White;
            btnConvertSystemToMatrix.Location = new Point(20, 380);
            btnConvertSystemToMatrix.Name = "btnConvertSystemToMatrix";
            btnConvertSystemToMatrix.Size = new Size(220, 40);
            btnConvertSystemToMatrix.TabIndex = 3;
            btnConvertSystemToMatrix.Text = "Convert System → Matrix";
            btnConvertSystemToMatrix.UseVisualStyleBackColor = false;
            btnConvertSystemToMatrix.Click += btnConvertSystemToMatrix_Click;
            // 
            // lblAlgorithm
            // 
            lblAlgorithm.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAlgorithm.ForeColor = Color.Brown;
            lblAlgorithm.Location = new Point(512, 20);
            lblAlgorithm.Name = "lblAlgorithm";
            lblAlgorithm.Size = new Size(198, 23);
            lblAlgorithm.TabIndex = 6;
            lblAlgorithm.Text = "Choose Algorithm:";
            // 
            // comboAlgorithm
            // 
            comboAlgorithm.Items.AddRange(new object[] { "Gaussian Elimination", "Gauss–Jordan Elimination" });
            comboAlgorithm.Location = new Point(718, 18);
            comboAlgorithm.Name = "comboAlgorithm";
            comboAlgorithm.Size = new Size(250, 28);
            comboAlgorithm.TabIndex = 7;
            // 
            // btnSolve
            // 
            btnSolve.BackColor = Color.SaddleBrown;
            btnSolve.ForeColor = Color.White;
            btnSolve.Location = new Point(1009, 18);
            btnSolve.Name = "btnSolve";
            btnSolve.Size = new Size(120, 30);
            btnSolve.TabIndex = 8;
            btnSolve.Text = "Solve System";
            btnSolve.UseVisualStyleBackColor = false;
            btnSolve.Click += btnSolve_Click;
            // 
            // rtbOutput
            // 
            rtbOutput.BackColor = Color.FloralWhite;
            rtbOutput.Font = new Font("Consolas", 9F);
            rtbOutput.Location = new Point(500, 70);
            rtbOutput.Name = "rtbOutput";
            rtbOutput.ReadOnly = true;
            rtbOutput.Size = new Size(647, 350);
            rtbOutput.TabIndex = 9;
            rtbOutput.Text = "";
            // 
            // txtFinalOutput
            // 
            txtFinalOutput.BackColor = Color.Linen;
            txtFinalOutput.Font = new Font("Consolas", 10F);
            txtFinalOutput.Location = new Point(500, 430);
            txtFinalOutput.Multiline = true;
            txtFinalOutput.Name = "txtFinalOutput";
            txtFinalOutput.ReadOnly = true;
            txtFinalOutput.ScrollBars = ScrollBars.Vertical;
            txtFinalOutput.Size = new Size(647, 69);
            txtFinalOutput.TabIndex = 10;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.Peru;
            panelBottom.Controls.Add(btnResetOutput);
            panelBottom.Controls.Add(btnResetAll);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 630);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1200, 70);
            panelBottom.TabIndex = 2;
            // 
            // btnResetOutput
            // 
            btnResetOutput.BackColor = Color.SaddleBrown;
            btnResetOutput.ForeColor = Color.White;
            btnResetOutput.Location = new Point(400, 20);
            btnResetOutput.Name = "btnResetOutput";
            btnResetOutput.Size = new Size(150, 35);
            btnResetOutput.TabIndex = 0;
            btnResetOutput.Text = "Reset Output";
            btnResetOutput.UseVisualStyleBackColor = false;
            btnResetOutput.Click += btnResetOutput_Click;
            // 
            // btnResetAll
            // 
            btnResetAll.BackColor = Color.SaddleBrown;
            btnResetAll.ForeColor = Color.White;
            btnResetAll.Location = new Point(580, 20);
            btnResetAll.Name = "btnResetAll";
            btnResetAll.Size = new Size(150, 35);
            btnResetAll.TabIndex = 1;
            btnResetAll.Text = "Reset All";
            btnResetAll.UseVisualStyleBackColor = false;
            btnResetAll.Click += btnResetAll_Click;
            // 
            // MainForm
            // 
            BackColor = Color.Beige;
            ClientSize = new Size(1200, 700);
            Controls.Add(panelTop);
            Controls.Add(panelInput);
            Controls.Add(panelBottom);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Linear System Solver";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelInput.ResumeLayout(false);
            panelInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMatrix).EndInit();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private Panel panelTop;
        private Label lblTitle;
        private RadioButton radioSystem;
        private RadioButton radioMatrix;
        private Panel panelInput;
        private Label lblInput;
        private TextBox txtEquations;
        private DataGridView dgvMatrix;
        private Button btnConvertSystemToMatrix;
        private Label lblAlgorithm;
        private ComboBox comboAlgorithm;
        private Button btnSolve;
        private RichTextBox rtbOutput;
        private TextBox txtFinalOutput;
        private Panel panelBottom;
        private Button btnResetOutput;
        private Button btnResetAll;
    }

}
