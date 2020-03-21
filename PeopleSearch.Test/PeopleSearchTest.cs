using System;
using Xunit;
using PeopleSearch.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PeopleSearch.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace PeopleSearch.Test
{
    public class PeopleSearchTest
    {
        /// <summary>
        /// Test to make sure view result is returned for Index of
        /// home controller.
        /// </summary>
        [Fact]
        public void HomeIndexIsView()
        {
            HomeController homeController = new HomeController(logger: null);
            var result = homeController.Index();
            Assert.IsType<ViewResult>(result);
        }

        /// <summary>
        /// Test to make sure view result is returned for Create
        /// of people controller.
        /// </summary>
        [Fact]
        public void PeopleCreateIsView()
        {
            PeopleController peopleController = new PeopleController(context: null);
            var result = peopleController.Create();
            Assert.IsType<ViewResult>(result);
        }

        /// <summary>
        /// Make sure NotFoundResult is returned when passing
        /// a not found Id value into the Edit action of the
        /// people controller.
        /// </summary>
        [Fact]
        public async void PeopleEditNotFound()
        {
            PeopleController peopleController = new PeopleController(context: null);
            var result = await peopleController.Edit(null);
            Assert.IsType<NotFoundResult>(result);
        }

        /// <summary>
        /// Make sure NotFoundResult is returned when passing
        /// an Id value that does not match the People object id
        /// value.
        /// </summary>
        [Fact]
        public async void PeopleEditNotFoundWithPeople()
        {
            PeopleController peopleController = new PeopleController(context: null);
            People people = new People { Id = 0 };
            var result = await peopleController.Edit(-99, people);
            Assert.IsType<NotFoundResult>(result);
        }

        /// <summary>
        /// Make sure NotFoundResult is returned when the Id value
        /// is not found.
        /// </summary>
        [Fact]
        public async void PeopleDeleteNotFound()
        {
            PeopleController peopleController = new PeopleController(context: null);
            var result = await peopleController.Delete(null);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
