using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorPatientSystem
{
    public partial class Form1 : Form
    {
        ArrayList patientList;
        ArrayList docList;
        ArrayList reqList;
        ArrayList prescripts;
        ArrayList refillList;
        int id;
        int lb2_selectedIndex;
        int cb3_index;

        Boolean sendNotice;
        Boolean docNotice;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SelectPanel.Visible = true;
            SelectLabel.Text = "Select Doctor";
            comboBox1.Items.Clear();
            docList = Doctor.getDoctorList();
            for(int i=0; i<docList.Count; i++)
            {
                Doctor temp = (Doctor)docList[i];
                comboBox1.Items.Add(temp.getName());
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SelectPanel.Visible = false;
            ConfirmationPanel.Visible = true;
            ConfirmationLabel.Text = "[CONFIRMATION]";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            ConfirmationPanel.Visible = false;
        }

        private void ViewRecordsButton_Click(object sender, EventArgs e)
        {
            ListPanel.Visible = true;
            patientList = Patient.getPatientList();
            listBox1.Items.Clear();
            Patient temp = (Patient)patientList[id-1];
            listBox1.Items.Add("Id: " + temp.getId());
            listBox1.Items.Add("Name: " + temp.getName());
            listBox1.Items.Add("Address: " + temp.getAdd());
            listBox1.Items.Add("Age: " + temp.getAge());
            listBox1.Items.Add("Phone: " + temp.getPhone());
            listBox1.Items.Add("Email: " + temp.getEmail());
        }

        private void ListButton_Click(object sender, EventArgs e)
        {
            ListPanel.Visible = false;
        }

        private void RequestRecordsButton_Click(object sender, EventArgs e)
        {
            SelectPanel.Visible = true;
            SelectLabel.Text = "Select Patient";
        }

        private void ProcessRequestsButton_Click(object sender, EventArgs e)
        {
            ProcessRequestsPanel.Visible = true;
            reqList = Request.getRequestList(id);
            listBox2.Items.Clear();
            for(int i = 0; i <reqList.Count; i++)
            {
                Request temp = (Request)reqList[i];
                listBox2.Items.Add("Sent From: " + temp.getSend());
                listBox2.Items.Add("Message: " + temp.getContent());
                string st = temp.getStat();
                if(st == "new")
                {
                    st = "new";
                }
                else if(st == "acc")
                {
                    st = "accepted";
                }

                else
                {
                    st = "rejected";
                }
                listBox2.Items.Add("Status: " + st);
            }

        }

        private void ProcessRequestsFinish_Click(object sender, EventArgs e)
        {
            ProcessRequestsPanel.Visible = false;
        }

        private void RequestCallButton_Click(object sender, EventArgs e)
        {
            SelectPanel.Visible = true;
            SelectLabel.Text = "Select Doctor";
        }

        private void RequestRefillButton_Click(object sender, EventArgs e)
        {
            SelectPanel.Visible = true;
            SelectLabel.Text = "Select Prescription";
        }

        private void PatientProcessRequestsButton_Click(object sender, EventArgs e)
        {
            ProcessRequestsPanel.Visible = true;
        }

        private void RequestStatusButton_Click(object sender, EventArgs e)
        {
            ListPanel.Visible = true;
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            SetupAppointmentPanel.Visible = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void SetupAppointmentSubmit_Click(object sender, EventArgs e)
        {
            SetupAppointmentPanel.Visible = false;
            ConfirmationPanel.Visible = true;
            ConfirmationLabel.Text = "[CONFIRMATION]";
        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            ViewRecordsPanel.Visible = false;

            if (sendNotice)
            {
                if (docNotice)
                {
                    docList = Doctor.getDoctorList();
                    int selectedIndex = cb3_index;
                    Doctor temp = (Doctor)docList[selectedIndex];
                    int docId = temp.getId();

                    string reqCon = "I would like to discuss a case with you.";
                    string type = "pat";
                    string status = "new";
                    string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
                    MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
                    try
                    {
                        Console.WriteLine("Connecting to MySQL...");
                        conn.Open();
                        string sql = "INSERT INTO sans_request3 (sendId, recieveId, type, reqContent, status) VALUES (@sid, @rid, @type, @req, @stat)";
                        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@sid", id);
                        cmd.Parameters.AddWithValue("@rid", docId);
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@req", reqCon);
                        cmd.Parameters.AddWithValue("@stat", status);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    conn.Close();
                    Console.WriteLine("Done.");
                    sendNotice = false;
                    docNotice = false;
                }

                else
                {
                    patientList = Patient.getPatientList();
                    int selectedIndex = cb3_index;
                    Patient temp = (Patient)patientList[selectedIndex];
                    int patId = temp.getId();

                    string reqCon = "I would like to discuss a case with you.";
                    string type = "pat";
                    string status = "new";
                    string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
                    MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
                    try
                    {
                        Console.WriteLine("Connecting to MySQL...");
                        conn.Open();
                        string sql = "INSERT INTO sans_request3 (sendId, recieveId, type, reqContent, status) VALUES (@sid, @rid, @type, @req, @stat)";
                        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@sid", id);
                        cmd.Parameters.AddWithValue("@rid", patId);
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@req", reqCon);
                        cmd.Parameters.AddWithValue("@stat", status);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    conn.Close();
                    Console.WriteLine("Done.");
                    sendNotice = false;
                }
            }
        }

        private void DoctorViewRecordsButton_Click(object sender, EventArgs e)
        {
            ViewRecordsPanel.Visible = true;
            ViewRecordsLabel.Text = "Select Patient";
        }

        private void UpdateRecordsButton_Click(object sender, EventArgs e)
        {
            UpdateRecordsPanel.Visible = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void UpdateSelectedButton_Click(object sender, EventArgs e)
        {
            UpdateRecordsPanel.Visible = false;
            InputRecordsPanel.Visible = true;
        }

        private void AddNewButton_Click(object sender, EventArgs e)
        {
            UpdateRecordsPanel.Visible = false;
            InputRecordsPanel.Visible = true;
        }

        private void InputRecordsSubmitButton_Click(object sender, EventArgs e)
        {
            InputRecordsPanel.Visible = false;
            ConfirmationPanel.Visible = true;
            ConfirmationLabel.Text = "Records updated successfully.";
        }

        private void ViewPrescriptionsButton_Click(object sender, EventArgs e)
        {
            ListPanel.Visible = true;
            listBox1.Items.Clear();
            prescripts = Prescription.getPrescriptList();
            for (int i = 0; i < prescripts.Count; i++)
            {
               Prescription temp = (Prescription)prescripts[i];
                listBox1.Items.Add("ID: " + temp.getPreId() + "  Patient: " + temp.getPatName() + " Doctor:  " + temp.getDocName());
            }

        }

        private void ViewRefillsButton_Click(object sender, EventArgs e)
        {
            ListPanel.Visible = true;
            listBox1.Items.Clear();
            refillList = Refill.getRefillList();
            for(int i = 0; i < refillList.Count; i++)
            {
                Refill temp = (Refill)refillList[i];
                string stat = temp.getStat();

                if (stat == "comp")
                    stat = "Complete";
                else if (stat == "prog")
                    stat = "In Progress";
                else if (stat == "new")
                    stat = "new";
                listBox1.Items.Add("ID: " + temp.getRefillId() + "  Patient: " + temp.getPatName() + "  Doctor: " + temp.getDocName() + "  Status: " + stat);
            }
        }

        private void PharmacyViewRecordsButton_Click(object sender, EventArgs e)
        {
            ViewRecordsPanel.Visible = true;
            ViewRecordsLabel.Text = "Select Patient";
            patientList = Patient.getPatientList();
            comboBox3.Items.Clear();
            for(int i = 0; i < patientList.Count; i++)
            {
                Patient temp = (Patient)patientList[i];
                comboBox3.Items.Add(temp.getName());
            }
        }

        private void SendNoticeButton_Click(object sender, EventArgs e)
        {
            ViewRecordsPanel.Visible = true;
            ViewRecordsLabel.Text = "Select Patient";
            sendNotice = true;
            patientList = Patient.getPatientList();
            comboBox3.Items.Clear();
            listBox3.Items.Clear();
            for (int i = 0; i < patientList.Count; i++)
            {
                Patient temp = (Patient)patientList[i];
                comboBox3.Items.Add(temp.getName());
            }
        }

        private void ViewNoticesButton_Click(object sender, EventArgs e)
        {
            ListPanel.Visible = true;
            reqList = Request.getRequestList(id);
            listBox1.Items.Clear();
            for (int i = 0; i < reqList.Count; i++)
            {
                Request temp = (Request)reqList[i];
                string st = temp.getStat();
                if (st == "new")
                {
                    st = "new";
                }
                else if (st == "acc")
                {
                    st = "accepted";
                }

                else
                {
                    st = "rejected";
                }
                listBox1.Items.Add("Sent From: " + temp.getSend() + "  Message: " + temp.getContent() + "  Status: " + st);
                //listBox1.Items.Add("Message: " + temp.getContent());
               //listBox1.Items.Add("Status: " + st);
            }
        }

        private void SendDoctorNoticeButton_Click(object sender, EventArgs e)
        {
            ViewRecordsPanel.Visible = true;
            ViewRecordsLabel.Text = "Select Doctor";
            comboBox3.Items.Clear();
            listBox3.Items.Clear();
            sendNotice = true;
            docNotice = true;
            docList = Doctor.getDoctorList();
            comboBox3.Items.Clear();
            listBox3.Items.Clear();
            for (int i = 0; i < docList.Count; i++)
            {
                Doctor temp = (Doctor)docList[i];
                comboBox3.Items.Add(temp.getName());
            }
        }

        private void LoginLabel_Click(object sender, EventArgs e)
        {

        }

        private void LoginPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            id = Int32.Parse(textBox1.Text);
            string type = IDCheck.getId(id);
            Console.WriteLine(id);
            if(id == 1 || id == 2 || id == 3) //patient
            {
                DoctorPanel.Visible = false;
                PharmacyPanel.Visible = false;
                LoginPanel.Visible = false;
            }

            else if (id == 4) //doctor
            {
                PatientPanel.Visible = false;
                PharmacyPanel.Visible = false;
                LoginPanel.Visible = false;
            }

            else if (id == 5) //pharmacy
            {
                PatientPanel.Visible = false;
                DoctorPanel.Visible = false;
                LoginPanel.Visible = false;
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;
            Doctor temp = (Doctor)docList[selectedIndex];
            int docId = temp.getId();

            string reqCon = "I would like to have an appointment setup.";
            string type = "app";
            string status = "new";
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "INSERT INTO sans_request3 (sendId, recieveId, type, reqContent, status) VALUES (@sid, @rid, @type, @req, @stat)";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@sid", id);
                cmd.Parameters.AddWithValue("@rid", docId);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@req", reqCon);
                cmd.Parameters.AddWithValue("@stat", status);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            lb2_selectedIndex = listBox2.SelectedIndex;
        }

        private void ProcessRequestsAccept_Click(object sender, EventArgs e)
        {
            Request temp = (Request)reqList[lb2_selectedIndex];
            int id = temp.getReqId();
            string status = "acc";
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "UPDATE sans_request3 SET status = 'acc' WHERE reqId = @id;";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

            private void ProcessRequestsReject_Click(object sender, EventArgs e)
        {
            Request temp = (Request)reqList[lb2_selectedIndex];
            int id = temp.getReqId();
            string status = "rej";
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "UPDATE sans_request3 SET status = 'acc' WHERE reqId = @id;";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (sendNotice) //send notices
            {
                if (docNotice) //notify doctors
                {
                    cb3_index = comboBox3.SelectedIndex;
                    docList = Doctor.getDoctorList();
                    Doctor temp = (Doctor)docList[cb3_index];
                    listBox3.Items.Clear();
                    listBox3.Items.Add("I would like to discuss a case with you.");
                }
                else //notify patients
                {
                    cb3_index = comboBox3.SelectedIndex;
                    patientList = Patient.getPatientList();
                    Patient temp = (Patient)patientList[cb3_index];
                    listBox3.Items.Clear();
                    listBox3.Items.Add("I would like to discuss a case with you.");
                }
            }
            else //view records only
            {
                cb3_index = comboBox3.SelectedIndex;
                patientList = Patient.getPatientList();
                Patient temp = (Patient)patientList[cb3_index];
                listBox3.Items.Clear();
                listBox3.Items.Add("ID: " + temp.getId());
                listBox3.Items.Add("Name: " + temp.getName());
                listBox3.Items.Add("Address: " + temp.getAdd());
                listBox3.Items.Add("Age: " + temp.getAge());
                listBox3.Items.Add("Phone: " + temp.getPhone());
                listBox3.Items.Add("Email: " + temp.getEmail());
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
