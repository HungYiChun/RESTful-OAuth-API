using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RESTful_API_OAuth.Models
{
    [MetadataType(typeof(ViewUserDetailMetadata))]
    public partial class ViewUserDetail
    {
        /// <summary>
        /// 使用者資料 Partial Model - UserDetail Partial Model.
        /// </summary>
        public class ViewUserDetailMetadata
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public System.DateTime EditTime { get; set; }
            public string RoleName { get; set; }
            public string StatusName { get; set; }
        }
    }
}