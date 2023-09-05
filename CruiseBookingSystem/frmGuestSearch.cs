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
    public partial class frmGuestSearch : Form
    {
        public frmGuestSearch()
        {
            InitializeComponent();
        }

        // on load
        private void frmGuestSearch_Load(object sender, EventArgs e)
        {
            // set the width of the search results columns to spread evenly across the listView.
            int parentWidth = ctrSearchResults.Width;
            for (int i = 0; i < ctrSearchResults.Columns.Count; i++)
            {
                ctrSearchResults.Columns[i].Width = parentWidth / 10;
            }
            Guest guest = new Guest();
            ArrayList guests = guest.GetAllGuests();

            if (guests.Count == 0)
            {
                ctrSearchResults.Items.Add("No Guests");
                return;
            }
            foreach (Guest guestObj in guests)
            {
                ListViewItem item = new ListViewItem(new string[] { guestObj.Id.ToString(), guestObj.FirstName.ToString(), guestObj.LastName, guestObj.StreetAddress, guestObj.TownCity, guestObj.County, guestObj.Country, guestObj.Postcode, guestObj.Phone, guestObj.Email });
                ctrSearchResults.Items.Add(item);
            }
        }

        // on click of the search button
        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            //get the values form the input boxes
            string name = ctrName.Text;
            string email = ctrEmail.Text;
            string postcode = ctrPostcode.Text;

            // if there nothing in the input boxes, display a dialogue box
            if (name == "" && email == "" && postcode == "")
            {
                MessageBox.Show("You need to enter some search critera first.", "Missing Search Criteria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //clear the results box.
            ctrSearchResults.Items.Clear();

            Guest guest = new Guest();
            ArrayList guests = guest.GetGuestsBySearchCriteria(name, email, postcode, "AND");

            // handle no results.
            if (guests.Count == 0)
            {
                ctrSearchResults.Items.Add("No guests match those criteria");
                return;
            }
            // populate the list.
            foreach (Guest guestObj in guests)
            {
                ListViewItem item = new ListViewItem(new string[] { guestObj.Id.ToString(), guestObj.FirstName.ToString(), guestObj.LastName, guestObj.StreetAddress, guestObj.TownCity, guestObj.County, guestObj.Country, guestObj.Postcode, guestObj.Phone, guestObj.Email });
                ctrSearchResults.Items.Add(item);
            }
        }

        // on selection of an item in the list.
        private void ctrSearchResults_MouseDoubleClick(object sender, MouseEventArgs e)
        {   
            // check for a hit on an item, not just empty whitespace
            ListViewHitTestInfo info = ctrSearchResults.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            // if there's a hit, open the guest form, passing in the clicked item.
            if (item != null)
            {
                Form frmExistingGuest = new frmExistingGuest(item.Text);
                frmExistingGuest.ShowDialog();
            }
            // otherwise, clear the selection
            else
            {
                this.ctrSearchResults.SelectedItems.Clear();
                MessageBox.Show("No Item is selected");
            }
        }

        // close button
        private void btnCloseRed_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
