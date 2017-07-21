using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RESTful_API_OAuth.Models
{
    [MetadataType(typeof(UsersMetadata))]
    public partial class Users
    {
        /// <summary>
        /// 使用者 Partial Model - User Partial Model.
        /// </summary>
        public class UsersMetadata
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public System.DateTime EditTime { get; set; }

            [JsonIgnore]
            public virtual Roles Roles { get; set; }

            [JsonIgnore]
            public virtual Status Status { get; set; }
        }
    }
}