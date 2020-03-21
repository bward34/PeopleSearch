using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PeopleSearch.Models;

namespace PeopleSearch.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PeopleDbContext _context;

        public PeopleController(PeopleDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the list of people when navigation to the index.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            List<People> peoples = await _context.People.ToListAsync();
            ViewBag.People = peoples;
            return View();
        }

        /// <summary>
        /// Called when searchng the list of people.
        /// </summary>
        /// <param name="searchName">The name the user is searching for.</param>
        /// <param name="delay">Set to true if user wants slow search</param>
        /// <returns>The list of people based on the searchName param</returns>
        [HttpPost]
        public async Task<IActionResult> SearchList(string searchName, bool delay)
        {
            //If delay is true, simulates a slow search
            if(delay)
            {
                await Task.Delay(5000);
            }

            string[] names = !string.IsNullOrEmpty(searchName) ? searchName.Split() : new string[] { "" };
            string firstName = names[0].ToLower();
            string lastName = names.Count() > 1 ? names[1].ToLower() : "";

            var people = from p in _context.People select p;

            if (!string.IsNullOrEmpty(searchName))
            {
                people = people.Where(p => ((p.FirstName.ToLower().StartsWith(firstName)) && (p.LastName.ToLower().StartsWith(lastName)))
                                         || ((p.LastName.ToLower().StartsWith(firstName)) && (p.FirstName.ToLower().StartsWith(lastName))));
            }
            List<People> peoples = await people.ToListAsync();
            return PartialView("SearchPartial", peoples);
        }

        /// <summary>
        /// Gets details of a individual person.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _context.People
                .FirstOrDefaultAsync(m => m.Id == id);
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        /// <summary>
        /// Called when creating a new person.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Saves a new person and returned the view with a
        /// newly created person.
        /// </summary>
        /// <param name="people"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Age,Email,Address,Interest,PictureUrl")] People people)
        {
            if (ModelState.IsValid)
            {
                _context.Add(people);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(people);
        }

        /// <summary>
        /// Directs to the edit page.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _context.People.FindAsync(id);
            if (people == null)
            {
                return NotFound();
            }
            return View(people);
        }

        /// <summary>
        /// Saves the editied person.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="people"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Age,Email,Address,Interest,PictureUrl")] People people)
        {
            if (id != people.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(people);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeopleExists(people.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(people);
        }

        /// <summary>
        /// Deletes a person from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _context.People
                .FirstOrDefaultAsync(m => m.Id == id);
            if (people == null)
            {
                return NotFound();
            }

            people = await _context.People.FindAsync(id);
            _context.People.Remove(people);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeopleExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}
