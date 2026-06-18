using Edutastic.API.Models;
using Edutastic.API.Repositories;
using Edutastic.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Edutastic.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class QuestionController : ControllerBase
	{
		private readonly QuestionRepository _questionRepo;

		public QuestionController(QuestionRepository questionRepo)
		{
			_questionRepo = questionRepo;
		}

		// GET /api/Question/{moduleId}/module - Get questions for a module
		// ⚠️ Does NOT return correct_answer (security!)
		[HttpGet("{moduleId}/module")]
		public async Task<ActionResult<List<QuestionDTO>>> GetQuestionsByModule(Guid moduleId)
		{
			var questions = await _questionRepo.GetByModuleId(moduleId);

			// Map to DTO (EXCLUDES correct_answer)
			var dtos = questions.Select(q => new QuestionDTO
			{
				id = q.id,
				module_id = q.module_id,
				question_text = q.question_text,
				question_type = q.question_type,
				options = q.options
			}).ToList();

			return Ok(dtos);
		}

		// GET /api/Question/{questionId} - Get single question (admin only)
		// ⚠️ This returns correct_answer - protect this endpoint later!
		[HttpGet("{questionId}")]
		public async Task<ActionResult<QuestionDTO>> GetQuestion(Guid questionId)
		{
			var question = await _questionRepo.GetById(questionId);
			if (question == null) return NotFound();

			var dto = new QuestionDTO
			{
				id = question.id,
				module_id = question.module_id,
				question_text = question.question_text,
				question_type = question.question_type,
				options = question.options
			};

			return Ok(dto);
		}
	}
}