using System;
using System.Runtime.Serialization;
using WatchAllApi.Enums;
using WatchAllApi.Models;

namespace WatchAllApi.Responses.UserResponses
{
    /// <summary>
    /// Full user model response
    /// </summary>
    [DataContract]
    public class UserProfileRequest
    {
        /// <summary>
        /// Id from DB
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Login
        /// </summary>
        [DataMember]
        public string Login { get; set; }

        /// <summary>
        /// Firstname
        /// </summary>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// Role of user
        /// </summary>
        [DataMember]
        public UserRole Role { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        [DataMember]
        public string Phone { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [DataMember]
        public string City { get; set; }

        /// <summary>
        /// Date of user creation
        /// </summary>
        [DataMember]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Create response from model
        /// </summary>
        /// <param name="userProfile">Model that will be transform to response</param>
        /// <returns></returns>
        public static UserProfileRequest Create(UserProfile userProfile)
        {
            return new UserProfileRequest
            {
                Id = userProfile.Id,
                Role = userProfile.Role,
                Login = userProfile.Login,
                LastName = userProfile.LastName,
                Email = userProfile.Email,
                CreatedDate = userProfile.CreatedDate,
                FirstName = userProfile.FirstName,
                City = userProfile.City,
                Phone = userProfile.Phone
            };
        }
    }
}
