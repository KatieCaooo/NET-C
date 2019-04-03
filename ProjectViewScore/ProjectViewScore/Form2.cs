using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectViewScore.sample.tblStudent;

namespace ProjectViewScore
{
    public partial class Form2 : Form
    {
        private string studentID = "";
        Form1 form1;
        public Form2(string semesterID, string subjectID, string subjectName, string score1, string score2, string average, string studentID, Form1 f)
        {
            InitializeComponent();
            txtSemesterID.Text = semesterID;
            txtSubjectID.Text = subjectID;
            txtSubjectName.Text = subjectName;
            txtScore1.Text = score1;
            txtScore2.Text = score2;
            txtAverage.Text = average;
            this.studentID = studentID;
            this.form1 = f;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string errorFormat = "";
            string errorNull = "";
            float score1 = 0;
            float score2 = 0;
            int semesterID = int.Parse(txtSemesterID.Text);
            // check score1 is null?
            if(txtScore1.Text.Length == 0 || txtScore1.Text == null)
            {
                errorNull += "Score1 can not null\n";
            }
            // check score2 is null?
            if (txtScore2.Text.Length == 0 || txtScore2.Text == null)
            {
                errorNull += "Score2 can not null";
            }
            // show message if null
            if(errorNull.Length > 0)
            {
                MessageBox.Show(errorNull);
            }
            else
            {
                // check score1 format
                try
                {
                    score1 = float.Parse(txtScore1.Text);
                }
                catch (FormatException)
                {
                    errorFormat += "Score1 must be a number\n";
                }
                // check score2 format
                try
                {
                    score2 = float.Parse(txtScore2.Text);
                }
                catch (FormatException)
                {
                    errorFormat += "Score2 must be a number";
                }
                // show message if wrong format
                if (errorFormat.Length > 0)
                {
                    MessageBox.Show(errorFormat);
                }
                else
                {
                    // check score1 < 0?
                    if(score1 < 0 || score1 > 10)
                    {
                        errorFormat += "Score1 must able from 0 to 10\n";
                    }
                    // check score2 < 0?
                    if (score2 < 0 || score2 > 10)
                    {
                        errorFormat += "Score2 must able from 0 to 10";
                    }
                    // show message if score < 0
                    if (errorFormat.Length > 0)
                    {
                        MessageBox.Show(errorFormat);
                    }
                    else
                    {
                        float average = (score1 + score2) / 2;
                        try
                        {
                            StudentDAO dao = new StudentDAO();
                            bool check = dao.updateScore(semesterID, studentID, score1, score2, average);
                            if (check == true)
                            {
                                txtAverage.Text = average.ToString();
                                form1.updateTableScore(studentID);
                                MessageBox.Show("Change success");                              
                            }
                            else
                            {
                                MessageBox.Show("Change fail !!!");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }                   
                }
            }                         
        }        
    }
}
