using TaxManager.Models;

namespace TaxManager.Services
{
    public class AccountReports : IReportable
    {
        public decimal TotalOwed(IEnumerable<TaxAccount> accounts)
        => accounts.Sum(a => a.BalanceOwed);

        public IEnumerable<TaxAccount> Debtors(IEnumerable<TaxAccount> accounts)
        => accounts.Where(a => a.BalanceOwed > 0).OrderByDescending(a => a.BalanceOwed);
    }
}