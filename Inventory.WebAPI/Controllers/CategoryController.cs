using AutoMapper;
using Inventory.Entities;
using Inventory.Persistence.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Dtos=Inventory.DTOs.Category;

namespace Inventory.WebAPI.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper) : base(mapper)
        {
            _categoryRepository=categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var categories  = await _categoryRepository.GetAllAsync();
            var CategoriesDto= _mapper.Map<List<Dtos.CategoryToListDTO>>(categories);
            return Ok(CategoriesDto);
        }      

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id){
            var category = await _categoryRepository.GetByIdAsync(id);
            if(category is null){
                return NotFound("Elemento no encontrado");
            }
            var categoryDTO = _mapper.Map<Dtos.CategoryToListDTO>(category);
            return Ok(categoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Dtos.CategoryToCreateDTOs categoryToCreateDTOs){

            var categoryToCreate = _mapper.Map<Category>(categoryToCreateDTOs);
            /*se remplaza con mapper
            var categoryToCreate = new Category {
                Name = categoryToCreateDTOs.Name,
                Description = categoryToCreateDTOs.Description,
                CreatedAt = DateTime.Now
            };
            */
            categoryToCreate.CreatedAt=DateTime.Now;
            var categoryCreated = await _categoryRepository.AddAsync(categoryToCreate);
            var categoryCreatedDto = _mapper.Map<Dtos.CategoryToListDTO>(categoryCreated);
            /* se remplaza con mapper
            var categoryCreatedDto = new CategoryToListDTO{
                Id=categoryCreated.Id,
                Name = categoryCreated.Name,
                Description=categoryCreated.Description,
                CreatedAt=categoryCreated.CreatedAt,
                UpdatedAt=categoryToCreate.UpdatedAt
            };
            */
            return Ok(categoryCreatedDto);
        }   

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Dtos.CategoryToEditDTO categoryToEditDTO){
            if(id != categoryToEditDTO.Id)
                return BadRequest("Error en los datos de entrada");
            
            var categoryToUdate = await _categoryRepository.GetByIdAsync(id);
            if(categoryToUdate is null){
                return BadRequest("Id no Encontrada");
            }
            //mapea de acyerdo al formato establecido
            _mapper.Map(categoryToEditDTO,categoryToUdate);
            //actualiza los datos
            categoryToUdate.UpdatedAt=DateTime.Now;
            var updated = await _categoryRepository.UpdateAsync(id,categoryToUdate);
            if(!updated){
                return NoContent();
            }
            //busca los datos a retornas despues de la busqueda
            var category = await _categoryRepository.GetByIdAsync(id);
            var categoryDto = _mapper.Map<Dtos.CategoryToListDTO>(category);
            return Ok(categoryDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){
            var categoryToDelete = await _categoryRepository.GetByIdAsync(id);
            if(categoryToDelete is null){
                return NotFound("El registro no existe");
            }
            var deleted = await _categoryRepository.DeleteAsync(categoryToDelete);
            return !deleted ? Ok("Registro no borrado") : Ok("Registro borrado correctamente");
            /*
            if(!deleted){
                return Ok("Registro no borrado");
            }
            return Ok("Registro borrado correctamente");*/
        }
    }
}