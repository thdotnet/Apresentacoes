using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AddressGenerator.Extension;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Text.RegularExpressions;
using Microsoft.Azure.Search;
using System.Configuration;
using Microsoft.Spatial;
using Microsoft.Azure.Search.Models;

namespace AddressGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            IndexContent();

            Console.Read();
        }

        private static void IndexContent()
        {
            var places = GenerateRandomData();

            var primaryKey = ConfigurationManager.AppSettings["searchPrimaryKey"];

            var searchServiceName = "places";
            var indexName = "ix-places";

            SearchServiceClient searchClient = new SearchServiceClient(searchServiceName, new SearchCredentials(primaryKey));
            SearchIndexClient indexClient = searchClient.Indexes.GetClient(indexName);

            UploadDocuments(indexClient, places);
        }

        private static List<Place> GenerateRandomData()
        {
            const string googleMapsApi = "https://www.google.com.br/maps/place/";

            var driver = new FirefoxDriver();
            var places = new List<Place>();

            for (int i = 0; i < 90; i++)
            {
                try
                {
                    var randomStreet = AddressGenerator.Get();

                    driver.Navigate().GoToUrl(googleMapsApi + randomStreet);

                    Thread.Sleep(5000);

                    var address = Address.ExtractAddress(driver.Url, randomStreet);
                    places.Add(new Place(address));
                }
                catch
                {

                }
            }

            driver.Quit();
            driver.Dispose();

            return places;
        }

        private static void UploadDocuments(SearchIndexClient indexClient, List<Place> places)
        {
            var documents = places.Select(x => new PlaceDocument 
            { 
                Id = Guid.NewGuid().ToString(),
                City = x.Address.City,
                State = x.Address.State,
                StreetName = x.Address.FullStreet,
                Neighborhood = x.Address.Neighborhood,
                FloorArea = x.FloorArea,
                Geolocation = GeographyPoint.Create(x.Address.Latitude, x.Address.Longitude),
                LocationValue = x.LocationValue,                
                NumberOfBedrooms = x.NumberOfBedrooms,
                NumberOfSuites = x.NumberOfSuites,
                PlaceType = x.Type                
            }).AsEnumerable();

            try
            {
                var batch = IndexBatch.Upload(documents);
                indexClient.Documents.Index(batch);
            }
            catch (IndexBatchException e)
            {
                // Sometimes when your Search service is under load, indexing will fail for some of the documents in
                // the batch. Depending on your application, you can take compensating actions like delaying and
                // retrying. For this simple demo, we just log the failed document keys and continue.
                Console.WriteLine(
                    "Failed to index some of the documents: {0}",
                    String.Join(", ", e.IndexingResults.Where(r => !r.Succeeded).Select(r => r.Key)));
            }

            // Wait a while for indexing to complete.
            Thread.Sleep(2000);
        }
    }



}
