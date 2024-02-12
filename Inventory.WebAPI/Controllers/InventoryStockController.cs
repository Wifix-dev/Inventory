
using AutoMapper;
using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Dtos = Inventory.DTOs.InventoryStock;

namespace Inventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryStockController : ControllerBase
    {
        private readonly IInventoryStockRepository _inventoryStockRepository;
        private readonly IMapper _mapper;

        public InventoryStockController(IInventoryStockRepository inventoryStockRepository, IMapper mapper)
        {
            _inventoryStockRepository=inventoryStockRepository;
            _mapper=mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventoryStocks = await _inventoryStockRepository.GetAllAsync();
            return Ok(_mapper.Map<List<Dtos.InventoryStockToListDto>>( inventoryStocks));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var inventoryStock = await _inventoryStockRepository.GetByIdAsync(id);
            return Ok(_mapper.Map<Dtos.InventoryStockToListDto>( inventoryStock));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Dtos.InventoryStockToCreateDto inventoryStockToCreateDto)
        {
            var inventoryStockToCreate = _mapper.Map<InventoryStock>(inventoryStockToCreateDto);
            inventoryStockToCreate.CreatedAt = DateTime.Now;
            var inventoryCreated = await _inventoryStockRepository.AddAsync(inventoryStockToCreate);

            return Ok(_mapper.Map<Dtos.InventoryStockToListDto>(inventoryCreated));

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Dtos.InventoryStockToEditDto inventoryStockToEditDto)
        {
            if(id != inventoryStockToEditDto.Id)
                return BadRequest("Error en los datos de entrada");

            var inventoryStockToUpdate = await _inventoryStockRepository.GetByIdAsync(id);
            if(inventoryStockToUpdate is null)
                return BadRequest("Id no encontrado");
            
            _mapper.Map(inventoryStockToEditDto,inventoryStockToUpdate);
            var updated = await _inventoryStockRepository.UpdateAsync(id,inventoryStockToUpdate);

            if(!updated)
                return NoContent();

            var inventoryStock = await _inventoryStockRepository.GetByIdAsync(id);
            return Ok(_mapper.Map<Dtos.InventoryStockToListDto>(inventoryStock));            

        }



    }
}