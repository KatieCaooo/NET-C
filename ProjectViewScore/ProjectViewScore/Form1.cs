using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ProjectViewScore.sample.tblStudent;
namespace ProjectViewScore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            loadStudentID();
            
        }

        private void loadStudentID()
        {
           // comboBoxStudentID.Items.Add("----Select StudentID----");
            StudentDAO dao = new StudentDAO();
            List<StudentDTO> result = null;
           
            try
            {
                result = dao.loadStudentID();
                if(result != null || result.Count > 0)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        comboBoxStudentID.Items.Add(result[i].StudentID);
                    }
                }
            }catch(Exception e)
            {
                MessageBox.Show(" ERROR: " + e.Message + "\n");
            }
        }

        public void updateTableScore(string studentID)
        {
            try
            {
                StudentDAO dao = new StudentDAO();
                DataTable dt = new DataTable();
                SqlDataAdapter da = dao.loadScoreData(studentID);
                da.Fill(dt);
                tblScore.DataSource = dt;
            }
            catch (SqlException e)
            {
                MessageBox.Show("error: " + e.Message);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //string studentID = comboBoxStudentID.GetItemText(comboBoxStudentID.SelectedItem);
            string studentID = comboBoxStudentID.Text;
           
            if (!studentID.Equals("----Select StudentID----"))
            {
                StudentDAO dao = new StudentDAO();
                List<StudentDTO> result = null;
                try
                {
                    result = dao.loadInforStudent(studentID);

                    if (result != null && result.Count > 0)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            txtFirstName.Text = result[i].FirstName;
                            txtLastName.Text = result[i].LastName;
                            txtSex.Text = result[i].Sex;
                            txtSpeciality.Text = result[i].SpecialityID;
                            txtBirthday.Text = result[i].Birthday.ToString("d");

                        }
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = dao.loadScoreData(studentID);
                        da.Fill(dt);
                        tblScore.DataSource = dt;
                    }
                    else if(result.Count == 0 || result == null)
                    {
                        txtFirstName.Text = "";
                        txtLastName.Text = "";
                        txtSex.Text = "";
                        txtSpeciality.Text = "";
                        txtBirthday.Text = "";

                        tblScore.DataSource = null;
                        tblScore.Rows.Clear();
                        tblScore.Columns.Clear();

                        MessageBox.Show("This studentID not existed!!!");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(" ERROR: " + ex.Message + "\n");
                }            
            }
            else
            {
                MessageBox.Show("Please select studentID Items");
            }
        }

        private void btnChangeScore_Click(object sender, EventArgs e)
        {
            try
            {
                string row = tblScore.CurrentRow.Index.ToString();
                if (row != null && row.Length > 0)
                {
                    int indext = int.Parse(row);
                    string semesterID = tblScore.Rows[indext].Cells[0].Value.ToString();
                    string subjectID = tblScore.Rows[indext].Cells[1].Value.ToString();
                    string subjectName = tblScore.Rows[indext].Cells[2].Value.ToString();
                    string score1 = tblScore.Rows[indext].Cells[3].Value.ToString();
                    string score2 = tblScore.Rows[indext].Cells[4].Value.ToString();
                    string average = tblScore.Rows[indext].Cells[5].Value.ToString();
                    string studentID = comboBoxStudentID.Text;
                    Form2 f = new Form2(semesterID, subjectID, subjectName, score1, score2, average, studentID, this);
                    f.Show();                 
                }              
            }
            catch(Exception)
            {
                MessageBox.Show("Please click row data or search student befor click this button!!");
            }          
        }
    }
}
