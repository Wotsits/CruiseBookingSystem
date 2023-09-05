namespace CruiseBookingSystem
{
    partial class frmCabinDetails
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrHeaderPanel = new System.Windows.Forms.Panel();
            this.ctrOccupantsSelect = new System.Windows.Forms.ComboBox();
            this.lblSlotDescriptionData = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCloseRed = new System.Windows.Forms.Button();
            this.ctrHeaderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Info;
            this.lblTitle.Location = new System.Drawing.Point(57, 43);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(309, 55);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Cabin Details";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cabin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(612, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Occupants";
            // 
            // ctrHeaderPanel
            // 
            this.ctrHeaderPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ctrHeaderPanel.Controls.Add(this.ctrOccupantsSelect);
            this.ctrHeaderPanel.Controls.Add(this.lblSlotDescriptionData);
            this.ctrHeaderPanel.Controls.Add(this.label1);
            this.ctrHeaderPanel.Controls.Add(this.label2);
            this.ctrHeaderPanel.Location = new System.Drawing.Point(67, 113);
            this.ctrHeaderPanel.Name = "ctrHeaderPanel";
            this.ctrHeaderPanel.Size = new System.Drawing.Size(724, 166);
            this.ctrHeaderPanel.TabIndex = 2;
            // 
            // ctrOccupantsSelect
            // 
            this.ctrOccupantsSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctrOccupantsSelect.FormattingEnabled = true;
            this.ctrOccupantsSelect.Items.AddRange(new object[] {
            "1",
            "2"});
            this.ctrOccupantsSelect.Location = new System.Drawing.Point(590, 76);
            this.ctrOccupantsSelect.Name = "ctrOccupantsSelect";
            this.ctrOccupantsSelect.Size = new System.Drawing.Size(117, 28);
            this.ctrOccupantsSelect.TabIndex = 3;
            this.ctrOccupantsSelect.SelectedIndexChanged += new System.EventHandler(this.ctrOccupantsSelect_SelectedIndexChanged);
            // 
            // lblSlotDescriptionData
            // 
            this.lblSlotDescriptionData.AutoSize = true;
            this.lblSlotDescriptionData.Location = new System.Drawing.Point(21, 84);
            this.lblSlotDescriptionData.Name = "lblSlotDescriptionData";
            this.lblSlotDescriptionData.Size = new System.Drawing.Size(0, 20);
            this.lblSlotDescriptionData.TabIndex = 2;
            // 
            // btnNext
            // 
            this.btnNext.Enabled = false;
            this.btnNext.Location = new System.Drawing.Point(634, 337);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(157, 53);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnCloseRed
            // 
            this.btnCloseRed.BackColor = System.Drawing.Color.Red;
            this.btnCloseRed.Location = new System.Drawing.Point(730, 43);
            this.btnCloseRed.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCloseRed.Name = "btnCloseRed";
            this.btnCloseRed.Size = new System.Drawing.Size(61, 52);
            this.btnCloseRed.TabIndex = 62;
            this.btnCloseRed.Text = "Close";
            this.btnCloseRed.UseVisualStyleBackColor = false;
            this.btnCloseRed.Click += new System.EventHandler(this.btnCloseRed_Click);
            // 
            // frmCabinDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(866, 443);
            this.ControlBox = false;
            this.Controls.Add(this.btnCloseRed);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.ctrHeaderPanel);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmCabinDetails";
            this.Text = "Cabin Details";
            this.Load += new System.EventHandler(this.frmCabinDetails_Load);
            this.ctrHeaderPanel.ResumeLayout(false);
            this.ctrHeaderPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel ctrHeaderPanel;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.ComboBox ctrOccupantsSelect;
        private System.Windows.Forms.Label lblSlotDescriptionData;
        private System.Windows.Forms.Button btnCloseRed;
    }
}