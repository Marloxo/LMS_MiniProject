using System;
using System.Web.Mvc;
using System.Web.Security;
using LMS_MiniProject.Controllers;
using LMS_MiniProject.Models;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcIntegrationTestFramework.Hosting;
using System.Collections.Specialized;
using System.Text;
using MvcIntegrationTestFramework.Browsing;
using NUnit.Framework;

namespace LMS_MiniProject.Tests.Controllers
{
    [TestFixture]
    class BookControllerIntegrationTest
    {
        private AppHost appHost;

        [SetUp]
        public void TestFixtureSetUp()
        {
            //If you MVC project is not in the root of your solution directory then include the path
            //e.g. AppHost.Simulate("Website\MyMvcApplication")

            try
            {
                AppHost.LoadAllBinaries = true;
                appHost = AppHost.Simulate("MyMvcApplication");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        [TearDown]
        public void TestFixtureTearDown()
        {
            appHost.Dispose();
        }

        [Test]
        public void Root_Url_Renders_Index_View()
        {
            appHost.Start(browsingSession =>
            {
                // Request the root URL
                RequestResult result = browsingSession.Get("");

                // Can make assertions about the ActionResult...
                var viewResult = (ViewResult)result.ActionExecutedContext.Result;
                Assert.AreEqual("Index", viewResult.ViewName);
                Assert.AreEqual("Welcome to ASP.NET MVC!", viewResult.ViewData["Message"]);

                // ... or can make assertions about the rendered HTML
                Assert.IsTrue(result.ResponseText.Contains("<!DOCTYPE html"));
            });
        }

        [Test]
        public void querying_a_non_existent_route_gives_a_404()
        {
            appHost.Start(session =>
            {
                var result = session.Get("/Home/MotTheHoople");
                Assert.That(result.IsClientError);
                Assert.That(result.Response.StatusCode, Is.EqualTo(404));
            });
        }


        [Test]
        public void stress_test__100_calls_in_single_session()
        {
            appHost.Start(session =>
            {
                for (int i = 0; i < 100; i++)
                {
                    var result = session.Get("");
                    Assert.That(result.IsSuccess);
                }
            });
        }

        [Test]
        public void stress_test__100_sessions_with_a_single_call()
        {
            for (int i = 0; i < 100; i++)
            {
                appHost.Start(session =>
                {
                    var result = session.Get("");
                    Assert.That(result.IsSuccess);
                });
            }
        }

    }
}
