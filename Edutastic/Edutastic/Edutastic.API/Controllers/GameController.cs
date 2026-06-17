using Edutastic.API.Models;
using Edutastic.API.Services;
using Edutastic.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Edutastic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("start")]
        public async Task<ActionResult<GameDTO>> StartGame([FromBody] StartGameRequest request)
        {
            var game = await _gameService.StartSession(request.user_id, request.module_id);
            if (game == null) return BadRequest("Failed to create game session");

            // Map model to DTO
            var dto = new GameDTO
            {
                id = game.id,
                user_id = game.user_id,
                module_id = game.module_id,
                started_at = game.started_at,
                ended_at = game.ended_at,
                score = game.score,
                xp_earned = game.xp_earned
            };

            return Ok(dto);
        }

        [HttpPost("{sessionId}/answer")]
        public async Task<ActionResult<GameAnswerDTO>> SubmitAnswer(
            Guid sessionId,
            [FromBody] SubmitAnswerRequest request)
        {
            var answer = await _gameService.SubmitAnswer(
                sessionId,
                request.question_id,
                request.user_answer ?? "",
                request.time_taken_seconds
            );
            if (answer == null) return BadRequest("Failed to submit answer");

            // Map model to DTO
            var dto = new GameAnswerDTO
            {
                id = answer.id,
                session_id = answer.session_id,
                question_id = answer.question_id,
                question_snapshot = answer.question_snapshot,
                user_answer = answer.user_answer,
                is_correct = answer.is_correct,
                time_taken_seconds = answer.time_taken_seconds
            };

            return Ok(dto);
        }

        [HttpPost("{sessionId}/end")]
        public async Task<ActionResult<GameResultDTO>> EndGame(Guid sessionId)
        {
            var result = await _gameService.EndSession(sessionId);
            return Ok(result);
        }
    }
}