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
            var input = new Member { FullName = "New Member" };
            var created = new Member { Id = 99, FullName = "New Member" };

            _mockService.Setup(s => s.CreateAsync(input)).ReturnsAsync(created);

            var result = await _controller.Create(input);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetById", createdResult.ActionName);
            Assert.Equal(99, ((Member)createdResult.Value!).Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenSuccess()
        {
            var member = new Member { Id = 1, FullName = "Updated" };
            _mockService.Setup(s => s.UpdateAsync(1, member)).ReturnsAsync(true);

            var result = await _controller.Update(1, member);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenMissing()
        {
            var member = new Member { Id = 2 };
            _mockService.Setup(s => s.UpdateAsync(2, member)).ReturnsAsync(false);

            var result = await _controller.Update(2, member);

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
            var existing = new Member { Id = 1, FullName = "Original Name", Email = "old@email.com" };

            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(existing);
            _mockService.Setup(s => s.UpdateAsync(1, It.IsAny<Member>())).ReturnsAsync(true);

            var patch = new Member { FullName = "Patched Name" };

            var result = await _controller.Patch(1, patch);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Patch_ReturnsNotFound_WhenMemberDoesNotExist()
        {
            _mockService.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((Member?)null);

            var patch = new Member { FullName = "Missing Member" };

            var result = await _controller.Patch(99, patch);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}