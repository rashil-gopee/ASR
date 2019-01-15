namespace ASR.Model
{
    public abstract class UserModel
    {
        private string userId { get; set; }
        private string name { get; set; }
        private string email { get; set; }

        public UserModel(string userId, string name, string email)
        {
            this.userId = userId;
            this.name = name;
            this.email = email;
        }
    }
}