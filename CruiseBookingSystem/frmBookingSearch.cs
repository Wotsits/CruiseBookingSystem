using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CruiseBookingSystem
{
    public partial class frmBookingSearch : Form
    {
        public frmBookingSearch()
        {
            InitializeComponent();
        }

        private void frmBookingSearch_Load(object sender, EventArgs e)
        {
            // set the width of the booking list columns to spread evenly across the listView.
            int parentWidth = ctrBookingList.Width;
            for (int i = 0; i < ctrBookingList.Columns.Count; i++)
            {
                ctrBookingList.Columns[i].Width = parentWidth / 11;
            }

            // Populate the booking list without any filtering applied. 
            Booking booking = new Booking();
            ArrayList bookings = booking.GetAllBookings();

            // Handle no bookings returned.
            if (bookings.Count == 0)
            {
                ctrBookingList.Items.Add("No Bookings");
                return;
            }

            // Populate the list.
            foreach (Booking bookingObj in bookings)
            {
                ListViewItem item = new ListViewItem(new string[] {
                        bookingObj.Id.ToString(),
                        bookingObj.GuestName,
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
                    });
                ctrBookingList.Items.Add(item);
            }

            //************************
             
            // Populate the Tour selection combo box.
            TourDate tourDate = new TourDate();
            ArrayList tourDates = tourDate.getAllTourDates();

            // Convert the ArrayList to a List of objects
            List<TourDate> tourDatesList = tourDates.Cast<TourDate>().ToList();

            ctrTourSelect.DataSource = tourDatesList;
            ctrTourSelect.DisplayMember = "Date";
            ctrTourSelect.ValueMember = "Id";

            // concat the tour date to the ship name
            ctrTourSelect.Format += (s, ev) => {
                {
                    int index = ctrTourSelect.Items.IndexOf(ev.ListItem);
                    ev.Value = DateTime.Parse(ev.Value.ToString()).ToString("dd/MM/yyyy") + " - " + tourDatesList[index].ShipName;
                }
            };

            // ensure that no items are selected on initial load.
            ctrTourSelect.SelectedIndex = -1;

            //************************

            // Populate the CabinID comboBox.

            Cabin cabin = new Cabin();
            ArrayList cabins = cabin.getAllCabins();

            // Convert the ArrayList to a List of objects
            List<Cabin> CabinList = cabins.Cast<Cabin>().ToList();

            //Populate Cabin Combo Box
            ctrCabinSelect.DataSource = CabinList;
            ctrCabinSelect.DisplayMember = "Id";
            ctrCabinSelect.ValueMember = "Id";

            // concat the tour date to the ship name
            ctrCabinSelect.Format += (s, ev) => {
                {
                    int index = ctrCabinSelect.Items.IndexOf(ev.ListItem);
                    ev.Value = CabinList[index].ShipName + " - " + ev.Value.ToString();
                }
            };

            // ensure that no items are selected on initial load.
            ctrCabinSelect.SelectedIndex = -1;

            //************************

            // Populate the CabinClass comboBox.

            CabinClass cabinClass = new CabinClass();
            ArrayList cabinClasses = cabinClass.GetAllCabinClasses();

            // Convert the ArrayList to a List of objects
            List<CabinClass> CabinClassList = cabinClasses.Cast<CabinClass>().ToList();

            //Populate Cabin Combo Box
            ctrClassSelect.DataSource = CabinClassList;
            ctrClassSelect.DisplayMember = "Name";
            ctrClassSelect.ValueMember = "Id";

            // ensure that no items are selected on initial load.
            ctrClassSelect.SelectedIndex = -1;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // clear existing items
            ctrBookingList.Items.Clear();

            // get the filter values
            int bookingId;
            int tourId;
            int cabinId;
            int cabinClassId;
            string surname = ctrGuestNameInput.Text;
            string email = ctrGuestEmailInput.Text;
            string status = ctrBookingStatusSelect.Text != null ? ctrBookingStatusSelect.Text : "";
            string financeStatus = ctrBookingFinanceStatusSelect.Text != null ? ctrBookingFinanceStatusSelect.Text.ToString() : "";

            try
            {
                bookingId = int.Parse(ctrBookingIdInput.Text);
            }
            catch
            {
                bookingId = 0;
            }

            try
            {
                string value = ctrTourSelect.SelectedValue != null ? ctrTourSelect.SelectedValue.ToString() : "0";
                tourId = Int32.Parse(value);
            }
            catch
            {
                tourId = 0;
            }

            try
            {
                string value = ctrCabinSelect.SelectedValue != null ? ctrCabinSelect.SelectedValue.ToString() : "0";
                cabinId = Int32.Parse(value);
            }
            catch
            {
                cabinId = 0;
            }

            try
            {
                string value = ctrClassSelect.SelectedValue != null ? ctrClassSelect.SelectedValue.ToString() : "0";
                cabinClassId = Int32.Parse(value);
            }
            catch
            {
                cabinClassId = 0;
            }

            // Instantiate
            Booking booking = new Booking();
            
            // Pass the filters to the GetFilteredBookings method
            ArrayList bookings = booking.GetFilteredBookings(tourId, cabinId, cabinClassId, surname, email, status, financeStatus, bookingId);

            // If nothing is returned, display no bookings message. 
            if (bookings.Count == 0)
            {
                ctrBookingList.Items.Add("No Bookings");
                return;
            }
            //otherwise, display each returned booking in the listView
            foreach (Booking bookingObj in bookings)
            {
                ListViewItem item = new ListViewItem(new string[] {
                        bookingObj.Id.ToString(),
                        bookingObj.GuestName.ToString(),
                        bookingObj.CreatedAt.ToString(),
                        bookingObj.CancelledAt.ToString() != "01/01/0001 00:00:00"  ? bookingObj.CancelledAt.ToString() : "",
                        bookingObj.TourDate != null ? bookingObj.TourDate.ToString() != "01/01/0001 00:00:00" ? DateTime.Parse(bookingObj.TourDate.ToString()).ToString("dd/MM/yyyy") : "" : "",
                        bookingObj.Ship != null ? bookingObj.Ship.ToString() : "",
                        bookingObj.Cabin != 0 ? bookingObj.Cabin.ToString() : "",
                        bookingObj.CabinClass != null ? bookingObj.CabinClass.ToString() : "",
                        "£" + bookingObj.Rate.ToString(),
                        "£" + bookingObj.PaymentTotal.ToString(),
                        // calculate balance
                        "£" + (bookingObj.Rate - bookingObj.PaymentTotal)
                    }); ;
                ctrBookingList.Items.Add(item);
            }
        }

        // The following methods are bound to the field reset buttons.
        private void btnResetTour_Click(object sender, EventArgs e)
        {
            ctrTourSelect.SelectedIndex = -1;
        }

        private void btnResetCabin_Click(object sender, EventArgs e)
        {
            ctrCabinSelect.SelectedIndex = -1;
        }

        private void btnResetClass_Click(object sender, EventArgs e)
        {
            ctrClassSelect.SelectedIndex = -1;
        }

        private void btnResetSurname_Click(object sender, EventArgs e)
        {
            ctrGuestNameInput.Text = "";
        }

        private void btnResetEmail_Click(object sender, EventArgs e)
        {
            ctrGuestEmailInput.Text = "";
        }

        private void btnResetStatus_Click(object sender, EventArgs e)
        {
            ctrBookingStatusSelect.SelectedIndex = -1;
        }

        private void btnResetFinStatus_Click(object sender, EventArgs e)
        {
            ctrBookingFinanceStatusSelect.SelectedIndex = -1;
        }

        private void btnResetBookingId_Click(object sender, EventArgs e)
        {
            ctrBookingIdInput.Text = "";
        }

        // The following method is bound to the resetAll button
        private void btnResetAll_Click(object sender, EventArgs e)
        {
            ctrTourSelect.SelectedIndex = -1;
            ctrCabinSelect.SelectedIndex = -1;
            ctrClassSelect.SelectedIndex = -1;
            ctrGuestNameInput.Text = "";
            ctrGuestEmailInput.Text = "";
            ctrBookingStatusSelect.Text = "";
            ctrBookingFinanceStatusSelect.Text = "";
            ctrBookingIdInput.Text = "";

            ctrBookingList.Items.Clear();

            // Reset the list
            Booking booking = new Booking();
            ArrayList bookings = booking.GetAllBookings();

            if (bookings.Count == 0)
            {
                ctrBookingList.Items.Add("No Bookings");
                return;
            }
            foreach (Booking bookingObj in bookings)
            {
                ListViewItem item = new ListViewItem(new string[] {
                        bookingObj.Id.ToString(),
                        bookingObj.GuestName,
                        bookingObj.CreatedAt.ToString(),
                        bookingObj.CancelledAt.ToString() != "01/01/0001 00:00:00"  ? bookingObj.CancelledAt.ToString() : "",
                        bookingObj.TourDate != null ? bookingObj.TourDate.ToString() != "01/01/0001 00:00:00" ? DateTime.Parse(bookingObj.TourDate.ToString()).ToString("dd/MM/yyyy") : "" : "",
                        bookingObj.Ship != null ? bookingObj.Ship.ToString() : "",
                        bookingObj.Cabin != 0 ? bookingObj.Cabin.ToString() : "",
                        bookingObj.CabinClass != null ? bookingObj.CabinClass.ToString() : "",
                        "£" + bookingObj.Rate.ToString(),
                        "£" + bookingObj.PaymentTotal.ToString(),
                        // calculate balance
                        "£" + (bookingObj.Rate - bookingObj.PaymentTotal)
                    });
                ctrBookingList.Items.Add(item);
            }
        }

        // Fired when a booking is selected.
        private void ctrBookingList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Check that an item has been clicked (not just whitespace)
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

        // Close button.
        private void btnCloseRed_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
