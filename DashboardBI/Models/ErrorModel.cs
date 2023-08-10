using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DashboardBI.Models
{
    public class ErrorModel
    {

        private static string connectionString = "Data Source=DESKTOP-1UDUIV0;Initial Catalog=DataWareHouse;Integrated Security=True;";

        public class ErrorData
        {
            public string Error { get; set; }
            public int EmailCount { get; set; }
        }

        public List<ErrorData> GetEmailsByEmailError()
        {
            List<ErrorData> errorList = new List<ErrorData>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetEmailsByEmailError", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ErrorData errorData = new ErrorData
                                {
                                    Error = reader["Error"].ToString(),
                                    EmailCount = Convert.ToInt32(reader["EmailCount"])
                                };

                                errorList.Add(errorData);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Lancer une exception personnalisée pour informer l'utilisateur
                    throw new Exception("Une erreur est survenue lors de la récupération des données : " + ex.Message);
                }
            }

            return errorList;
        }
    }
}
