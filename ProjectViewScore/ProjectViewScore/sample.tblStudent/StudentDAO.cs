using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using ProjectViewScore.sample.connection;

namespace ProjectViewScore.sample.tblStudent
//namespace ProjectViewScore.sample.MyConnection
{
    public class StudentDAO
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        DbDataReader reader = null;

        private void closeConnection()
        {
            if (reader != null)
            {
                reader.Close();
                //reader.Dispose();
            }
            if (cmd != null)
            {
                cmd.Dispose();
            }
            if (con != null)
            {
                con.Close();
                //con.Dispose();
            }
        }

        public List<StudentDTO> loadStudentID()
        {
            List<StudentDTO> result = null;
            try
            {
                con = MyConnection.getMyConnection();
                if(con != null)
                {
                    string sql = "Select MASV from SVIEN";
                    cmd = con.CreateCommand();
                    cmd.CommandText = sql;
                    reader = cmd.ExecuteReader();

                    string studentID = "aa";
                    result = new List<StudentDTO>();

                    while (reader.Read())
                    {
                        int studentIdIndext = reader.GetOrdinal("MASV");
                        studentID = reader.GetString(studentIdIndext);

                        StudentDTO dto = new StudentDTO(studentID);
                        result.Add(dto);
                    }
                   
                }
            }catch(SqlException)
            {

            }
            finally
            {
                closeConnection();
            }
            return result;
        }
        public List<StudentDTO> loadInforStudent(string studentID)
        {
            List<StudentDTO> result = null;
            try
            {
                con = MyConnection.getMyConnection();
                if(con != null)
                {
                    string sql = "Select TEN, HO, MAKH, SEX, NGAYSINH from SVIEN where MASV = @studentID";
                    cmd = con.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@studentID", SqlDbType.NVarChar).Value = studentID;
                    reader = cmd.ExecuteReader();

                    string firstName = "";
                    string lastName = "";
                    string specialityID = "";
                    string sex = "";
                    DateTime birthday;
                    result = new List<StudentDTO>();
                    while (reader.Read())
                    {
                        int indextFirstName = reader.GetOrdinal("TEN");
                        firstName = reader.GetString(indextFirstName);

                        int indextLastName = reader.GetOrdinal("HO");
                        lastName = reader.GetString(indextLastName);

                        int indextSpecialityID = reader.GetOrdinal("MAKH");
                        specialityID = reader.GetString(indextSpecialityID);

                        int indextBirthday = reader.GetOrdinal("NGAYSINH");
                        birthday = reader.GetDateTime(indextBirthday);

                        int indextSex = reader.GetOrdinal("SEX");
                        sex = reader.GetString(indextSex);

                        StudentDTO dto = new StudentDTO(firstName, lastName, specialityID, sex, birthday);
                        result.Add(dto);
                       
                    }
                }
            }
            catch (SqlException)
            {

            }
            finally
            {
                closeConnection();
            }
            return result;
        }
        public SqlDataAdapter loadScoreData(string studentID)
        {
            SqlDataAdapter da = null;
            try
            {
                con = MyConnection.getMyConnection();
                if(con != null)
                {
                    string sql = "select HP.MAHP as SemesterID, MH.MAMH as SubjectID, MH.TENMH as SubjectName," +
                        " KQ.DIEM as Score1, KQ.DIEM2 as Score2, KQ.DIEMTB as Average " +
                        "from MHOC as MH, HPHAN as HP, KQUA as KQ " +
                        "where MH.MAMH = (select HP.MAMH " +
                                         "where HP.MAHP = (select KQ.MAHP " +
                                                         " where KQ.MASV = @studentID ))";
                    cmd = con.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@studentID", SqlDbType.NVarChar).Value = studentID;
                    da = new SqlDataAdapter(cmd);


                }
            }
            catch (SqlException)
            {

            }
            finally
            {
                closeConnection();
            }
            return da;
        }
        public bool updateScore(int semesterID, string studentID, float score1, float score2, float average)
        {
            try
            {
                con = MyConnection.getMyConnection();
                if(con != null)
                {
                    string sql = "Update KQUA set DIEM = @score1, DIEM2 = @score2, DIEMTB = @average " +
                        "where MASV = @studentID and MAHP = @semesterID";
                    cmd = con.CreateCommand();
                    cmd.CommandText = sql;
                    
                    cmd.Parameters.Add("@score1", SqlDbType.Float).Value = score1;
                    cmd.Parameters.Add("@score2", SqlDbType.Float).Value = score2;
                    cmd.Parameters.Add("@average", SqlDbType.Float).Value = average;
                    cmd.Parameters.Add("@semesterID", SqlDbType.Int).Value = semesterID;
                    cmd.Parameters.Add("@studentID", SqlDbType.NVarChar).Value = studentID;

                    int row = cmd.ExecuteNonQuery();
                    if(row > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                closeConnection();
            }
            return false;
        }
    }
}
