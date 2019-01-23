using System.Collections.Generic;
using System.Runtime.Serialization;
using WatchAllApi.Models;
using WatchAllApi.Models.UserStat;

namespace WatchAllApi.Requests.UserRequests
{
    /// <summary>
    /// Full user model response
    /// </summary>
    [DataContract]
    public class UpdateUserProfileRequest
    {
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
        /// Shows
        /// </summary>
        [DataMember]
        public List<UserShowModel> Shows { get; set; }

        /// <summary>
        ///  Create model of UserProfile from request
        /// </summary>
        /// <returns></returns>
        public UserProfile MergeToModel(UserProfile userProfile)
        {
            userProfile.Login = Login;
            userProfile.LastName = LastName;
            userProfile.Email = Email;
            userProfile.FirstName = FirstName;
            userProfile.City = City;
            userProfile.Phone = Phone;
            userProfile.Shows = Shows;

            return userProfile;
        }
    }
}
