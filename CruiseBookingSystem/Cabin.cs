using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CruiseBookingSystem
{
    class Cabin
    {
        private int m_Id;
        private int m_ClassId;
        private int m_ShipId;
        private string m_ShipName;

        //*************************
        //GETTERS & SETTERS

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
        public int ClassId 
        { 
            get 
            { 
                return m_ClassId; 
            } 
            set 
            { 
                m_ClassId = value; 
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
        public string ShipName
        {
            get { return m_ShipName; }
            set { m_ShipName = value; }
        }

        //***************************
        //METHODS

        public ArrayList getAllCabins()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand getCabinsByShipId = new SqlCommand();
            getCabinsByShipId.Connection = conn;
            getCabinsByShipId.CommandType = CommandType.Text;
            getCabinsByShipId.CommandText = "SELECT tblCabin.*, tblShip.name FROM tblCabin JOIN tblShip ON tblShip.id=tblCabin.shipId";
            SqlDataReader cabinResults = getCabinsByShipId.ExecuteReader();

            ArrayList CabinList = new ArrayList();

            while (cabinResults.Read())
            {
                Cabin x = new Cabin();
                x.m_Id = (int)cabinResults[0];
                x.m_ClassId = (int)cabinResults[1];
                x.m_ShipId = (int)cabinResults[2];
                x.m_ShipName = (string)cabinResults[3];

                CabinList.Add(x);
            }

            cabinResults.Close();
            conn.Close();
            return CabinList;
        }

        public ArrayList getCabinsByShipId(int shipId)
        {
            Console.WriteLine(shipId);
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand getCabinsByShipId = new SqlCommand();
            getCabinsByShipId.Connection = conn;
            getCabinsByShipId.CommandType = CommandType.Text;
            getCabinsByShipId.CommandText = "SELECT * FROM tblCabin WHERE shipId='" + shipId + "'";
            SqlDataReader cabinResults = getCabinsByShipId.ExecuteReader();

            ArrayList CabinList = new ArrayList();

            while (cabinResults.Read())
            {
                Cabin x = new Cabin();
                x.m_Id = (int)cabinResults[0];
                x.m_ClassId = (int)cabinResults[1];
                x.m_ShipId = (int)cabinResults[2];

                CabinList.Add(x);
            }

            cabinResults.Close();
            conn.Close();
            return CabinList;
        }

       
    }
}
