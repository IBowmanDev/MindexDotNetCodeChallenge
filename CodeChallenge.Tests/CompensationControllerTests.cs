using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var compensation = new Compensation()
            {
                EmployeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f",
                Salary = 1000000,
                EffectiveDate = DateTime.Now.AddDays(7)
            };
            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var getRequestTask = _httpClient.PostAsync("api/compensation",
                new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.CompensationId);
            Assert.AreEqual(compensation.EmployeeId, newCompensation.EmployeeId);
            Assert.AreEqual(compensation.Salary, newCompensation.Salary);
        }

        [TestMethod]        
        public void CreateCompensation_Returns_BadRequest()
        {
            // Arrange
            var compensation = new Compensation()
            {
                EmployeeId = null,
                Salary = 1000000,
                EffectiveDate = DateTime.Now.AddDays(7)
            };
            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var getRequestTask = _httpClient.PostAsync("api/compensation",
                new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void GetCompensation_Returns_Ok()
        {
            // Arrange
            var employeeId = "62c1084e-6e34-4630-93fd-9153afb65309";
            var compensation = new Compensation()
            {
                EmployeeId = "62c1084e-6e34-4630-93fd-9153afb65309",
                Salary = 100000,
                EffectiveDate = DateTime.UtcNow
            };

            var createRequestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var createRequestTask = _httpClient.PostAsync("api/compensation",
                new StringContent(createRequestContent, Encoding.UTF8, "application/json"));
            
            var createResponse = createRequestTask.Result;

            Assert.AreEqual(HttpStatusCode.Created, createResponse.StatusCode);
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var getResponse = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
            var newCompensation = getResponse.DeserializeContent<Compensation>();
            Assert.AreEqual(employeeId, newCompensation.EmployeeId);
            Assert.AreEqual(compensation.Salary, newCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
        }
    }
}
