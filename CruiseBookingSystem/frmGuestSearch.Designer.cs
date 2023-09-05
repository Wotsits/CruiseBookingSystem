namespace CruiseBookingSystem
{
    partial class frmGuestSearch
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
            this.panel6 = new System.Windows.Forms.Panel();
            this.ctrSearchResults = new System.Windows.Forms.ListView();
            this.idHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.firstNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lastNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.streetAddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.townCityHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.countyHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.countryHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.postcodeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.phoneHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.emailHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSearch = new System.Windows.Forms.Button();
            this.ctrPostcode = new System.Windows.Forms.TextBox();
            this.lblPostcode = new System.Windows.Forms.Label();
            this.lblMoreInfo = new System.Windows.Forms.Label();
            this.lblSearchResults = new System.Windows.Forms.Label();
            this.ctrEmail = new System.Windows.Forms.TextBox();
            this.ctrName = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnCloseRed = new System.Windows.Forms.Button();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Control;
            this.panel6.Controls.Add(this.ctrSearchResults);
            this.panel6.Controls.Add(this.btnSearch);
            this.panel6.Controls.Add(this.ctrPostcode);
            this.panel6.Controls.Add(this.lblPostcode);
            this.panel6.Controls.Add(this.lblMoreInfo);
            this.panel6.Controls.Add(this.lblSearchResults);
            this.panel6.Controls.Add(this.ctrEmail);
            this.panel6.Controls.Add(this.ctrName);
            this.panel6.Controls.Add(this.lblEmail);
            this.panel6.Controls.Add(this.lblName);
            this.panel6.Location = new System.Drawing.Point(87, 131);
            this.panel6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1766, 940);
            this.panel6.TabIndex = 21;
            // 
            // ctrSearchResults
            // 
            this.ctrSearchResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idHeader,
            this.firstNameHeader,
            this.lastNameHeader,
            this.streetAddressHeader,
            this.townCityHeader,
            this.countyHeader,
            this.countryHeader,
            this.postcodeHeader,
            this.phoneHeader,
            this.emailHeader});
            this.ctrSearchResults.FullRowSelect = true;
            this.ctrSearchResults.HideSelection = false;
            this.ctrSearchResults.Location = new System.Drawing.Point(55, 264);
            this.ctrSearchResults.MultiSelect = false;
            this.ctrSearchResults.Name = "ctrSearchResults";
            this.ctrSearchResults.Size = new System.Drawing.Size(1635, 581);
            this.ctrSearchResults.TabIndex = 58;
            this.ctrSearchResults.UseCompatibleStateImageBehavior = false;
            this.ctrSearchResults.View = System.Windows.Forms.View.Details;
            this.ctrSearchResults.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ctrSearchResults_MouseDoubleClick);
            // 
            // idHeader
            // 
            this.idHeader.Text = "Guest ID";
            // 
            // firstNameHeader
            // 
            this.firstNameHeader.Text = "First Name";
            // 
            // lastNameHeader
            // 
            this.lastNameHeader.Text = "Last Name";
            // 
            // streetAddressHeader
            // 
            this.streetAddressHeader.Text = "Street Address";
            // 
            // townCityHeader
            // 
            this.townCityHeader.Text = "Town/City";
            // 
            // countyHeader
            // 
            this.countyHeader.Text = "County";
            // 
            // countryHeader
            // 
            this.countryHeader.Text = "Country";
            // 
            // postcodeHeader
            // 
            this.postcodeHeader.Text = "Postcode";
            // 
            // phoneHeader
            // 
            this.phoneHeader.Text = "Phone Number";
            // 
            // emailHeader
            // 
            this.emailHeader.Text = "Email Address";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(911, 152);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(96, 35);
            this.btnSearch.TabIndex = 57;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ctrPostcode
            // 
            this.ctrPostcode.Location = new System.Drawing.Point(376, 155);
            this.ctrPostcode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrPostcode.Name = "ctrPostcode";
            this.ctrPostcode.Size = new System.Drawing.Size(464, 26);
            this.ctrPostcode.TabIndex = 56;
            // 
            // lblPostcode
            // 
            this.lblPostcode.AutoSize = true;
            this.lblPostcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPostcode.Location = new System.Drawing.Point(54, 152);
            this.lblPostcode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPostcode.Name = "lblPostcode";
            this.lblPostcode.Size = new System.Drawing.Size(184, 29);
            this.lblPostcode.TabIndex = 55;
            this.lblPostcode.Text = "Guest Postcode";
            // 
            // lblMoreInfo
            // 
            this.lblMoreInfo.AutoSize = true;
            this.lblMoreInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoreInfo.Location = new System.Drawing.Point(372, 220);
            this.lblMoreInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMoreInfo.Name = "lblMoreInfo";
            this.lblMoreInfo.Size = new System.Drawing.Size(274, 22);
            this.lblMoreInfo.TabIndex = 54;
            this.lblMoreInfo.Text = "(double click a customer to open)";
            // 
            // lblSearchResults
            // 
            this.lblSearchResults.AutoSize = true;
            this.lblSearchResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchResults.Location = new System.Drawing.Point(50, 220);
            this.lblSearchResults.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchResults.Name = "lblSearchResults";
            this.lblSearchResults.Size = new System.Drawing.Size(189, 29);
            this.lblSearchResults.TabIndex = 53;
            this.lblSearchResults.Text = "Search Results";
            // 
            // ctrEmail
            // 
            this.ctrEmail.Location = new System.Drawing.Point(376, 100);
            this.ctrEmail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrEmail.Name = "ctrEmail";
            this.ctrEmail.Size = new System.Drawing.Size(464, 26);
            this.ctrEmail.TabIndex = 51;
            // 
            // ctrName
            // 
            this.ctrName.Location = new System.Drawing.Point(376, 49);
            this.ctrName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrName.Name = "ctrName";
            this.ctrName.Size = new System.Drawing.Size(464, 26);
            this.ctrName.TabIndex = 50;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(50, 100);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(143, 29);
            this.lblEmail.TabIndex = 46;
            this.lblEmail.Text = "Guest Email";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(50, 46);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(179, 29);
            this.lblName.TabIndex = 36;
            this.lblName.Text = "Guest Surname";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Info;
            this.lblTitle.Location = new System.Drawing.Point(76, 55);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(583, 55);
            this.lblTitle.TabIndex = 20;
            this.lblTitle.Text = "Search For Existing Guest";
            // 
            // btnCloseRed
            // 
            this.btnCloseRed.BackColor = System.Drawing.Color.Red;
            this.btnCloseRed.Location = new System.Drawing.Point(1792, 55);
            this.btnCloseRed.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCloseRed.Name = "btnCloseRed";
            this.btnCloseRed.Size = new System.Drawing.Size(61, 52);
            this.btnCloseRed.TabIndex = 62;
            this.btnCloseRed.Text = "Close";
            this.btnCloseRed.UseVisualStyleBackColor = false;
            this.btnCloseRed.Click += new System.EventHandler(this.btnCloseRed_Click);
            // 
            // frmGuestSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1965, 1172);
            this.ControlBox = false;
            this.Controls.Add(this.btnCloseRed);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmGuestSearch";
            this.Text = "Guest Search";
            this.Load += new System.EventHandler(this.frmGuestSearch_Load);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox ctrPostcode;
        private System.Windows.Forms.Label lblPostcode;
        private System.Windows.Forms.Label lblMoreInfo;
        private System.Windows.Forms.Label lblSearchResults;
        private System.Windows.Forms.TextBox ctrEmail;
        private System.Windows.Forms.TextBox ctrName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListView ctrSearchResults;
        private System.Windows.Forms.ColumnHeader idHeader;
        private System.Windows.Forms.ColumnHeader firstNameHeader;
        private System.Windows.Forms.ColumnHeader lastNameHeader;
        private System.Windows.Forms.ColumnHeader streetAddressHeader;
        private System.Windows.Forms.ColumnHeader townCityHeader;
        private System.Windows.Forms.ColumnHeader countyHeader;
        private System.Windows.Forms.ColumnHeader countryHeader;
        private System.Windows.Forms.ColumnHeader postcodeHeader;
        private System.Windows.Forms.ColumnHeader phoneHeader;
        private System.Windows.Forms.ColumnHeader emailHeader;
        private System.Windows.Forms.Button btnCloseRed;
    }
}