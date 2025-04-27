using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApiBusinessLayer;
using StudentApiDataAccessLayer;

namespace Student_Api.Controllers
{
    [Route("api/Students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet("All",Name ="GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDTO>> GetAllStudents()
        {
            List<StudentDTO> studentsList = clsStudentDataAccess.GetAllStudents();
            if (studentsList.Count == 0)
                return NotFound("There is no Students");
            else
                return Ok(studentsList);
        }

        [HttpGet("Passed", Name = "GetAllPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDTO>> GetAllPassedStudents()
        {
            List<StudentDTO> studentsList = clsStudentDataAccess.GetAllPassedStudents();
            if (studentsList.Count == 0)
                return NotFound("There is no Students");
            else
                return Ok(studentsList);
        }

        [HttpGet("Average",Name="GetGradeAverage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<double>GetGradeAverage()
        {
            double average = clsStudentDataAccess.GetAverageGrades();
            if (average != 0)
                return Ok(average);
            else
                return NotFound("There is no students");
        }



        [HttpGet("{ID}", Name = "GetStudentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> GetStudent(int ID)
        {
            if (ID < 1)
                return BadRequest("Bad Request");
            else
            {
                clsStudents student = clsStudents.Find(ID);
                if (student != null)
                    return Ok(student.DTO);
                return NotFound("Not Found");
            }
        }


        [HttpPost("AddNewStudent",Name ="AddNewStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> AddNewStudent(StudentDTO studentDTO)
        {
            if (studentDTO.Grade > 100 || studentDTO.Name == string.Empty || studentDTO.Age > 100)
                return BadRequest("Bad Request");
            clsStudents student = new clsStudents(studentDTO);
            if (student.Save())
            {
                studentDTO.Id = student.ID;
                return CreatedAtRoute("GetStudentByID", new { id = student.ID }, studentDTO);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);

        }

        [HttpPut("{id}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> UpdateStudent(int id,StudentDTO updatedStudent)
        {
            if (id < 1 || updatedStudent == null || string.IsNullOrEmpty(updatedStudent.Name) || updatedStudent.Age < 0 || updatedStudent.Grade < 0)
            {
                return BadRequest("Invalid student data.");
            }
            clsStudents student= new clsStudents(updatedStudent,clsStudents.enMode.Update);
            if (student.Save())
                return Ok(student.DTO);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("{id}",Name ="DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteStudent(int id)
        {
            if (id < 0)
                return BadRequest("Bac Request");
            else
            {
                if (clsStudents.Delete(id))
                    return Ok();
                return NotFound("Not found");
            }
        }

    }
}
