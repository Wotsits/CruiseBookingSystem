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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CruiseBookingSystem
{
    class Booking
    {
        private int m_Id;
        private DateTime m_CreatedAt;
        private DateTime m_CancelledAt;
        private DateTime m_TourDate;
        private string m_Ship;
        private int m_Cabin;
        private string m_CabinClass;
        private Double m_Rate;
        private Double m_PaymentTotal;
        private string m_GuestName;
        private string m_GuestPhone;
        private string m_GuestEmail;

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
        public DateTime CreatedAt {
            get
            {
                return m_CreatedAt;
            }
            set
            {
                m_CreatedAt = value;
            }
        }
        public DateTime CancelledAt {
            get
            {
                return m_CancelledAt;
            }
            set
            {
                 m_CancelledAt = value;
            }
        }
        public DateTime TourDate
        {
            get
            {
                return m_TourDate;
            }
            set
            {
                m_TourDate = value;
            }
        }
        public string Ship {
            get
            {
                return m_Ship;
            }
            set
            {
                m_Ship = value;
            }
        }
        public int Cabin {
            get
            {
                return m_Cabin;
            }
            set
            {
                m_Cabin = value;
            }
        }
        
        public string CabinClass {
            get
            {
                return m_CabinClass;
            }
            set
            {
                m_CabinClass = value;
            }
        }
        public Double Rate {
            get
            {
                return m_Rate;
            }
            set
            {
                m_Rate = value;
            }
        }
        public Double PaymentTotal {
            get
            {
                return m_PaymentTotal;
            }

            set
            {
                m_PaymentTotal = value;
            }
        }
        public string GuestName
        {
            get
            {
                return m_GuestName;
            }
            set
            {
                m_GuestName = value;
            }
        }
        public string GuestEmail
        {
            get
            {
                return m_GuestEmail;
            }
            set
            {
                m_GuestEmail = value;
            }
        }
        public string GuestPhone
        {
            get
            {
                return m_GuestPhone;
            }
            set
            {
                m_GuestPhone = value;
            }
        }

        //For a given id, a Booking object is retrieved.
        public void GetBookingById(int id)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand getBookingById = new SqlCommand();
            getBookingById.Connection = conn;
            getBookingById.CommandType = CommandType.Text;
            getBookingById.CommandText = "SELECT tblBooking.id, tblBooking.createdAt, tblBooking.cancelledAt, tblTourDate.date, tblShip.name, tblCabin.id, tblClass.name, tblBooking.value, SUM(tblPayment.value) AS PaymentValue, tblGuest.firstName, tblGuest.lastName, tblGuest.email, tblGuest.phone As totalPayment " +
                "FROM tblBooking " +
                "LEFT JOIN tblPayment ON tblBooking.id = tblPayment.bookingId " +
                "LEFT JOIN tblCabinBookingTourDateMap ON tblBooking.id = tblCabinBookingTourDateMap.bookingId " +
                "LEFT JOIN tblTourDate ON tblCabinBookingTourDateMap.tourDateId = tblTourDate.id " +
                "LEFT JOIN tblCabin ON tblCabinBookingTourDateMap.cabinId = tblCabin.id " +
                "LEFT JOIN tblClass ON tblCabin.classId = tblClass.id " +
                "LEFT JOIN tblShip ON tblTourDate.shipId = tblShip.id " +
                "LEFT JOIN tblGuest ON tblBooking.guestId = tblGuest.id " +
                "WHERE(tblCabinBookingTourDateMap.bookingId IS NULL OR(tblCabinBookingTourDateMap.bookingId IS NOT NULL)) " +
                "AND tblBooking.id='" + id + "' " +
                "GROUP BY tblBooking.id, tblBooking.guestId, tblBooking.createdAt, tblBooking.cancelledAt, tblTourDate.date, tblShip.name, tblCabin.id, tblClass.name, tblBooking.value, tblGuest.firstName, tblGuest.lastName, tblGuest.phone, tblGuest.email " +
                "ORDER BY tblBooking.createdAt";
            SqlDataReader bookingResults = getBookingById.ExecuteReader();
            if (!bookingResults.HasRows)
            {
                throw new Exception("Booking Not Found");
            }
            else
            {
                bookingResults.Read();
                m_Id = (int)bookingResults[0];
                m_CreatedAt = DateTime.Parse(bookingResults[1].ToString());
                // check for null before attempting to convert.
                if (!Convert.IsDBNull(bookingResults[2]))  // check if column is null;
                {
                    m_CancelledAt = bookingResults.GetDateTime(2);
                }
                if (!Convert.IsDBNull(bookingResults[3]))
                {
                    m_TourDate = bookingResults.GetDateTime(3);
                }
                if (!Convert.IsDBNull(bookingResults[4]))
                {
                    m_Ship = bookingResults[4].ToString();
                }
                if (!Convert.IsDBNull(bookingResults[5]))
                {
                    m_Cabin = (int)bookingResults[5];
                }
                if (!Convert.IsDBNull(bookingResults[6]))
                {
                    m_CabinClass = bookingResults[6].ToString();
                }
                m_Rate = bookingResults.GetDouble(7);
                if (!Convert.IsDBNull(bookingResults[8]))  // check if column is null;
                {
                    m_PaymentTotal = bookingResults.GetDouble(8);
                }
                else
                {
                    m_PaymentTotal = 0.0;
                }
                m_GuestName = bookingResults[9].ToString() + " " + bookingResults[10].ToString();
                m_GuestEmail = bookingResults[11].ToString();
                m_GuestPhone = bookingResults[12].ToString();
            }
            conn.Close();
        }

        //An ArrayList of all booking objects is returned
        public ArrayList GetAllBookings()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand getBookings = new SqlCommand();
            getBookings.Connection = conn;
            getBookings.CommandType = CommandType.Text;
            getBookings.CommandText = "SELECT tblBooking.id, tblBooking.createdAt, tblBooking.cancelledAt, tblTourDate.date, tblShip.name, tblCabin.id, tblClass.name, tblBooking.value, SUM(tblPayment.value) AS PaymentValue, tblGuest.firstName, tblGuest.lastName, tblGuest.email, tblGuest.phone As totalPayment " +
                "FROM tblBooking " + 
                "LEFT JOIN tblPayment ON tblBooking.id = tblPayment.bookingId " +
                "LEFT JOIN tblCabinBookingTourDateMap ON tblBooking.id = tblCabinBookingTourDateMap.bookingId " +
                "LEFT JOIN tblTourDate ON tblCabinBookingTourDateMap.tourDateId = tblTourDate.id " +
                "LEFT JOIN tblCabin ON tblCabinBookingTourDateMap.cabinId = tblCabin.id " +
                "LEFT JOIN tblClass ON tblCabin.classId = tblClass.id " +
                "LEFT JOIN tblShip ON tblTourDate.shipId = tblShip.id " +
                "LEFT JOIN tblGuest ON tblBooking.guestId = tblGuest.id " +
                "WHERE(tblCabinBookingTourDateMap.bookingId IS NULL OR(tblCabinBookingTourDateMap.bookingId IS NOT NULL)) " +
                "GROUP BY tblBooking.id, tblBooking.guestId, tblBooking.createdAt, tblBooking.cancelledAt, tblTourDate.date, tblShip.name, tblCabin.id, tblClass.name, tblBooking.value, tblGuest.firstName, tblGuest.lastName, tblGuest.phone, tblGuest.email " +
                "ORDER BY tblBooking.createdAt";
            SqlDataReader bookingResults = getBookings.ExecuteReader();

            ArrayList BookingList = new ArrayList();


            while (bookingResults.Read())
            {
                // The null value checks here (except the cancelledAt one) are because cancelled bookings still exist but they have been unassigned from tours and cabins etc.  Therefore, their returned values are null for may of the fields. 
                Booking x = new Booking();
                x.m_Id = (int)bookingResults[0];
                x.m_CreatedAt = DateTime.Parse(bookingResults[1].ToString());
                // check for null before attempting to convert.
                if (!Convert.IsDBNull(bookingResults[2]))  // check if column is null;
                {
                    x.m_CancelledAt = bookingResults.GetDateTime(2);
                }
                if (!Convert.IsDBNull(bookingResults[3])) {
                    x.m_TourDate = bookingResults.GetDateTime(3);
                }
                if (!Convert.IsDBNull(bookingResults[4]))
                {
                    x.m_Ship = bookingResults[4].ToString();
                }
                if (!Convert.IsDBNull(bookingResults[5]))
                {
                    x.m_Cabin = (int)bookingResults[5];
                }
                if (!Convert.IsDBNull(bookingResults[6]))
                {
                    x.m_CabinClass = bookingResults[6].ToString();
                }
                x.m_Rate = bookingResults.GetDouble(7);
                if (!Convert.IsDBNull(bookingResults[8]))  // check if column is null;
                {
                    x.m_PaymentTotal = bookingResults.GetDouble(8);
                }
                else
                {
                    x.m_PaymentTotal = 0.0;
                }
                
                x.m_GuestName = bookingResults[9].ToString() + " " + bookingResults[10].ToString();
                x.m_GuestEmail = bookingResults[11].ToString();
                x.m_GuestPhone = bookingResults[12].ToString();

                BookingList.Add(x);
            }
            bookingResults.Close();
            conn.Close();
            return BookingList;
        }

        // A method which builds out a WHERE clause to filter data based on the content of the booking filters. 
        private string BuildWhereClause(int tourId, int cabinId, int cabinClassId, string guestSurname, string guestEmail, string activeOrCancelled, int bookingId)
        {
            if (tourId == 0 && cabinId == 0 && cabinClassId == 0 && guestSurname == "" && guestEmail == "" && activeOrCancelled == "" && bookingId == 0)
            {
                return "";
            }
            string whereClause = "AND ";

            //each if block builds a statement and adds " AND " if there are more conditions to come. 
            if (tourId > 0)
            {
                whereClause += "tblTourDate.id = '" + tourId + "' ";
                if (cabinId > 0 || cabinClassId > 0 || guestSurname != "" || guestEmail != "" || activeOrCancelled != "" || bookingId > 0)
                {
                    whereClause += "AND ";
                }
            }
            if (cabinId > 0)
            {
                whereClause += "tblCabin.id = '" + cabinId + "' ";
                if (cabinClassId > 0 || guestSurname != "" || guestEmail != "" || activeOrCancelled != "" || bookingId > 0)
                {
                    whereClause += " AND ";
                }
            }
            if (cabinClassId > 0)
            {
                whereClause += "tblCabin.classId = '" + cabinClassId + "' ";
                if (guestSurname != "" || guestEmail != "" || activeOrCancelled != "" || bookingId > 0)
                {
                    whereClause += " AND ";
                }
            }
            if (guestSurname != "")
            {
                whereClause += "tblGuest.lastName = '" + guestSurname + "' ";
                if (guestEmail != "" || activeOrCancelled != "" || bookingId > 0)
                {
                    whereClause += " AND ";
                }
            }
            if (guestEmail != "")
            {
                whereClause += "tblGuest.email = '" + guestEmail + "' ";
                if (activeOrCancelled != "" || bookingId > 0)
                {
                    whereClause += " AND ";
                }
            }
            if (activeOrCancelled != "")
            {
                if (activeOrCancelled == "Active")
                {
                    whereClause += "tblBooking.cancelledAt IS NULL ";
                }
                else
                {
                    whereClause += "tblBooking.cancelledAt IS NOT NULL ";
                }
                if (bookingId > 0)
                {
                    whereClause += " AND ";
                }
            }
            if (bookingId != 0)
            {
                whereClause += "tblBooking.id='" + bookingId + "' ";
            }

            return whereClause;
        }

        // A method which builds a HAVING clause which ascertains whether the booking is in debit, credit or balance.
        private string BuildHavingClause(string paidOrCreditOrDebit)
        {
            if (paidOrCreditOrDebit == "")
            {
                return "";
            }
            if (paidOrCreditOrDebit == "Paid") 
            {
                return "HAVING SUM(tblPayment.Value)=tblBooking.value ";
            }
            if (paidOrCreditOrDebit == "Credit")
            {
                return "HAVING SUM(tblPayment.Value)>tblBooking.value ";
            }
            if (paidOrCreditOrDebit == "Debit")
            {
                return "HAVING SUM(tblPayment.Value)<tblBooking.value ";
            }
            return "";
        }

        //An ArrayList of filtered booking objects is returned
        public ArrayList GetFilteredBookings(int tourId, int cabinId, int cabinClassId, string guestSurname, string guestEmail, string activeOrCancelled, string paidOrCreditOrDebit, int bookingId)
        {
            //short circuit to all bookings if none of the filters are set. 
            if (tourId == 0 && cabinId == 0 && cabinClassId == 0 && guestSurname == "" && guestEmail == "" && activeOrCancelled == "" && paidOrCreditOrDebit == "" && bookingId == 0)
            {
                return GetAllBookings();
            }

            Console.WriteLine("getFilteredBookings called");

            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand getBookings = new SqlCommand();
            getBookings.Connection = conn;
            getBookings.CommandType = CommandType.Text;
            getBookings.CommandText = "SELECT tblBooking.id, tblBooking.createdAt, tblBooking.cancelledAt, tblTourDate.date, tblShip.name, tblCabin.id, tblClass.name, tblBooking.value, SUM(tblPayment.value) AS PaymentValue, tblGuest.firstName, tblGuest.lastName, tblGuest.email, tblGuest.phone As totalPayment " +
                "FROM tblBooking " +
                "LEFT JOIN tblPayment ON tblBooking.id = tblPayment.bookingId " +
                "LEFT JOIN tblCabinBookingTourDateMap ON tblBooking.id = tblCabinBookingTourDateMap.bookingId " +
                "LEFT JOIN tblTourDate ON tblCabinBookingTourDateMap.tourDateId = tblTourDate.id " +
                "LEFT JOIN tblCabin ON tblCabinBookingTourDateMap.cabinId = tblCabin.id " +
                "LEFT JOIN tblClass ON tblCabin.classId = tblClass.id " +
                "LEFT JOIN tblShip ON tblTourDate.shipId = tblShip.id " +
                "LEFT JOIN tblGuest ON tblBooking.guestId = tblGuest.id " +
                "WHERE(tblCabinBookingTourDateMap.bookingId IS NULL OR(tblCabinBookingTourDateMap.bookingId IS NOT NULL)) " +
                BuildWhereClause(tourId, cabinId, cabinClassId, guestSurname, guestEmail, activeOrCancelled, bookingId) +
                "GROUP BY tblBooking.id, tblBooking.guestId, tblBooking.createdAt, tblBooking.cancelledAt, tblTourDate.date, tblShip.name, tblCabin.id, tblClass.name, tblBooking.value, tblGuest.firstName, tblGuest.lastName, tblGuest.phone, tblGuest.email " +
                BuildHavingClause(paidOrCreditOrDebit) +
                "ORDER BY tblBooking.createdAt";
            SqlDataReader bookingResults = getBookings.ExecuteReader();
            
            ArrayList BookingList = new ArrayList();

            while (bookingResults.Read())
            {
                Booking x = new Booking();
                x.m_Id = (int)bookingResults[0];
                x.m_CreatedAt = DateTime.Parse(bookingResults[1].ToString());
                // check for null before attempting to convert.
                if (!Convert.IsDBNull(bookingResults[2]))  // check if column is null;
                {
                    x.m_CancelledAt = bookingResults.GetDateTime(2);
                }
                if (!Convert.IsDBNull(bookingResults[3]))
                {
                    x.m_TourDate = bookingResults.GetDateTime(3);
                }
                if (!Convert.IsDBNull(bookingResults[4]))
                {
                    x.m_Ship = bookingResults[4].ToString();
                }
                if (!Convert.IsDBNull(bookingResults[5]))
                {
                    x.m_Cabin = (int)bookingResults[5];
                }
                if (!Convert.IsDBNull(bookingResults[6]))
                {
                    x.m_CabinClass = bookingResults[6].ToString();
                }
                x.m_Rate = bookingResults.GetDouble(7);
                if (!Convert.IsDBNull(bookingResults[8]))  // check if column is null;
                {
                    x.m_PaymentTotal = bookingResults.GetDouble(8);
                }
                else
                {
                    x.m_PaymentTotal = 0.0;
                }

                x.m_GuestName = bookingResults[9].ToString() + " " + bookingResults[10].ToString();
                x.m_GuestEmail = bookingResults[11].ToString();
                x.m_GuestPhone = bookingResults[12].ToString();

                BookingList.Add(x);
            }
            bookingResults.Close();
            conn.Close();
            return BookingList;
        }

        //An ArrayList of booking objects is returned which have the supplied guest Id.
        public ArrayList GetBookingsByGuestId(int guestId)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand getBookings = new SqlCommand();
            getBookings.Connection = conn;
            getBookings.CommandType = CommandType.Text;
            getBookings.CommandText = "SELECT tblBooking.id, tblBooking.createdAt, tblBooking.cancelledAt, tblTourDate.date, tblShip.name, tblCabin.id, tblClass.name, tblBooking.value, SUM(tblPayment.value) AS PaymentValue, tblGuest.firstName, tblGuest.lastName, tblGuest.email, tblGuest.phone As totalPayment " +
                "FROM tblBooking " +
                "LEFT JOIN tblPayment ON tblBooking.id = tblPayment.bookingId " +
                "LEFT JOIN tblCabinBookingTourDateMap ON tblBooking.id = tblCabinBookingTourDateMap.bookingId " +
                "LEFT JOIN tblTourDate ON tblCabinBookingTourDateMap.tourDateId = tblTourDate.id " +
                "LEFT JOIN tblCabin ON tblCabinBookingTourDateMap.cabinId = tblCabin.id " +
                "LEFT JOIN tblClass ON tblCabin.classId = tblClass.id " +
                "LEFT JOIN tblShip ON tblTourDate.shipId = tblShip.id " +
                "LEFT JOIN tblGuest ON tblBooking.guestId = tblGuest.id " +
                "WHERE(tblCabinBookingTourDateMap.bookingId IS NULL OR(tblCabinBookingTourDateMap.bookingId IS NOT NULL)) " +
                "AND tblBooking.guestId='" + guestId + "' " +
                "GROUP BY tblBooking.id, tblBooking.guestId, tblBooking.createdAt, tblBooking.cancelledAt, tblTourDate.date, tblShip.name, tblCabin.id, tblClass.name, tblBooking.value, tblGuest.firstName, tblGuest.lastName, tblGuest.phone, tblGuest.email " +
                "ORDER BY tblBooking.createdAt";
            SqlDataReader bookingResults = getBookings.ExecuteReader();

            ArrayList BookingList = new ArrayList();

            while (bookingResults.Read())
            {
                Booking x = new Booking();
                x.m_Id = (int)bookingResults[0];
                x.m_CreatedAt = bookingResults.GetDateTime(1); ;
                // check for null before attempting to convert.
                if (!Convert.IsDBNull(bookingResults[2]))  // check if column is null;
                {
                    x.m_CancelledAt = bookingResults.GetDateTime(2);
                }
                if (!Convert.IsDBNull(bookingResults[3]))
                {
                    x.m_TourDate = bookingResults.GetDateTime(3);
                }
                if (!Convert.IsDBNull(bookingResults[4]))
                {
                    x.m_Ship = bookingResults[4].ToString();
                }
                if (!Convert.IsDBNull(bookingResults[5]))
                {
                    x.m_Cabin = (int)bookingResults[5];
                }
                if (!Convert.IsDBNull(bookingResults[6]))
                {
                    x.m_CabinClass = bookingResults[6].ToString();
                }
                x.m_Rate = bookingResults.GetDouble(7);
                if (!Convert.IsDBNull(bookingResults[8]))  // check if column is null;
                {
                    x.m_PaymentTotal = bookingResults.GetDouble(8);
                }
                else
                {
                    x.m_PaymentTotal = 0.0;
                }
                x.m_GuestName = bookingResults[9].ToString() + " " + bookingResults[10].ToString();
                x.m_GuestEmail = bookingResults[10].ToString();
                x.m_GuestPhone = bookingResults[11].ToString();

                BookingList.Add(x);
            }
            bookingResults.Close();
            conn.Close();
            return BookingList;
        }

        // Method to cancel the booking.  Refund amount passed in from caller.
        public int Cancel(Double refundAmount)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // variable to hold the response code.
            int responseCode = 0;

            // setup of connection. 
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cancelBooking = new SqlCommand();
            cancelBooking.Connection = conn;
            cancelBooking.CommandType = CommandType.Text;

            // this process requires the update of a number of tables.  As such, it is wrapped in a transaction to allow rollback if any stage fails.  
            cancelBooking.CommandText = "BEGIN TRANSACTION";
            responseCode = cancelBooking.ExecuteNonQuery();
            if (responseCode != -1)
            {
                return -1;
            }

            // Update the tblBooking with the cancelledAt dateTime and update the booking rate to be the current value minus the refund amount.
            cancelBooking.CommandText = "UPDATE tblBooking SET CancelledAt=GETDATE(), Value=Value-" + refundAmount + " WHERE id='" + m_Id + "'";
            responseCode = cancelBooking.ExecuteNonQuery();
            if (responseCode < 1)
            {
                cancelBooking.CommandText = "ROLLBACK TRANSACTION";
                cancelBooking.ExecuteNonQuery();
                return -1;
            }

            // Remove the bookingId from the relevant cabinBookingTourDateMap entry.
            cancelBooking.CommandText = "UPDATE tblCabinBookingTourDateMap SET bookingId=NULL WHERE bookingId='" + m_Id + "'";
            responseCode = cancelBooking.ExecuteNonQuery();
            if (responseCode < 1)
            {
                cancelBooking.CommandText = "ROLLBACK TRANSACTION";
                cancelBooking.ExecuteNonQuery();
                return -1;
            }

            // Add the refund to the payments
            cancelBooking.CommandText = "INSERT INTO tblPayment (bookingId, processedAt, value) VALUES ('" + m_Id + "', GETDATE(), '-" + refundAmount.ToString() + "')";
            responseCode = cancelBooking.ExecuteNonQuery();
            if (responseCode < 1)
            {
                cancelBooking.CommandText = "ROLLBACK TRANSACTION";
                cancelBooking.ExecuteNonQuery();
                return -1;
            }

            cancelBooking.CommandText = "COMMIT TRANSACTION";
            cancelBooking.ExecuteNonQuery();
            return 0;
        }

        public int CreateBooking(int guestId, string slotId, Double bookingValue)
        {

            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // setup of connection. 
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand createBooking = new SqlCommand();
            createBooking.Connection = conn;
            createBooking.CommandType = CommandType.Text;

            // this process requires the update of a number of tables.  As such, it is wrapped in a transaction to allow rollback if any stage fails. 
            createBooking.CommandText = "BEGIN TRANSACTION";
            
            try
            {
                createBooking.ExecuteNonQuery();
            }
            catch
            {
                conn.Close();
                return -1;
            }

            // create the booking
            createBooking.CommandText = "INSERT INTO tblBooking (guestId, createdAt, value) VALUES ('" + guestId + "', GETDATE(), '" + bookingValue + "')";

            try
            {
                createBooking.ExecuteNonQuery();
            }
            catch
            {
                createBooking.CommandText = "ROLLBACK TRANSACTION";
                createBooking.ExecuteNonQuery();
                conn.Close();
                return -1;
            }

            // get that bookingId
            createBooking.CommandText = "SELECT max(id) FROM tblBooking";
            int createdBookingId;
            try
            {
                createdBookingId = (int)createBooking.ExecuteScalar();
            }
            catch
            {
                createBooking.CommandText = "ROLLBACK TRANSACTION";
                createBooking.ExecuteNonQuery();
                conn.Close();
                return -1;
            }

            // update the cabinBookingTourDatesMap table
            createBooking.CommandText = "UPDATE tblCabinBookingTourDateMap SET bookingId = '" + createdBookingId + "' WHERE id='" + slotId + "'";  
            try
            {
                createBooking.ExecuteNonQuery();
            }
            catch
            {
                createBooking.CommandText = "ROLLBACK TRANSACTION";
                createBooking.ExecuteNonQuery();
                conn.Close();
                return -1;
            }

            // record the payment 
            createBooking.CommandText = "INSERT INTO tblPayment (bookingId, processedAt, value) VALUES ('" + createdBookingId + "', GETDATE(), '" + bookingValue + "')";

            try
            {
                createBooking.ExecuteNonQuery();
            }
            catch
            {
                createBooking.CommandText = "ROLLBACK TRANSACTION";
                createBooking.ExecuteNonQuery();
                conn.Close();
                return -1;
            }

            // commit the transaction.
            createBooking.CommandText = "COMMIT TRANSACTION";
            try
            {
                createBooking.ExecuteNonQuery();
            }
            catch
            {
                createBooking.CommandText = "ROLLBACK TRANSACTION";
                createBooking.ExecuteNonQuery();
                conn.Close();
                return -1;
            }
            
            conn.Close();

            // populate this booking instance with the booking data.
            GetBookingById(createdBookingId);

            return 0;
        }
    }
}
