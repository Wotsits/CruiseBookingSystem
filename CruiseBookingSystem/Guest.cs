using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.Data.SqlClient;
using System.Data;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Drawing.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace CruiseBookingSystem
{
    class Guest
    {
        private int m_Id;
        private string m_FirstName;
        private string m_LastName;
        private string m_StreetAddress;
        private string m_TownCity;
        private string m_County;
        private string m_Country;
        private string m_Postcode;
        private string m_Phone;
        private string m_Email;

        public int Id {
            get
            {
                return m_Id;
            }

            set
            {
                m_Id = value;
            }
        }
        public string FirstName {
            get
            {
                return m_FirstName;
            }

            set
            {
                m_FirstName = value;
            }
        }
        public string LastName {
            get
            {
                return m_LastName;
            }

            set
            {
                m_LastName = value;
            }
        }
        public string StreetAddress {
            get
            {
                return m_StreetAddress;
            }

            set
            {
                m_StreetAddress = value;
            }
        }
        public string TownCity {
            get
            {
                return m_TownCity;
            }

            set
            {
                m_TownCity = value;
            }
        }
        public string County {
            get
            {
                return m_County;
            }

            set
            {
                m_County = value;
            }
        }
        public string Country {
            get
            {
                return m_Country;
            }

            set
            {
                m_Country = value;
            }
        }
        public string Postcode {
            get
            {
                return m_Postcode;
            }

            set
            {
                m_Postcode = value;
            }
        }
        public string Phone {
            get
            {
                return m_Phone;
            }

            set
            {
                m_Phone = value;
            }
        }
        public string Email {
            get
            {
                return m_Email;
            }

            set
            {
                m_Email = value;
            }
        }

        //For a given id, a Guest object is retrieved.
        public void GetGuestById(int id)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand getGuestById = new SqlCommand();
            getGuestById.Connection = conn;
            getGuestById.CommandType = CommandType.Text;
            getGuestById.CommandText = "Select id, firstName, lastName, streetAddress, townCity, county, country, postcode, phone, email from tblGuest where id = '" + id + "'";
            SqlDataReader guestResults = getGuestById.ExecuteReader();
            if (!guestResults.HasRows)
            {
                throw new Exception("Guest Not Found");
            }
            else
            {
                guestResults.Read();
                m_Id = (int)guestResults[0];
                m_FirstName = guestResults[1].ToString();
                m_LastName = guestResults[2].ToString();
                m_StreetAddress = guestResults[3].ToString();
                m_TownCity = guestResults[4].ToString();
                m_County = guestResults[5].ToString();
                m_Country = guestResults[6].ToString();
                m_Postcode = guestResults[7].ToString();
                m_Phone = guestResults[8].ToString();
                m_Email = guestResults[9].ToString();
            }
            conn.Close();
        }

        //An ArrayList of all guest objects is returned
        public ArrayList GetAllGuests()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand getGuests = new SqlCommand();
            getGuests.Connection = conn;
            getGuests.CommandType = CommandType.Text;
            getGuests.CommandText = "Select id, firstName, lastName, streetAddress, townCity, county, country, postcode, phone, email from tblGuest";
            SqlDataReader guestList = getGuests.ExecuteReader();

            ArrayList GuestList = new ArrayList();

            while (guestList.Read())
            {
                Guest x = new Guest();
                x.m_Id = (int)guestList[0];
                x.m_FirstName = (string)guestList[1];
                x.m_LastName = (string)guestList[2];
                x.m_StreetAddress = (string)guestList[3];
                x.m_TownCity = (string)guestList[4];
                x.m_County = (string)guestList[5];
                x.m_Country = (string)guestList[6];
                x.m_Postcode = (string)guestList[7];
                x.m_Phone = (string)guestList[8];
                x.m_Email = (string)guestList[9];
                GuestList.Add(x);
            }
            guestList.Close();
            conn.Close();
            return GuestList;
        }

        //the andOr param passed in allows this function to build the WHERE as either and'd or or'd.  Default AND.
        private string BuildWhereClause(string name, string email, string postcode, string andOr)
        {
            string whereClause = "";
            
            //UPPER and LIKE used here to make search case insensitive.
            if (name != "")
            {
                whereClause = whereClause + " UPPER(lastName) LIKE UPPER('" + name + "')";
            }
            if (name != "" && (email != "" || postcode != ""))
            {
                whereClause = whereClause + " " + andOr + " ";
            }
            if (email != "")
            {
                whereClause = whereClause + " UPPER(email) LIKE UPPER('" + email + "')";
            }
            if (email != "" && postcode != "")
            {
                whereClause = whereClause + " " + andOr + " ";
            }
            if (postcode != "")
            {
                whereClause = whereClause + " UPPER(postcode) LIKE UPPER('" + postcode + "')";
            }
            return whereClause;
        }

        //An ArrayList of guest objects is returned which match the search criteria submitted as params to the function.
        public ArrayList GetGuestsBySearchCriteria(string name, string email, string postcode, string andOr)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand getGuests = new SqlCommand();
            getGuests.Connection = conn;
            getGuests.CommandType = CommandType.Text;
            getGuests.CommandText = "Select id, firstName, lastName, streetAddress, townCity, county, country, postcode, phone, email from tblGuest WHERE" + BuildWhereClause(name, email, postcode, andOr) + ";";
            SqlDataReader guestList = getGuests.ExecuteReader();

            ArrayList GuestList = new ArrayList();

            while (guestList.Read())
            {
                Guest x = new Guest();
                x.m_Id = (int)guestList[0];
                x.m_FirstName = (string)guestList[1];
                x.m_LastName = (string)guestList[2];
                x.m_StreetAddress = (string)guestList[3];
                x.m_TownCity = (string)guestList[4];
                x.m_County = (string)guestList[5];
                x.m_Country = (string)guestList[6];
                x.m_Postcode = (string)guestList[7];
                x.m_Phone = (string)guestList[8];
                x.m_Email = (string)guestList[9];
                GuestList.Add(x);
            }
            guestList.Close();
            conn.Close();
            return GuestList;
        }

        // Create a new guest in the DB
        public int CreateNewGuest(string firstName, string lastName, string email, string phone, string streetAddress, string townCity, string county, string country, string postcode)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand createGuest = new SqlCommand();
            createGuest.Connection = conn;
            createGuest.CommandType = CommandType.Text;
            createGuest.CommandText = String.Format("INSERT INTO tblGuest VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')", firstName, lastName, streetAddress, townCity, county, country, postcode, phone, email);
            Console.WriteLine(createGuest.CommandText);
            int response = createGuest.ExecuteNonQuery();

            // success
            if (response > 0) 
            {
                createGuest.CommandText = "SELECT max(id) FROM tblGuest";
                SqlDataReader createdId = createGuest.ExecuteReader();
                createdId.Read();
                int newGuestId = createdId.GetInt32(0);
                createdId.Close();
                return newGuestId;
            }
            else return 0;
        }
    }
}
