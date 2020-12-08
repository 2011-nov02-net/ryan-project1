using System.ComponentModel.DataAnnotations;

namespace Project1.BusinessLibrary
{
    /// <summary>
    /// storelocation class. Contains storelocation fields and constructor
    /// </summary>
    public class StoreLocation
    {
        [Display(Name = "Store Id")]
        public int StoreLocationId { get; }

        [Display(Name = "Store Name")]
        public string StoreLocationName { get; }

        public StoreLocation() { }

        public StoreLocation(int id, string name)
        {
            StoreLocationId = id;
            StoreLocationName = name;
        }
    }
}
