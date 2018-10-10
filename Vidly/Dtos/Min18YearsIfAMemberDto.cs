using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class Min18YearsIfAMemberDto : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customerDto = (CustomerDto)validationContext.ObjectInstance;
            if (customerDto.MembershipTypeId == MembershipType.Unknown || customerDto.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (customerDto.BirthDate == null)
                return new ValidationResult("BirthDate is required");

            var age = DateTime.Today.Year - customerDto.BirthDate.Value.Year;
            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 years old to go for membership plan");

        }
    }
}