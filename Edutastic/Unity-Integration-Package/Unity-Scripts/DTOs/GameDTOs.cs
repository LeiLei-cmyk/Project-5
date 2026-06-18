using System;

namespace Edutastic.API.DTOs
{
    /// <summary>
    /// Data Transfer Objects for Game-related API responses.
    /// These match the ASP.NET Core API DTOs exactly.
    /// </summary>
    
    [Serializable]
    public class GameDTO
    {
        public string id;
        public string user_id;
        public string module_id;
        public string started_at;
        public string ended_at;
        public int score;
        public int xp_earned;
    }
    
    [Serializable]
    public class GameAnswerDTO
    {
        public string id;
        public string session_id;
        public string question_id;
        public string question_snapshot;
        public string user_answer;
        public bool is_correct;
        public int time_taken_seconds;
    }
    
    [Serializable]
    public class GameResultDTO
    {
        public string session_id;
        public int score;
        public int total_correct;
        public int total_questions;
        public int xp_earned;
        public int updated_total_xp;
    }
}
