using System.ComponentModel.DataAnnotations;

namespace AutoDeskTest.MemberService.Api.Models
{
    /// <summary>
    /// Represents a member in the system.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Unique identifier for the member.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Full name of the member.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string? FullName { get; set; }

        /// <summary>
        /// Email address of the member.
        /// </summary>
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// Contact phone number of the member.
        /// </summary>
        [Phone]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Member's date of birth.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Status of the member (e.g., Active, Inactive).
        /// </summary>
        [Required]
        public string? Status { get; set; } = "Active";
    }

    /// <summary>
    /// DTO for partially updating a member.
    /// </summary>
    /// <summary>
    /// DTO for partially updating a member.
    /// </summary>
    public class UpdateMemberDto
    {
        /// <summary>Full name of the member.</summary>
        public string? FullName { get; set; }

        /// <summary>Email address of the member.</summary>
        public string? Email { get; set; }

        /// <summary>Phone number of the member.</summary>
        public string? PhoneNumber { get; set; }

        /// <summary>Date of birth of the member.</summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>Status of the member.</summary>
        public string? Status { get; set; }
    }

    /// <summary>
    /// DTO for creating a new member.
    /// </summary>
    /// <summary>
    /// DTO for creating a new member.
    /// </summary>
    public class CreateMemberDto
    {
        /// <summary>Full name of the new member.</summary>
        [Required]
        [MaxLength(100)]
        public required string FullName { get; set; }

        /// <summary>Email address of the new member.</summary>
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        /// <summary>Phone number of the new member.</summary>
        [Phone]
        public string? PhoneNumber { get; set; }

        /// <summary>Date of birth of the new member.</summary>
        [Required]
        public required DateTime DateOfBirth { get; set; }

        /// <summary>Status of the new member.</summary>
        [Required]
        public required string Status { get; set; }
    }

    /// <summary>
    /// DTO for fully replacing an existing member.
    /// </summary>
    /// <summary>
    /// DTO for fully replacing an existing member.
    /// </summary>
    public class PutMemberDto
    {
        /// <summary>Full name of the member.</summary>
        [Required]
        [MaxLength(100)]
        public required string FullName { get; set; }

        /// <summary>Email address of the member.</summary>
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        /// <summary>Phone number of the member.</summary>
        [Phone]
        public string? PhoneNumber { get; set; }

        /// <summary>Date of birth of the member.</summary>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>Status of the member.</summary>
        [Required]
        public required string Status { get; set; }
    }
}