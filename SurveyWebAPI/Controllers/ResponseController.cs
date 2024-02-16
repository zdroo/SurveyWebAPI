using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SurveyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        List<Response> responseList = new List<Response>();

        [HttpGet]
        public async Task<ActionResult<List<Response>>> Get()
        {
            DataAccess db = new DataAccess();
            responseList = await db.GetResponsesAsync();
            if (responseList == null)
                return NotFound("There are no responses to be shown");
            return Ok(responseList);
        }

        [HttpPost("{q_id}")]
        public async Task<ActionResult<List<Response>>> RandomPost(string q_id)
        {
            DataAccess db = new DataAccess();
            var questions = await db.GetQuestionsAsync();
            var question = questions.Find(q => q.Id == q_id);
            if (question == null)
                return BadRequest("No Question with this id.");

            question.AutoResponse();
            responseList = await db.GetResponsesAsync();
            return Ok(responseList);
        }

        [HttpPost]
        public async Task<ActionResult<List<Response>>> Post(Response response)
        {
            DataAccess db = new DataAccess();
            responseList = db.GetResponsesAsync().Result.ToList();
            responseList.Add(response);
            await db.InsertInResponsesAsync(response.User_id, response.Q_id, response.ResponseText);
            return Ok(responseList);
        }
        [HttpPut]
        public async Task<ActionResult<List<Response>>> Put(Response request)
        {
            DataAccess db = new DataAccess();
            responseList = db.GetResponsesAsync().Result.ToList();
            var response = responseList.Find(r => r.User_id == request.User_id);
            if (response == null)
                return BadRequest("Response not found.");

            await Task.Run(() => response.User_id = request.User_id);
            await Task.Run(() => response.Q_id = request.Q_id);
            await Task.Run(() => response.ResponseText = request.ResponseText);
            return Ok(responseList); //de facut query de update
        }

        [HttpDelete]
        public async Task<ActionResult<List<Response>>> Delete(string q_id)
        {
            DataAccess db = new DataAccess();
            responseList = db.GetResponsesAsync().Result.ToList();
            var response = responseList.Find(r => r.Q_id == q_id);
            if (response == null)
                return BadRequest("Response not found.");

            await db.DeleteFromResponsesAsync(q_id);
            responseList.Remove(response);
            return Ok(db.GetResponsesAsync().Result.ToList());
        }

    }
}
