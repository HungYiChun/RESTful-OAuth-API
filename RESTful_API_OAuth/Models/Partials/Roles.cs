using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RESTful_API_OAuth.Models
{
    [MetadataType(typeof(RolesMetadata))]
    public partial class Roles
    {
        /// <summary>
        /// 權限 Partial Model - Roles Partial Model.
        /// </summary>
        public class RolesMetadata
        {
            public int Id { get; set; }
            public string RoleName { get; set; }

            [JsonIgnore]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<Users> Users { get; set; }
        }
    }
}