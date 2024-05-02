using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {   
        private readonly TrainingService _trainingService;
        private List<string> _errorMessages = new List<string>();

        public TrainingController(TrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        // GET: api/Training
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> GetTrainings()
        {
            IEnumerable<TrainingDTO> trainingsDTO = await _trainingService.GetAll();
            return Ok(trainingsDTO);
        }

        // GET: api/Training/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> GetTrainingsById(long id)
        {
             IEnumerable<TrainingDTO> trainingsDTO = await _trainingService.GetTrainingById(id);
            if (trainingsDTO == null)
            {
                return NotFound();
            }
            return Ok(trainingsDTO);
        }

        // GET: api/Training/4
        [HttpGet("periods/{colabId}")]
        public async Task<ActionResult<List<TrainingPeriodDTO>>> GetTrainingPeriodsOnTrainingById(long colabId, DateOnly startDate, DateOnly endDate)
        {
            IEnumerable<TrainingPeriodDTO> trainingPeriodDTOs = await _trainingService.GetTrainingPeriodsOnTrainingById(colabId,startDate,endDate,_errorMessages);
            if (trainingPeriodDTOs == null)
            {
                return NotFound();
            }
            return Ok(trainingPeriodDTOs);
        }

        // GET: api/Training/4
        [HttpGet("{xDias}/colabsComFeriasSuperioresAXDias")]
        public async Task<ActionResult<List<long>>> GetColabsComFeriasSuperioresAXDias(long xDias)
        {
            List<long> colabsComFeriasSuperioresAXDias = await _trainingService.GetColabsComFeriasSuperioresAXDias(xDias,_errorMessages);
            if (colabsComFeriasSuperioresAXDias == null)
            {
                return NotFound();
            }
            return Ok(colabsComFeriasSuperioresAXDias);
            return Ok();
        }

    }
}
