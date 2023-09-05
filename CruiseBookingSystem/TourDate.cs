using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruiseBookingSystem
{
    class TourDate
    {
        private int m_Id;
        private DateTime m_Date;
        private string m_ShipName;
        private int m_ShipId;

        public int Id
        {
            get
            {
                return m_Id;
            }
            set
            {
                m_Id = value;
            }
        }
        public DateTime Date
        {
            get
            {
                return m_Date;
            }
            set
            {
                m_Date = value;
            }
        }
        public string ShipName
        {
            get
            {
                return m_ShipName;
            }
            set
            {
                m_ShipName = value;
            }
        }
        public int ShipId
        {
            get
            {
                return m_ShipId;
            }
            set
            {
                m_ShipId = value;
            }
        }

        public ArrayList getAllTourDates()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand getTourDates = new SqlCommand();
            getTourDates.Connection = conn;
            getTourDates.CommandType = CommandType.Text;
            getTourDates.CommandText = "SELECT tblTourDate.id, tblTourDate.date, tblShip.name FROM tblTourDate JOIN tblShip ON tblShip.id=tblTourDate.shipId;";
            SqlDataReader tourDateResults = getTourDates.ExecuteReader();

            ArrayList TourDateList = new ArrayList();

            while (tourDateResults.Read())
            {
                TourDate x = new TourDate();
                x.m_Id = (int)tourDateResults[0];
                x.m_Date = DateTime.Parse(tourDateResults[1].ToString());
                x.m_ShipName = tourDateResults[2].ToString();

                TourDateList.Add(x);
            }
            tourDateResults.Close();
            conn.Close();
            return TourDateList;
        }

        public void getTourDate(int id)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand getTourDate = new SqlCommand();
            getTourDate.Connection = conn;
            getTourDate.CommandType = CommandType.Text;
            getTourDate.CommandText = "SELECT * FROM tblTourDate";
            SqlDataReader tourDateResult = getTourDate.ExecuteReader();

            if (!tourDateResult.HasRows)
            {
                throw new Exception("Tour Date Not Found");
            }
            else
            {
                tourDateResult.Read();
                m_Id = (int)tourDateResult[0];
                m_Date = DateTime.Parse(tourDateResult[1].ToString());
                m_ShipId = (int)tourDateResult[2];
            }
            conn.Close();
        }

        private string BuildWhereClause(string pastOrFuture)
        {
            string whereClause = "";
            if (pastOrFuture == "")
            {
                return "";
            }   
            if (pastOrFuture == "PAST")
            {
                return " WHERE tblTourDate.date < GETDATE()";
            }
            if (pastOrFuture == "FUTURE")
            {
                return " WHERE tblTourDate.date > GETDATE()";
            }
            return whereClause;
        }

        public ArrayList getFilteredTourDate(string pastOrFuture)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand getTourDates = new SqlCommand();
            getTourDates.Connection = conn;
            getTourDates.CommandType = CommandType.Text;
            getTourDates.CommandText = "SELECT tblTourDate.id, tblTourDate.date, tblShip.name FROM tblTourDate JOIN tblShip ON tblShip.id=tblTourDate.shipId" + BuildWhereClause(pastOrFuture);
            SqlDataReader tourDateResults = getTourDates.ExecuteReader();

            ArrayList TourDateList = new ArrayList();

            while (tourDateResults.Read())
            {
                TourDate x = new TourDate();
                x.m_Id = (int)tourDateResults[0];
                x.m_Date = DateTime.Parse(tourDateResults[1].ToString());
                x.m_ShipName = tourDateResults[2].ToString();

                TourDateList.Add(x);
            }
            tourDateResults.Close();
            conn.Close();
            return TourDateList;
        }

        public int Create(int shipId, DateTime date)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand newTourDate = new SqlCommand();
            newTourDate.Connection = conn;
            newTourDate.CommandType = CommandType.Text;

            // first start a transaction
            newTourDate.CommandText = "BEGIN TRANSACTION";
            int response = newTourDate.ExecuteNonQuery();

            // then, create the tourDate
            newTourDate.CommandText = "INSERT INTO tblTourDate VALUES ('" + date.ToString("MM/dd/yyyy") + "', '" + shipId + "')";
            response = newTourDate.ExecuteNonQuery();

            if (response != 1)
            {
                newTourDate.CommandText = "ROLLBACK TRANSACTION";
                newTourDate.ExecuteNonQuery();
                conn.Close();
                return -1;
            }

            // then, get the newly created TourDateId
            newTourDate.CommandText = "SELECT max(id) FROM tblTourDate";
            m_Id = (int)newTourDate.ExecuteScalar();

            // then, create the cabinBookingTourDateMaps
            // to do this, first get the list of cabins on the ship

            Cabin cabin = new Cabin();
            ArrayList cabins = cabin.getCabinsByShipId(shipId);
            foreach (Cabin cab in cabins)
            {
                // now, create a cabinMap for each cabin on that ship.
                newTourDate.CommandText = "INSERT INTO tblCabinBookingTourDateMap (tourDateId, cabinId) VALUES ('" + m_Id + "', '" + cab.Id + "')";
                Console.WriteLine(newTourDate.CommandText);
                response = newTourDate.ExecuteNonQuery();
                if (response != 1)
                {
                    newTourDate.CommandText = "ROLLBACK TRANSACTION";
                    newTourDate.ExecuteNonQuery();
                    conn.Close();
                    return -1;
                }
            }

            newTourDate.CommandText = "COMMIT";
            newTourDate.ExecuteNonQuery();

            conn.Close();
           
            return response;
        }
    }
}
