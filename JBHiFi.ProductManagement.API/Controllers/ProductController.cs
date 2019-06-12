using AutoMapper;
using CSharpFunctionalExtensions;
using JBHiFi.ProductManagement.API.Model;
using JBHiFi.ProductManagement.Business.Entities;
using JBHiFi.ProductManagement.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JBHiFi.ProductManagement.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //private  readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        //public ProductsController(IProductRepository repository,IMapper mapper )
        //{
        //    _mapper = mapper;
        //    _productRepository = repository;
        //}
        private readonly IQueryHandler _queryHandler;
        private readonly ICommandHandler _commandHandler;

        public ProductsController(IQueryHandler queryHandler, ICommandHandler commandHandler, IMapper mapper)
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
            _mapper = mapper;
        }
        // GET api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var productEntity = _queryHandler.Handle<string,Result<Product>>(id);
            if (productEntity.IsFailure)
            {
                return BadRequest("Unable to fetch details");

            }
            var result = Mapper.Map<ProductDto>(productEntity.Value);
            return Ok(result);
        }

        // GET api/products
        [HttpGet]
        public IActionResult Get([FromQuery]ProductFilterDto productFilter)
        {
            var productEntity = _queryHandler.Handle<ProductFilterDto, Result<IEnumerable<Product>>>(productFilter);
            if (productEntity.IsFailure)
            {
                return BadRequest("Unable to fetch details");

            }
            var result = Mapper.Map<ProductDto>(productEntity.Value);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductForCreationDto productForCreation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product productEntity = new Product()
            {
                Brand = productForCreation.Brand,
                Description = productForCreation.Description,
                Id = productForCreation.Id,
                Model = productForCreation.Model

            };
            _commandHandler.Handle(productEntity);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(string id,[FromBody] ProductForUpdateDto productForUpdation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productForUpdation.Description == productForUpdation.Brand)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the brand.");
            }

            
            Product productEntity = new Product()
            {
                Brand = productForUpdation.Brand,
                Description = productForUpdation.Description,
                Id = productForUpdation.Id,
                Model = productForUpdation.Model

            };
            _commandHandler.Handle<Product>(productEntity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(string id)
        { 
            _commandHandler.Handle<string>(id);
            return NoContent();
        }
    }
}
