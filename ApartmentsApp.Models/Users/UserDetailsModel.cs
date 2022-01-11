using System;
using System.Text.Json.Serialization;

namespace ApartmentsApp.Models.Users
{
    public class UserDetailsModel
    {
        public int Id { get; set; }
        public string TcNo { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string CarPlate { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }
    }
}
