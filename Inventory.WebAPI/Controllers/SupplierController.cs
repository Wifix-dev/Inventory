using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Dtos = Inventory.DTOs.Supplier;

namespace Inventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;
        public SupplierController(ISupplierRepository supplierRepository,IMapper mapper)
        {
          _supplierRepository = supplierRepository;   
          _mapper= mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var suppliers = await _supplierRepository.GetAllAsync();

            return Ok(_mapper.Map<List<Dtos.SupplierToListDto>>(suppliers));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            return Ok(_mapper.Map<Dtos.SupplierToListDto>(supplier));
        }

        

         [HttpPost]
        public async Task<IActionResult> Post(Dtos.SupplierToCreateDto supplierToCreateDto)
        {
            var supplierToCreate = _mapper.Map<Supplier>(supplierToCreateDto);
            supplierToCreate.CreatedAt= DateTime.Now;
            var supplierCreated = await _supplierRepository.AddAsync(supplierToCreate);

            return Ok( _mapper.Map<Dtos.SupplierToListDto>(supplierCreated));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Dtos.SupplierToEditDto supplierToEditDto)
        {
            if( id != supplierToEditDto.Id)
                return BadRequest();

            var supplierToUpdate = await _supplierRepository.GetByIdAsync(id);
            if(supplierToUpdate is null)
                return BadRequest("Id no encontrado");

            _mapper.Map(supplierToEditDto,supplierToUpdate);
            
            supplierToUpdate.UpdatedAt = DateTime.Now;
            var updated = await _supplierRepository.UpdateAsync(id,supplierToUpdate);
            if(!updated)
                return NoContent();

            var supplier = await _supplierRepository.GetByIdAsync(id);
            return Ok(_mapper.Map<Dtos.SupplierToListDto>(supplier));
            
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            var supplierToDelete = await _supplierRepository.GetByIdAsync(id);

            if(supplierToDelete is null)
            return NotFound("Supplier no encontrado");

            var deleted = await _supplierRepository.DeleteAsync(supplierToDelete);

            if(!deleted)
                return Ok("Supplier no borrado contacte al administrador");
            
            return Ok("El Supplier fue borrado");
        }
    }
}