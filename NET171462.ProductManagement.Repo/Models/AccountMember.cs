using SE171462.ProductManagement.Repo.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NET171462.ProductManagement.Repo.Models
{
    public partial class AccountMember : ISoftDelete
    {
        public string MemberId { get; set; } = null!;

        public string MemberPassword { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string EmailAddress { get; set; }
        public int MemberRole { get; set; }

        public bool IsDeleted { get; set; }
        //public DateTimeOffset? DeletedAt { get; set; }
    }
}
