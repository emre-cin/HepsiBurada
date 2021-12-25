using HepsiBurada.Core.Models;
using HepsiBurada.Model.Model;

namespace HepsiBurada.Service.OrderService
{
    public interface IOrderService
    {
        #region Methods
        ReturnModel Add(OrderModel order);
        #endregion
    }
}
