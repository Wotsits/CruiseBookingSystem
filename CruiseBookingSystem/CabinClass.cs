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
    internal class CabinClass
    {
        private int m_Id;
        private string m_Name;

        public int Id { get { return m_Id; } set { m_Id = value; } }
        public string Name { get { return m_Name; } set { m_Name = value; } }

        public ArrayList GetAllCabinClasses()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand getAllClasses = new SqlCommand();
            getAllClasses.Connection = conn;
            getAllClasses.CommandType = CommandType.Text;
            getAllClasses.CommandText = "SELECT * FROM tblClass";
            SqlDataReader ClassesResults = getAllClasses.ExecuteReader();

            ArrayList ClassList = new ArrayList();

            while (ClassesResults.Read())
            {
                CabinClass x = new CabinClass();
                x.m_Id = (int)ClassesResults[0];
                x.m_Name = ClassesResults[1].ToString();

                ClassList.Add(x);
            }
            ClassesResults.Close();
            conn.Close();
            return ClassList;
        }
    }
}
