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
    class Ship
    {
        private int m_Id;
        private string m_Name;

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
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public ArrayList getAllShips()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand getShips = new SqlCommand();
            getShips.Connection = conn;
            getShips.CommandType = CommandType.Text;
            getShips.CommandText = "SELECT * FROM tblShip;";
            SqlDataReader shipResults = getShips.ExecuteReader();

            ArrayList shipList = new ArrayList();

            while (shipResults.Read())
            {
                Ship x = new Ship();
                x.m_Id = (int)shipResults[0];
                x.m_Name = shipResults[1].ToString();

                shipList.Add(x);
            }
            shipResults.Close();
            conn.Close();
            return shipList;
        }
    }
}
