using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DatabaseTest
{

    public class Publishers
    {
        public string Pub_id { get; set; }
        public string Pub_name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public int Age { get; set; }
        public DateTime FirstBookPublished { get; set; }

    }





    class Program
    {
        private static string
            ConnectionString = 
                "Data Source=localhost;Initial Catalog=pubs;Integrated Security=SSPI;";

        static void Demo123()
        {
            //INSERT, UPDATE, DELETE, SELECT MED PARAMETRAR
            string sql = @"INSERT INTO [publishers]
           ([pub_id]
           ,[pub_name]
           ,[city]
           ,[state]
           ,[country]
           ,[age]
           ,[firstbookpublished])
     VALUES
           (@pub_id
           ,@pub_name
           ,@city
           ,@state
           ,@country
           ,@age
           ,@firstbookpublished)
";
            int pubid = 1199;
            string name = "Test";//COnsole.Realine
            string city = "Test2";//COnsole.Realine
            string state = "NA";//COnsole.Realine
            string country = "Sverige";//COnsole.Realine
            int age = 1;//COnsole.Realine
            DateTime dt = new DateTime(1972,8,3);

            var o = new
            {
                pub_id = pubid,
                pub_name=name,
                city = city,
                state = state,
                age = age,
                country = country,
                firstbookpublished = dt
            };
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                conn.Execute(sql, o);
                //conn.Execute(sql, new
                //{
                //    stor_id=9999,stor_name="2313", stor_address ="eqw",
                //    city="sdfadf", state="2", zip="231"
                //});
            }

        }


        static void Main(string[] args)
        {
            FirstDemo5();
            FirstDemo5();
            Demo123();
            FirstDemo5();
            FirstDemo3();
            FirstDemo();
        }

        public class OrderTotal
        {
            public int OrderID { get; set; }
            public decimal Total { get; set; }
        }
        static void FirstDemo5()
        {
            string conn2 =
                "Data Source=localhost;Initial Catalog=Northwind;Integrated Security=SSPI;";


            int minTotal = 1000; //Matas in 


            var sql1 = @"select o.OrderID, sum(od.Quantity * od.UnitPrice) as Total from Orders o JOIN [Order Details] od on
od.OrderID = o.OrderID
group by o.OrderID
having sum(od.Quantity * od.UnitPrice) > @minTotal";
            using (var conn = new SqlConnection(conn2))
            {
                conn.Open();
                var list = conn.Query<OrderTotal>(sql1,new{ minTotal = minTotal });
                foreach (var q in list)
                {
                    Console.WriteLine($"{q.OrderID}  {q.Total}");
                }
            }




            var newCity = "Sthlm";
            string id = "7066";

            var updateSql = @"update stores set city = @cityNamn where stor_id = @id";
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                conn.Execute(updateSql, new
                {
                    cityNamn = newCity,
                    stor_id=id
                });
            }



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

            //var store = new Store { Stor_id  = 3333,
            //    Stor_name = "2313",
            //    Stor_Address = "eqw",
            //    City = "sdfadf",
            //    State = "2",
            //    Zip = "231"};

        using (var conn = new SqlConnection(ConnectionString))
        {
            var store = new {
                Stor_id = 3334,
                Stor_name = "2313",
                Stor_Address = "eqw",
                City = "sdfadf",
                State = "2",
                Zip = "231"
            };
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
