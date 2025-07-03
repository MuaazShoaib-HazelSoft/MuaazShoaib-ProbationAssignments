using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.DTOS.CoursesDTO;
using UserManagementSystem.DTOS.RolesDto;
using UserManagementSystem.Repositories.UserCourseRepository;
using UserManagementSystem.Services.CourseService;
using UserManagementSystem.Services.RoleService;

namespace UserManagementSystem.Controllers.CoursesController
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CourseController:BaseApiController
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        /// <summary>
        /// To add/create all Courses.
        /// </summary
        [HttpPost("addcourse")]
        public async Task<IActionResult> AddCourse([FromBody] CourseDto courseDto)
        {
            try
            {
                await _courseService.AddCourse(courseDto);
                return Ok(MessagesConstants.CourseAdded, true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message, false);
            }
        }
        /// <summary>
        /// To get all courses.
        /// </summary
        [HttpGet("getallcourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var courses = await _courseService.GetAllCourses();
                return Ok(courses, MessagesConstants.CourseFetched, true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message, false);
            }
        }
        /// <summary>
        /// To update the course
        /// with course Id.
        /// </summary
        [HttpPut("updatecourse/{id?}")]
        public async Task<IActionResult> UpdateCourse(int? id, [FromBody] CourseDto courseDto)
        {
            try
            {
                await _courseService.UpdateCourse(id.Value, courseDto);
                return Ok(MessagesConstants.CourseUpdated, true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message, false);
            }
        }
        /// <summary>
        /// To Delete the course with 
        /// course Id.
        /// </summary
        [HttpDelete("deletecourse/{id?}")]
        public async Task<IActionResult> DeleteRole(int? id)
        {
            try
            {
                await _courseService.DeleteCourse(id.Value);
                return Ok(MessagesConstants.CourseDeleted, true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message, false);
            }
        }
        /// <summary>
        /// To Assign the course with 
        /// user and course Id.
        /// </summary
        [HttpPost("assigncourse")]
        public async Task<IActionResult> AssignCourseToUser([FromBody] AssignUserCourseDto assignCourse)
        {
            try
            {
                await _courseService.AssignCourseToUser(assignCourse);
                return Ok(MessagesConstants.CourseAssigned, true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message, false);
            }
        }
    }
}

