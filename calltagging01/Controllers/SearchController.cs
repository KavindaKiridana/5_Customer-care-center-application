using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using calltagging01.Data;
using calltagging01.Models;
using ClosedXML.Excel;

namespace calltagging01.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _context;

        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Search
        public async Task<IActionResult> Index()
        {
            var top10Calls = _context.Calls.AsQueryable().Take(0).ToList(); 

            return View(top10Calls);
        }
        
        [HttpPost]
        public IActionResult Index(DateTime fromDate, DateTime toDate, string Category, string ConnectionType)
        {
            //tempdata for export function
            TempData["ConnectionType"] = ConnectionType;
            TempData["Category"] = Category;
            TempData["toDate"] = toDate.ToString();
            TempData["fromDate"] = fromDate.ToString();

            // Filter data based on user selections
            var filteredCalls = _context.Calls.AsQueryable(); // Get all calls

            if (fromDate != DateTime.MinValue && toDate != DateTime.MinValue)
            {
                filteredCalls = filteredCalls.Where(c => c.OccurredAt >= fromDate && c.OccurredAt <= toDate);
            }

            if (!string.IsNullOrEmpty(Category) && Category != "All")
            {
                filteredCalls = filteredCalls.Where(c => c.Category == Category);
            }

            if (!string.IsNullOrEmpty(ConnectionType) && ConnectionType != "All")
            {
                filteredCalls = filteredCalls.Where(c => c.ConnectionType == ConnectionType);
            }

            // Get the top 10 rows

            return View("Index", filteredCalls);
        }

        //exporting to excel
        public IActionResult ExportToExcel()
        {
            //getting values for fromDate,toDate,Category,ConnectionType
            string ConnectionType = TempData["ConnectionType"] as string;
            string Category = TempData["Category"] as string;
            DateTime fromDate = Convert.ToDateTime(TempData["fromDate"]);
            DateTime toDate = Convert.ToDateTime(TempData["toDate"]);

            var filteredCalls = _context.Calls.AsQueryable();

            if (fromDate != DateTime.MinValue && toDate != DateTime.MinValue)
            {
                filteredCalls = filteredCalls.Where(c => c.OccurredAt >= fromDate && c.OccurredAt <= toDate);
            }

            if (!string.IsNullOrEmpty(Category) && Category != "ALL")
            {
                filteredCalls = filteredCalls.Where(c => c.Category == Category);
            }

            if (!string.IsNullOrEmpty(ConnectionType) && ConnectionType != "ALL")
            {
                filteredCalls = filteredCalls.Where(c => c.ConnectionType == ConnectionType);
            }

            var callsList = filteredCalls.ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Calls");
                var currentRow = 1;

                // Adding Headers
                worksheet.Cell(currentRow, 1).Value = "No";
                worksheet.Cell(currentRow, 2).Value = "AgentId";
                worksheet.Cell(currentRow, 3).Value = "ConnectionType";
                worksheet.Cell(currentRow, 4).Value = "Category";
                worksheet.Cell(currentRow, 5).Value = "Problems";
                worksheet.Cell(currentRow, 6).Value = "IssueType";
                worksheet.Cell(currentRow, 7).Value = "OccurredAt";

                // Adding Data
                foreach (var call in callsList)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = currentRow - 1;
                    worksheet.Cell(currentRow, 2).Value = call.AgentPhone;
                    worksheet.Cell(currentRow, 3).Value = call.ConnectionType;
                    worksheet.Cell(currentRow, 4).Value = call.Category;
                    worksheet.Cell(currentRow, 5).Value = call.Problems;
                    worksheet.Cell(currentRow, 6).Value = call.IssueType;
                    worksheet.Cell(currentRow, 7).Value = call.OccurredAt.ToString("yyyy-MM-dd HH:mm:ss");
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Calls.xlsx");
                }
            }
        }

        // GET: Search/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await _context.Calls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        // POST: Search/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var call = await _context.Calls.FindAsync(id);
            if (call != null)
            {
                _context.Calls.Remove(call);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
