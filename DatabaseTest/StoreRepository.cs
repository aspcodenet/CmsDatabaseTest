using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DatabaseTest
{
    public class StoreRepository
    {
        private static string
            ConnectionString =
                "Data Source=localhost;Initial Catalog=pubs;Integrated Security=SSPI;";

        
        public List<Store> GetAll()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                return conn.Query<Store>("select * from stores").ToList();
            }
        }

        public Store GetById(int id)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                return conn.Query<Store>("select * from stores where stor_id=@stor_id",
                    new {stor_id = id}).First();
            }
        }


        public void Add(Store newStore)
        {
        }

        public StoreRepository()
        {
        }
    }
}