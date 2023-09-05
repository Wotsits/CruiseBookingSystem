namespace CruiseBookingSystem
{
    partial class frmSwitchboard
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
            this.btnNewBooking = new System.Windows.Forms.Button();
            this.btnBookingSearch = new System.Windows.Forms.Button();
            this.btnGuestSearch = new System.Windows.Forms.Button();
            this.lblNewBooking = new System.Windows.Forms.Label();
            this.lblBookingSearch = new System.Windows.Forms.Label();
            this.lblGuestSearch = new System.Windows.Forms.Label();
            this.grpOperations = new System.Windows.Forms.GroupBox();
            this.grpAdmin = new System.Windows.Forms.GroupBox();
            this.btnTrips = new System.Windows.Forms.Button();
            this.lblTrips = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpOperations.SuspendLayout();
            this.grpAdmin.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Info;
            this.lblTitle.Location = new System.Drawing.Point(72, 35);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(305, 55);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "BookACruise";
            // 
            // btnNewBooking
            // 
            this.btnNewBooking.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnNewBooking.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnNewBooking.Location = new System.Drawing.Point(34, 69);
            this.btnNewBooking.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNewBooking.Name = "btnNewBooking";
            this.btnNewBooking.Size = new System.Drawing.Size(52, 52);
            this.btnNewBooking.TabIndex = 1;
            this.btnNewBooking.UseVisualStyleBackColor = false;
            this.btnNewBooking.Click += new System.EventHandler(this.btnNewBooking_Click);
            // 
            // btnBookingSearch
            // 
            this.btnBookingSearch.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnBookingSearch.Location = new System.Drawing.Point(34, 217);
            this.btnBookingSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBookingSearch.Name = "btnBookingSearch";
            this.btnBookingSearch.Size = new System.Drawing.Size(52, 52);
            this.btnBookingSearch.TabIndex = 2;
            this.btnBookingSearch.UseVisualStyleBackColor = false;
            this.btnBookingSearch.Click += new System.EventHandler(this.btnBookingSearch_Click);
            // 
            // btnGuestSearch
            // 
            this.btnGuestSearch.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnGuestSearch.Location = new System.Drawing.Point(34, 143);
            this.btnGuestSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGuestSearch.Name = "btnGuestSearch";
            this.btnGuestSearch.Size = new System.Drawing.Size(52, 52);
            this.btnGuestSearch.TabIndex = 3;
            this.btnGuestSearch.UseVisualStyleBackColor = false;
            this.btnGuestSearch.Click += new System.EventHandler(this.btnGuestSearch_Click);
            // 
            // lblNewBooking
            // 
            this.lblNewBooking.AutoSize = true;
            this.lblNewBooking.Location = new System.Drawing.Point(96, 80);
            this.lblNewBooking.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNewBooking.Name = "lblNewBooking";
            this.lblNewBooking.Size = new System.Drawing.Size(255, 29);
            this.lblNewBooking.TabIndex = 5;
            this.lblNewBooking.Text = "Create a New Booking";
            // 
            // lblBookingSearch
            // 
            this.lblBookingSearch.AutoSize = true;
            this.lblBookingSearch.Location = new System.Drawing.Point(94, 229);
            this.lblBookingSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBookingSearch.Name = "lblBookingSearch";
            this.lblBookingSearch.Size = new System.Drawing.Size(230, 29);
            this.lblBookingSearch.TabIndex = 6;
            this.lblBookingSearch.Text = "Search for Bookings";
            // 
            // lblGuestSearch
            // 
            this.lblGuestSearch.AutoSize = true;
            this.lblGuestSearch.Location = new System.Drawing.Point(96, 154);
            this.lblGuestSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGuestSearch.Name = "lblGuestSearch";
            this.lblGuestSearch.Size = new System.Drawing.Size(204, 29);
            this.lblGuestSearch.TabIndex = 7;
            this.lblGuestSearch.Text = "Search for Guests";
            // 
            // grpOperations
            // 
            this.grpOperations.BackColor = System.Drawing.SystemColors.Control;
            this.grpOperations.Controls.Add(this.lblBookingSearch);
            this.grpOperations.Controls.Add(this.lblGuestSearch);
            this.grpOperations.Controls.Add(this.btnBookingSearch);
            this.grpOperations.Controls.Add(this.btnNewBooking);
            this.grpOperations.Controls.Add(this.lblNewBooking);
            this.grpOperations.Controls.Add(this.btnGuestSearch);
            this.grpOperations.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpOperations.Location = new System.Drawing.Point(82, 159);
            this.grpOperations.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpOperations.Name = "grpOperations";
            this.grpOperations.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpOperations.Size = new System.Drawing.Size(471, 313);
            this.grpOperations.TabIndex = 8;
            this.grpOperations.TabStop = false;
            this.grpOperations.Text = "Booking Management";
            // 
            // grpAdmin
            // 
            this.grpAdmin.BackColor = System.Drawing.SystemColors.Control;
            this.grpAdmin.Controls.Add(this.btnTrips);
            this.grpAdmin.Controls.Add(this.lblTrips);
            this.grpAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAdmin.Location = new System.Drawing.Point(82, 519);
            this.grpAdmin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpAdmin.Name = "grpAdmin";
            this.grpAdmin.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpAdmin.Size = new System.Drawing.Size(471, 158);
            this.grpAdmin.TabIndex = 9;
            this.grpAdmin.TabStop = false;
            this.grpAdmin.Text = "Administration";
            // 
            // btnTrips
            // 
            this.btnTrips.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnTrips.Location = new System.Drawing.Point(34, 69);
            this.btnTrips.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTrips.Name = "btnTrips";
            this.btnTrips.Size = new System.Drawing.Size(52, 52);
            this.btnTrips.TabIndex = 1;
            this.btnTrips.UseVisualStyleBackColor = false;
            this.btnTrips.Click += new System.EventHandler(this.btnTrips_Click);
            // 
            // lblTrips
            // 
            this.lblTrips.AutoSize = true;
            this.lblTrips.Location = new System.Drawing.Point(96, 80);
            this.lblTrips.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTrips.Name = "lblTrips";
            this.lblTrips.Size = new System.Drawing.Size(305, 44);
            this.lblTrips.TabIndex = 5;
            this.lblTrips.Text = "Trip Management";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(154, 717);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(329, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "A Solution Developed By Sexton Software :-P";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Red;
            this.btnClose.Location = new System.Drawing.Point(492, 35);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(61, 52);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmSwitchboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(646, 776);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.grpAdmin);
            this.Controls.Add(this.grpOperations);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "frmSwitchboard";
            this.Text = "Welcome to BookACruise";
            this.grpOperations.ResumeLayout(false);
            this.grpOperations.PerformLayout();
            this.grpAdmin.ResumeLayout(false);
            this.grpAdmin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnNewBooking;
        private System.Windows.Forms.Button btnBookingSearch;
        private System.Windows.Forms.Button btnGuestSearch;
        private System.Windows.Forms.Label lblNewBooking;
        private System.Windows.Forms.Label lblBookingSearch;
        private System.Windows.Forms.Label lblGuestSearch;
        private System.Windows.Forms.GroupBox grpOperations;
        private System.Windows.Forms.GroupBox grpAdmin;
        private System.Windows.Forms.Button btnTrips;
        private System.Windows.Forms.Label lblTrips;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClose;
    }
}

