namespace SurveyWebAPI
{
    public class OpenTextQuestion : Question
    {
        public OpenTextQuestion(string q_id, string text, string qtype, int survey_id)
        {
            this.Id = q_id;
            this.Text = text;
            this.Type = qtype;
            this.InstructionText = "Write your answer below:";
            this.SurveyId = survey_id;
        }

        public override async void AutoResponse()     //used for initial version
        {
            DataAccess db = new DataAccess();
            Random random = new Random();
            int randomUId = random.Next(1, db.GetUsersAsync().Result.Count() + 1);

            string RandomString(int length)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, length)

                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }

            string ans_text = RandomString(20);
            await db.InsertInResponsesAsync(randomUId, Id, ans_text);
        }
    }
}
