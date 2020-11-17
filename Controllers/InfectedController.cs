using System;
using MongoApi.Data.Collections;
using MongoApi.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace MongoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectedController : ControllerBase
    {
        Data.MongoDbContext _mongoDbContext;

        IMongoCollection<Infected> _infectedCollection;

        public InfectedController(Data.MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _infectedCollection = _mongoDbContext.DB.GetCollection<Infected>(typeof(Infected).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SaveInfected([FromBody] InfectedDto dto)
        {
            var infected = new Infected(dto.BirthDate, dto.Gender, dto.Latitude, dto.Longitude);

            _infectedCollection.InsertOne(infected);

            return StatusCode(201, "Infected created successfully");
        }

        [HttpGet]
        public ActionResult GetInfected()
        {
            var infected = _infectedCollection.Find(Builders<Infected>.Filter.Empty).ToList();

            return Ok(infected);
        }

        [HttpPut]
        public ActionResult UpdateInfected([FromBody] InfectedDto dto)
        {
            _infectedCollection.UpdateOne(Builders<Infected>.Filter
            .Where(_ => _.BirthDate == dto.BirthDate),
            Builders<Infected>.Update.Set("gender", dto.Gender));

            return Ok("Infected updated successfully");
        }

        [HttpDelete("{birthDate}")]
        public ActionResult Delete(DateTime birthDate)
        {
            _infectedCollection.DeleteOne(Builders<Infected>.Filter
            .Where(_ => _.BirthDate == birthDate));


            return Ok("Infected deleted successfully");
        }
    }
}