using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DatabaseTest
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Homepage { get; set; }
    }

    public class NorthwindRepository
    {
        private string connString =
            "Data Source=localhost;Initial Catalog=Northwind;Integrated Security=SSPI;";


        public Supplier GetSupplier(int supplierId)
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                return conn.Query<Supplier>("select * from Suppliers where supplierid=@p1",new{ p1 = supplierId }).FirstOrDefault();
            }

        }


        public List<Supplier> GetAllSuppliers()
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                return conn.Query<Supplier>("select * from Suppliers").ToList();
            }

        }

        public void Insert(Supplier supplier)
        {

            string sql = @"
INSERT INTO [Suppliers]
           ([CompanyName]
           ,[ContactName]
           ,[ContactTitle]
           ,[Address]
           ,[City]
           ,[Region]
           ,[PostalCode]
           ,[Country]
           ,[Phone]
           ,[Fax]
           ,[HomePage])
     VALUES
           (@CompanyName
           ,@ContactName
           ,@ContactTitle
           ,<Address, nvarchar(60),>
           ,<City, nvarchar(15),>
           ,<Region, nvarchar(15),>
           ,<PostalCode, nvarchar(10),>
           ,<Country, nvarchar(15),>
           ,<Phone, nvarchar(24),>
           ,<Fax, nvarchar(24),>
           ,<HomePage, ntext,>)
GO


";
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                conn.Execute(sql, supplier);
            }
        }



    public void Update(Supplier supplier)
        {

            string sql = @"UPDATE [Suppliers]
   SET [CompanyName] = @CompanyName
      ,[ContactName] = @ContactName
      ,[ContactTitle] = @ContactTitle
      ,[Address] = @Address
      ,[City] = @City
      ,[Region] = @Region
      ,[PostalCode] = @PostalCode
      ,[Country] = @Country
      ,[Phone] = @Phone
      ,[Fax] = @Fax
      ,[HomePage] = @HomePage
 WHERE SupplierID = @SupplierID
";
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                conn.Execute(sql, supplier);
            }
        }
    }
}