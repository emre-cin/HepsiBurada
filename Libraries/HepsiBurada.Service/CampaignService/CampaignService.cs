using System;
using HepsiBurada.Core.UnitOfWork;
using HepsiBurada.Model.DbEntity;
using HepsiBurada.Core.Models;
using HepsiBurada.Model.Model;
using HepsiBurada.Core.Extensions;
using System.Linq;
using System.Collections.Generic;

namespace HepsiBurada.Service.CampaignService
{
    public class CampaignService : ICampaignService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Campaign> _repository;
        private readonly IRepository<Product> _productRepository;
        #endregion

        #region Ctor
        public CampaignService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository ??= _unitOfWork.GetRepository<Campaign>();
            _productRepository ??= _unitOfWork.GetRepository<Product>();
        }
        #endregion

        #region Methods
        public ReturnModel Add(CampaignModel campaign)
        {
            ReturnModel returnModel = new ReturnModel();

            try
            {
                var product = _productRepository.Get(x => x.ProductCode == campaign.ProductCode);

                if (product == null)
                {
                    returnModel.IsSuccess = false;
                    returnModel.Message = "Product is required!";

                    return returnModel;
                }

                campaign.ProductId = product.Id;
                campaign.Price = product.UnitPrice - (product.UnitPrice * campaign.DiscountPercent / 100);
                campaign.StartDate = DateTime.Now;
                campaign.EndDate = campaign.StartDate.AddHours(campaign.Duration);

                Campaign campaignEntity = campaign.MapTo<Campaign>();

                _repository.Insert(campaignEntity);
                _unitOfWork.SaveChanges();

                returnModel.IsSuccess = true;
                returnModel.Message = $"Campaign created; name {campaign.CampaignName}, product {campaign.ProductCode}, duration {campaign.Duration} limit {campaign.Limit}, target sales count {campaign.TargetSalesCount}";
            }
            catch
            {
                returnModel.IsSuccess = false;
            }

            return returnModel;
        }

        public ReturnModel EndCampaignsByEndDate()
        {
            ReturnModel returnModel = new ReturnModel();

            try
            {
                List<Campaign> campaigns = _repository.GetAll(x => x.EndDate >= DateTime.Now && x.IsActive).ToList();

                campaigns.ForEach(x => x.IsActive = false);

                _repository.Update(campaigns.ToArray());
                _unitOfWork.SaveChanges();

                returnModel.IsSuccess = true;
            }
            catch
            {
                returnModel.IsSuccess = false;
            }

            return returnModel;
        }

        public ReturnModel<CampaignModel> GetCampaignByName(string campaignName)
        {
            ReturnModel<CampaignModel> returnModel = new ReturnModel<CampaignModel>();

            try
            {
                Campaign campaign = _repository.Get(x => x.CampaignName == campaignName);

                if (campaign == null)
                {
                    returnModel.Message = "Campaign not found!";
                    return returnModel;
                }

                returnModel.IsSuccess = true;
                returnModel.Data = campaign.MapTo<CampaignModel>();
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
