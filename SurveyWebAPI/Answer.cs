namespace SurveyWebAPI
{
    public class Answer
    {
        private int id;
        private string ans_text;
        private int chosen;
        private string q_id;
        private int optionNumber;
        private int survey_id;

        public Answer()
        {

        }

        public int Chosen
        {
            get { return chosen; }
            set { chosen = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Text
        {
            get { return ans_text; }
            set { ans_text = value; }
        }
        public string Q_id
        {
            get { return q_id; }
            set { q_id = value; }
        }
        public int SurveyId
        {
            get { return survey_id; }
            set { survey_id = value; }
        }
        public int OptionNumber
        {
            get { return optionNumber; }
            set { optionNumber = value; }
        }
    }
}
