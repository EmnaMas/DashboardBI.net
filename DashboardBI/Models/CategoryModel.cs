using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardBI.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    namespace DashboardBI.Models
    {
        public class CategoryModel
        {
            private static string connectionString = "Data Source=DESKTOP-1UDUIV0;Initial Catalog=DataWareHouse;Integrated Security=True;";

            public class CategoryData
            {
                public string Category { get; set; }
                public int EmailCount { get; set; }
            }

            public List<CategoryData> GetEmailsByCategory()
            {
                List<CategoryData> categoryList = new List<CategoryData>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("GetEmailsByCategory", connection))
                        {
                            // Ajouter le délai d'exécution maximal de 60 secondes
                            command.CommandTimeout = 90;
                            command.CommandType = CommandType.StoredProcedure;

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    CategoryData categoryData = new CategoryData
                                    {
                                        Category = reader["Category"].ToString(),
                                        EmailCount = Convert.ToInt32(reader["EmailCount"])
                                    };

                                    categoryList.Add(categoryData);
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

                return categoryList;
            }
        }
    }

}