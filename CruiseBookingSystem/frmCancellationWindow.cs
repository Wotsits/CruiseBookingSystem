using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyPaymentProcessor;

namespace CruiseBookingSystem
{
    public partial class frmCancellationWindow : Form
    {
        public frmCancellationWindow()
        {
            InitializeComponent();
        }

        //---------------------------------
        // this form can take in data from it's caller.  This data is the attributes of the booking being cancelled by the window.
        
        private int m_bookingId = 0;
        private string m_guestName = "";
        private DateTime m_tourDate = DateTime.MinValue;
        private Double m_rate = 0.0;
        private Double m_paid = 0.0;
        private Double m_refund = 0.0;

        public frmCancellationWindow(int bookingId, string guestName, DateTime tourDate, Double rate, Double paid)
        {
            m_bookingId = bookingId;
            m_guestName = guestName;
            m_tourDate = tourDate;
            m_rate = rate;
            m_paid = paid;
            InitializeComponent();
        }

        //---------------------------------

        // On Load
        private void frmCancellationWindow_Load(object sender, EventArgs e)
        {
            // populate the fields on the form with the stuff that was passed in from the calling form.
            lblBookingIdData.Text = m_bookingId.ToString();
            lblGuestNameData.Text = m_guestName;
            lblTourDateData.Text = DateTime.Parse(m_tourDate.ToString()).ToString("dd/MM/yyyy");
            lblRateData.Text = "£" + m_rate.ToString("0.00");
            lblPaidData.Text = "£" + m_paid.ToString("0.00");

            // calculate available refund
            // 50% refund given if 10 days notice given of cancellation.
            DateTime now = DateTime.Now;
            int diffOfDays = (now - m_tourDate).Days;

            // if there is sufficient notice, refund is calc'd at 50% 
            if (diffOfDays < -9)
            {
                m_refund = m_paid / 2;
            }
            // else, no refund. 
            else m_refund = 0;

            // display the calculated refund.
            lblRefund.Text = "£" + String.Format("{0:0.00}", m_refund);
        }

        // On click of the Confirm button
        private void btnConfirmCancellation_Click(object sender, EventArgs e)
        {
            int response = 3;
            // the payment processing method will return either a 0, 1 or 2.  Zero indicates success.  
            response = CruisePaymentDll.ProcessPayment();

            while (response > 0)
            {
                MessageBox.Show("Refund failed - please try again.");
                response = CruisePaymentDll.ProcessPayment();
            }

            // refund has been successfully made at this point. 
            // now, update the database.

            // reset response to 3 for the next stage.
            response = 3;
            Booking booking = new Booking();
            // this is a little confusing - I'm setting the id of the Booking instance to the booking Id that was passed into this form. 
            booking.Id = m_bookingId;
            // perform cancellation and update database accordingly.
            response = booking.Cancel(m_refund);

            // if it fails, try again.
            while (response > 0)
            {
                MessageBox.Show("Refund was made but the communication with the database failed to update the booking state.  Please press retry to try the update again.", "Information", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                response = booking.Cancel(m_refund);
            }

            // display confirmation message. 
            MessageBox.Show("Booking has been cancelled successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            // reload the booking form with the cancelled booking. 
            frmExistingBooking form = new frmExistingBooking(m_bookingId.ToString());
            form.Show();
            this.Close();
        }

        // close button
        private void btnCloseRed_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
