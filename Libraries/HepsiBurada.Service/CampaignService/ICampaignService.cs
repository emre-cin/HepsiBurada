using HepsiBurada.Core.Models;
using HepsiBurada.Model.Model;

namespace HepsiBurada.Service.CampaignService
{
    public interface ICampaignService
    {
        #region Methods
        ReturnModel Add(CampaignModel campaign);
        ReturnModel<CampaignModel> GetCampaignByName(string campaignName);
        ReturnModel EndCampaignsByEndDate();
        #endregion
    }
}
