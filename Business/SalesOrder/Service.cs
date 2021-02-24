
using DataAccess;
using Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Repository;

namespace Business.SalesOrder
{
    public class Service : IService
    {
        Repository dao = new Repository();

        public OrderListViewModel GetOrderId()
        {
            OrderListViewModel model = new OrderListViewModel();
            var sql = "EXEC Proc_List @Flag='GetOrderId'";
            var dt = dao.ExecuteDataRow(sql);
            if (dt != null)
            {
                model.OrderId = Convert.ToInt32(dt["OrderId"]);
            }
            return model;
        }

        public DbResponse Manage(OrderListViewModel model)
        {
            var sql = "EXEC Proc_List @Flag='Manage'";
            sql+= ",@OrderId="+model.OrderId;
            sql+= ",@Customer="+dao.FilterString(model.Customer);
            sql+= ",@Date="+dao.FilterString(model.Date);
            sql+= ",@JsonProduct=" + dao.FilterString(model.JsonProduct);
            return dao.ParseDbResponse(sql);
        }

        public List<CustomerViewModel> GetCustomerList()
        {
            List<CustomerViewModel> model = new List<CustomerViewModel>();

            var sql = "EXEC Proc_List @Flag='GetCustomerList'";
            var dt = dao.ExecuteDataTable(sql);
            try
            {
                if (dt != null)
                {
                    foreach(DataRow item in dt.Rows)
                    {
                        var common = new CustomerViewModel()
                        {
                            RowId=Convert.ToInt32(item["Code"]),
                            CustomerName=item["CustomerName"].ToString()
                        };
                        model.Add(common);
                    }
                    return model;
                }
                return model;
            }
            catch (Exception)
            {

                return model;
            }
        }

        public List<ProductViewModel> SearchProduct(string param)
        {
            List<ProductViewModel> model = new List<ProductViewModel>();

            var sql = "EXEC Proc_List @Flag='SearchProduct'";
            sql += ", @param1 = " + dao.FilterString(param);
            var dt = dao.ExecuteDataTable(sql);
            try
            {
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        var common = new ProductViewModel()
                        {
                            RowId = Convert.ToInt32(item["Code"]),
                            ProductName = item["ProductName"].ToString()
                        };
                        model.Add(common);
                    }
                    return model;
                }
                return model;
            }
            catch (Exception)
            {

                return model;
            }
        }

        public List<CustomerViewModel> GetCustomerName(string param)
        {
            List<CustomerViewModel> model = new List<CustomerViewModel>();
            var sql = "exec Proc_List @flag='GetCustomerName'";
            sql += ", @param1 = " + dao.FilterString(param);
            var dt = dao.ExecuteDataTable(sql);
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    var common = new CustomerViewModel()
                    {
                        RowId = Convert.ToInt32(item["Code"]),
                        CustomerName = item["CustomerName"].ToString()
                    };
                    model.Add(common);
                }
                return model;
            }
            return model;
        }

        public List<ProductViewModel> GetProductNameList(string Prefix)
        {
            List<ProductViewModel> model = new List<ProductViewModel>();
            var sql = "exec Proc_List @flag='GetProductName'";
            sql += ", @param1 = " + dao.FilterString(Prefix);
            var dt = dao.ExecuteDataTable(sql);
            if (dt != null)
            {
                foreach(DataRow item in dt.Rows)
                {
                    var common = new ProductViewModel()
                    {
                        ProductName = item["ProductName"].ToString()
                    };
                    model.Add(common);
                }
                return model;
            }
            return model;
        }

        public List<ProductViewModel> GetProductList()
        {
            List<ProductViewModel> model = new List<ProductViewModel>();

            var sql = "EXEC Proc_List @Flag='GetProductList'";
            var dt = dao.ExecuteDataTable(sql);
            try
            {
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        var common = new ProductViewModel()
                        {
                            RowId = Convert.ToInt32(item["Code"]),
                            ProductName = item["ProductName"].ToString()
                        };
                        model.Add(common);
                    }
                    return model;
                }
                return model;
            }
            catch (Exception)
            {

                return model;
            }
        }
    }
}
