using Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.SalesOrder
{
    public interface IService
    {
        List<CustomerViewModel> GetCustomerList();
        List<ProductViewModel> GetProductList();
    }
}
