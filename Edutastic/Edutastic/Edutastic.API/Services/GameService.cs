using Edutastic.API.Models;
using Edutastic.API.Repositories;
using Edutastic.API.DTOs;

namespace Edutastic.API.Services
{
    public class GameService
    {
        private readonly GameRepository _gameRepo;
        private readonly GameAnswerRepository _answerRepo;
        private readonly QuestionRepository _questionRepo;
        private readonly UserProfileRepository _userRepo;

        public GameService(
            GameRepository gameRepo,
            GameAnswerRepository answerRepo,
            QuestionRepository questionRepo,
            UserProfileRepository userRepo)
        {
            _gameRepo = gameRepo;
            _answerRepo = answerRepo;
            _questionRepo = questionRepo;
            _userRepo = userRepo;
        }

        public async Task<Game?> StartSession(Guid userId, Guid moduleId)
        {
            var game = new Game
            {
                id = Guid.NewGuid(),
                user_id = userId,
                module_id = moduleId,
                started_at = DateTime.UtcNow,
                score = 0,
                xp_earned = 0
            };

            return await _gameRepo.Create(game);
        }

        public async Task<GameAnswer?> SubmitAnswer(
            Guid sessionId,
            Guid questionId,
            string userAnswer,
            int timeTakenSeconds)
        {
            var question = await _questionRepo.GetById(questionId);
            if (question == null) throw new Exception("Question not found");

            var isCorrect = string.Equals(
                question.correct_answer?.Trim().ToLower(),
                userAnswer?.Trim().ToLower()
            );

            var gameAnswer = new GameAnswer
            {
                id = Guid.NewGuid(),
                session_id = sessionId,
                question_id = questionId,
                question_snapshot = question.question_text,
                user_answer = userAnswer,
                is_correct = isCorrect,
                time_taken_seconds = timeTakenSeconds
            };

            return await _answerRepo.Create(gameAnswer);
        }

        public async Task<GameResultDTO> EndSession(Guid sessionId)
        {
            var game = await _gameRepo.GetById(sessionId);
            if (game == null) throw new Exception("Game session not found");

            var answers = await _answerRepo.GetBySessionId(sessionId);
            var totalQuestions = answers.Count;
            var correctAnswers = answers.Count(a => a.is_correct);
            var score = totalQuestions > 0 ? (correctAnswers * 100) / totalQuestions : 0;
            var xpEarned = correctAnswers * 10;

            game.ended_at = DateTime.UtcNow;
            game.score = score;
            game.xp_earned = xpEarned;
            await _gameRepo.Update(sessionId, game);

            var profile = await _userRepo.UpdateXP(game.user_id, xpEarned);
            if (profile == null) throw new Exception("Profile not found");

            return new GameResultDTO
            {
                session_id = sessionId,
                score = score,
                total_correct = correctAnswers,
                total_questions = totalQuestions,
                xp_earned = xpEarned,
                updated_total_xp = profile.total_xp
            };
        }
    }
}