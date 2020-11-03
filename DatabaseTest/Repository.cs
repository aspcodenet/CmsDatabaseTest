using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DatabaseTest
{
    public class Titles
    {
        public string Title_id { get; set; }
        public string BookName { get; set; }
        public string Type { get; set; }
        public string Pub_id { get; set; }

        public Publishers Publisher { get; set; }

        public int Royalty { get;set; }

        public decimal AdvancedPrice { get; set; }

        public DateTime Pubdate { get; set; }

    }

    public class Publishers
    {
        public string Pub_id { get; set; }
        public string Pub_name { get; set; }

    }


    public class Repository
    {
        private static string
            ConnectionString =
                "Data Source=localhost;Initial Catalog=pubs;Integrated Security=SSPI;";

        public List<Titles> GetAllTitles()
        {
            var allPublishers = GetAllPublishers();
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var list = conn.Query<Titles>(@"select Title_id, Title as BookName,Type,Pub_id, Royalty, 
                                                Pubdate,
                                                price + advance as AdvancedPrice from titles").ToList();
                foreach (var t in list)
                {
                    t.Publisher = allPublishers.FirstOrDefault(p => p.Pub_id == t.Pub_id);
                }
                return list;
            }

        }



        public List<Publishers> GetAllPublishers()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                return conn.Query<Publishers>("select Pub_id, Pub_name from publishers").ToList();
            }
        }

    }
}