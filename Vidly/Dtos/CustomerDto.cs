using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required()]
        [StringLength(255)]
        public string Name { get; set; }


        //[Min18YearsIfAMemberDto]
        public DateTime? BirthDate { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        public MembershipType MembershipType { get; set; }


        public byte MembershipTypeId { get; set; }
    }
}