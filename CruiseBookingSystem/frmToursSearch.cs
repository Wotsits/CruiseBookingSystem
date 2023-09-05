using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CruiseBookingSystem
{
    public partial class frmToursSearch : Form
    {

        public frmToursSearch()
        {
            InitializeComponent();
        }

        // on load
        private void frmToursSearch_Load(object sender, EventArgs e)
        {
            // populate the tours list.
            TourDate tourDate = new TourDate();

            ArrayList tourDates = tourDate.getAllTourDates();

            ctrToursGrid.DataSource = tourDates;
            ctrToursGrid.Columns["id"].Visible= false;
            ctrToursGrid.Columns["shipId"].Visible = false;
        }

        // on click of add new.
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            // fire up the newTour form.
            frmNewTour form = new frmNewTour();
            form.Show();
            // this form will need reloading once the new tour is added.
            this.Close();
        }

        // close button
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
