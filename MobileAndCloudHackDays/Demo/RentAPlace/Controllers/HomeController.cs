using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using RentAPlace.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentAPlace.Controllers
{
    public class HomeController : Controller
    {
        private const string _searchServiceName = "places";
        private const string _indexName = "ix-places";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(InputSearch model)
        {
            var primaryKey = ConfigurationManager.AppSettings["searchPrimaryKey"];
            var _searchClient = new SearchServiceClient(_searchServiceName, new SearchCredentials(primaryKey));
            var _indexClient = _searchClient.Indexes.GetClient(_indexName);

            SearchParameters parameters = new SearchParameters()
            {
                SearchMode = SearchMode.Any,
                Top = 10,
                Filter = model.Filter,
                Skip = model.CurrentPage > 0 ? model.CurrentPage - 1 : 0,
                IncludeTotalResultCount = true,                
                Facets = new List<string> { "city", "state", "neighborhood", "placeType", "numberOfBedrooms" , "numberOfSuites", "floorArea", "locationValue"}
            };

            var response = _indexClient.Documents.Search(model.Q, parameters);
            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new PlaceModel() { Results = response.Results, Facets = response.Facets, Count = Convert.ToInt32(response.Count) }
            };

        }
    }
}