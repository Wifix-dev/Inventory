using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Dtos = Inventory.DTOs.InventoryMovement;
using Inventory.Persistence.Repositories;

namespace Inventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryMovementController : ControllerBase
    {
        private readonly IInventoryMovementRepository _inventoryMovementRepository;
        private readonly IMapper _mapper;

        public InventoryMovementController(IInventoryMovementRepository inventoryMovementRepository, IMapper mapper)
        {
            _inventoryMovementRepository=inventoryMovementRepository;
            _mapper=mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventoryMovement = await _inventoryMovementRepository.GetAllAsync();
            return Ok(_mapper.Map<List<Dtos.InventoryMovementToListDto>>(inventoryMovement));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var inventoryMovement = await _inventoryMovementRepository.GetByIdAsync(id);
            return Ok(_mapper.Map<Dtos.InventoryMovementToListDto>(inventoryMovement));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Dtos.InventoryMovementToCreateDto inventoryMovementToCreateDto)
        {
            var inventoryMovementToCreate = _mapper.Map<InventoryMovement>(inventoryMovementToCreateDto);
            var inventoryMovementCreated = await _inventoryMovementRepository.AddAsync(inventoryMovementToCreate);

            return Ok(_mapper.Map<Dtos.InventoryMovementToListDto>(inventoryMovementCreated));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Dtos.InventoryMovementToEditDto inventoryMovementToEditDto)
        {
            if(id != inventoryMovementToEditDto.Id)
                return BadRequest("Error en los datos de entrada")  ;

                var inventoryMovementToUpdate = await _inventoryMovementRepository.GetByIdAsync(id);
                if(inventoryMovementToUpdate != null)
                    return BadRequest("Error en los datos de entrada");

                _mapper.Map(inventoryMovementToEditDto,inventoryMovementToUpdate);

                var updated = await _inventoryMovementRepository.UpdateAsync(id,inventoryMovementToUpdate);

                if(!updated)
                    return NoContent();
                
                var inventoryMovement = await _inventoryMovementRepository.GetByIdAsync(id);

                return Ok(_mapper.Map<Dtos.InventoryMovementToListDto>(inventoryMovement));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var inventoryMovementToDelete = await _inventoryMovementRepository.GetByIdAsync(id);
            if(inventoryMovementToDelete is null)
                return NotFound("Id no encontrado");
            
            var deleted = await _inventoryMovementRepository.DeleteAsync(id);
            if(!deleted)
                return Ok("Registro no borrado consulte al administrados");
            
            return Ok("Registro borrado");
        }

    }
}