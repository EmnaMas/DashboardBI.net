using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace DashboardBI.Models
{
    public class AgeModel
    {
        private static string connectionString = "Data Source=DESKTOP-1UDUIV0;Initial Catalog=DataWareHouse;Integrated Security=True;";

        public class AgeData
        {
            public int AgeIDs { get; set; }
            public string AgeDesc { get; set; }
            public int EmailCount { get; set; }
        }

        public List<AgeData> GetEmailsByAge()
        {
            List<AgeData> ageList = new List<AgeData>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetEmailsByAge", connection))
                    {
                        // Ajouter le délai d'exécution maximal de 60 secondes
                        command.CommandTimeout = 180;
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AgeData ageData = new AgeData
                                {
                                    AgeDesc = reader["AgeDesc"].ToString(),
                                    EmailCount = Convert.ToInt32(reader["EmailCount"])
                                };

                                ageList.Add(ageData);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Une erreur est survenue lors de la récupération des données : " + ex.Message);
                }
            }

            return ageList;
        }


       
     



    }

}





















/*  
 *   // Méthode de filtre pour l'âge
 *   public List<AgeData> FilterAgeData(string ageDesc)
        {
            List<AgeData> ageList = GetEmailsByAge();

            if (!string.IsNullOrEmpty(ageDesc))
            {
                // Assurez-vous que les noms de champ sont identiques à ceux du modèle AgeData
                ageList = ageList.Where(age => age.AgeDesc == ageDesc).ToList();
            }

            return ageList;
        }*/