namespace Masha.Foundation.Tests
{
    using System;
    public class User : IEquatable<User>
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public int Age { get; set; }
        public Option<string> Gender { get; set; }

        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public User()
        { }

        public override bool Equals(object obj)
        {
            if (!(obj is User)) return false;
            var other = (User)obj;
            return this.Name == other.Name && this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Id.GetHashCode();
        }

        public bool Equals(User other)
        {
            return this.Name == other.Name && this.Id == other.Id;
        }
    }
}
