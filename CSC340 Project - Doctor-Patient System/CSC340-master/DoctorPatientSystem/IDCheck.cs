using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPatientSystem
{
    class IDCheck
    {
        int pid;
        string name;
        string type;
        //string p_type;
        
        public String getType()
        {
            return type;
        }

        public static String getId (int enteredID)
        {
            DataTable myTable = new DataTable();
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT p_type FROM sans_peopleid ORDER BY idNum";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //cmd.Parameters.AddWithValue("enteredID", enteredID);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmd);
                myAdapter.Fill(myTable);
                //Console.WriteLine("Table is ready.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            IDCheck newId = new IDCheck();
            foreach (DataRow row in myTable.Rows)
            {
                newId.type = row["p_type"].ToString();
            }

            string p_type = myTable.Rows[enteredID - 1].ToString();
            return (p_type);
        }

       /* public static ArrayList getPersonList(int enteredID)
        {
            ArrayList personList = new ArrayList();  //a list to save the person's data
            //prepare an SQL query to retrieve all the people ids 
            DataTable myTable = new DataTable();
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT p_type FROM sans_peopleid WHERE idNum = @enteredID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@enteredID", enteredID);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmd);
                myAdapter.Fill(myTable);
                Console.WriteLine("Table is ready.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            //convert the retrieved data to events and save them to the list
            foreach (DataRow row in myTable.Rows)
            {
                IDCheck newID = new IDCheck();
                newID.type = row["type"].ToString();
                personList.Add(newID);
            }


        }*/
    }



}
