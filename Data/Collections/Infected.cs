using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace MongoApi.Data.Collections
{
    public class Infected
    {
        public Infected(DateTime birthDate, string gender, double latitude, double longitude)
        {
            this.BirthDate = birthDate;
            this.Gender = gender;
            this.Location = new GeoJson2DGeographicCoordinates(longitude, latitude);
        }

        public DateTime BirthDate { get; set; }

        public string Gender { get; set; }

        public GeoJson2DGeographicCoordinates Location { get; set; }
    }
}