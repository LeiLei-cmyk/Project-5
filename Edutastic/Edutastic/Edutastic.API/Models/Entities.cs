using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Edutastic.API.Models
{
    [Table("profiles")]
    public class UserProfile : BaseModel
    {
        [PrimaryKey("id")]
        public Guid id { get; set; }

        [Column("username")]
        public string? username { get; set; }

        [Column("age")]
        public int age { get; set; }

        [Column("total_xp")]
        public int total_xp { get; set; }

        [Column("current_streak")]
        public int current_streak { get; set; }

        [Column("last_activity_date")]
        public DateTime? last_activity_date { get; set; }

        [Column("created_at")]
        public DateTime? created_at { get; set; }
    }

    [Table("courses")]
    public class Course : BaseModel
    {
        [PrimaryKey("id")]
        public Guid id { get; set; }

        [Column("name")]
        public string? name { get; set; }

        [Column("description")]
        public string? description { get; set; }

        [Column("thumbnail_url")]
        public string? thumbnail_url { get; set; }
    }

    [Table("modules")]
    public class Module : BaseModel
    {
        [PrimaryKey("id")]
        public Guid id { get; set; }

        [Column("course_id")]
        public Guid course_id { get; set; }

        [Column("name")]
        public string? name { get; set; }

        [Column("order_index")]
        public int order_index { get; set; }
    }

    [Table("questions")]
    public class Question : BaseModel
    {
        [PrimaryKey("id")]
        public Guid id { get; set; }

        [Column("module_id")]
        public Guid module_id { get; set; }

        [Column("question_text")]
        public string? question_text { get; set; }

        [Column("question_type")]
        public string? question_type { get; set; }

        [Column("options")]
        public object? options { get; set; }

        [Column("correct_answer")]
        public string? correct_answer { get; set; }
    }

    [Table("games")]
    public class Game : BaseModel
    {
        [PrimaryKey("id")]
        public Guid id { get; set; }

        [Column("user_id")]
        public Guid user_id { get; set; }

        [Column("module_id")]
        public Guid module_id { get; set; }

        [Column("started_at")]
        public DateTime? started_at { get; set; }

        [Column("ended_at")]
        public DateTime? ended_at { get; set; }

        [Column("score")]
        public int? score { get; set; }

        [Column("xp_earned")]
        public int? xp_earned { get; set; }
    }

    [Table("game_answers")]
    public class GameAnswer : BaseModel
    {
        [PrimaryKey("id")]
        public Guid id { get; set; }

        [Column("session_id")]
        public Guid session_id { get; set; }

        [Column("question_id")]
        public Guid question_id { get; set; }

        [Column("question_snapshot")]
        public string? question_snapshot { get; set; }

        [Column("user_answer")]
        public string? user_answer { get; set; }

        [Column("is_correct")]
        public bool is_correct { get; set; }  // ✅ MUST BE bool, NOT string

        [Column("time_taken_seconds")]
        public int? time_taken_seconds { get; set; }
    }

    [Table("enrollments")]
    public class Enrollment : BaseModel
    {
        [Column("user_id")]
        public Guid user_id { get; set; }

        [Column("course_id")]
        public Guid course_id { get; set; }

        [Column("progress_percentage")]
        public int? progress_percentage { get; set; }

        [Column("last_played")]
        public DateTime? last_played { get; set; }
    }

    [Table("achievements")]
    public class Achievement : BaseModel
    {
        [PrimaryKey("id")]
        public Guid id { get; set; }

        [Column("name")]
        public string? name { get; set; }

        [Column("description")]
        public string? description { get; set; }

        [Column("icon_url")]
        public string? icon_url { get; set; }

        [Column("xp_reward")]
        public int xp_reward { get; set; }
    }

    [Table("user_achievements")]
    public class UserAchievement : BaseModel
    {
        [Column("user_id")]
        public Guid user_id { get; set; }

        [Column("achievement_id")]
        public Guid achievement_id { get; set; }

        [Column("unlocked_at")]
        public DateTime? unlocked_at { get; set; }
    }
}