namespace SurveyWebAPI
{
    public class User
    {
        private int u_id;
        private string name;
        private int age;

        DataAccess db = new DataAccess();


        public int Id
        {
            get { return u_id; }
            set { u_id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public User()
        {

        }
        public User(string name, int age)
        {
            this.Id = Convert.ToInt32(db.GetLastUserIdAsync()) + 1;
            this.Name = name;
            this.Age = age;
        }
    }
}
