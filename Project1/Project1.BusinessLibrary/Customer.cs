using System.ComponentModel.DataAnnotations;

namespace Project1.BusinessLibrary
{
    /// <summary>
    /// Customer class. Contains customers fields and constructor
    /// </summary>
    public class Customer
    {
        [Display(Name = "Customer Id")]
        public int CustomerId { get; }

        [Display(Name = "First Name")]
        public string FirstName { get; }

        [Display(Name = "Last Name")]
        public string LastName { get; private set; }

        [Display(Name = "User Type")]
        public int UserType { get; private set; }
        public string Email { get; }

        [Display(Name = "Password Hash")]
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
