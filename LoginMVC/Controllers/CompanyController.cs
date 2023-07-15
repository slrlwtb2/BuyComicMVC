using LoginMVC.Models;
using LoginMVC.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoginMVC.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository= companyRepository;
        }
        public async Task<IActionResult> Index()
        {
            var companys = await _companyRepository.GetCompanys();
            return View(companys);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("Company/Create")]
        public async Task<IActionResult> Create(Company company)
        {
            await _companyRepository.AddCompany(company);
            _companyRepository.Save();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0 || id == null)
            {
                return RedirectToAction("Index");
            }
            var company = await _companyRepository.GetCompany(id);
            return View(company);
        }
        [HttpPost("Company/Edit")]
        public  IActionResult Edit(Company company)
        {
            _companyRepository.Update(company);
            TempData["update"] = "Update category successfully";
            _companyRepository.Save();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || id == null)
            {
                return RedirectToAction("Index");
            }
            var company = await _companyRepository.GetCompany(id);
            return View(company);
        }
        [HttpPost("Company/Delete")]
        public IActionResult Delete(Company company)
        {
            _companyRepository.Delete(company);
            _companyRepository.Save();
            TempData["delete"] = "Delete successfully";
            return RedirectToAction("Index");
        }
    }
}
