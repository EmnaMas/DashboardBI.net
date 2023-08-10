using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DashboardBI.Models
{
    public class TimeOfDayModel
    {
        private static string connectionString = "Data Source=DESKTOP-1UDUIV0;Initial Catalog=DataWareHouse;Integrated Security=True;";

        // Déplacer la déclaration de TimeOfDayData en dehors de la classe TimeOfDayModel
        public class TimeOfDayData
        {
            public string TimeOfDay { get; set; }
            public int EmailCount { get; set; }
        }

        public static List<TimeOfDayData> GetEmailsByTimeOfDayOpened()
        {
            List<TimeOfDayData> dataList = new List<TimeOfDayData>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetEmailsByTimeOfDayOpened", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TimeOfDayData data = new TimeOfDayData
                            {
                                TimeOfDay = reader["TimeOfDay"].ToString(),
                                EmailCount = Convert.ToInt32(reader["EmailCount"])
                            };
                            dataList.Add(data);
                        }
                    }
                }
            }

            return dataList;
        }
    }
}
