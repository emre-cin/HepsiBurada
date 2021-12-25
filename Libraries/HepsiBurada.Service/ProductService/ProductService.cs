using System;
using HepsiBurada.Core.UnitOfWork;
using HepsiBurada.Model.DbEntity;
using HepsiBurada.Core.Models;
using HepsiBurada.Model.Model;
using HepsiBurada.Core.Extensions;
using System.Linq;
using System.Collections.Generic;

namespace HepsiBurada.Service.ProductService
{
    public class ProductService : IProductService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _repository;
        #endregion

        #region Ctor
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository ??= _unitOfWork.GetRepository<Product>();
        }
        #endregion

        #region Methods
        public ReturnModel Add(ProductModel product)
        {
            ReturnModel returnModel = new ReturnModel();

            try
            {
                Product productEntity = product.MapTo<Product>();

                _repository.Insert(productEntity);
                _unitOfWork.SaveChanges();

                returnModel.IsSuccess = true;
                returnModel.Message = $"Product created; code {product.ProductCode}, price {product.UnitPrice} stock {product.UnitsInStock}";
            }
            catch
            {
                returnModel.IsSuccess = false;
            }

            return returnModel;
        }

        public ReturnModel<ProductModel> GetProductByCode(string productCode)
        {
            ReturnModel<ProductModel> returnModel = new ReturnModel<ProductModel>();

            try
            {
                Product product = _repository.Get(x => x.ProductCode == productCode);

                if (product == null)
                {
                    returnModel.Message = "Product not found!";
                    return returnModel;
                }

                returnModel.IsSuccess = true;
                returnModel.Data = product.MapTo<ProductModel>();
            }
            catch
            {
                returnModel.IsSuccess = false;
            }

            return returnModel;
        }
        #endregion
    }
}
