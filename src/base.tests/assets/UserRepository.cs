namespace Masha.Foundation.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static Masha.Foundation.Core;

    public class UserRepository
    {
        private Dictionary<int, User> users = new Dictionary<int, User>()
        {
            {1, new User() {Id = 1, Name = "Sheik", Gender = Some("Male"), Age = 40} },
            {2, new User() {Id = 2, Name = "Raja", Gender = Some("Male"), Age = 29} },
            {3, new User() {Id = 3, Name = "Mozhi", Gender = Some("Female"), Age = 28} },
            {4, new User() {Id = 4, Name = "Mac", Gender = Some("Female"), Age = 16} }
        };

        public Option<User> FindById(int id)
        {            
            return users.Get(id);
        }

        public Result<User> FindByIdAsResult(int id)
        {
            return users.Get(id)
                .Match(u => Result(u), () => Error.Of("Not Found"));
        }
    }
}
