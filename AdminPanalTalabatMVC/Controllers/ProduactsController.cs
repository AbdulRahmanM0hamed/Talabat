using AdminPanalTalabatMVC.Helpers;
using AdminPanalTalabatMVC.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.Entities;

namespace AdminPanalTalabatMVC.Controllers
{
    public class ProduactsController : Controller
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProduactsController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			var product = await _unitOfWork.Repositort<Product>().GetAllAsync();
			var mappedProduct = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductViewModel>>(product);
			return View(mappedProduct);
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(ProductViewModel model)
		{
			//if (ModelState.IsValid)
			{
				if (model.Image != null)
				{
					model.PictureUrl = PictuerSettings.UploadImage(model.Image, "products");
				}
				else
					model.PictureUrl = "images/products/hat-react2.png";


				var mapedProduct = _mapper.Map<ProductViewModel, Product>(model);
				await _unitOfWork.Repositort<Product>().Add(mapedProduct);
				await _unitOfWork.Complete();
				return RedirectToAction(nameof(Index));
			}
			return View(model);


		}

		public async Task<IActionResult> Edit(int id)
		{
			var product = await _unitOfWork.Repositort<Product>().GetByIdAsync(id);
			var mappedProducts = _mapper.Map<Product, ProductViewModel>(product);
			return View(mappedProducts);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, IFormFile Image, ProductViewModel model)
		{
			if (id != model.Id)
			{
				return NotFound();
			}

			//if (ModelState.IsValid)
			{
				if (model.Image != null)
				{
					if (model.PictureUrl != null)
					{
						PictuerSettings.DeleteFile(model.PictureUrl, "products");
						model.PictureUrl = PictuerSettings.UploadImage(model.Image, "products");
					}
					else
					{
						model.PictureUrl = PictuerSettings.UploadImage(model.Image, "products");
					}
				}
				else
				{
					model.Image = Image;
				}
				var mappedProduct = _mapper.Map<ProductViewModel, Product>(model);
				_unitOfWork.Repositort<Product>().update(mappedProduct);
				var result = await _unitOfWork.Complete();
				if (result > 0)
					return RedirectToAction("Index");
			}
			return View(model);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var product = await _unitOfWork.Repositort<Product>().GetByIdAsync(id);
			var mappedProducts = _mapper.Map<Product, ProductViewModel>(product);
			return View(mappedProducts);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, ProductViewModel model)
		{
			if (id != model.Id)
			{
				return NotFound();
			}

			try
			{
				var prodct = await _unitOfWork.Repositort<Product>().GetByIdAsync(id);
				if (prodct.PictureUrl != null)
				{
					PictuerSettings.DeleteFile(prodct.PictureUrl, "products");
				}
				_unitOfWork.Repositort<Product>().delete(prodct);
				await _unitOfWork.Complete();
				return RedirectToAction("Index");

			}
			catch (System.Exception)
			{
				return View(model);
			}
		}




	}
}
