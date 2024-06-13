using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PrimeiraAPI.Data;
using PrimeiraAPI.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMongoCollection<Customer>? _customers;

        public CustomerController(MongoDbService mongoDbService)
        {
            _customers = mongoDbService.Database?.GetCollection<Customer>("costumer");
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _customers.Find(FilterDefinition<Customer>.Empty).ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Customer?>> GetById(string id)
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Id, id);
            var customer = _customers.Find(filter).FirstOrDefault();
            return customer is not null ? Ok(customer) : NotFound();
        }

        [HttpPost]

        public async Task<ActionResult> Creat([FromForm] Customer customerView)
        {
            var filePath = Path.Combine("Storage", customerView.Photo.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            customerView.Photo.CopyTo(fileStream);

            var customer = new Customer(customerView.CustomerName, customerView.Email, customerView.Photo, filePath);
            await _customers.InsertOneAsync(customerView);
            return CreatedAtAction(nameof(GetById), new { id = customerView.Id }, customerView);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Customer customer)
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Id, customer.Id);
            //var update = Builders<Customer>.Update
            //    .Set(x => x.CustomerName, customer.CustomerName)
            //    .Set(x => x.Email, customer.Email);
            //await _customers.UpdateOneAsync(filter, update);

            await _customers.ReplaceOneAsync(filter, customer);
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(string id)
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Id, id);
            await _customers.DeleteOneAsync(filter);
            return Ok();
        }

    }
}

