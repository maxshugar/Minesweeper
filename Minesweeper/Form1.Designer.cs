namespace Minesweeper
{
    partial class MinesweeperForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MinesweeperForm));
            this.tblGrid = new System.Windows.Forms.TableLayoutPanel();
            this.lblHighscore = new System.Windows.Forms.Label();
            this.tileGrid = new Minesweeper.MinesweeperForm.TileGrid();
            this.lblTime = new System.Windows.Forms.Label();
            this.flagCounter = new System.Windows.Forms.Label();
            this.cbDifficulty = new System.Windows.Forms.ComboBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.pbHS = new System.Windows.Forms.PictureBox();
            this.pbWin = new System.Windows.Forms.PictureBox();
            this.pbGO = new System.Windows.Forms.PictureBox();
            this.pbTrophy = new System.Windows.Forms.PictureBox();
            this.pbClock = new System.Windows.Forms.PictureBox();
            this.pbFlag = new System.Windows.Forms.PictureBox();
            this.btnHelp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbHS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTrophy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFlag)).BeginInit();
            this.SuspendLayout();
            // 
            // tblGrid
            // 
            this.tblGrid.BackColor = System.Drawing.Color.Gray;
            this.tblGrid.ColumnCount = 20;
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tblGrid.Location = new System.Drawing.Point(30, 45);
            this.tblGrid.Name = "tblGrid";
            this.tblGrid.RowCount = 20;
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGrid.Size = new System.Drawing.Size(400, 400);
            this.tblGrid.TabIndex = 0;
            // 
            // lblHighscore
            // 
            this.lblHighscore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHighscore.AutoSize = true;
            this.lblHighscore.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHighscore.ForeColor = System.Drawing.Color.Black;
            this.lblHighscore.Location = new System.Drawing.Point(404, 91);
            this.lblHighscore.Name = "lblHighscore";
            this.lblHighscore.Size = new System.Drawing.Size(59, 20);
            this.lblHighscore.TabIndex = 1;
            this.lblHighscore.Text = "00:00.00";
            this.lblHighscore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tileGrid
            // 
            this.tileGrid.BackColor = System.Drawing.SystemColors.Control;
            this.tileGrid.Location = new System.Drawing.Point(12, 12);
            this.tileGrid.Name = "tileGrid";
            this.tileGrid.Size = new System.Drawing.Size(350, 266);
            this.tileGrid.TabIndex = 0;
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.Black;
            this.lblTime.Location = new System.Drawing.Point(404, 16);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(59, 20);
            this.lblTime.TabIndex = 5;
            this.lblTime.Text = "00:00.00";
            // 
            // flagCounter
            // 
            this.flagCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flagCounter.AutoSize = true;
            this.flagCounter.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagCounter.ForeColor = System.Drawing.Color.Black;
            this.flagCounter.Location = new System.Drawing.Point(404, 52);
            this.flagCounter.Name = "flagCounter";
            this.flagCounter.Size = new System.Drawing.Size(16, 20);
            this.flagCounter.TabIndex = 7;
            this.flagCounter.Text = "0";
            // 
            // cbDifficulty
            // 
            this.cbDifficulty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDifficulty.FormattingEnabled = true;
            this.cbDifficulty.Items.AddRange(new object[] {
            "Beginner",
            "Intermediate",
            "Expert",
            "Insane"});
            this.cbDifficulty.Location = new System.Drawing.Point(368, 252);
            this.cbDifficulty.Name = "cbDifficulty";
            this.cbDifficulty.Size = new System.Drawing.Size(100, 21);
            this.cbDifficulty.TabIndex = 9;
            this.cbDifficulty.SelectedIndexChanged += new System.EventHandler(this.cbDifficulty_SelectedIndexChanged);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.BackColor = System.Drawing.Color.Transparent;
            this.btnReset.Location = new System.Drawing.Point(368, 223);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 23);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pbHS
            // 
            this.pbHS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbHS.BackColor = System.Drawing.Color.Transparent;
            this.pbHS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbHS.Image = global::Minesweeper.Properties.Resources.hs;
            this.pbHS.Location = new System.Drawing.Point(368, 135);
            this.pbHS.Name = "pbHS";
            this.pbHS.Size = new System.Drawing.Size(100, 53);
            this.pbHS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbHS.TabIndex = 16;
            this.pbHS.TabStop = false;
            // 
            // pbWin
            // 
            this.pbWin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbWin.BackColor = System.Drawing.Color.Transparent;
            this.pbWin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbWin.Image = global::Minesweeper.Properties.Resources.win;
            this.pbWin.Location = new System.Drawing.Point(368, 135);
            this.pbWin.Name = "pbWin";
            this.pbWin.Size = new System.Drawing.Size(100, 53);
            this.pbWin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbWin.TabIndex = 15;
            this.pbWin.TabStop = false;
            // 
            // pbGO
            // 
            this.pbGO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbGO.BackColor = System.Drawing.Color.Transparent;
            this.pbGO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbGO.Image = global::Minesweeper.Properties.Resources.game_over;
            this.pbGO.Location = new System.Drawing.Point(368, 135);
            this.pbGO.Name = "pbGO";
            this.pbGO.Size = new System.Drawing.Size(100, 53);
            this.pbGO.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbGO.TabIndex = 14;
            this.pbGO.TabStop = false;
            // 
            // pbTrophy
            // 
            this.pbTrophy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTrophy.Image = ((System.Drawing.Image)(resources.GetObject("pbTrophy.Image")));
            this.pbTrophy.Location = new System.Drawing.Point(368, 84);
            this.pbTrophy.Name = "pbTrophy";
            this.pbTrophy.Size = new System.Drawing.Size(30, 30);
            this.pbTrophy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTrophy.TabIndex = 13;
            this.pbTrophy.TabStop = false;
            // 
            // pbClock
            // 
            this.pbClock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbClock.Image = ((System.Drawing.Image)(resources.GetObject("pbClock.Image")));
            this.pbClock.Location = new System.Drawing.Point(368, 12);
            this.pbClock.Name = "pbClock";
            this.pbClock.Size = new System.Drawing.Size(30, 30);
            this.pbClock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbClock.TabIndex = 12;
            this.pbClock.TabStop = false;
            // 
            // pbFlag
            // 
            this.pbFlag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbFlag.Image = ((System.Drawing.Image)(resources.GetObject("pbFlag.Image")));
            this.pbFlag.Location = new System.Drawing.Point(368, 48);
            this.pbFlag.Name = "pbFlag";
            this.pbFlag.Size = new System.Drawing.Size(30, 30);
            this.pbFlag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFlag.TabIndex = 11;
            this.pbFlag.TabStop = false;
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.BackColor = System.Drawing.Color.Transparent;
            this.btnHelp.Location = new System.Drawing.Point(368, 194);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(100, 23);
            this.btnHelp.TabIndex = 17;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // MinesweeperForm
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(476, 294);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.cbDifficulty);
            this.Controls.Add(this.pbGO);
            this.Controls.Add(this.pbHS);
            this.Controls.Add(this.pbWin);
            this.Controls.Add(this.pbTrophy);
            this.Controls.Add(this.pbClock);
            this.Controls.Add(this.pbFlag);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblHighscore);
            this.Controls.Add(this.flagCounter);
            this.Controls.Add(this.tileGrid);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MinesweeperForm";
            this.Text = "Minesweeper";
            ((System.ComponentModel.ISupportInitialize)(this.pbHS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTrophy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFlag)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblGrid;
        private System.Windows.Forms.Label lblHighscore;
        private TileGrid tileGrid;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label flagCounter;
        private System.Windows.Forms.ComboBox cbDifficulty;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.PictureBox pbFlag;
        private System.Windows.Forms.PictureBox pbClock;
        private System.Windows.Forms.PictureBox pbTrophy;
        private System.Windows.Forms.PictureBox pbGO;
        private System.Windows.Forms.PictureBox pbWin;
        private System.Windows.Forms.PictureBox pbHS;
        private System.Windows.Forms.Button btnHelp;
    }
}

