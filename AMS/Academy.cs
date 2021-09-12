using System;
using DataAccessLayer;
using Model;

namespace AMS
{
    class Academy
    {
        static void Main(string[] args)
        {

            //^^^^^^^^^^^^^^^^^^^^^^^^    CRUD Operations    ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            DAL C = new DAL();
            C.DisplayStudents();

            C.AddStudent();
            C.DisplayStudents();

            C.EditStudent();
            C.DisplayStudents();

            C.DeleteStudent();
            C.DisplayStudents();

            C.EditMarks();
            C.DisplayStudents();

            C.DeleteResult();
            C.DisplayStudents();

            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^Records ^ ^^^^^^^^^^^^^^^^^^^^^^^^^C.TopStudentsForDotNet();

            C.TopStudents();

            C.AvgResult();

            C.FailedStudents();


        }
    }
}
