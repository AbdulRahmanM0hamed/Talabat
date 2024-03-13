using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace AdminPanalTalabatMVC.Controllers
{
	public class BrandController : Controller
	{
		private readonly IUnitOfWork unitOfWork;

		public BrandController(IUnitOfWork unitOfWork)
        {
			this.unitOfWork = unitOfWork;
		}


        public async Task<IActionResult> Index()
		{
			var brands = await unitOfWork.Repositort<ProductBrand>().GetAllAsync();
			return View(brands);
		}

		[HttpPost]
        public async Task<IActionResult> Create( ProductBrand productBrand)
		{
			try
			{
				await unitOfWork.Repositort<ProductBrand>().Add(productBrand);
				await unitOfWork.Complete();
				return RedirectToAction("Index");
			}
			catch
			{
				ModelState.AddModelError("Name", "Please enter a new brad");
				return View("Index",await unitOfWork.Repositort<ProductBrand>().GetAllAsync());
			}
		}


        public async Task<IActionResult> Delete(int id)
		{
			var brand = await unitOfWork.Repositort<ProductBrand>().GetByIdAsync(id);
			unitOfWork.Repositort<ProductBrand>().delete(brand);
			await unitOfWork.Complete();
			return RedirectToAction("Index");
		}





    }
}
