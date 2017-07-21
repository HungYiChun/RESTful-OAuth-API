using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RESTful_API_OAuth.Models
{
    [MetadataType(typeof(StatusMetadata))]
    public partial class Status
    {
        /// <summary>
        /// 使用者狀態 Partial Model - User Status Partial Model.
        /// </summary>
        public class StatusMetadata
        {
            public int Id { get; set; }
            public string StatusName { get; set; }

            [JsonIgnore]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<Users> Users { get; set; }
        }
    }
}