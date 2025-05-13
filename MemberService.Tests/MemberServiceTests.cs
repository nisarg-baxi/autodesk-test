using Xunit;
using Moq;
using System.Threading.Tasks;
using AutoDeskTest.MemberService.Api.Models;
using AutoDeskTest.MemberService.Api.Repositories;
using AutoDeskTest.MemberService.Api.Services;

namespace MemberService.Tests
{
    public class MemberServiceTests
    {
        private readonly Mock<IMemberRepository> _mockRepo;
        private readonly MemberServices _service;

        public MemberServiceTests()
        {
            _mockRepo = new Mock<IMemberRepository>();
            _service = new MemberServices(_mockRepo.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnMember_WhenExists()
        {
            // Arrange
            var expected = new Member { Id = 1, FullName = "Alice" };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(expected);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Alice", result!.FullName);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenMemberDoesNotExist()
        {
            _mockRepo.Setup(r => r.ExistsAsync(1)).ReturnsAsync(false);

            var updated = new Member { Id = 1, FullName = "Updated" };
            var result = await _service.UpdateAsync(1, updated);

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateAndReturnTrue_WhenMemberExists()
        {
            _mockRepo.Setup(r => r.ExistsAsync(1)).ReturnsAsync(true);
            _mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Member>())).Returns(Task.CompletedTask);

            var updated = new Member { Id = 1, FullName = "Updated" };
            var result = await _service.UpdateAsync(1, updated);

            Assert.True(result);
            _mockRepo.Verify(r => r.UpdateAsync(It.Is<Member>(m => m.Id == 1 && m.FullName == "Updated")), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenMemberDoesNotExist()
        {
            _mockRepo.Setup(r => r.ExistsAsync(2)).ReturnsAsync(false);

            var result = await _service.DeleteAsync(2);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallDelete_WhenMemberExists()
        {
            _mockRepo.Setup(r => r.ExistsAsync(3)).ReturnsAsync(true);
            _mockRepo.Setup(r => r.DeleteAsync(3)).Returns(Task.CompletedTask);

            var result = await _service.DeleteAsync(3);

            Assert.True(result);
            _mockRepo.Verify(r => r.DeleteAsync(3), Times.Once);
        }
    }
}