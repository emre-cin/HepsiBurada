using HepsiBurada.Core.Models;
using HepsiBurada.Model.Model;

namespace HepsiBurada.Service.ProductService
{
    public interface IProductService
    {
        #region Methods
        ReturnModel Add(ProductModel product);
        ReturnModel<ProductModel> GetProductByCode(string productCode);
        #endregion
    }
}
