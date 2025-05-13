using Moq;
using Microsoft.AspNetCore.Mvc;
using AutoDeskTest.MemberService.Api.Controllers;
using AutoDeskTest.MemberService.Api.Models;
using AutoDeskTest.MemberService.Api.Services;

namespace MemberService.Tests
{
    public class MembersControllerTests
    {
        private readonly Mock<IMemberServices> _mockService;
        private readonly MembersController _controller;

        public MembersControllerTests()
        {
            _mockService = new Mock<IMemberServices>();
            _controller = new MembersController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithMembers()
        {
            _mockService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<Member> { new Member { Id = 1, FullName = "John" } });

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var members = Assert.IsAssignableFrom<IEnumerable<Member>>(okResult.Value);
            Assert.Single(members);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenFound()
        {
            var member = new Member { Id = 1, FullName = "Alice" };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(member);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Alice", ((Member)okResult.Value!).FullName);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenMissing()
        {
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((Member?)null);

            var result = await _controller.GetById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedResult_WithLocation()
        {
            var input = new CreateMemberDto { FullName = "New Member", Email = "new@member.com", DateOfBirth = DateTime.Now, Status = "Active" };
            var created = new Member { Id = 99, FullName = "New Member" };

            _mockService.Setup(s => s.CreateAsync(It.IsAny<Member>())).ReturnsAsync(created);

            var result = await _controller.Create(input);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetById", createdResult.ActionName);
            Assert.Equal(99, ((Member)createdResult.Value!).Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenSuccess()
        {
            var input = new PutMemberDto { FullName = "Updated", Email = "a@b.com", PhoneNumber = "123456", DateOfBirth = DateTime.Today, Status = "Active" };
            var existing = new Member { Id = 1 };

            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(existing);
            _mockService.Setup(s => s.UpdateAsync(1, It.IsAny<Member>())).ReturnsAsync(true);

            var result = await _controller.Update(1, input);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenMissing()
        {
            var input = new PutMemberDto { FullName = "X", Email = "x@y.com", DateOfBirth = DateTime.Today, Status = "Inactive" };

            _mockService.Setup(s => s.GetByIdAsync(2)).ReturnsAsync((Member?)null);

            var result = await _controller.Update(2, input);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccess()
        {
            _mockService.Setup(s => s.DeleteAsync(3)).ReturnsAsync(true);

            var result = await _controller.Delete(3);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenMissing()
        {
            _mockService.Setup(s => s.DeleteAsync(4)).ReturnsAsync(false);

            var result = await _controller.Delete(4);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Patch_ReturnsNoContent_WhenSuccessful()
        {
            var existing = new Member { Id = 1, FullName = "Original Name" };

            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(existing);
            _mockService.Setup(s => s.UpdateAsync(1, It.IsAny<Member>())).ReturnsAsync(true);

            var patch = new UpdateMemberDto { FullName = "Patched Name" };

            var result = await _controller.Patch(1, patch);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Patch_ReturnsNotFound_WhenMemberDoesNotExist()
        {
            _mockService.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((Member?)null);

            var patch = new UpdateMemberDto { FullName = "Missing" };

            var result = await _controller.Patch(99, patch);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}