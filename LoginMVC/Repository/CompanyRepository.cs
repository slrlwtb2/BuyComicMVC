using LoginMVC.Data;
using LoginMVC.Models;
using LoginMVC.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoginMVC.Repository
{
    public class CompanyRepository: ICompanyRepository
    {
        private readonly ApplicationDbContext _context;
        public CompanyRepository(ApplicationDbContext context)
        {
            _context= context;
        }

        public async Task<List<Company>> GetCompanys()
        {
            return await _context.Companies.ToListAsync();
        }
        public async Task<Company> GetCompany(int id)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);
            return company;
        }
        public void Update(Company company)
        {
            _context.Companies.Update(company);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task AddCompany(Company company)
        {
            await _context.Companies.AddAsync(company);
        }

        public void Delete(Company company)
        {
            _context.Companies.Remove(company);
        }
        
    }
}
