using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vehicules.API.Data;
using Vehicules.API.Data.Entities;
using Vehicules.API.Helpers;
using Vehicules.API.Models;
using Vehicules.Common.Enums;

namespace Vehicules.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IBlobHelper _blobHelper;

        public UsersController(DataContext context, IUserHelper userHelper, ICombosHelper combosHelper, IConverterHelper converterHelper, IBlobHelper blobHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _blobHelper = blobHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users
                .Include(x => x.DocumentType)
                .Include(x => x.Vehicles)
                .Where(x => x.UserType == UserType.User)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            UserViewModel model = new UserViewModel
            {
                DocumentTypes = _combosHelper.GetCombosDocumentTypes()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }

                User user = await _converterHelper.ToUserAsync(model, imageId, true);
                user.UserType = UserType.User;
                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, user.UserType.ToString());
                return RedirectToAction(nameof(Index));
            }

            model.DocumentTypes = _combosHelper.GetCombosDocumentTypes();
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(Guid.Parse(id));
            if (user == null)
            {
                return NotFound();
            }

            UserViewModel model = _converterHelper.ToUserViewModel(user);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = model.ImageId;
                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }

                User user = await _converterHelper.ToUserAsync(model, imageId, false);
                await _userHelper.UpdateUserAsync(user);
                return RedirectToAction(nameof(Index));
            }

            model.DocumentTypes = _combosHelper.GetCombosDocumentTypes();
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(Guid.Parse(id));
            if (user == null)
            {
                return NotFound();
            }
            await _blobHelper.DeleteBlobAsync(user.ImageId, "users");
            await _userHelper.DeleteUserAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user = await _context.Users
                .Include(x => x.DocumentType)
                .Include(x => x.Vehicles)
                .ThenInclude(x => x.Brand)
                .Include(x => x.Vehicles)
                .ThenInclude(x => x.VehicleType)
                .Include(x => x.Vehicles)
                .ThenInclude(x => x.VehiclePhotos)
                .Include(x => x.Vehicles)
                .ThenInclude(x => x.Histories)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
