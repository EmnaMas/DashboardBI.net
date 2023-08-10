using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DashboardBI.Models
{
    public class StatusModel
    {
        private static string connectionString = "Data Source=DESKTOP-1UDUIV0;Initial Catalog=DataWareHouse;Integrated Security=True;";

        public class StatusData
        {
            public string StatusDesc { get; set; }
            public double EmailCount { get; set; }
        }

        public List<StatusData> GetEmailsAverageByStatus()
        {
            List<StatusData> statusList = new List<StatusData>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetEmailsAverageByStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StatusData statusData = new StatusData
                                {
                                    StatusDesc = reader["StatusDesc"].ToString(),
                                    EmailCount = Convert.ToDouble(reader["EmailCount"])
                                };

                                statusList.Add(statusData);
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

            return statusList;
        }
    }
}
