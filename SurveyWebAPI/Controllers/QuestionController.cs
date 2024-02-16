using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SurveyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        public static List<Question> questions = new List<Question>();

        [HttpGet("{survey_id}")]
        public async Task<ActionResult<List<Question>>> GetQuestionsForSurvey(int survey_id)
        {
            DataAccess db = new DataAccess();
            questions = await db.GetQuestionsBySurveyIdAsync(survey_id);
            return Ok(questions);
        }

        [HttpGet("{survey_id}/q_id")]
        public async Task<ActionResult<List<Question>>> GetOne(int survey_id, string q_id)
        {
            DataAccess db = new DataAccess();
            questions = await db.GetQuestionsBySurveyIdAsync(survey_id);
            var question = questions.Find(q => q.Id == q_id);
            if (question == null)
                return BadRequest("Question not found.");
            return Ok(question);
        }

        [HttpPost]
        public async Task<ActionResult<List<Question>>> AddQuestion(Question question)
        {
            await Task.Run(() => questions.Add(question));
            return Ok(questions);
        }

        [HttpPut("{survey_id}")]
        public async Task<ActionResult<List<Question>>> UpdateQuestion(int survey_id, Question request)
        {
            DataAccess db = new DataAccess();
            questions = await db.GetQuestionsBySurveyIdAsync(survey_id);
            var question = questions.Find(q => q.Id == request.Id);
            if (question == null)
                return BadRequest("Question not found.");

            await Task.Run(() => question.Text = request.Text);
            await Task.Run(() => question.InstructionText = request.InstructionText);
            await Task.Run(() => question.Type = request.Type);
            await Task.Run(() => question.SurveyId = request.SurveyId);

            return Ok(questions);
        }

        [HttpDelete("{survey_id}/q_id")]
        public async Task<ActionResult<List<Question>>> Delete(int survey_id, string q_id)
        {
            DataAccess db = new DataAccess();
            questions = await db.GetQuestionsBySurveyIdAsync(survey_id);
            var question = questions.Find(q => q.Id == q_id);
            if (question == null)
                return BadRequest("Question not found.");

            await Task.Run(() => questions.Remove(question));
            return Ok(questions);
        }
    }
}
