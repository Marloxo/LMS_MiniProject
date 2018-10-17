using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using LMS_MiniProject.Controllers;
using LMS_MiniProject.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LMS_MiniProject.Tests.Controllers
{
    [TestClass]
    public class BookControllerTest
    {
        private LMSDBEntities db = new LMSDBEntities();

        //Index
        [TestMethod]
        public void Index()
        {
            // Arrange
            BookController controller = new BookController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        //Details
        [TestMethod]
        public void TestDetailIsNull()
        {
            // Arrange
            BookController controller = new BookController();

            // Act
            ViewResult result = controller.Details(null) as ViewResult;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestDetailNotFound()
        {
            // Arrange
            BookController controller = new BookController();

            // Act
            ViewResult result = controller.Details(-15) as ViewResult;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestDetailView()
        {
            // Arrange
            BookController controller = new BookController();

            // Act
            ViewResult result = controller.Details(1) as ViewResult;

            // Assert
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void TestDetailsViewData()
        {
            // Arrange
            BookController controller = new BookController();

            // Act
            ViewResult result = controller.Details(1) as ViewResult;
            BookTbl product = (BookTbl)result.ViewData.Model;

            // Assert
            Assert.AreEqual(1, product.Id);
        }


        //Create
        [TestMethod]
        public void TestCreateView()
        {
            // Arrange
            BookController controller = new BookController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCreateRedirect()
        {
            // Arrange
            BookController controller = new BookController();

            // Act
            BookTbl newbook = new BookTbl
            {
                Id = 0,
                Title = "Test Book-" + Membership.GeneratePassword(8, 0),
                Author = "Test Author-" + Membership.GeneratePassword(8, 0),
                Tag = "Test Tag-" + Membership.GeneratePassword(4, 0),
                Image = "https://d1e4pidl3fu268.cloudfront.net/66963e4a-ccba-4fdd-ba18-d5862fb4dba7/test.png?" + Membership.GeneratePassword(8, 0),
                Year_of_publishing = 2000
            };
            var result = controller.Create(newbook) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"].ToString());
        }

        [TestMethod]
        public void TestCreateFail()
        {
            // Arrange
            BookController controller = new BookController();

            // Act
            BookTbl newbook = new BookTbl
            {
                Id = 0,
                Title = "Test Book",
                Author = "Test Author",
                Tag = "Test Tag",
                Year_of_publishing = -50
            };
            var result = controller.Create(newbook) as ViewResult;

            // Assert
            Assert.AreEqual("Create", result.ViewName);
        }


        //Edit
        [TestMethod]
        public void TestEditIsNull()
        {
            // Arrange
            BookController controller = new BookController();

            // Act
            ViewResult result = controller.Edit(id: null) as ViewResult;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestEditNotFound()
        {
            // Arrange
            BookController controller = new BookController();

            // Act
            ViewResult result = controller.Edit(-15) as ViewResult;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestEditViewData()
        {
            // Arrange
            BookController controller = new BookController();

            // Act
            ViewResult result = controller.Edit(1) as ViewResult;
            BookTbl product = (BookTbl)result.ViewData.Model;

            // Assert
            Assert.AreEqual(1, product.Id);
        }

        [TestMethod]
        public void TestEditRedirect()
        {
            // Arrange
            BookController controller = new BookController();

            // Act
            BookTbl dbBook = new BookTbl
            {
                Id = 1,
                Title = "Test Book New-" + Membership.GeneratePassword(8, 0),
                Author = "Test Author New-" + Membership.GeneratePassword(8, 0),
                Tag = "Test Tag New-" + Membership.GeneratePassword(4, 0),
                Image = "https://d1e4pidl3fu268.cloudfront.net/66963e4a-ccba-4fdd-ba18-d5862fb4dba7/test.png?" + Membership.GeneratePassword(8, 0),
                Year_of_publishing = 2000
            };
            var result = controller.Edit(dbBook) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"].ToString());
        }

        [TestMethod]
        public void TestEditFail()
        {
            // Arrange
            BookController controller = new BookController();

            // Act
            BookTbl dbBook = new BookTbl
            {
                Id = 9,
                Title = "Test Book New",
                Author = "Test Author New",
                Tag = "Test Tag New",
                Image = "https://d1e4pidl3fu268.cloudfront.net/66963e4a-ccba-4fdd-ba18-d5862fb4dba7/test.png",
                Year_of_publishing = -50
            };
            var result = controller.Edit(dbBook) as ViewResult;

            // Assert
            Assert.AreEqual("Edit", result.ViewName);
        }


        //Delete
        [TestMethod]
        public void DeleteIsNull()
        {
            // Arrange
            BookController controller = new BookController();

            // Act
            ViewResult result = controller.Delete(id: null) as ViewResult;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestDeleteNotFound()
        {
            // Arrange
            BookController controller = new BookController();

            // Act
            ViewResult result = controller.Delete(-15) as ViewResult;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestDeleteViewData()
        {
            // Arrange
            BookController controller = new BookController();

            // Act
            ViewResult result = controller.Delete(1) as ViewResult;
            BookTbl product = (BookTbl)result.ViewData.Model;

            // Assert
            Assert.AreEqual(1, product.Id);
        }

        [TestMethod]
        public void TestDeleteConfirmedRedirect()
        {
            // Arrange
            BookController controller = new BookController();

            // Act
            var bookList = db.BookTbls.ToList();

            Random rnd = new Random();
            int r = rnd.Next(1, bookList.Count);

            var selectedId = bookList[r].Id;
            var result = controller.DeleteConfirmed(selectedId) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"].ToString());
        }




    }
}
