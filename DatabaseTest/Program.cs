using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DatabaseTest
{
    class Program
    {
        private static string
            ConnectionString = 
                "Data Source=localhost;Initial Catalog=pubs;Integrated Security=SSPI;";
        static void Main(string[] args)
        {
            FirstDemo5();
            FirstDemo4();
            FirstDemo3();
            FirstDemo();
        }

        static void FirstDemo5()
        {
            var sql = @"INSERT INTO [stores]
           (stor_id
           ,stor_name
           ,stor_address
           ,city
           ,[state]
           ,zip)
     VALUES
           (@stor_id
           ,@stor_name
           ,@stor_address
           ,@city
           ,@state
           ,@zip)
";

            var store = new Store { Stor_id  = 3333,
                Stor_name = "2313",
                Stor_Address = "eqw",
                City = "sdfadf",
                State = "2",
                Zip = "231"};

        using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                conn.Execute(sql, store);
                //conn.Execute(sql, new
                //{
                //    stor_id=9999,stor_name="2313", stor_address ="eqw",
                //    city="sdfadf", state="2", zip="231"
                //});
            }
        }

        static void FirstDemo4()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            string inputState1 = "WA"; //Readline


            var stores = conn
                .Query<Store>("select * from stores where state=@inputState",
                    new { inputState=inputState1 }
                    )
                .ToList();
            foreach (var stiore in stores)
            {
                Console.WriteLine($"{stiore.Stor_name}");
            }

            conn.Close();


        }

        private static void FirstDemo()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var command = new SqlCommand("update stores set city='Stockholm' where id=8042", conn);
            command.ExecuteNonQuery();
            conn.Close();

            //Men detta är ju HELT hårdkodat...
        }

        private static void FirstDemo2()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var command = new SqlCommand("update stores set city=@cityNamn where id=@id", conn);
            //command.Parameters.Add(new SqlParameter())
            command.ExecuteNonQuery();
            conn.Close();

        }

        //Get some data
        private static void FirstDemo3()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            var command = new SqlCommand("select * from stores", conn);
            //command.Parameters.Add(new SqlParameter())
            var stores = new List<Store>();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var store = new Store();
                store.Stor_name = reader["Stor_name"].ToString();
                store.Stor_id = Convert.ToInt32(reader["Stor_id"]);
                store.Stor_Address = reader["Stor_Address"].ToString();
                store.City = reader["City"].ToString();
                store.State = reader["State"].ToString();
                store.Zip = reader["Zip"].ToString();
                stores.Add(store);
            }
            //NEVER DO THE ABOVE AGAIN. WHO'S GONNA PAY YOU FOR THAT???
            //Tänk 100 tabeller. 10-30 kolumner per tabell
            //
            //Waste of time...error prone 
            //Hello Dapper
            
            conn.Close();

        }

    }
}
