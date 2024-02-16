using Microsoft.Extensions.Options;

namespace SurveyWebAPI
{
    public class MultiQuestion : Question
    {
        public MultiQuestion(string q_id, string text, string qtype, int survey_id)
        {
            this.Id = q_id;
            this.Text = text;
            this.Type = qtype;
            this.InstructionText = "Enter number of answers and then select them: ";
            this.SurveyId = survey_id;
        }
        public override async void AutoResponse()    //used for initial version
        {
            DataAccess db = new DataAccess();
            Random random = new Random();
            string resp = String.Empty;
            int randomUId = random.Next(1, db.GetUsersAsync().Result.Count() + 1);

            string RandomString(int length)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, length)

                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            int numberOfChoices = random.Next(1, options.Count + 1);
            List<string> chosenValues = new List<string>();

            while (chosenValues.Count < numberOfChoices)
            {
                int myNumber = random.Next(1, options.Count + 1);
                var questions = await db.GetQuestionsAsync();
                var question = questions.Find(q => q.Id == Id);
                var chosenOption = question.Options.Find(o => o.OptionNumber == myNumber);

                if (!chosenValues.Contains(chosenOption.Text))
                {
                    chosenValues.Add(chosenOption.Text);
                    resp += chosenOption.Text + " ";
                }

            }
            await db.InsertInResponsesAsync(randomUId, Id, resp);
        }
    }
}
