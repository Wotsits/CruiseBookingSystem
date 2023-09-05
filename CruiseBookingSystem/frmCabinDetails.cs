using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CruiseBookingSystem
{
    public partial class frmCabinDetails : Form
    {
        public frmCabinDetails()
        {
            InitializeComponent();
        }

        // this dialogue box takes in a reference to the form that called it as a param.  This is so that it can 1) obtain the selected slots from that calling form and 2) update the calling form with the values that a entered.  
        private frmNewBooking m_callingForm;
        private string m_slotId;
        public frmCabinDetails(frmNewBooking callingForm)
        {
            m_slotId = callingForm.SlotId;
            m_callingForm = callingForm;
            InitializeComponent();
        }

        // On Load
        private void frmCabinDetails_Load(object sender, EventArgs e)
        { 
            // get the details of the booking.
            CabinBookingTourDateMap cabinBookingTourDateMap = new CabinBookingTourDateMap();
            cabinBookingTourDateMap.GetSlotById(Int32.Parse(m_slotId));

            // populate the label with the obtained details. 
            lblSlotDescriptionData.Text = cabinBookingTourDateMap.CabinId + " - " + cabinBookingTourDateMap.ClassName;
        }

        // On Click of the next button, pass the occupant details back to the calling form.
        private void btnNext_Click(object sender, EventArgs e)
        {
            // update the variable in the calling form.
            m_callingForm.Occupants = Int32.Parse(ctrOccupantsSelect.SelectedItem.ToString());
            m_callingForm.TabsActiveIndex = 2;

            // close this form
            this.Close();
        }

        // On change of the occupant select.
        private void ctrOccupantsSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if an item is selected, enable the next button
            if (ctrOccupantsSelect.SelectedText != null)
            {
                btnNext.Enabled = true;
            }
        }

        // CLose button
        private void btnCloseRed_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
