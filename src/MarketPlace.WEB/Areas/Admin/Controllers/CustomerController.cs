﻿using MarketPlace.WEB.Areas.Admin.Models;

namespace MarketPlace.WEB.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CustomerController : Controller
{
    private ICustomerService _customerService;
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response = await _customerService.GetAsync();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(new CustomerListViewModel()
            {
                Customers = response.Data!.OrderBy(c => c.Login)
            });
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }

    [HttpGet]
    public async Task<IActionResult> Save(int id)
    {
        // Create.
        if (id == 0)
        {
            return View(new DAL.Entities.Customer());
        }

        // Update.
        var response = await _customerService.GetByIdAsync(id);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(response.Data);
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save([Bind("Id,Login,Password,Profile")] DAL.Entities.Customer item)
    {
        if (ModelState.IsValid)
        {
            // Create.
            if (item.Id == 0)
            {
                var response = await _customerService.CreateAsync(item);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", response.Description);
            }
            // Update.
            else
            {
                var response = await _customerService.UpdateAsync(item);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", response.Description);
            }
        }
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _customerService.DeleteAsync(id);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return RedirectToAction("Index");
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }
}
