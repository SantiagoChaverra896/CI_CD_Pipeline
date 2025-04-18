using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.Core;
using Newtonsoft.Json.Linq;
using RestSharp;
using log4net.Config;
using log4net;

namespace Task_TestAutomationFramework.API_TestCases
{
    [TestFixture]
    [Category("API")]
    [Parallelizable(ParallelScope.All)]
    public class APITestCases
    {
#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        protected RestClient _client;
#pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method

        [SetUp]
        public void Setup()
        {
            _client = new RestClient("https://jsonplaceholder.typicode.com");
        }

        [Test]
        public async Task Task1_VerifyThatTheListOfUsersCanBeReceived()
        {
            Logger.Info("Starting Task1: Fetch list of users.");

            var request = new RequestBuilder()
                .SetMethod(Method.Get)
                .SetResource("/users")
                .Build();

            var response = _client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code 200 OK");
            Logger.Info("Received 200 OK status.");

            var users = JArray.Parse(response.Content);
            Assert.That(users.Count, Is.GreaterThan(0), "Expected at least one user on the list");
            Logger.Info($"User count received: {users.Count}");

            foreach (var user in users)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(user["id"], Is.Not.Null, "Missing 'id'");
                    Assert.That(user["name"], Is.Not.Null, "Missing 'name'");
                    Assert.That(user["username"], Is.Not.Null, "Missing 'username'");
                    Assert.That(user["email"], Is.Not.Null, "Missing 'email'");
                    Assert.That(user["address"], Is.Not.Null, "Missing 'address'");
                    Assert.That(user["phone"], Is.Not.Null, "Missing 'phone'");
                    Assert.That(user["website"], Is.Not.Null, "Missing 'website'");
                    Assert.That(user["company"], Is.Not.Null, "Missing 'company'");
                });
            }

            Logger.Info("Task1 completed successfully.");
        }

        [Test]
        public async Task Task2_ValidateResponseHeaderForAListOfUsers()
        {
            Logger.Info("Starting Task2: Validate response headers for /users.");

            var request = new RequestBuilder()
                .SetMethod(Method.Get)
                .SetResource("/users")
                .Build();

            var response = _client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code 200 OK");

            var contentTypeHeader = response.Headers.FirstOrDefault(h => h.Name.Equals("Content-Type", StringComparison.OrdinalIgnoreCase));
            Assert.That(contentTypeHeader, Is.Not.Null, "Content-Type header is missing");
            Assert.That(contentTypeHeader.Value?.ToString(), Is.EqualTo("application/json; charset=utf-8"), "Unexpected Content-Type value");

            Assert.That(response.ErrorMessage, Is.Null.Or.Empty, "Unexpected error message in response");

            Logger.Info("Task2 completed successfully.");


        }

        [Test]
        public async Task Task3_ValidateResponseHeaderForListOfUsers()
        {
            Logger.Info("Starting Task3: Validate user content and structure.");

            var request = new RequestBuilder()
                .SetMethod(Method.Get)
                .SetResource("/users")
                .Build();

            var response = _client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code 200 OK");
            Assert.That(response.ErrorMessage, Is.Null.Or.Empty, "Unexpected error message");

            var users = JArray.Parse(response.Content);
            Assert.That(users.Count, Is.EqualTo(10), "Expected 10 users in the array");

            var ids = new HashSet<int>();

            foreach (var user in users)
            {
                var id = (int)user["id"];
                var name = (string)user["name"];
                var username = (string)user["username"];
                var company = user["company"];
                var companyName = (string?)company?["name"];

                Assert.Multiple(() =>
                {
                    Assert.That(ids.Contains(id), Is.False, $"Duplicate ID found: {id}");
                    ids.Add(id);

                    Assert.That(name, Is.Not.Null.And.Not.Empty, $"Name is empty for user ID {id}");
                    Assert.That(username, Is.Not.Null.And.Not.Empty, $"Username is empty for user ID {id}");
                    Assert.That(company, Is.Not.Null, $"Missing company for user ID {id}");
                    Assert.That(companyName, Is.Not.Null.And.Not.Empty, $"Company name is empty for user ID {id}");
                });
            }

            Logger.Info("Task3 completed successfully.");
        }

        [Test]
        public async Task Task4_ValidateThatUserCanBeCreated()
        {
            Logger.Info("Starting Task4: Create a new user.");

            var request = new RequestBuilder()
                .SetMethod(Method.Post)
                .SetResource("/users")
                .AddJsonBody(new { name = "Santiago Tester", username = "santi_dev" })
                .Build();

            var response = _client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Expected status code 201 Created");
            Assert.That(response.ErrorMessage, Is.Null.Or.Empty, "Unexpected error message");
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty, "Response content is empty");

            var json = JObject.Parse(response.Content);
            Assert.That(json["id"], Is.Not.Null, "Response does not contain 'id'");
            Assert.That(int.TryParse(json["id"]?.ToString(), out _), Is.True, "ID value is not a valid integer");

            Logger.Info("Task4 completed successfully.");
        }

        [Test]
        public async Task Task5_VerifyThatUserIsNotifiedIfResourceDoesNotExist()
        {
            Logger.Info("Starting Task5: Request to an invalid endpoint.");

            var request = new RequestBuilder()
                .SetMethod(Method.Get)
                .SetResource("/invalidendpoint")
                .Build();

            var response = _client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Expected status code 404 Not Found");
            Assert.That(response.ErrorMessage, Is.Null.Or.Empty, "Unexpected error message in response");

            Logger.Info("Task5 completed successfully.");
        }

    }
}
