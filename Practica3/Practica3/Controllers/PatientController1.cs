using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientEntities1.Managers;

namespace Practica3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController1 : ControllerBase
    {
        private readonly PatientManager1 _patientManager;

        public PatientController1(PatientManager1 patientManager)
        {
            _patientManager = patientManager;
        }

        [HttpPost]
        public IActionResult GeneratePatientCode([FromBody] PatientInfo patientInfo)
        {
            string code = _patientManager.GeneratePatientCode(patientInfo.Name, patientInfo.LastName, patientInfo.CI);
            return Ok(new { PatientCode = code });
        }

        public class PatientInfo
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public string CI { get; set; }
        }
    }
}
