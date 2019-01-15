using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ASR.Model;

namespace ASR.Controller
{
    abstract class UserController
    {
        public abstract UserModel GetUser(string userID);

        public abstract List<UserModel> GetUsers();
    }
}
