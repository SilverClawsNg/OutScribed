using OutScribed.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Application.Queries.DTOs.Identity
{
    public class AccountTeam
    {
        public int Id { get; set; }

        public Ulid AccountId { get; set; }

        public Country Country { get; set; }

        public string Address { get; set; } = default!;

        public string Username { get; set; } = default!;

        public string ApplicationUrl { get; set; } = default!;

        public DateTime AppliedAt { get; set; }
    }
}
