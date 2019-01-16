namespace ASR.Model
{
    public abstract class UserModel
    {
        public string userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }

        public UserModel(string userId, string name, string email)
        {
            this.userId = userId;
            this.name = name;
            this.email = email;
        }
    }
}