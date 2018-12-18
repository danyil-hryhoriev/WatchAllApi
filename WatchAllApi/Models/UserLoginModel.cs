using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WatchAllApi.Models
{
    public class UserLoginModel
    {
        /// <summary>
        /// User name
        /// </summary>
        [DataMember, Required(AllowEmptyStrings = false)]
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [DataMember, Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
