using System.Collections;

namespace Project1.BusinessLibrary
{
    /// <summary>
    /// Customer class. Contains customers fields and constructor
    /// </summary>
    public class Customer
    {
        public int CustomerId { get; }
        public string FirstName { get; }
        public string LastName { get; private set; }
        public int UserType { get; private set; }
        public string Email { get; }
        public string PasswordHash { get; }

        public Customer(int id, string first, string last, int type, string email, string passHash)
        {
            CustomerId = id;
            FirstName = first;
            LastName = last;
            UserType = type;
            Email = email;
            PasswordHash = passHash;
        }

        public Customer(int id, string first, string last, int type)
        {
            CustomerId = id;
            FirstName = first;
            LastName = last;
            UserType = type;
        }
    }
}
