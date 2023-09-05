using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;
using System.Net;
using System.Text.RegularExpressions;
using MyPaymentProcessor;

namespace CruiseBookingSystem
{
    public partial class frmNewBooking : Form
    {
        // the currently active tab.
        private int m_tabsActiveIndex = 0;
        // whether the guest search pane is open.
        private bool m_searchPaneOpen = false;
        // the guest id selected (or created) in step 1
        private int m_guestId;
        // the slot selected in step two.
        private string m_slotId;
        // a variable to hold the number of occupants
        private int m_occupants;
        // to store the booking value as that is calculated
        private Double m_bookingValue = 0.0;
        // a variable to hold the created booking which can then be displayed on the summary page.
        private Booking m_booking;

        public frmNewBooking()
        {
            InitializeComponent();
        }

        // allow the active tab index to be controlled by another form.
        public int TabsActiveIndex
        {
            set
            {
                m_tabsActiveIndex = value;
                tabContent.SelectedIndex = value;
            }
        }

        // this is only ever updated from the dialogue box, so no getter defined. 
        public int Occupants
        {
            set
            {
                m_occupants = value;
            }
        }

        public string SlotId
        {
            get { return m_slotId; }
            set { m_slotId = value; }
        }

        // Load logic
        private void frmNewBooking_Load(object sender, EventArgs e)
        {
            // **************************
            // Tour Dates Load

            TourDate tourDate = new TourDate();
            ArrayList tourDates = tourDate.getFilteredTourDate("FUTURE");

            if (tourDates.Count == 0)
            {
                ctrTourSelect.Items.Add("No tours returned");
                return;
            }

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

            // **************************
            // Cabin Class Load

            CabinClass cabinClass = new CabinClass();
            ArrayList classes = cabinClass.GetAllCabinClasses();

            if (classes.Count == 0)
            {
                ctrTourSelect.Items.Add("No classes returned");
                return;
            }

            // Convert the ArrayList to a List of objects
            List<CabinClass> classList = classes.Cast<CabinClass>().ToList();

            ctrClassSelect.DataSource = classList;
            ctrClassSelect.DisplayMember = "Name";
            ctrClassSelect.ValueMember = "Id";

            // ensure that no items are selected on initial load.
            ctrClassSelect.SelectedIndex = -1;

        }

        // ------------------
        // Helpers

        // A helper which calculates the single occupancy rate
        private Double GenerateSingleOccRate(double rate, double discountPercentage)
        {
            Double discount = rate / 100.0 * discountPercentage;
            return rate - discount;
        }

        // handle the load of the payment tab.
        private void OnPaymentTabLoad()
        {
            // get the rate of the slot 
            CabinBookingTourDateMap slot = new CabinBookingTourDateMap();
            slot.GetSlotById(Int32.Parse(m_slotId));

            // start the rate at the full rate
            Double rate = slot.Rate;

            // if single occ, apply discount
            if (m_occupants == 1)
            {
                Double discountPercentage = slot.SingleOccupancyDiscount;
                rate = GenerateSingleOccRate(rate, discountPercentage);
            }

            // commit the rate to a local variable for use later.
            m_bookingValue = rate;

            // place the calculated rate in the label that is there to display it. 
            lblValue.Text = "£" + m_bookingValue.ToString("0.00");
        }

        // handle the summary tab load.
        private void OnSummaryTabLoad()
        {
            // populate the labels with the created booking details.
            lblTourData.Text = DateTime.Parse(m_booking.TourDate.ToString()).ToString("dd/MM/yyyy");
            lblShipData.Text = m_booking.Ship;
            lblCabinData.Text = m_booking.Cabin.ToString();
            lblCabinClassData.Text = m_booking.CabinClass;
            lblPriceData.Text = "£" + m_booking.Rate.ToString("0.00");
            lblPaidData.Text = "£" + m_booking.PaymentTotal.ToString("0.00");
            lblBalanceData.Text = "£" + (m_booking.Rate - m_booking.PaymentTotal).ToString("0.00");
        }

