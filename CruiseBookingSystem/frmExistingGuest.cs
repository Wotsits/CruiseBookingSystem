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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CruiseBookingSystem
{
    public partial class frmExistingGuest : Form
    {
        public frmExistingGuest()
        {
            InitializeComponent();
        }

        // Override to allow a guest ID to be passed into the form for display.
        private string m_GuestId = "0";
        public frmExistingGuest(string guestId)
        {
            m_GuestId = guestId;
            InitializeComponent();
        }

        // When the form first loads, set it up.
        private void frmExistingGuest_Load(object sender, EventArgs e)
        {
            // set the width of the booking list columns to spread evenly across the listView.
            int parentWidth = ctrBookingList.Width;
            for (int i = 0; i < ctrBookingList.Columns.Count; i++)
            {
                ctrBookingList.Columns[i].Width = parentWidth / 10;
            }

            // get the selected guest from the search results listView.
            int selectedGuestId = int.Parse(this.m_GuestId);
            
            if (selectedGuestId != 0)
            {
                // Populate the guest details
                Guest guest = new Guest();

                guest.GetGuestById(selectedGuestId);

                lblFNameData.Text = guest.FirstName;
                lblLNameData.Text= guest.LastName;
                lblPhoneData.Text = guest.Phone;
                lblEmailData.Text = guest.Email;
                lblStreetAddressData.Text = guest.StreetAddress;
                lblTownCityData.Text = guest.TownCity;
                lblCountyData.Text = guest.County;
                lblCountryData.Text = guest.Country;
                lblPostcodeData.Text = guest.Postcode;

                // Populate the booking listView
                Booking booking = new Booking();

                ArrayList bookings = booking.GetBookingsByGuestId(selectedGuestId);

                if (bookings.Count == 0)
                {
                    ctrBookingList.Items.Add("No Bookings");
                    return;
                }
                foreach (Booking bookingObj in bookings)
                {
                    ListViewItem item = new ListViewItem(new string[] {
                        bookingObj.Id.ToString(),
                        bookingObj.CreatedAt.ToString(),
                        bookingObj.CancelledAt.ToString() != "01/01/0001 00:00:00"  ? bookingObj.CancelledAt.ToString() : "",
                        bookingObj.TourDate != null ? bookingObj.TourDate.ToString() != "01/01/0001 00:00:00" ? DateTime.Parse(bookingObj.TourDate.ToString()).ToString("dd/MM/yyyy") : "" : "",
                        bookingObj.Ship != null ? bookingObj.Ship.ToString() : "",
                        bookingObj.Cabin != 0 ? bookingObj.Cabin.ToString() : "",
                        bookingObj.CabinClass != null ? bookingObj.CabinClass.ToString() : "",
                        "£" + bookingObj.Rate.ToString("0.00"),
                        "£" + bookingObj.PaymentTotal.ToString("0.00"),
                        // calculate balance
                        "£" + (bookingObj.Rate - bookingObj.PaymentTotal).ToString("0.00")
                    }) ;
                    ctrBookingList.Items.Add(item);
                }

            }
        }

        //Triggered when an item in the Booking List receives a double click.
        private void ctrBookingList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Check that an item has been clicked (not just whitespace)
            ListViewHitTestInfo info = ctrBookingList.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            // if an item was clicked, open the frmExistingBooking to display that booking.
            if (item != null)
            {
                Form frmExistingBooking = new frmExistingBooking(ctrBookingList.SelectedItems[0].Text);
                frmExistingBooking.Show();
            }
            // otherwise, remove any selection from the list.
            else
            {
                this.ctrBookingList.SelectedItems.Clear();
            }
        }

        // close button
        private void btnCloseRed_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
