using System;
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
    public partial class frmExistingBooking : Form
    {
        public frmExistingBooking()
        {
            InitializeComponent();
        }

        private string m_bookingId;
        public frmExistingBooking(string bookingId)
        {
            m_bookingId = bookingId;
            InitializeComponent();
        }

        // on load
        private void frmExistingBooking_Load(object sender, EventArgs e)
        {
            int selectedBookingId = int.Parse(this.m_bookingId);

            // assuming that a booking number was passed in, load the existing booking into the form fields.  
            if (selectedBookingId != 0)
            {
                // Populate the booking details. 
                Booking booking = new Booking();
                
                booking.GetBookingById(selectedBookingId);

                lblBookingIdData.Text = booking.Id.ToString();

                lblGuestNameData.Text = booking.GuestName;
                lblGuestEmailData.Text = booking.GuestEmail;
                lblGuestPhoneData.Text = booking.GuestPhone;

                lblTourDateData.Text = booking.TourDate.ToString() != "01/01/0001 00:00:00" ? DateTime.Parse(booking.TourDate.ToString()).ToString("dd/MM/yyyy") : "";
                
                lblShipData.Text = booking.Ship != null ? booking.Ship : "";
                lblCabinIdData.Text = booking.Cabin != 0 ? booking.Cabin.ToString() : "";
                lblCabinClassData.Text = booking.CabinClass != null ? booking.CabinClass.ToString() : "";

                lblRateData.Text = "£" + booking.Rate.ToString("0.00");
                lblPaymentTotalData.Text = "£" + booking.PaymentTotal.ToString("0.00");
                lblBalanceData.Text = "£" + (booking.Rate - booking.PaymentTotal).ToString("0.00");

                if (booking.CancelledAt.ToString() != "01/01/0001 00:00:00" )
                {
                    lblCancelledAtData.Text = booking.CancelledAt.ToString();
                    lblCancelledAtData.Visible = true;
                    lblCancelledAt.Visible = true;
                    btnCancel.Visible = false;
                }
            }
        }

        // on click of booking cancellation button.
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // grab the booking details from the form.
            int bookingId = int.Parse(lblBookingIdData.Text);
            string guestName = lblGuestNameData.Text;
            DateTime tourDate = DateTime.Parse(lblTourDateData.Text);
            Double rate = Double.Parse(lblRateData.Text.Substring(1));
            Double paid = Double.Parse(lblPaymentTotalData.Text.Substring(1));

            // load the cancellation dialog
            Form cancellationWindow = new frmCancellationWindow(bookingId, guestName, tourDate, rate, paid);
            // close this form as once the cancellation is complete, it's content will be out of date, so the form will need to be reinitialized. 
            this.Close();
            cancellationWindow.Show();
        }

        // close button
        private void btnCloseRed_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
