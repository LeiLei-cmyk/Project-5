using Edutastic.API.Models;
using Edutastic.API.Services;
using Edutastic.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Edutastic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        // GET /api/Course - List all courses
        [HttpGet]
        public async Task<ActionResult<List<CourseDTO>>> GetAllCourses()
        {
            var courses = await _courseService.GetAllCourses();
            var dtos = courses.Select(c => new CourseDTO
            {
                id = c.id,
                name = c.name,
                description = c.description,
                thumbnail_url = c.thumbnail_url
            }).ToList();

            return Ok(dtos);
        }

        // GET /api/Course/{courseId}/modules - Get modules for a course
        [HttpGet("{courseId}/modules")]
        public async Task<ActionResult<List<ModuleDTO>>> GetModules(Guid courseId)
        {
            var modules = await _courseService.GetModulesByCourse(courseId);
            var dtos = modules.Select(m => new ModuleDTO
            {
                id = m.id,
                course_id = m.course_id,
                name = m.name,
                order_index = m.order_index
            }).ToList();

            return Ok(dtos);
        }

        // GET /api/Course/{courseId} - Get single course
        [HttpGet("{courseId}")]
        public async Task<ActionResult<CourseDTO>> GetCourse(Guid courseId)
        {
            var course = await _courseService.GetCourseById(courseId);
            if (course == null) return NotFound();

            var dto = new CourseDTO
            {
                id = course.id,
                name = course.name,
                description = course.description,
                thumbnail_url = course.thumbnail_url
            };

            return Ok(dto);
        }
    }
}