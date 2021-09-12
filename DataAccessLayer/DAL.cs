using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Model;
namespace DataAccessLayer
{
    public class DAL
    {
        static string constr = "data source=LENOVO\\SQLEXPRESS;initial catalog=SMS;integrated security=True;";
        public void DisplayStudents()
        {
            DataTable DT = ExecuteData("select * from StudentDetails");
            if (DT.Rows.Count > 0)
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("=====================================================================");
                Console.WriteLine("*********************     ACADEMY RECORDS   *************************");
                Console.WriteLine("=====================================================================");
                foreach (DataRow row in DT.Rows)
                {
                    Console.WriteLine(row["Stud_ID"].ToString() + "|\t|" + row["Stud_FirstName"].ToString() + "|\t|" + row["Stud_LastName"].ToString() + "|\t|" + row["Stud_Address"].ToString() + "|\t|" + row["Stud_Age"].ToString() + "|\t|" + row["Stud_Email"].ToString() + "|\t|" + row["Stud_Trainer"].ToString() + "|\t|" + row["Stud_BatchName"].ToString() + "|\t|" + row["Stud_Marks_Test1"].ToString() + "|\t|" + row["Stud_Marks_Test2"].ToString() + "|\t|" + row["Stud_Result"].ToString() + "\n");
                }
                Console.WriteLine("======================================================================" + Environment.NewLine);
            }
            else
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("No Student found in database table!!!");
                Console.Write(Environment.NewLine);
            }
        }
        public DataTable ExecuteData(String Query)
        {
            DataTable result = new DataTable();

            try
            {
                using (SqlConnection sqlcon = new SqlConnection(constr))
                {
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand(Query, sqlcon);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(result);
                    sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }
        public void AddStudent()
        {
            string Stud_ID = string.Empty;
            string Stud_FirstName = string.Empty;
            string Stud_LastName = string.Empty;
            string Stud_Address = string.Empty;
            string Stud_Age = string.Empty;
            string Stud_Email = string.Empty;
            string Stud_Trainer = string.Empty;
            string Stud_BatchName = string.Empty;
            int Stud_Marks_Test1;
            int Stud_Marks_Test2;
            int Stud_Result;

            Console.WriteLine("Insert new StudentDetails: ");

            Console.Write("Enter ID: ");
            Stud_ID = Console.ReadLine();

            Console.Write("Enter FirstName: ");
            Stud_FirstName = Console.ReadLine();
            while (string.IsNullOrEmpty(Stud_FirstName))
            {

                Console.WriteLine("Student Firstname cannot be blank");
                Console.WriteLine("Enter FirstName once again");
                Stud_FirstName = Console.ReadLine();
            }


            Console.Write("Enter LastName: ");
            Stud_LastName = Console.ReadLine();
            while (string.IsNullOrEmpty(Stud_LastName))
            {

                Console.WriteLine("Student Lastname cannot be blank");
                Console.WriteLine("Enter LastName once again");
                Stud_LastName = Console.ReadLine();
            }

            Console.Write("Enter Address: ");
            Stud_Address = Console.ReadLine();

            Console.Write("Enter Age: ");
            Stud_Age = Console.ReadLine();
            while (true)
            {
                Console.Write("Enter Email: ");
                Stud_Email = Console.ReadLine();
                bool status = isValidEmail(Stud_Email);
                if (status == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("You have to enter valid email");
                    continue;
                }

            }

            Console.Write("Enter Trainer: ");
            Stud_Trainer = Console.ReadLine();

            while (true)
            {
                Console.Write("Enter Stud_BatchName: ");
                Stud_BatchName = Console.ReadLine();
                if (Stud_Trainer == Stud_BatchName)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            while (true)
            {
                Console.Write("Enter Marks for Test1: ");
                Stud_Marks_Test1 = Convert.ToInt32(Console.ReadLine());

                if (Stud_Marks_Test1 > 50)
                {
                    Console.WriteLine("Marks Should not be greater than 50");
                    continue;
                }
                else

                {

                    break;
                }
            }

            while (true)
            {
                Console.Write("Enter Marks for Test2: ");
                Stud_Marks_Test2 = Convert.ToInt32(Console.ReadLine());

                if (Stud_Marks_Test1 > 50)
                {
                    Console.WriteLine("Marks Should not be greater than 50");
                    continue;
                }
                else

                {

                    break;
                }
            }

            Console.Write("Enter Result: ");
            Stud_Result = Convert.ToInt32(Console.ReadLine());

            if (((Stud_Marks_Test1 + Stud_Marks_Test2) / 2) > 50)
            {
                Console.WriteLine("Student Fail");

            }
            else
            {
                Console.WriteLine("Student pass");

            }

            ExecuteCommand(string.Format("Insert into StudentDetails(Stud_ID,Stud_FirstName,Stud_LastName,Stud_Address,Stud_Age,Stud_Email,Stud_Trainer, Stud_BatchName,Stud_Marks_Test1,Stud_Marks_Test2, Stud_Result) values ('{0}','{1}','{2}','{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}','{10}')", Stud_ID, Stud_FirstName, Stud_LastName, Stud_Address, Stud_Age, Stud_Email, Stud_Trainer, Stud_BatchName, Stud_Marks_Test1, Stud_Marks_Test2, Stud_Result));


        }

        public static bool isValidEmail(string inputEmail)
        {

            // This Pattern is use to verify the email
            string strRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase);

            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        public bool ExecuteCommand(string queury)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(constr))
                {
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand(queury, sqlcon);
                    cmd.ExecuteNonQuery();
                    sqlcon.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
                throw;
            }
            return true;
        }
        public void EditStudent()
        {
            string Stud_ID = string.Empty;
            string Stud_FirstName = string.Empty;
            string Stud_LastName = string.Empty;
            string Stud_Address = string.Empty;
            string Stud_Age = string.Empty;
            string Stud_Email = string.Empty;
            string Stud_Trainer = string.Empty;
            string Stud_BatchName = string.Empty;
            string Stud_Marks_Test1 = string.Empty;
            string Stud_Marks_Test2 = string.Empty;
            string Stud_Result = string.Empty;
            Console.WriteLine("Update StudentDetails:  ");
            Console.WriteLine("Insert new Student: ");

            Console.Write("Enter ID: ");
            Stud_ID = Console.ReadLine();

            Console.Write("Enter FirstName: ");
            Stud_FirstName = Console.ReadLine();

            Console.Write("Enter LastName: ");
            Stud_LastName = Console.ReadLine();

            Console.Write("Enter Address: ");
            Stud_Address = Console.ReadLine();

            Console.Write("Enter Age: ");
            Stud_Age = Console.ReadLine();

            Console.Write("Enter Email: ");
            Stud_Email = Console.ReadLine();

            Console.Write("Enter Trainer: ");
            Stud_Trainer = Console.ReadLine();

            Console.Write("Enter Stud_BatchName: ");
            Stud_BatchName = Console.ReadLine();

            Console.Write("Enter Marks for Test1: ");
            Stud_Marks_Test1 = Console.ReadLine();


            Console.Write("Enter Marks for Test2: ");
            Stud_Marks_Test2 = Console.ReadLine();

            Console.Write("Enter Result: ");
            Stud_Result = Console.ReadLine();

            ExecuteCommand(String.Format("Update StudentDetails set Stud_FirstName = '{0}', Stud_LastName = '{1}',Stud_Address = '{2}',Stud_Age = '{3}',Stud_Email = '{4}',Stud_Trainer = '{5}',Stud_BatchName = '{6}',Stud_Marks_Test1 = '{7}',Stud_Marks_Test2 = '{8}', Stud_Result = '{9}',Stud_ID = '{10}' where Stud_ID = {10}", Stud_FirstName, Stud_LastName, Stud_Address, Stud_Age, Stud_Email, Stud_Trainer, Stud_BatchName, Stud_Marks_Test1, Stud_Marks_Test2, Stud_Result, Stud_ID));
        }


        public void DeleteStudent()
        {
            string Stud_ID = string.Empty;

            Console.WriteLine("Delete StudentDetails: ");

            Console.Write("Enter Stud_ID: ");
            Stud_ID = Console.ReadLine();

            ExecuteCommand(String.Format("Delete from StudentDetails where Stud_ID = '{0}'", Stud_ID));

            Console.WriteLine("Delete student from StudentDetails!" + Environment.NewLine);
        }


        public void EditMarks()
        {
            string Stud_ID = string.Empty;
            string Stud_Marks_Test1 = string.Empty;
            string Stud_Marks_Test2 = string.Empty;

            Console.WriteLine("Add Student Marks:  ");
            Console.Write("Enter Stud_ID: ");
            Stud_ID = Console.ReadLine();

            Console.Write("Enter Marks for Test1: ");
            Stud_Marks_Test1 = Console.ReadLine();

            Console.Write("Enter Marks for Test2: ");
            Stud_Marks_Test2 = Console.ReadLine();



            ExecuteCommand(String.Format("update StudentDetails set Stud_Marks_Test1 = '{0}',Stud_Marks_Test2 = '{1}',Stud_ID = '{2}' where Stud_ID = {2}", Stud_Marks_Test1, Stud_Marks_Test2, Stud_ID));
        }


        public void DeleteResult()
        {
            DataTable DT = ExecuteData(" UPDATE StudentDetails SET Stud_Marks_Test1 = Null; ");
            if (DT.Rows.Count > 0)
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("=====================================================================");
                Console.WriteLine("*******************   Delete Result Of Student   ********************");
                Console.WriteLine("=====================================================================");
                foreach (DataRow row in DT.Rows)
                {
                    Console.WriteLine(row["Stud_ID"].ToString() + "|\t|" + row["Stud_FirstName"].ToString() + "|\t|" + row["Stud_LastName"].ToString() + "|\t|" + row["Stud_Address"].ToString() + "|\t|" + row["Stud_Age"].ToString() + "|\t|" + row["Stud_Email"].ToString() + "|\t|" + row["Stud_Trainer"].ToString() + "|\t|" + row["Stud_BatchName"].ToString() + "|\t|" + row["Stud_Marks_Test1"].ToString() + "|\t|" + row["Stud_Marks_Test2"].ToString() + "|\t|" + row["Stud_Result"].ToString());
                }
                Console.WriteLine("======================================================================");
                Console.WriteLine("Delete Marks of Test1" + Environment.NewLine);
            }


            else
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("!!!Delete Marks of Test1 for all Student!!!");
                Console.Write(Environment.NewLine);
            }
        }
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^   RECORDS       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        public void TopStudentsForDotNet()
        {
            DataTable DT = ExecuteData("select Stud_ID,Stud_FirstName,Stud_Marks_Test1 from StudentDetails where Stud_BatchName='DotNet_Batch_1' order by Stud_Marks_Test1 desc");
            if (DT.Rows.Count > 0)
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("=====================================================================");
                Console.WriteLine("*************  Merit List Of Students For DotNet Batch  *************");
                Console.WriteLine("=====================================================================");
                foreach (DataRow row in DT.Rows)
                {
                    Console.WriteLine(row["Stud_ID"].ToString() + "|\t|" + row["Stud_FirstName"].ToString() + "|\t|" + row["Stud_Marks_Test1"].ToString());
                }
                Console.WriteLine("======================================================================");
                Console.WriteLine("Merit List of Student For DotNet Batch" + Environment.NewLine);
            }
        }
        public void TopStudents()
        {
            DataTable DT = ExecuteData("SELECT Stud_ID,Stud_FirstName,Stud_Trainer,Stud_Marks_Test1,Stud_Marks_Test2,Stud_Result,RANK() OVER (ORDER BY Stud_Marks_Test2 DESC) merit_list FROM StudentDetails");
            if (DT.Rows.Count > 0)
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("=====================================================================");
                Console.WriteLine("*************  Merit List Of Students For All Batches   *************");
                Console.WriteLine("=====================================================================");
                foreach (DataRow row in DT.Rows)
                {
                    Console.WriteLine(row["Stud_ID"].ToString() + "|\t|" + row["Stud_FirstName"].ToString() + "|\t|" + row["Stud_Trainer"].ToString() + "|\t|" + row["Stud_Marks_Test1"].ToString() + "|\t|" + row["Stud_Marks_Test2"].ToString() + "|\t|" + row["Stud_Result"].ToString() + "|\t|" + row["merit_list"].ToString());
                }
                Console.WriteLine("======================================================================");
                Console.WriteLine("Merit List of Students" + Environment.NewLine);
            }


            else
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("!!!No Records Found!!!");
                Console.Write(Environment.NewLine);
            }
        }
        public void AvgResult()
        {
            DataTable DT = ExecuteData("SELECT  Stud_BatchName,Stud_Trainer ,AVG((Stud_Marks_Test1 + Stud_Marks_Test2)/2)as Avg_Percentage FROM StudentDetails  Group by  Stud_BatchName,Stud_Trainer");
            if (DT.Rows.Count > 0)
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("=====================================================================");
                Console.WriteLine("*************** Average Percentage Result of Batches  ***************");
                Console.WriteLine("=====================================================================");
                foreach (DataRow row in DT.Rows)
                {
                    Console.WriteLine(row["Stud_Trainer"].ToString() + "|\t|" + row["Stud_BatchName"].ToString() + "|\t|" + row["Avg_Percentage"].ToString());
                }
                Console.WriteLine("======================================================================" + Environment.NewLine);

            }


            else
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("!!!No Records Found!!!");
                Console.Write(Environment.NewLine);
            }
        }
        public void FailedStudents()
        {
            DataTable DT = ExecuteData("select Stud_FirstName,Stud_batchName from StudentDetails where Stud_result<=50 ");
            if (DT.Rows.Count > 0)
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("=====================================================================");
                Console.WriteLine("*******************    List of Fail Students     ********************");
                Console.WriteLine("=====================================================================");
                foreach (DataRow row in DT.Rows)
                {
                    Console.WriteLine(row["Stud_FirstName"].ToString() + "|\t|" + row["Stud_BatchName"]);
                }
                Console.WriteLine("======================================================================" + Environment.NewLine);

            }


            else
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("!!!No record Found!!!");
                Console.Write(Environment.NewLine);
            }
        }
    }
}