        // when the tab changes.
        private void tabContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if the user has navigated there manually, put them in their place and switch back to the intended stage. 
            if (tabContent.SelectedIndex != m_tabsActiveIndex)
            {
                tabContent.SelectedIndex = m_tabsActiveIndex;
                MessageBox.Show("Please navigate through the booking process using the 'Next' buttons at the foot of each form.", "Incomplete Tab", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            //if we're legitimately on the payment tab, load that tab.
            if (tabContent.SelectedIndex == 2)
            {
                OnPaymentTabLoad();
            }

            //if we're legitimately on the summary tab, load that tab.
            if (tabContent.SelectedIndex == 3)
            {
                OnSummaryTabLoad();
            }
        }

        // ------------------
        // Guest Details Tab

        // the following two methods control the guest search placeholder.
        private void ctrGuestSearch_Enter(object sender, EventArgs e)
        {
            if (ctrGuestSearch.Text == "Search for a surname, email address or postcode.")
            {
                ctrGuestSearch.Text = "";
            }
        }

        private void ctrGuestSearch_Leave(object sender, EventArgs e)
        {
            if (ctrGuestSearch.Text == "")
            {
                ctrGuestSearch.Text = "Search for a surname, email address or postcode.";
            }
        }

        // this controls the guest search logic.
        private void ctrGuestSearch_TextChanged(object sender, EventArgs e)
        {
            // get the search string string.
            string searchString = ctrGuestSearch.Text;

            // if the string is the placeholder, or empty, do nothing. 
            if (searchString == "Search for a surname, email address or postcode." || searchString == "")
            {
                return;
            }

            // get the guest by the search criteria passed as OR to each searchable field.
            Guest guest = new Guest();
            ArrayList guests = guest.GetGuestsBySearchCriteria(searchString, searchString, searchString, "OR");

            // toggle the local variable which monitors whether the guest search pane is open.
            m_searchPaneOpen = true;
            // expose the guest list. 
            ctrGuestList.Visible = true;
            // clear it.
            ctrGuestList.Items.Clear();
            // if no guests are returned, display no guest error.
            if (guests.Count == 0)
            {
                ctrGuestList.Items.Add("No Existing Guests match that search string");
                return;
            }
            // otherwise, populate the list. 
            foreach (Guest guestObj in guests)
            {
                ListViewItem item = new ListViewItem(new string[] { guestObj.Id.ToString(), guestObj.FirstName + " " + guestObj.LastName, guestObj.Postcode, guestObj.Email, guestObj.Phone, });
                ctrGuestList.Items.Add(item);
            }
        }

        // when a guest is selected from the search result list. 
        private void ctrGuestList_MouseClick(object sender, MouseEventArgs e)
        {
            //check for a hit
            ListViewHitTestInfo info = ctrGuestList.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            if (item != null)
            {
                // get the selected Id
                int selectedGuestId = Int32.Parse(item.Text);
                // reset the search box
                ctrGuestSearch.Text = "Search for a surname, email address or postcode.";
                // reset the listView
                ctrGuestList.Items.Clear();
                ctrGuestList.Visible = false;

                // get the guest details.
                Guest guest = new Guest();
                guest.GetGuestById(selectedGuestId);


                // populate and disable the fields
                ctrFirstName.Text = guest.FirstName;
                ctrFirstName.Enabled = false;

                ctrLastName.Text = guest.LastName;
                ctrLastName.Enabled = false;

                ctrEmail.Text = guest.Email;
                ctrEmail.Enabled = false;

                ctrPhone.Text = guest.Phone;
                ctrPhone.Enabled = false;

                ctrStreetAddress.Text = guest.StreetAddress;
                ctrStreetAddress.Enabled = false;

                ctrTownCity.Text = guest.TownCity;
                ctrTownCity.Enabled = false;

                ctrCounty.Text = guest.County;
                ctrCounty.Enabled = false;

                ctrCountry.Text = guest.Country;
                ctrCountry.Enabled = false;

                ctrPostcode.Text = guest.Postcode;
                ctrPostcode.Enabled = false;

                btnCreateGuest.Visible = false;

                // record the guest Id
                m_guestId = selectedGuestId;

                // enable the next button
                btnNext1.Enabled = true;
            }
            // if no hit, clear the selected items. 
            else
            {
                this.ctrGuestList.SelectedItems.Clear();
            }
        }

        // handler for the createGuest button
        private void btnCreateGuest_Click(object sender, EventArgs e)
        {
            // get the field values
            string firstName = ctrFirstName.Text;
            string lastName = ctrLastName.Text;
            string email = ctrEmail.Text;
            string phone = ctrPhone.Text;
            string streetAddress = ctrStreetAddress.Text;
            string townCity = ctrTownCity.Text;
            string county = ctrCounty.Text;
            string country = ctrCountry.Text;
            string postcode = ctrPostcode.Text;

            // check whether any of the fields are incomplete
            if (firstName == "" || lastName == "" || email == "" || phone == "" || streetAddress == "" || townCity == "" || county == "" || country == "" || postcode == "")
            {
                MessageBox.Show("Please complete all fields.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // regex string from https://stackoverflow.com/questions/16167983/best-regular-expression-for-email-validation-in-c-sharp 
            Regex emailRegex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
            if (!Regex.IsMatch(email, emailRegex.ToString(), RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Email address is not valid.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // create the new guest record.  
            Guest guest = new Guest();
            int createdId = guest.CreateNewGuest(firstName, lastName, email, phone, streetAddress, townCity, county, country, postcode);
            // ocmmit the created guest ID to local variable. 
            m_guestId = createdId;

            // disable the fields
            ctrFirstName.Enabled = false;
            ctrLastName.Enabled = false;
            ctrEmail.Enabled = false;
            ctrPhone.Enabled = false;
            ctrStreetAddress.Enabled = false;
            ctrTownCity.Enabled = false;
            ctrCounty.Enabled = false;
            ctrCountry.Enabled = false;
            ctrPostcode.Enabled = false;

            // remove the create guest button.
            btnCreateGuest.Visible = false;
            // enable the next button. 
            btnNext1.Enabled = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            m_tabsActiveIndex++;
            tabContent.SelectedIndex = m_tabsActiveIndex;
        }

        // ------------------
        // Booking Details Tab

        // handler for the update button in the booking select tab.
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // clear the existing content of the ListView
            ctrSlotList.Items.Clear();

            // get the currently selected values.
            string tourIdValue = ctrTourSelect.SelectedValue != null ? ctrTourSelect.SelectedValue.ToString() : "0";
            int tourId = Int32.Parse(tourIdValue);
            string classIdValue = ctrClassSelect.SelectedValue != null ? ctrClassSelect.SelectedValue.ToString() : "0";
            int classId = Int32.Parse(classIdValue);

            // search for available slots.
            CabinBookingTourDateMap cabinBookingTourDateMap = new CabinBookingTourDateMap();
            ArrayList slots = cabinBookingTourDateMap.GetAvailableSlots(tourId, classId);

            // populare the ListView with available slots. 
            if (slots.Count == 0)
            {
                ctrSlotList.Items.Add("No Availability");
                return;
            }
            foreach (CabinBookingTourDateMap slotObj in slots)
            {
                ListViewItem item = new ListViewItem(new string[] {
                        slotObj.Id.ToString(),
                        slotObj.CabinId.ToString(),
                        slotObj.ClassName.ToString(),
                        "£" + slotObj.Rate.ToString("0.00"),
                        "£" + GenerateSingleOccRate(slotObj.Rate, slotObj.SingleOccupancyDiscount).ToString("0.00")
                    });
                ctrSlotList.Items.Add(item);
            }
        }

        // handle the selection of a slot.
        private void ctrSlotList_MouseClick(object sender, MouseEventArgs e)
        {
            // first, clear the local slot array
            if (m_slotId != null)
            {
                m_slotId = "";
            }

            // then, commit the slot Id to the local variable
            m_slotId = ctrSlotList.SelectedItems[0].Text;
            
            // if slots have been selected, enable the next button.  
            if (m_slotId != null && m_slotId != "")
            {
                btnNext2.Enabled = true;
            }
            else
            {
                btnNext2.Enabled = false;
            }
        }

        private void btnNext2_Click(object sender, EventArgs e)
        {
            // load cabin details as a dialogue
            frmCabinDetails frmCabinDetails = new frmCabinDetails(this);
            frmCabinDetails.ShowDialog();
        }

        // ---------------------
        // Payment Tab

        // placeholder add and remove functions follow.
        private void ctrCardNumber_Enter(object sender, EventArgs e)
        {
            if (ctrCardNumber.Text == "e.g. 1234-5678-1234-5678")
            {
                ctrCardNumber.Text = "";
            }
        }

        private void ctrCardNumber_Leave(object sender, EventArgs e)
        {
            if (ctrCardNumber.Text == "")
            {
                ctrCardNumber.Text = "e.g. 1234-5678-1234-5678";
            }

            // this part detects a card number in 1111111111111111 format and converts it to 1111-1111-1111-1111
            Regex regex = new Regex(@"\d{16}");
            if (Regex.IsMatch(ctrCardNumber.Text, regex.ToString())) 
            {
                ctrCardNumber.Text = Regex.Replace(ctrCardNumber.Text, @"(\d{4})(\d{4})(\d{4})(\d{4})", "$1-$2-$3-$4");
            }
        }

        private void ctrExpDate_Enter(object sender, EventArgs e)
        {
            if (ctrExpDate.Text == "e.g. 01/21")
            {
                ctrExpDate.Text = "";
            }
        }

        private void ctrExpDate_Leave(object sender, EventArgs e)
        {
            if (ctrExpDate.Text == "")
            {
                ctrExpDate.Text = "e.g. 01/21";
            }
            // this part detects an expiry date 0123 format and converts it to 01/23
            Regex regex = new Regex(@"\d{4}");
            if (Regex.IsMatch(ctrExpDate.Text, regex.ToString()))
            {
                ctrExpDate.Text = Regex.Replace(ctrExpDate.Text, @"(\d{2})(\d{2})", "$1/$2");
            }
        }

        private void ctrCVC_Enter(object sender, EventArgs e)
        {
            if (ctrCVC.Text == "e.g. 000")
            {
                ctrCVC.Text = "";
            }
        }

        private void ctrCVC_Leave(object sender, EventArgs e)
        {
            if (ctrCVC.Text == "")
            {
                ctrCVC.Text = "e.g. 000";
            }
        }

        // this function handles the payment process.  
        private void btnMakeBooking_Click(object sender, EventArgs e)
        {
            // VALIDATION STARTS HERE
            //get the field values
            string cardType = ctrCardType.SelectedItem != null ? ctrCardType.SelectedItem.ToString() : "";
            string cardNumber = ctrCardNumber.Text;
            string cardExp = ctrExpDate.Text;
            string cardCvc = ctrCVC.Text;

            //start true
            bool valid = true;

            // check that the fields all have content.
            if (cardType == "" || cardType == null)
            {
                valid = false;
            }
            if (cardNumber == "e.g. 1234-5678-1234-5678")
            {
                valid = false;
            }
            if (cardExp == "e.g. 01/21")
            {
                valid = false;
            }
            if (cardCvc == "e.g. 000")
            {
                valid = false;
            }

            if (!valid)
            {
                MessageBox.Show("Fields are invalid, try again", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // check the validity of the content. 
            // I used chatGPT to produce these
            string cardPattern = @"^\d{4}-\d{4}-\d{4}-\d{4}$"; 
            string expPattern = @"^(0[1-9]|1[0-2])\/\d{2}$";
            string cvcPattern = @"^\d{3}$";

            bool cardNumberValid = Regex.IsMatch(cardNumber, cardPattern);
            bool expDateValid = Regex.IsMatch(cardExp, expPattern);
            bool cardExpValid = Regex.IsMatch(cardCvc, cvcPattern);

            if (!cardNumberValid)
            {
                MessageBox.Show("The card number you have entered is invalid.  Please try again.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!expDateValid)
            {
                MessageBox.Show("The expiry date you have entered is invalid.  Please try again.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!cardExpValid)
            {
                MessageBox.Show("The CVC code that you have entered is invalid.  Please try again.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // if we've got here, we have valid content in the fields.  Last thing to check is that the expiry date hasn't passed.
            if (expDateValid)
            {
                DateTime date = DateTime.ParseExact(cardExp, "MM/yy", null);
                int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
                DateTime lastDayOfMonth = new DateTime(date.Year, date.Month, daysInMonth);
                bool hasLastDayPassed = DateTime.Now > lastDayOfMonth;
                if (hasLastDayPassed)
                {
                    expDateValid = false;
                }
            }

            if (!expDateValid)
            {
                MessageBox.Show("The expiry date of that card has passed.  Please try again.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // VALIDITY ENDS HERE
            
            // SUBMISSION STARTS HERE

            // if we've got to here, everything in the form is valid, so we can process the payment.
            int response = CruisePaymentDll.ProcessPayment();

            // handle payment failures
            if (response != 0) {
                if (response == 1)
                {
                    MessageBox.Show("We've tried to take the payment but it failed due to insufficient funds.  Please try again.", "Payment Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (response == 2)
                {
                    MessageBox.Show("We've tried to take the payment but it failed as the card issuer has locked the card.  Please try again.", "Payment Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // now we're processed the payment, we need to make the booking.  We need to catch any DB failures here.
            Booking booking = new Booking();
           
            int createBookingResponse = booking.CreateBooking(m_guestId, m_slotId, m_bookingValue);
            // handle booking creation failure. 
            while (createBookingResponse != 0)
            {
                // keep trying until creating succeeds. 
                createBookingResponse = booking.CreateBooking(m_guestId, m_slotId, m_bookingValue);
            }


            // give the created booking to a local variable.
            m_booking = booking;

            // when booking is made successfully, switch to the last tab
            TabsActiveIndex = 3;
        }

        // this is the button on the summary page.  Thought it best to place one in the same location as the previous 'Next' buttons for reasons of HCI.
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // main close button.
        private void btnCloseRed_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // if anywhere else on the form receives a click, close the guest search results list.
        private void closeSearchResultPaneIfOpen(object sender, EventArgs e)
        {
            if (m_searchPaneOpen)
            {
                m_searchPaneOpen = false;
                ctrGuestList.Visible = false;
            }
        }
    }
}
