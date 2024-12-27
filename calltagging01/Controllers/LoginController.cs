using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;

namespace calltagging01.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string AgentNumber, string Password)
        {
            AgentNumber = AgentNumber.Trim();
            Password = Password?.Trim();

            string savingPin = "$2a$11$84hR65G9JFT86mPGnXlyk.XGfToUlk6SCoiIMomOqbOSRVEcJ4j0y";//encrypt using bcrypt.net-next

            if (AgentNumber.Length == 6)
            {
                if (BCrypt.Net.BCrypt.EnhancedVerify(Password, savingPin))
                {
                    HttpContext.Session.SetString("AgentNumber", AgentNumber);
                    return RedirectToAction("Create", "Calls", new { agentNumber = AgentNumber });

                }
                else
                {
                    TempData["error"] = "Invalid credentials. Please try again.";
                    return View();
                }
            }
            else
            {
                TempData["error"] = "Invalid Agent Number. Please try again.(Agent Number should have 6 digits.)";
                return View();
            }
        }
    }
}
