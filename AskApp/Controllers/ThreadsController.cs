using System;
using AskApp.BLL;
using AskApp.Cross_Cutting.TransferObjects;
using AskApp.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AskApp.UI.Controllers
{
    public class ThreadsController : Controller
    {
        private UserUseCases userRole;

        public ThreadsController()
        {
            userRole = userRole = new UserUseCases();
        }

        // GET: Threads
        public ActionResult Index()
        {
            try
            {
                ThreadsVM threads = new ThreadsVM()
                {
                    Threads = userRole.GetQuestionsByDate()
                };
                return View(threads);
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Threads/Details/5
        public ActionResult Details(int id)
        {
            var thread = userRole.GetByID(id);

            return View(thread);
        }

        // GET: ThreadsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ThreadsController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(string title, string body)
        {
            ThreadTO thread = new ThreadTO()
            {
                Question = new MessageTO()
                {
                    Title = title,
                    Body = body
                }
            };
            thread = userRole.CreateThread(thread);
            return RedirectToAction("Details", new { id = thread.Id });
        }

        // GET: ThreadsController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // PUT: ThreadsController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string body)
        {
            var thread = userRole.GetByID(id);
            var reply = new MessageTO()
            {
                Body = body
            };
            userRole.Reply(thread, reply);
            return RedirectToAction("Details", new { id = thread.Id });
        }

        // GET: ThreadsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ThreadsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}