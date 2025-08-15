using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserGroupManagement.MVC.Models;
using UserGroupManagement.MVC.Services;

namespace UserGroupManagement.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApiService _apiService;

        public UsersController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _apiService.GetUsersAsync();
            return View(users);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                await _apiService.CreateUserAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _apiService.GetUserAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                await _apiService.UpdateUserAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _apiService.GetUserAsync(id);
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
