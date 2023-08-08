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
        /// ��������� ������� ��������
        /// </summary>
        /// <returns>������ ��������</returns>
        [SwaggerOperation(Summary = "��������� ������ ��������")]
        [SwaggerResponse(200, "��������� ��������", typeof(PlatformReadDTO))]
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDTO>> GetPlatforms()
        {
            Console.WriteLine("--- Getting Platforms...");

            var platforms = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDTO>>(platforms)); 
        }


        /// <summary>
        /// ��������� ��������� �� ��������������
        /// </summary>
        /// <param name="id">������������� ���������</param>
        /// <returns>��������� � ��������� ���������������</returns>
        [SwaggerOperation(Summary = "��������� ��������� �� ��������������", Description = "������� Id ���������")]
        [SwaggerResponse(200, "��������� ��������", typeof(PlatformReadDTO))]
        [SwaggerResponse(404,"�������� Id", typeof(PlatformReadDTO))]
        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDTO> GetPlatformById(int id) 
        {          
            var platform = _repository.GetPlatformById(id);
            if (platform is null)
                return NotFound();

            return (Ok(_mapper.Map<PlatformReadDTO> (platform)));
        }

        /// <summary>
        /// �������� ���������
        /// </summary>
        /// <param name="platformCreateDTO">����������� ���������</param>
        /// <returns>���������� 201 � PlatformReadDTO</returns>
        [SwaggerOperation(Summary = "�������� ���������")]
        [SwaggerResponse(201, "��������� �������", typeof(PlatformReadDTO))]
        [HttpPost]
        public ActionResult<PlatformReadDTO> CreatePlatform(PlatformCreateDTO platformCreateDTO) // ��������� ����� ���������� �� ���� ������� � ��������� ����� �� ��������� ��� ��������
        {
            //� ����� ���������� �������� ���������? ���� ������� �������� ���������� 201 � �������� ��������
            var platform = _mapper.Map<Platform>(platformCreateDTO);

            _repository.CreatePlatform(platform);
            _repository.SaveChanges();

            var platformReadDTO =  _mapper.Map<PlatformReadDTO>(platform);

            return CreatedAtAction(nameof(GetPlatformById), new { Id = platformReadDTO.Id}, platformReadDTO);
        }
    }
}