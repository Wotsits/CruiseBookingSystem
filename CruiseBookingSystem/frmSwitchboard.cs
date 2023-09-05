using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyPaymentProcessor;

namespace CruiseBookingSystem
{
    public partial class frmSwitchboard : Form
    {
        public frmSwitchboard()
        {
            InitializeComponent();
        }

        // OPERATIONS 
        private void btnNewBooking_Click(object sender, EventArgs e)
        {
            var form = new frmNewBooking();
            form.Show();
        }

        private void btnBookingSearch_Click(object sender, EventArgs e)
        {
            var form = new frmBookingSearch();
            form.Show();
        }

        private void btnGuestSearch_Click(object sender, EventArgs e)
        {
            var form = new frmGuestSearch();
            form.Show();
        }

        // ADMIN
        private void btnTrips_Click(object sender, EventArgs e)
        {
            var form = new frmToursSearch();
            form.Show();
        }

        // CLOSE BUTTON
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
