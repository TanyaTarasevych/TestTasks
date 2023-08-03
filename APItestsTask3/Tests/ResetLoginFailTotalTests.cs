using APItestsTask3.Models.DB;
using FluentAssertions.Execution;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using APItestsTask3.Utils;

namespace APItestsTask3.Tests
{
    [TestFixture]
    public class ResetLoginFailTotalTests : BaseTest
    {
        [Test]
        public async Task Put_ResetLoginFailTotal_SpecifyUser_OkReturned()
        {
            var userHistoriesList = new List<UserLoginHistoryDao>
            {
                new() { UserName = "User1@test", LoginFailCount = 5 },
                new() { UserName = "User2@test", LoginFailCount = 3 },
                new() { UserName = "User3@test", LoginFailCount = 10 },
            };
            await _userLoginHistorySeeder.InsertAsync(userHistoriesList);

            var userName = "User3@test";
            var query = new QueryBuilder()
                .WithParameter("userName", userName)
                .Build();

            var response = await _service.PutResetLoginFailTotal(_testDataStorage.Token, query);
            var dbResponse = await _userLoginHistorySeeder.GetAsync(userName);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                dbResponse.LoginFailCount.Should().Be(0);
            }
        }

        [Test]
        public async Task Put_ResetLoginFailTotal_SpecifyNonExistingUser_BadRequestReturned()
        {
            var userName = "User3@test";
            var query = new QueryBuilder()
                .WithParameter("userName", userName)
                .Build();

            var response = await _service.PutResetLoginFailTotal(_testDataStorage.Token, query);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
