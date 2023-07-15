using LoginMVC.Models;

namespace LoginMVC.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetCompanys();
        Task<Company> GetCompany(int id);
        void Update(Company company);
        void Save();
        Task AddCompany(Company company);
        void Delete(Company company);
    }
}
