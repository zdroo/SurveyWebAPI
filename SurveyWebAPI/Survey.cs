namespace SurveyWebAPI
{
    public class Survey
    {
        private int survey_id;
        private string name;
        private string description;
        public List<Question> questions = new List<Question>();

        public List<Question> Questions
        {
            set { questions = value; }
            get { return questions; }
        }

        public Survey()
        {

        }
        public int SurveyId
        {
            get { return this.survey_id; }
            set { this.survey_id = value; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        /*public async Task DbRun()
        {
            DataAccess db = new DataAccess();

            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your age: ");
            int age = Convert.ToInt32(Console.ReadLine());
            User user = new User(name, age);
            foreach (Question question in questions)
            {
                var result = question.AnswerRetAsync();
                await db.InsertInResponsesAsync(user.Name, result.Result.Q_id, result.Result.ResponseText);
            }
        }*/

    }
}
