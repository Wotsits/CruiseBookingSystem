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
    class CabinBookingTourDateMap
    {
        private int m_Id;
        private int m_CabinId;
        private int m_BookingId;
        private string m_ClassName;
        private Double m_Rate;
        private Double m_SingleOccupancyDiscount;

        public int Id { get { return m_Id; } set { m_Id = value; } }
        public int CabinId { get { return m_CabinId; } set { m_CabinId = value; } }
        public int BookingId { get { return m_BookingId; } set { m_BookingId = value; } }
        public string ClassName { get { return m_ClassName; } set { m_ClassName = value; } }
        public Double Rate { get { return m_Rate; } set { m_Rate = value; } }
        public Double SingleOccupancyDiscount { get { return m_SingleOccupancyDiscount; } set { m_SingleOccupancyDiscount = value; } }

        private string BuildWhereClause(int tourId, int classId)
        {
            string whereClause = "";
            if (tourId != 0)
            {
                whereClause += " AND tblCabinBookingTourDateMap.tourDateId='" + tourId + "'";
            } 
            if (classId != 0)
            {
                whereClause += " AND tblClass.id='" + classId + "'";
            }
            return whereClause;
        }

        //An ArrayList of all guest objects is returned
        public ArrayList GetAvailableSlots(int tourId, int classId)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand getSlots = new SqlCommand();
            getSlots.Connection = conn;
            getSlots.CommandType = CommandType.Text;
            getSlots.CommandText = "Select tblCabinBookingTourDateMap.id, tblCabinBookingTourDateMap.tourDateId, tblCabinBookingTourDateMap.cabinId, tblCabinBookingTourDateMap.bookingId, tblClass.name, tblClass.rate, tblClass.singleOccupancyDiscount FROM tblCabinBookingTourDateMap " +
                "JOIN tblCabin ON tblCabin.id=tblCabinBookingTourDateMap.cabinId " +
                "JOIN tblClass ON tblClass.id = tblCabin.classId " +
                "WHERE bookingId IS NULL" + BuildWhereClause(tourId, classId);
            SqlDataReader slotList = getSlots.ExecuteReader();

            ArrayList SlotList = new ArrayList();

            while (slotList.Read())
            {
                CabinBookingTourDateMap x = new CabinBookingTourDateMap();
                x.m_Id = (int)slotList[0];
                x.m_CabinId = (int)slotList[2];
                x.m_ClassName = slotList[4].ToString();
                x.m_Rate = Double.Parse(slotList[5].ToString());
                x.m_SingleOccupancyDiscount = Double.Parse(slotList[6].ToString());

                SlotList.Add(x);
            }
            slotList.Close();
            conn.Close();
            return SlotList;
        }

        public void GetSlotById(int id)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand getSlotById = new SqlCommand();
            getSlotById.Connection = conn;
            getSlotById.CommandType = CommandType.Text;
            getSlotById.CommandText = "Select tblCabinBookingTourDateMap.id, tblCabinBookingTourDateMap.tourDateId, tblCabinBookingTourDateMap.cabinId, tblCabinBookingTourDateMap.bookingId, tblClass.name, tblClass.rate, tblClass.singleOccupancyDiscount FROM tblCabinBookingTourDateMap " +
                "JOIN tblCabin ON tblCabin.id=tblCabinBookingTourDateMap.cabinId " +
                "JOIN tblClass ON tblClass.id = tblCabin.classId " +
                "WHERE tblCabinBookingTourDateMap.id='" + id + "'";
            SqlDataReader slot = getSlotById.ExecuteReader();

            slot.Read();

            m_Id = (int)slot[0];
            m_CabinId = (int)slot[2];
            m_ClassName = slot[4].ToString();
            m_Rate = Double.Parse(slot[5].ToString());
            m_SingleOccupancyDiscount = Double.Parse(slot[6].ToString());

            slot.Close();
            conn.Close();
        }
    }

    
}
