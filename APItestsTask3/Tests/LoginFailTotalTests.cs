using System.Net;
using APItestsTask3.Models;
using APItestsTask3.Models.DB;
using APItestsTask3.Utils;
using APItestsTask3.Utils.Mapping;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;

namespace APItestsTask3.Tests
{
    [TestFixture]
    public class LoginFailTotalTests : BaseTest
    {
        [Test]
        public async Task Get_LoginFailTotal_WithoutParameters_AllUsersReturned()
        {
            var userHistoriesList = new List<UserLoginHistoryDao>
            {
                new() { UserName = "User1@test", LoginFailCount = 5 },
                new() { UserName = "User2@test", LoginFailCount = 3 },
                new() { UserName = "User3@test", LoginFailCount = 10 },

            };
            await _userLoginHistorySeeder.InsertAsync(userHistoriesList);
            
            var response = await _service.GetLoginFailTotal(_testDataStorage.Token);

            var expectedUserHistories = userHistoriesList.Select(UserLoginHistoryMapper.MapTo);
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Data!.Count().Should().Be(expectedUserHistories.Count());
                response.Data.Should()
                    .BeEquivalentTo(expectedUserHistories, config => config.WithoutStrictOrdering());
            }
        }

        [Test]
        public async Task Get_LoginFailTotal_WithoutParameters_EmptyListReturned()
        {
            var response = await _service.GetLoginFailTotal(_testDataStorage.Token);
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Data.Should().BeEmpty();
            }
        }

        [Test]
        public async Task Get_LoginFailTotal_SpecifySpecificUser_UserReturned()
        {
            var userHistoriesList = new List<UserLoginHistoryDao>
            {
                new() { UserName = "User1@test", LoginFailCount = 5 },
                new() { UserName = "User2@test", LoginFailCount = 3 },
                new() { UserName = "User3@test", LoginFailCount = 10 },
            };
            await _userLoginHistorySeeder.InsertAsync(userHistoriesList);

            var userName = "User2@test";
            var query = new QueryBuilder()
                .WithParameter("user_Name", userName)
                .Build();

            var response = await _service.GetLoginFailTotal(_testDataStorage.Token, query);

            var expectedData = UserLoginHistoryMapper.MapTo(userHistoriesList.First(user => user.UserName == userName));
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Data!.Count().Should().Be(1);
                response.Data.Should().Contain(expectedData);
            }
        }

        [Test]
        public async Task Get_LoginFailTotal_SpecifyNonExistingUser_NotFound()
        {
            var userHistoriesList = new List<UserLoginHistoryDao>
            {
                new() { UserName = "User1@test", LoginFailCount = 5 }
            };
            await _userLoginHistorySeeder.InsertAsync(userHistoriesList);

            var userName = "User2@test";
            var query = new QueryBuilder()
                .WithParameter("user_Name", userName)
                .Build();

            var response = await _service.GetLoginFailTotal(_testDataStorage.Token, query);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        [Test]
        public async Task Get_LoginFailTotal_SpecifyFailCount_UsersReturned()
        {
            var userHistoriesList = new List<UserLoginHistoryDao>
            {
                new() { UserName = "User1@test", LoginFailCount = 5 },
                new() { UserName = "User2@test", LoginFailCount = 3 },
                new() { UserName = "User3@test", LoginFailCount = 10 },
            };
            await _userLoginHistorySeeder.InsertAsync(userHistoriesList);

            var query = new QueryBuilder()
                .WithParameter("fail_count", "5")
                .Build();

            var response = await _service.GetLoginFailTotal(_testDataStorage.Token, query);

            var expectedUsers = userHistoriesList.Where(user => user.LoginFailCount >= 5);
            var expectedData = expectedUsers.Select(UserLoginHistoryMapper.MapTo);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Data!.Count().Should().Be(expectedData.Count());
                response.Data.Should().BeEquivalentTo(expectedData, config => config.WithoutStrictOrdering());
            }
        }

        [Test]
        public async Task Get_LoginFailTotal_SpecifyFailCount_EmptyListReturned()
        {
            var userHistoriesList = new List<UserLoginHistoryDao>
            {
                new() { UserName = "User1@test", LoginFailCount = 5 },
                new() { UserName = "User2@test", LoginFailCount = 3 },
                new() { UserName = "User3@test", LoginFailCount = 10 },
            };
            await _userLoginHistorySeeder.InsertAsync(userHistoriesList);

            var query = new QueryBuilder()
                .WithParameter("fail_count", "11")
                .Build();

            var response = await _service.GetLoginFailTotal(_testDataStorage.Token, query);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Data.Should().BeEmpty();
            }
        }

        [Test]
        public async Task Get_LoginFailTotal_SpecifyFetchLimit_UsersReturned()
        {
            var userHistoriesList = new List<UserLoginHistoryDao>
            {
                new() { UserName = "User1@test", LoginFailCount = 5 },
                new() { UserName = "User2@test", LoginFailCount = 3 },
                new() { UserName = "User3@test", LoginFailCount = 10 },
            };
            await _userLoginHistorySeeder.InsertAsync(userHistoriesList);

            var query = new QueryBuilder()
                .WithParameter("fetch_limit", "2")
                .Build();

            var response = await _service.GetLoginFailTotal(_testDataStorage.Token, query);

            //Default sorting should be specified
            //UserName sorted by Descending using in current example
            var expectedUsers = userHistoriesList.OrderByDescending(x => x.UserName).Take(2).Select(UserLoginHistoryMapper.MapTo);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Data!.Count().Should().Be(expectedUsers.Count());
                response.Data.Should().BeEquivalentTo(expectedUsers);
            }
        }

        [Test]
        public void Get_LoginFailTotal_SpecifyFailCountAndFetchLimitParameters_UsersReturned()
        {
            var userHistoriesList = new List<UserLoginHistoryDao>
            {
                new() { UserName = "User1@test", LoginFailCount = 5 },
                new() { UserName = "User2@test", LoginFailCount = 3 },
                new() { UserName = "User3@test", LoginFailCount = 10 },
                new() { UserName = "User4@test", LoginFailCount = 6 },
                new() { UserName = "User5@test", LoginFailCount = 12 },
            };
            _userLoginHistorySeeder.InsertAsync(userHistoriesList);

            var query = new QueryBuilder()
                .WithParameters(new List<Query>
                {
                    new() { Field = "fail_count", Value = "5" },
                    new() { Field = "fetch_limit", Value = "2" },
                })
                .Build();

            var response = _service.GetLoginFailTotal(_testDataStorage.Token, query);

            //Default sorting should be specified
            //UserName sorted by Descending using in current example
            var expectedUsers = userHistoriesList.OrderByDescending(x => x.UserName)
                .Where(user => user.LoginFailCount >= 5).Take(2).Select(UserLoginHistoryMapper.MapTo);

            using (new AssertionScope())
            {
                response.Result.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Result.Data!.Count().Should().Be(expectedUsers.Count());
                response.Result.Data.Should().BeEquivalentTo(expectedUsers);
            }
        }

        //Additional cases that can be automated after clarifying AC:
        // fetch_limit<=0 - what response should be returned?
        // fail_count <=0 - what response should be returned?
        // user with failCount = 3 is specified in user_Name, fail_count = 5 - what response should be returned?
    }
}
