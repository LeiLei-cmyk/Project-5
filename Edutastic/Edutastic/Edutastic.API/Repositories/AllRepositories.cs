using Supabase.Postgrest;
using Supabase.Postgrest.Responses;
using Edutastic.API.Models;

namespace Edutastic.API.Repositories
{
    public class UserProfileRepository
    {
        private readonly Client _client;
        public UserProfileRepository(Client client) => _client = client;

        public async Task<UserProfile?> GetById(Guid id)
        {
            var response = await _client.Table<UserProfile>().Get();
            return response.Models.FirstOrDefault(x => x.id == id);
        }

        public async Task<UserProfile?> Create(UserProfile profile)
        {
            var response = await _client.Table<UserProfile>().Insert(profile);
            return response.Model;
        }

        public async Task<UserProfile?> UpdateXP(Guid userId, int xpToAdd)
        {
            var profile = await GetById(userId);
            if (profile == null) throw new Exception("Profile not found");
            profile.total_xp += xpToAdd;
            profile.last_activity_date = DateTime.UtcNow;
            var response = await _client.Table<UserProfile>().Update(profile);
            return response.Model;
        }
    }

    public class GameRepository
    {
        private readonly Client _client;
        public GameRepository(Client client) => _client = client;

        public async Task<Game?> Create(Game game)
        {
            var response = await _client.Table<Game>().Insert(game);
            return response.Model;
        }

        public async Task<Game?> GetById(Guid id)
        {
            var response = await _client.Table<Game>().Get();
            return response.Models.FirstOrDefault(x => x.id == id);
        }

        public async Task<Game?> Update(Guid id, Game game)
        {
            game.id = id;
            var response = await _client.Table<Game>().Update(game);
            return response.Model;
        }

        public async Task<List<Game>> GetByUserId(Guid userId)
        {
            var response = await _client.Table<Game>().Get();
            return response.Models.Where(x => x.user_id == userId).ToList();
        }
    }

    public class GameAnswerRepository
    {
        private readonly Client _client;
        public GameAnswerRepository(Client client) => _client = client;

        public async Task<GameAnswer?> Create(GameAnswer answer)
        {
            var response = await _client.Table<GameAnswer>().Insert(answer);
            return response.Model;
        }

        public async Task<List<GameAnswer>> GetBySessionId(Guid sessionId)
        {
            var response = await _client.Table<GameAnswer>().Get();
            return response.Models.Where(x => x.session_id == sessionId).ToList();
        }
    }

    public class QuestionRepository
    {
        private readonly Client _client;
        public QuestionRepository(Client client) => _client = client;

        public async Task<Question?> GetById(Guid id)
        {
            var response = await _client.Table<Question>().Get();
            return response.Models.FirstOrDefault(x => x.id == id);
        }

        public async Task<List<Question>> GetByModuleId(Guid moduleId)
        {
            var response = await _client.Table<Question>().Get();
            return response.Models.Where(x => x.module_id == moduleId).ToList();
        }
    }

    public class CourseRepository
    {
        private readonly Client _client;
        public CourseRepository(Client client) => _client = client;

        public async Task<List<Course>> GetAll()
        {
            var response = await _client.Table<Course>().Get();
            return response.Models;
        }

        public async Task<Course?> GetById(Guid id)
        {
            var response = await _client.Table<Course>().Get();
            return response.Models.FirstOrDefault(x => x.id == id);
        }
    }

    public class ModuleRepository
    {
        private readonly Client _client;
        public ModuleRepository(Client client) => _client = client;

        public async Task<List<Module>> GetByCourseId(Guid courseId)
        {
            var response = await _client.Table<Module>().Get();
            return response.Models.Where(x => x.course_id == courseId).ToList();
        }

        public async Task<Module?> GetById(Guid id)
        {
            var response = await _client.Table<Module>().Get();
            return response.Models.FirstOrDefault(x => x.id == id);
        }
    }

    public class EnrollmentRepository
    {
        private readonly Client _client;
        public EnrollmentRepository(Client client) => _client = client;

        public async Task<Enrollment?> GetByUserAndCourse(Guid userId, Guid courseId)
        {
            var response = await _client.Table<Enrollment>().Get();
            return response.Models.FirstOrDefault(x => x.user_id == userId && x.course_id == courseId);
        }

        public async Task<Enrollment?> Create(Enrollment enrollment)
        {
            var response = await _client.Table<Enrollment>().Insert(enrollment);
            return response.Model;
        }

        public async Task<Enrollment?> UpdateProgress(Guid userId, Guid courseId, int percentage)
        {
            var enrollment = await GetByUserAndCourse(userId, courseId);
            if (enrollment == null)
            {
                enrollment = new Enrollment
                {
                    user_id = userId,
                    course_id = courseId,
                    progress_percentage = percentage,
                    last_played = DateTime.UtcNow
                };
                return await Create(enrollment);
            }

            enrollment.progress_percentage = percentage;
            enrollment.last_played = DateTime.UtcNow;
            var response = await _client.Table<Enrollment>().Update(enrollment);
            return response.Model;
        }

        public async Task<List<Enrollment>> GetByUserId(Guid userId)
        {
            var response = await _client.Table<Enrollment>().Get();
            return response.Models.Where(x => x.user_id == userId).ToList();
        }
    }

    public class AchievementRepository
    {
        private readonly Client _client;
        public AchievementRepository(Client client) => _client = client;

        public async Task<List<Achievement>> GetAll()
        {
            var response = await _client.Table<Achievement>().Get();
            return response.Models;
        }

        public async Task<List<UserAchievement>> GetUnlockedByUser(Guid userId)
        {
            var response = await _client.Table<UserAchievement>().Get();
            return response.Models.Where(x => x.user_id == userId).ToList();
        }

        public async Task<UserAchievement?> Unlock(Guid userId, Guid achievementId)
        {
            var userAchievement = new UserAchievement
            {
                user_id = userId,
                achievement_id = achievementId,
                unlocked_at = DateTime.UtcNow
            };
            var response = await _client.Table<UserAchievement>().Insert(userAchievement);
            return response.Model;
        }
    }
}