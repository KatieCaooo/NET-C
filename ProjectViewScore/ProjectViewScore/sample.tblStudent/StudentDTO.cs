using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewScore.sample.tblStudent
{
    public class StudentDTO
    {
        private string studentID;
        private string firstName;
        private string lastName;
        private string specialityID;
        private string sex;
        private DateTime birthday;



        public StudentDTO(string firstName, string lastName, string specialityID, string sex, DateTime birthday)
        {          
            this.firstName = firstName;
            this.lastName = lastName;
            this.specialityID = specialityID;
            this.sex = sex;
            this.birthday = birthday;
        }

        public StudentDTO(string studentID)
        {
            this.studentID = studentID;
        }

        public string StudentID { get => studentID; set => studentID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string SpecialityID { get => specialityID; set => specialityID = value; }
        public string Sex { get => sex; set => sex = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
    }
}
