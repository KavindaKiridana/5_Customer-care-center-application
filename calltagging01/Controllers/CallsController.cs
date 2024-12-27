using calltagging01.Data;
using calltagging01.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace calltagging01.Controllers
{
    public class CallsController : Controller
    {
        private readonly AppDbContext _context;

        public CallsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult PageRefresh(string agentNumber)
        {
            return RedirectToAction("Create", new { agentNumber });
        }

        public IActionResult Create(string agentNumber)
        {
            if (!string.IsNullOrEmpty(agentNumber))
            {
                TempData["userName"] = agentNumber; // Store the username for later use
            }
            return View();
        }

        // POST: Call/Create
        [HttpPost]
        public IActionResult Create(Call obj)
        {
            string userName = obj.AgentPhone;
            TempData["userName"] = userName;

            if (string.IsNullOrEmpty(userName))
            {
                TempData["error"] = "You must be logged in to access this page.";
                return RedirectToAction("Login", "Login");
            }
            else if(obj.Problems=="" || obj.ProblemsList.Count==0)
            {
                TempData["error"] = "Please select relevant complaint/inquiry.";
                return RedirectToAction("Create", new { agentNumber = userName });

            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Calls.Add(obj);
                        _context.SaveChanges();
                        TempData["success"] = "Call added successfully";
                        return RedirectToAction("Create", new { agentNumber = userName });
                    }
                    catch (Exception ex)
                    {
                        TempData["error"] = ex.Message;
                        return RedirectToAction("Create", new { agentNumber = userName });
                    }
                }
                else if (ModelState.Values.SelectMany(v => v.Errors).Any(e => e.ErrorMessage == "The Category field is required."))
                {
                    obj.Category = "Broadband"; // Assign default category
                    try
                    {
                        _context.Calls.Add(obj);
                        _context.SaveChanges();
                        TempData["success"] = "Call added successfully";
                        return RedirectToAction("Create", new { agentNumber = userName });
                    }
                    catch (Exception ex)
                    {
                        TempData["error"] = ex.Message;
                        return View(obj); // Return the view with the current object
                    }
                }
                else
                {
                    TempData["error"] = ModelState.Values.SelectMany(v => v.Errors).First().ErrorMessage;
                    return View(obj); // Return the view with the current object
                }
            }
        }
    }
}
