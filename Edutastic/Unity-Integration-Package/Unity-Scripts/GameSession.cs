using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Edutastic.API
{
    /// <summary>
    /// Manages game session lifecycle (start, submit answers, end).
    /// Tracks session ID and communicates with backend for scoring.
    /// </summary>
    public class GameSession
    {
        public string SessionId { get; private set; }
        public bool IsActive => !string.IsNullOrEmpty(SessionId);
        
        /// <summary>
        /// Start a new game session
        /// </summary>
        /// <param name="userId">The user's profile ID</param>
        /// <param name="moduleId">The module being played</param>
        /// <returns>True if session started successfully</returns>
        public async Task<bool> Start(string userId, string moduleId)
        {
            try
            {
                var request = new StartGameRequest { user_id = userId, module_id = moduleId };
                var game = await APIClient.Instance.PostAsync<GameDTO>("api/Game/start", request);
                SessionId = game.id;
                Debug.Log($"[GameSession] Started: {SessionId}");
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[GameSession] Failed to start: {e.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// Submit an answer during gameplay
        /// </summary>
        /// <param name="questionId">The question being answered</param>
        /// <param name="answer">The user's answer</param>
        /// <param name="timeTaken">Time taken in seconds</param>
        /// <returns>True if answer was correct</returns>
        public async Task<bool> SubmitAnswer(string questionId, string answer, int timeTaken)
        {
            if (!IsActive)
            {
                Debug.LogError("[GameSession] No active session!");
                return false;
            }
            
            try
            {
                var request = new SubmitAnswerRequest
                {
                    question_id = questionId,
                    user_answer = answer,
                    time_taken_seconds = timeTaken
                };
                
                var result = await APIClient.Instance.PostAsync<GameAnswerDTO>(
                    $"api/Game/{SessionId}/answer", request);
                
                Debug.Log($"[GameSession] Answer: {result.is_correct}");
                return result.is_correct;
            }
            catch (Exception e)
            {
                Debug.LogError($"[GameSession] Failed to submit: {e.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// End the game session and save score
        /// </summary>
        /// <returns>Game results with score and XP earned</returns>
        public async Task<GameResultDTO> End()
        {
            if (!IsActive)
            {
                Debug.LogError("[GameSession] No active session!");
                return null;
            }
            
            try
            {
                var result = await APIClient.Instance.PostAsync<GameResultDTO>(
                    $"api/Game/{SessionId}/end", null);
                
                Debug.Log($"[GameSession] Ended! Score: {result.score}, XP: {result.xp_earned}");
                SessionId = null;
                return result;
            }
            catch (Exception e)
            {
                Debug.LogError($"[GameSession] Failed to end: {e.Message}");
                return null;
            }
        }
    }
    
    [Serializable]
    public class StartGameRequest
    {
        public string user_id;
        public string module_id;
    }
    
    [Serializable]
    public class SubmitAnswerRequest
    {
        public string question_id;
        public string user_answer;
        public int time_taken_seconds;
    }
    
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
