using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTO;
using PlatformService.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController: ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        /// <summary>
        /// Получение списках платформ
        /// </summary>
        /// <returns>Список платформ</returns>
        [SwaggerOperation(Summary = "Получение списка платформ")]
        [SwaggerResponse(200, "Платформы получены", typeof(PlatformReadDTO))]
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDTO>> GetPlatforms()
        {
            Console.WriteLine("--- Getting Platforms...");

            var platforms = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDTO>>(platforms)); 
        }


        /// <summary>
        /// Получение платформы по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор Платформы</param>
        /// <returns>Платформу с указанным идентификатором</returns>
        [SwaggerOperation(Summary = "Получение платформы по идентификатору", Description = "Введите Id платформы")]
        [SwaggerResponse(200, "Платформа получена", typeof(PlatformReadDTO))]
        [SwaggerResponse(404,"Неверный Id", typeof(PlatformReadDTO))]
        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDTO> GetPlatformById(int id) 
        {          
            var platform = _repository.GetPlatformById(id);
            if (platform is null)
                return NotFound();

            return (Ok(_mapper.Map<PlatformReadDTO> (platform)));
        }

        /// <summary>
        /// Создание платформы
        /// </summary>
        /// <param name="platformCreateDTO">Создающаяся платформа</param>
        /// <returns>Возвращает 201 и PlatformReadDTO</returns>
        [SwaggerOperation(Summary = "Создание платформы")]
        [SwaggerResponse(201, "Платформа создана", typeof(PlatformReadDTO))]
        [HttpPost]
        public ActionResult<PlatformReadDTO> CreatePlatform(PlatformCreateDTO platformCreateDTO) // Платформа будет собираться из тела запроса и поведение сбора по умолчанию нам подходит
        {
            //А зачем возвращать созданую платформу? Типо хорошая практика возвращать 201 с созданым объектом
            var platform = _mapper.Map<Platform>(platformCreateDTO);

            _repository.CreatePlatform(platform);
            _repository.SaveChanges();

            var platformReadDTO =  _mapper.Map<PlatformReadDTO>(platform);

            return CreatedAtAction(nameof(GetPlatformById), new { Id = platformReadDTO.Id}, platformReadDTO);
        }
    }
}