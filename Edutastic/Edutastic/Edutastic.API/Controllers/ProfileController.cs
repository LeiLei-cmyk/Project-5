using Edutastic.API.Models;
using Edutastic.API.Repositories;
using Edutastic.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Edutastic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly UserProfileRepository _userRepo;
        private readonly EnrollmentRepository _enrollmentRepo;

        public ProfileController(
            UserProfileRepository userRepo,
            EnrollmentRepository enrollmentRepo)
        {
            _userRepo = userRepo;
            _enrollmentRepo = enrollmentRepo;
        }

        // GET /api/Profile/{userId} - Get user profile
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserProfileDTO>> GetProfile(Guid userId)
        {
            var profile = await _userRepo.GetById(userId);
            if (profile == null) return NotFound();

            var dto = new UserProfileDTO
            {
                id = profile.id,
                username = profile.username,
                age = profile.age,
                total_xp = profile.total_xp,
                current_streak = profile.current_streak,
                last_activity_date = profile.last_activity_date,
                created_at = profile.created_at
            };

            return Ok(dto);
        }

        // GET /api/Profile/{userId}/enrollments - Get user's course enrollments
        [HttpGet("{userId}/enrollments")]
        public async Task<ActionResult<List<EnrollmentDTO>>> GetEnrollments(Guid userId)
        {
            var enrollments = await _enrollmentRepo.GetByUserId(userId);
            var dtos = enrollments.Select(e => new EnrollmentDTO
            {
                user_id = e.user_id,
                course_id = e.course_id,
                progress_percentage = e.progress_percentage,
                last_played = e.last_played
            }).ToList();

            return Ok(dtos);
        }
    }
}