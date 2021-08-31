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
    class Doctor
    {
        int did;
        String name;
        String address;
        //int age;
        String phone;
        String email;

        public int getId()
        {
            return did;
        }

        public String getName()
        {
            return name;
        }

        public String getAdd()
        {
            return address;
        }
        /*public int getAge()
        {
            return age;
        }*/

        public String getPhone()
        {
            return phone;
        }

        public String getEmail()
        {
            return email;
        }


        public static ArrayList getDoctorList()
        {
            ArrayList DocList = new ArrayList();  //a list to save the patient's data
            //prepare an SQL query to retrieve all the patients 
            DataTable myTable = new DataTable();
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM sans_doctors ORDER BY d_id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //cmd.Parameters.AddWithValue("@myDate", dateString);
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
                Doctor newDoc = new Doctor();
                newDoc.did = Int32.Parse(row["d_ID"].ToString());
                newDoc.name = row["name"].ToString();
                newDoc.address = row["officeAdd"].ToString();
                //newDoc.age = Int32.Parse(row["age"].ToString());
                newDoc.phone = row["phone"].ToString();
                newDoc.email = row["email"].ToString();
                DocList.Add(newDoc);
            }
            return DocList;  //return the event list
        }
    }
}
