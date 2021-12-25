using Microsoft.Extensions.DependencyInjection;
using HepsiBurada.Core.Models;
using HepsiBurada.Model.Model;
using System.Collections.Generic;
using System;
using HepsiBurada.Service.ProductService;
using HepsiBurada.Service.CampaignService;
using System.ComponentModel;
using System.Reflection;
using System.Collections;
using HepsiBurada.Service.OrderService;

namespace HepsiBurada.App
{
    public class Dispatcher
    {
        #region Delegate Dictionary
        public static Dictionary<string, Delegate> _CommandSet;

        public static Dictionary<string, Delegate> CommandSet
        {
            get
            {
                if (_CommandSet == null)
                {
                    _CommandSet = new Dictionary<string, Delegate>
                    {
                        { "create_product", new DelegateAddProduct(AddProduct) },
                        { "get_product_info", new DelegateGetProductInfo(GetProductInfo) },
                        { "create_campaign", new DelegateAddCampaign(AddCampaign) },
                        { "get_campaign_info", new DelegateGetCampaignInfo(GetCampaignInfo) },
                        { "create_order", new DelegateAddOrder(AddOrder) },
                    };
                }
                return _CommandSet;
            }
        }
        #endregion

        #region Product
        public delegate ReturnModel DelegateAddProduct(string productCode, decimal unitPrice, int unitInStock);
        public delegate ReturnModel<ProductModel> DelegateGetProductInfo(string productCode);
        public static ReturnModel AddProduct(string productCode, decimal unitPrice, int unitInStock)
        {
            ProductModel productModel = new ProductModel
            {
                ProductCode = productCode,
                UnitPrice = unitPrice,
                UnitsInStock = unitInStock,
            };

            var productService = Startup.ServiceProvider.GetService<IProductService>();

            ReturnModel returnModel = productService.Add(productModel);

            return returnModel;
        }
        public static ReturnModel<ProductModel> GetProductInfo(string productCode)
        {
            var productService = Startup.ServiceProvider.GetService<IProductService>();
            ReturnModel<ProductModel> returnModel = productService.GetProductByCode(productCode);

            if (returnModel.IsSuccess)
                returnModel.Message = $"Product {returnModel.Data.ProductCode} info; price {returnModel.Data.UnitPrice}, stock {returnModel.Data.UnitsInStock}";

            return returnModel;
        }
        #endregion

        #region Campaign 
        public delegate ReturnModel DelegateAddCampaign(string campaignName, string productCode, int duration, decimal limit, int targetSalesCount);
        public delegate ReturnModel<CampaignModel> DelegateGetCampaignInfo(string campaignName);

        public static ReturnModel AddCampaign(string campaignName, string productCode, int duration, decimal limit, int targetSalesCount)
        {
            CampaignModel campaignModel = new CampaignModel
            {
                ProductCode = productCode,
                Limit = limit,
                CampaignName = campaignName,
                Duration = duration,
                TargetSalesCount = targetSalesCount,
            };

            var campaignService = Startup.ServiceProvider.GetService<ICampaignService>();
            ReturnModel returnModel = campaignService.Add(campaignModel);

            return returnModel;
        }

        public static ReturnModel<CampaignModel> GetCampaignInfo(string campaignName)
        {
            var campaignService = Startup.ServiceProvider.GetService<ICampaignService>();
            ReturnModel<CampaignModel> returnModel = campaignService.GetCampaignByName(campaignName);

            if (returnModel.IsSuccess)
                returnModel.Message = $"Campaign {returnModel.Data.CampaignName} info; Status {(returnModel.Data.IsActive ? "Active" : "Ended")}, Target Sales 100,Total Sales 50, Turnover 5000, Average Item Price 100";

            return returnModel;
        }
        #endregion

        #region Order

        public delegate ReturnModel DelegateAddOrder(string productCode, int quantity);

        public static ReturnModel AddOrder(string productCode, int quantity)
        {
            var orderModel = new OrderModel
            {
                ProductCode = productCode,
                Quantity = quantity,
                IsActive = true,
            };

            var orderService = Startup.ServiceProvider.GetService<IOrderService>();
            ReturnModel returnModel = orderService.Add(orderModel);
            return returnModel;
        }

        #endregion

        #region Conversion Methods

        public static object ConvertSingleItem(string value, Type newType)
        {
            if (typeof(IConvertible).IsAssignableFrom(newType))
            {
                return Convert.ChangeType(value, newType);
            }
            else
            {
                return "";
            }
        }

        public static object ConvertStringToNewNonNullableType(string value, Type newType)
        {
            if (newType.IsArray)
            {
                Type singleItemType = newType.GetElementType();

                var elements = new ArrayList();
                foreach (var element in value.Split(','))
                {
                    var convertedSingleItem = ConvertSingleItem(element, singleItemType);
                    elements.Add(convertedSingleItem);
                }
                return elements.ToArray(singleItemType);
            }
            return ConvertSingleItem(value, newType);
        }

        public static object ConvertStringToNewType(string value, Type newType)
        {
            if (newType.IsGenericType && newType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }
                return ConvertStringToNewNonNullableType(value, new NullableConverter(newType).UnderlyingType);
            }
            return ConvertStringToNewNonNullableType(value, newType);
        }

        public static object CallMethod(object instance, MethodInfo methodInfo, List<object> parameters)
        {
            var methodParameters = methodInfo.GetParameters();
            var parametersWithValue = GetParameterWithValue(methodParameters, parameters);

            var parametersForInvocation = new List<object>();
            foreach (var methodParameter in methodParameters)
            {
                string value;
                if (parametersWithValue.TryGetValue(methodParameter.Name, out value))
                {
                    var convertedValue = ConvertStringToNewType(value, methodParameter.ParameterType);
                    parametersForInvocation.Add(convertedValue);
                }
                else
                {
                    var defaultValue = Activator.CreateInstance(methodParameter.ParameterType);
                    parametersForInvocation.Add(defaultValue);
                }
            }
            return methodInfo.Invoke(instance, parametersForInvocation.ToArray());
        }

        #endregion

        #region Common Methods

        public static Dictionary<string, string> GetParameterWithValue(ParameterInfo[] methodParameters, List<object> parameterValues)
        {
            var dic = new Dictionary<string, string>();

            for (int i = 0; i < methodParameters.Length; i++)
                dic.Add(methodParameters[i].Name, parameterValues[i].ToString());

            return dic;
        }

        #endregion
    }
}
