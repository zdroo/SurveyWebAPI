using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SurveyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        public static List<Survey> surveys = new List<Survey>();

        [HttpGet]
        public async Task<ActionResult<List<Survey>>> Get()
        {
            DataAccess db = new DataAccess();
            surveys = await db.GetSurveysAsync();
            foreach (Survey survey in surveys)
            {
                survey.Questions = await db.GetQuestionsBySurveyIdAsync(survey.SurveyId);
                foreach (Question question in survey.Questions)
                {
                    question.Options = await db.GetAnswersByQuestionIdAsync(question.Id);
                }
            }
            return Ok(surveys);
        }
    }
}
