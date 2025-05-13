using Microsoft.AspNetCore.Mvc;
using AutoDeskTest.MemberService.Api.Models;
using AutoDeskTest.MemberService.Api.Services;

namespace AutoDeskTest.MemberService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMemberServices _service;

        public MembersController(IMemberServices service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all members in the system.
        /// </summary>
        /// <returns>List of members</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var members = await _service.GetAllAsync();
            return Ok(members);
        }

        /// <summary>
        /// Retrieves a specific member by their ID.
        /// </summary>
        /// <param name="id">The ID of the member</param>
        /// <returns>Member data if found; otherwise, 404 Not Found</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _service.GetByIdAsync(id);
            return member is not null ? Ok(member) : NotFound();
        }

        /// <summary>
        /// Updates the entire record of an existing member.
        /// </summary>
        /// <param name="id">The ID of the member to update</param>
        /// <param name="memberData">The new member data</param>
        /// <returns>NoContent if successful; NotFound if the member doesn't exist</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PutMemberDto memberData)
        {
            var existingMember = await _service.GetByIdAsync(id);
            if (existingMember is null)
                return NotFound();

            existingMember.FullName = memberData.FullName;
            existingMember.Email = memberData.Email;
            existingMember.PhoneNumber = memberData.PhoneNumber;
            existingMember.DateOfBirth = memberData.DateOfBirth;
            existingMember.Status = memberData.Status;

            var success = await _service.UpdateAsync(id, existingMember);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Creates a new member record.
        /// </summary>
        /// <param name="memberData">The member data to create</param>
        /// <returns>Created result with member ID and data</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMemberDto memberData)
        {
            var newMember = new Member
            {
                FullName = memberData.FullName,
                Email = memberData.Email,
                PhoneNumber = memberData.PhoneNumber,
                DateOfBirth = memberData.DateOfBirth,
                Status = memberData.Status
            };

            var createdMember = await _service.CreateAsync(newMember);
            return CreatedAtAction(nameof(GetById), new { id = createdMember.Id }, createdMember);
        }

        /// <summary>
        /// Partially updates an existing member with only provided fields.
        /// </summary>
        /// <param name="id">The ID of the member to update</param>
        /// <param name="memberData">Partial data for update</param>
        /// <returns>NoContent if successful; NotFound if the member doesn't exist</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateMemberDto memberData)
        {
            var member = await _service.GetByIdAsync(id);
            if (member is null)
                return NotFound();

            if (memberData.FullName != null) member.FullName = memberData.FullName;
            if (memberData.Email != null) member.Email = memberData.Email;
            if (memberData.PhoneNumber != null) member.PhoneNumber = memberData.PhoneNumber;
            if (memberData.DateOfBirth != null) member.DateOfBirth = memberData.DateOfBirth.Value;
            if (memberData.Status != null) member.Status = memberData.Status;

            await _service.UpdateAsync(id, member);
            return NoContent();
        }

        /// <summary>
        /// Deletes a member by their ID.
        /// </summary>
        /// <param name="id">The ID of the member to delete</param>
        /// <returns>NoContent if successful; NotFound if the member doesn't exist</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}