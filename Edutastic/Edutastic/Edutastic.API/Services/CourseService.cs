using Edutastic.API.Models;
using Edutastic.API.Repositories;

namespace Edutastic.API.Services
{
    public class CourseService
    {
        private readonly CourseRepository _courseRepo;
        private readonly ModuleRepository _moduleRepo;

        public CourseService(CourseRepository courseRepo, ModuleRepository moduleRepo)
        {
            _courseRepo = courseRepo;
            _moduleRepo = moduleRepo;
        }

        public async Task<List<Course>> GetAllCourses()
        {
            return await _courseRepo.GetAll();
        }

        public async Task<Course?> GetCourseById(Guid courseId)
        {
            return await _courseRepo.GetById(courseId);
        }

        public async Task<List<Module>> GetModulesByCourse(Guid courseId)
        {
            return await _moduleRepo.GetByCourseId(courseId);
        }
    }
}