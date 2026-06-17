namespace Edutastic.API.DTOs
{
    // Game DTOs
    public class GameDTO
    {
        public Guid id { get; set; }
        public Guid user_id { get; set; }
        public Guid module_id { get; set; }
        public DateTime? started_at { get; set; }
        public DateTime? ended_at { get; set; }
        public int? score { get; set; }
        public int? xp_earned { get; set; }
    }

    public class GameAnswerDTO
    {
        public Guid id { get; set; }
        public Guid session_id { get; set; }
        public Guid question_id { get; set; }
        public string? question_snapshot { get; set; }
        public string? user_answer { get; set; }
        public bool is_correct { get; set; }
        public int? time_taken_seconds { get; set; }
    }

    public class GameResultDTO
    {
        public Guid session_id { get; set; }
        public int score { get; set; }
        public int total_correct { get; set; }
        public int total_questions { get; set; }
        public int xp_earned { get; set; }
        public int updated_total_xp { get; set; }
    }

    // User/Profile DTOs
    public class UserProfileDTO
    {
        public Guid id { get; set; }
        public string? username { get; set; }
        public int age { get; set; }
        public int total_xp { get; set; }
        public int current_streak { get; set; }
        public DateTime? last_activity_date { get; set; }
        public DateTime? created_at { get; set; }
    }

    // Course DTOs
    public class CourseDTO
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? thumbnail_url { get; set; }
    }

    public class ModuleDTO
    {
        public Guid id { get; set; }
        public Guid course_id { get; set; }
        public string? name { get; set; }
        public int order_index { get; set; }
    }

    // Question DTO (NO correct_answer!)
    public class QuestionDTO
    {
        public Guid id { get; set; }
        public Guid module_id { get; set; }
        public string? question_text { get; set; }
        public string? question_type { get; set; }
        public object? options { get; set; }
    }

    // Enrollment DTOs
    public class EnrollmentDTO
    {
        public Guid user_id { get; set; }
        public Guid course_id { get; set; }
        public int? progress_percentage { get; set; }
        public DateTime? last_played { get; set; }
    }

    // Achievement DTOs
    public class AchievementDTO
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? icon_url { get; set; }
        public int xp_reward { get; set; }
    }

    public class UserAchievementDTO
    {
        public Guid user_id { get; set; }
        public Guid achievement_id { get; set; }
        public DateTime? unlocked_at { get; set; }
    }

    // Request DTOs
    public class StartGameRequest
    {
        public Guid user_id { get; set; }
        public Guid module_id { get; set; }
    }

    public class SubmitAnswerRequest
    {
        public Guid question_id { get; set; }
        public string? user_answer { get; set; }
        public int time_taken_seconds { get; set; }
    }

    public class LoginRequest
    {
        public Guid auth_user_id { get; set; }
    }
}