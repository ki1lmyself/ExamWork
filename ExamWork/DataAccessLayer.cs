using Microsoft.Data.SqlClient;
using System.Windows;

namespace ExamWork
{
    public static class DataAccessLayer
    {
        public static string Server { get; set; } = @"localhost";//prserver\SQLEXPRESS
        //public static string Username { get; set; } = @"LAPTOP-E9101FGT\sofia";
        //public static string Login { get; set; } = "ispp2104";
        //public static string Password { get; set; } = "2104";
        public static string Database { get; set; } = "Exam";//ispp2104
        public static bool TrustServerCertificate { get; set; } = true;

        public static string ConnectionString
        {
            get
            {
                SqlConnectionStringBuilder builder = new()
                {
                    DataSource = Server,
                    //UserID = Username,
                    //Password = Password,

                    InitialCatalog = Database,
                    TrustServerCertificate = true,
                };
                return builder.ConnectionString;
            }
        }

        public static bool CheckUser(string UserLogin, string UserPassword)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException ex)
            {
                // Handle the exception
                MessageBox.Show("Error opening connection: " + ex.Message);
                return false;
            }

            string query = "SELECT COUNT(*) FROM ExamUser WHERE UserLogin = @UserLogin AND UserPassword = @UserPassword";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserLogin", UserLogin);
            command.Parameters.AddWithValue("@UserPassword", UserPassword);

            int count = (int)command.ExecuteScalar();

            return count > 0;
        }




        public static List<ExamProduct> GetProductList()
        {
            List<ExamProduct> products = new List<ExamProduct>();

            string query = "SELECT ProductName, ProductDescription, ProductManufacturer, ProductCost FROM ExamProduct";

            using SqlConnection connection = new(ConnectionString);
            connection.Open();

            SqlCommand command = new(query, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var product = new ExamProduct()
                    {
                        ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                        ProductDescription = reader.GetString("ProductDescription"),
                        ProductManufacturer = reader.GetString("ProductManufacturer"),
                        ProductCost = Convert.ToDouble(reader.GetDecimal("ProductPrice")),
                    };
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
        }
        public class ExamProduct
        {
            public string ProductName { get; set; }
            public string ProductDescription { get; set; }
            public string ProductManufacturer { get; set; }
            public double ProductCost { get; set; }
        }
    }
}
