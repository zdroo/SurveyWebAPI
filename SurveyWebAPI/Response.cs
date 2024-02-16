namespace SurveyWebAPI
{
    public class Response
    {
        //private string response_id;
        private int u_id;
        private string q_id;
        private string response = string.Empty;

        public Response()
        {

        }

        //public string ResponseId { get { return response_id; } set { response_id = value; } }
        public string Q_id { get { return q_id; } set { q_id = value; } }
        public string ResponseText { get { return response; } set { response = value; } }
        public int User_id { get { return u_id; } set { u_id = value; } }
    }
}
