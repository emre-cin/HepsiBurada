using System;
using HepsiBurada.Core.UnitOfWork;
using HepsiBurada.Model.DbEntity;
using HepsiBurada.Core.Models;
using HepsiBurada.Model.Model;
using HepsiBurada.Core.Extensions;
using System.Linq;
using System.Collections.Generic;

namespace HepsiBurada.Service.OrderService
{
    public class OrderService : IOrderService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Order> _repository;
        private readonly IRepository<Product> _productRepository;
        #endregion

        #region Ctor
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository ??= _unitOfWork.GetRepository<Order>();
            _productRepository ??= _unitOfWork.GetRepository<Product>();
        }
        #endregion

        #region Methods

        public ReturnModel Add(OrderModel order)
        {
            ReturnModel returnModel = new ReturnModel();

            try
            {
                var product = _productRepository.Get(x => x.ProductCode == order.ProductCode);

                if (product == null)
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Product is required!";

                    return returnModel;
                }

                var campaign = _repository.Get(x => x.ProductId == product.Id && x.IsActive);

                if (campaign == null)
                {
                    order.UnitPrice = product.UnitPrice;
                    order.TotalPrice = product.UnitPrice * order.Quantity;
                }
                else // Kampanya varsa siparişe kampanya fiyatını uygula
                {
                    order.UnitPrice = campaign.UnitPrice;
                    order.TotalPrice = campaign.UnitPrice * order.Quantity;
                    order.Discount = campaign.Discount;
                    order.CampaignId = campaign.CampaignId;
                }

                order.ProductId = product.Id;

                product.UnitsInStock -= order.Quantity;

                Order orderEntity = order.MapTo<Order>();

                _productRepository.Update(product);
                _repository.Insert(orderEntity);

                _unitOfWork.SaveChanges();

                returnModel.IsSuccess = true;
                returnModel.Message = $"Order created; product {product.ProductCode}, quantity {order.Quantity}";
            }
            catch (Exception ex)
            {
                returnModel.IsSuccess = false;
            }

            return returnModel;
        }
        #endregion
    }
}
