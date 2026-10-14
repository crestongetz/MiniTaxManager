using System.Linq;

namespace TaxManager.Models
{
    public interface IReportable
    {
        //IEnumerable dtype is used to allow for any collection type to be passed in, not just List<TaxAccount>
        decimal TotalOwed(IEnumerable<TaxAccount> accounts) => accounts.Sum(a => a.BalanceOwed);
        IEnumerable<TaxAccount> Debtors(IEnumerable<TaxAccount> accounts); 
    }
}