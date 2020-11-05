using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DatabaseTest
{

    public class Student
    {
        public string Fornamn { get; set; }
        public string Efternamn { get; set; }
        public DateTime Birthdate { get; set; }
        public int Kurs_id { get; set; }
    }


    public class StudentRepository
    {
        private static string
            ConnectionString =
                "Data Source=localhost;Initial Catalog=Student;Integrated Security=SSPI;";



        public List<Student> GetAll(DateTime dt)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                return conn.Query<Student>("select * from Student").ToList();
            }
        }


        //public class Param
        //{
        //    public DateTime Datum { get; set; }
        //}
        public List<Student> GetAllBefore(DateTime dt)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                return conn.Query<Student>("select * from Student where birthdate <@dt", new  { dt  }).ToList();

                //var p = new Param();
                //p.Datum = dt;
                //conn.Open();

                //return conn.Query<Student>("select * from Student where birthdate <@datum", p).ToList();


            }
        }

        public void Insert(Student st)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                conn.Execute(@"insert into Student(fornamn,efternamn,birthdate) 
                    values(@fornamn, @efternamn, @birthdate)", st);


                conn.Execute(@"insert into Student(fornamn,efternamn,birthdate) 
                    values(@fornamn1, @efternamn1, @birthdate1)",new
                {
                    fornamn1 = st.Fornamn,
                    efternamn1 = st.Efternamn,
                    Birthdate1 = st.Birthdate
                });

            }
        }
    }


    internal class StudentAdmin
    {
        public void Run()
        {
            var rep = new StudentRepository();
            
            var st = new Student();
            st.Efternamn = "Holnverg";
            st.Fornamn = "Stefan ";
            st.Birthdate = new DateTime(1972, 8, 3);

            rep.Insert(st);
            foreach(var s in rep.GetAllBefore(new DateTime(1980,1,1)))
            {
                //s.Efternamn
            }
        }
    }
}