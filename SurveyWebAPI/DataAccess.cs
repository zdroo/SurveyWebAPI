using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace SurveyWebAPI
{
    public class DataAccess
    {
        public async Task<List<Survey>> GetSurveysAsync()
        {
            using (IDbConnection connection = new SqlConnection(Globals.ConnectionString))
            {
                var output = await connection.QueryAsync<Survey>($"select * from {Globals.DataBaseName}.{Globals.SchemaName}.Surveys");
                return output.ToList();
            }
        }
        public async Task<int> GetLastUserIdAsync()
        {
            using (IDbConnection connection = new SqlConnection(Globals.ConnectionString))
            {
                var output = await connection.QueryAsync<int>($"SELECT TOP 1 * u_id FROM {Globals.DataBaseName}.{Globals.SchemaName}.Users ORDER BY u_id DESC");
                return output.ToList()[0];
            }
        }
        public async Task<List<User>> GetUsersAsync()
        {
            using (IDbConnection connection = new SqlConnection(Globals.ConnectionString))
            {
                var output = await connection.QueryAsync<User>($"select * from {Globals.DataBaseName}.{Globals.SchemaName}.Users");
                return output.ToList();
            }
        }

        public async Task AddUserAsync(string name, int age)
        {
            using (IDbConnection connection = new SqlConnection(Globals.ConnectionString))
            {
                await connection.ExecuteAsync("insert into {Globals.DataBaseName}.{Globals.SchemaName}.Users(name,age) values (@name,@age)", new { name, age });
            }
        }
        public async Task DeleteUserAsync(int u_id)
        {
            using (IDbConnection connection = new SqlConnection(Globals.ConnectionString))
            {
                await connection.ExecuteAsync($"delete from {Globals.DataBaseName}.{Globals.SchemaName}.Users where u_id = '{u_id}'");
            }
        }
        public async Task<List<Question>> GetQuestionsAsync()
        {
            using (IDbConnection connection = new SqlConnection(Globals.ConnectionString))
            {
                var questions = new List<Question>();
                var output = await connection.QueryAsync<Question>($"select * from {Globals.DataBaseName}.{Globals.SchemaName}.Questions");

                foreach (Question question in output)
                {
                    if (question.Type == "single")
                        questions.Add(new SingleQuestion(question.Id, question.Text, question.Type, question.SurveyId));
                    else if (question.Type == "multi")
                        questions.Add(new MultiQuestion(question.Id, question.Text, question.Type, question.SurveyId));
                    else
                        questions.Add(new OpenTextQuestion(question.Id, question.Text, question.Type, question.SurveyId));
                }
                foreach (Question q in questions)
                {
                    int number = 1;
                    q.options = await GetAnswersByQuestionIdAsync(q.Id);
                    foreach (Answer option in q.options)
                    {
                        option.OptionNumber = number++;
                    }
                }
                return questions;
            }
        }
        public async Task<List<Question>> GetQuestionsBySurveyIdAsync(int survey_id)
        {
            using (IDbConnection connection = new SqlConnection(Globals.ConnectionString))
            {
                var questions = new List<Question>();
                var output = await connection.QueryAsync<Question>($"select * from {Globals.DataBaseName}.{Globals.SchemaName}.Questions where survey_id = '{survey_id}'");

                foreach (Question question in output)
                {
                    if (question.Type == "single")
                        questions.Add(new SingleQuestion(question.Id, question.Text, question.Type, question.SurveyId));
                    else if (question.Type == "multi")
                        questions.Add(new MultiQuestion(question.Id, question.Text, question.Type, question.SurveyId));
                    else
                        questions.Add(new OpenTextQuestion(question.Id, question.Text, question.Type, question.SurveyId));
                }
                foreach (Question q in questions)
                {
                    int number = 1;
                    q.options = await GetAnswersByQuestionIdAsync(q.Id);
                    foreach (Answer option in q.options)
                    {
                        option.OptionNumber = number++;
                    }
                }
                return questions;
            }
        }
        public async Task<List<Answer>> GetAnswersByQuestionIdAsync(string q_id)
        {
            using (IDbConnection connection = new SqlConnection(Globals.ConnectionString))
            {
                var output = await connection.QueryAsync<Answer>($"select * from {Globals.DataBaseName}.{Globals.SchemaName}.Answers where q_id = '{q_id}'");
                return output.ToList();
            }
        }
        public async Task<List<Response>> GetResponsesAsync()
        {
            using (IDbConnection connection = new SqlConnection(Globals.ConnectionString))
            {
                var output = await connection.QueryAsync<Response>($"select * from {Globals.DataBaseName}.{Globals.SchemaName}.Responses");
                return output.ToList();
            }
        }
        public async Task InsertInResponsesAsync(int u_id, string q_id, string response)
        {
            using (IDbConnection connection = new SqlConnection(Globals.ConnectionString))
            {
                await connection.ExecuteAsync($"insert into {Globals.DataBaseName}.{Globals.SchemaName}.Responses(u_id,q_id,response) values (@u_id,@q_id,@response)", new { u_id, q_id, response });
            }
        }
        public async Task DeleteFromResponsesAsync(string q_id)
        {
            using (IDbConnection connection = new SqlConnection(Globals.ConnectionString))
            {
                await connection.ExecuteAsync($"delete from {Globals.DataBaseName}.{Globals.SchemaName}.Responses where q_id = '{q_id}'");
            }
        }
    }
}
