using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SperingTask.DAL;
using SperingTask.Models;
using SperingTask.ViewModels;

namespace SperingTask.Areas.Admin.Controllers
{
	[Area("admin")]
	public class HomeController : Controller
	{
		private readonly SperingContext _context;
		public HomeController(SperingContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index()
		{
			List<Category> categories = await _context.Categories.ToListAsync();
			return View(categories);
		}
		public async Task<IActionResult> Create()
		{
			//CreateCategoryVM CategoryVM = new CreateCategoryVM
			//{
			//	Categories = await _context.Categories.ToListAsync(),
			//};
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreateCategoryVM vm)
		{
			if (!ModelState.IsValid)
			{

				return View(vm);
			}
			await _context.Categories.AddAsync(new Category
			{
				Name = vm.Name,
				Icons = vm.Icons,
			});
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Update(int? id)
		{
			if (id == null || id < 1) return BadRequest();
			Category existed = await _context.Categories.FirstOrDefaultAsync(s => s.Id == id);
			if (existed is null) return NotFound();
			return View(existed);
		}
		[HttpPost]
		public async Task<IActionResult> Update(int? id, Category category)
		{
			if (id == null || id < 1) return BadRequest();
			Category existed = await _context.Categories.FirstOrDefaultAsync(s => s.Id == id);
			if (existed is null) return NotFound();
			existed.Name = category.Name;
			existed.Icons = category.Icons;
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || id < 1) return BadRequest();
			var existed = await _context.Categories.FindAsync(id);
			if (existed is null) return NotFound();
			_context.Remove(existed);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}









