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
    public partial class frmNewTour : Form
    {
        public frmNewTour()
        {
            InitializeComponent();
        }

        // On Load
        private void frmNewTour_Load(object sender, EventArgs e)
        {
            // pupulate the ship combobox.
            Ship ship = new Ship();

            ArrayList ships = ship.getAllShips();

            // Convert the ArrayList to a List of objects
            List<Ship> shipsList = ships.Cast<Ship>().ToList();

            ctrShipSelect.DataSource = shipsList;
            ctrShipSelect.DisplayMember = "Name";
            ctrShipSelect.ValueMember = "Id";
        }

        // On click of the save button
        private void btnSave_Click(object sender, EventArgs e)
        {
            // check if a ship has been selected.
            string selectedShip = ctrShipSelect.SelectedValue != null ? ctrShipSelect.SelectedValue.ToString() : "0";
            // if not, display error.
            if (selectedShip == "0")
            {
                MessageBox.Show("Please select a ship.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int shipId = Int32.Parse(selectedShip);

            // check if a date has been selected, if not display error. 
            DateTime date;
            try
            {
                date = DateTime.Parse(ctrDate.Text);
            }
            catch
            {
                MessageBox.Show("Please select a date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            // if all is well, create the tour date.
            TourDate tourDate = new TourDate();
            int response = tourDate.Create(shipId, date);
            // handle failure.
            if (response != 1)
            {
                MessageBox.Show("Failed to create tour date.  Please try again", "Creation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // reload the tours form.
            frmToursSearch form = new frmToursSearch();
            form.Show();
            this.Close();
        }

        // close button
        private void btnCloseRed_Click(object sender, EventArgs e)
        {
            frmToursSearch form = new frmToursSearch();
            form.Show();
            this.Close();
        }
    }
}
