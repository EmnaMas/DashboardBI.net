using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DashboardBI.Models;
using DashboardBI.Models.DashboardBI.Models;

namespace DashboardBI.Controllers
{



    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            GenderModel genderModel = new GenderModel();

            // Vérifier si la base de données est connectée
            bool isDatabaseConnected = genderModel.IsDatabaseConnected();

            if (isDatabaseConnected)
            {
                // Instancier le modèle genderModel
                var genderDataList = genderModel.GetEmailsByGender("m");
                ViewData["GenderData"] = Newtonsoft.Json.JsonConvert.SerializeObject(genderDataList);


                // Instancier le modèle AgeModel
                AgeModel ageModel = new AgeModel();
                var ageDataList = ageModel.GetEmailsByAge();
                ViewData["AgeData"] = Newtonsoft.Json.JsonConvert.SerializeObject(ageDataList);



                // Instancier le modèle CategoryModel
                CategoryModel categoryModel = new CategoryModel();
                var categoryDataList = categoryModel.GetEmailsByCategory();
                ViewData["CategoryData"] = Newtonsoft.Json.JsonConvert.SerializeObject(categoryDataList);


                // Instancier le modèle ErrorModel
                ErrorModel errorModel = new ErrorModel();
                var errorData = errorModel.GetEmailsByEmailError();
                ViewData["ErrorData"] = Newtonsoft.Json.JsonConvert.SerializeObject(errorData);

                // Instancier le modèle statusModel
                StatusModel statusModel = new StatusModel();
                var statusData = statusModel.GetEmailsAverageByStatus();
                ViewData["StatusData"] = Newtonsoft.Json.JsonConvert.SerializeObject(statusData);

                // Instancier le modèle statusModel
                //  TimeOfDayModel timeOfDayModel = new TimeOfDayModel();
                //var timeOfDayData = timeOfDayModel.GetEmailsByTimeOfDayOpened();
                // ViewData["timeOfDayData"] = Newtonsoft.Json.JsonConvert.SerializeObject(timeOfDayData);




                return View();
            }
            else
            {
                // La base de données n'est pas connectée, afficher un message d'erreur
                ViewData["ErrorMessage"] = "Impossible de se connecter à la base de données.";
                return View("Error");
            }
        }




    }
}










/*
        // Méthode AJAX pour récupérer les données filtrées d'âge
        [HttpPost]
        public ActionResult GetFilteredAgeData(string ageDesc)
        {
            AgeModel ageModel = new AgeModel();
            var filteredAgeDataList = ageModel.FilterAgeData(ageDesc);

            // Mettez à jour les noms des champs pour correspondre aux noms utilisés dans le graphique
            var chartData = filteredAgeDataList.Select(data => new {
                AgeDesc = data.AgeDesc,
                EmailCount = data.EmailCount
            });

            // Ajouter un point de débogage ici pour vérifier les données
            System.Diagnostics.Debug.WriteLine("Filtered Age Data: ");
            foreach (var data in chartData)
            {
                System.Diagnostics.Debug.WriteLine(data.AgeDesc + ": " + data.EmailCount);
            }

            return Json(chartData, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult GetFilteredGenderData(string genderId)
        {
            GenderModel genderModel = new GenderModel();

            // Convertir "unknown" en minuscules pour correspondre à la valeur dans la base de données
            string genderFilter = genderId == "unknown" ? genderId : genderId.ToLower();

            var filteredGenderDataList = genderModel.GetEmailsByGender(genderFilter);

            // Mettez à jour les noms des champs pour correspondre aux noms utilisés dans le graphique
            var chartData = filteredGenderDataList.Select(data => new {
                Gender = data.Gender,
                EmailCount = data.EmailCount
            });

            return Json(chartData, JsonRequestBehavior.AllowGet);
        }*/