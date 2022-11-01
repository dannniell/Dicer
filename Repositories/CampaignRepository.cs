using Dicer.Models;
using Dicer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Dicer.Repositories
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly ApplicationDbContext _context;

        public CampaignRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void UpsertCampaign(CampaignModel model)
        {
            /*string sp = Constants.Constants.upsertCampaign + "@CampaignId = '{0}',@CampaignName = '{1}',@ContentType = '{2}'," +
                "@Description = '{3}',@Commission = '{4}',@Task = '{5}',@Gender = '{6}',@Provinsi = '{7}',@Kota = '{8}'" +
                ",@CampaignImg = '{9}',@MinFollowers = '{10}',@MinAge = '{11}',@MaxAge = {12}, @UserId = '{13}'";

            var sql = @"EXEC UPSERT_CAMPAIGN @CampaignId = {id},@CampaignName = '{campaignName}',@ContentType = '{contentType}',@Description = '{description}',@Commission = {commission},@Task = '{task}',@Gender = '{gender}',@Provinsi = {provinsi},@Kota = {kota},@CampaignImg = '{img}',@MinFollowers = {minFollowers},@MinAge = {minAge},@MaxAge = {maxAge}, @UserId = '6eb25288-243b-4143-abf9-9fe391da537d'";

var paramObject = new { index = 1, row = "abc", lookup = 100 };
            var result = _context.Database.ExecuteSqlRaw(sql, paramObject);*/

            /*var result = _context.Database.ExecuteSqlRaw(sp, id, campaignName, contentType, description, commission, task, gender, provinsi, kota,
                img, minFollowers, minAge, maxAge, userId);*/
            /*var result = _context.Database.ExecuteSqlInterpolated(
                $"EXEC UPSERT_CAMPAIGN @CampaignId = {id},@CampaignName = '{campaignName}',@ContentType = '{contentType}',@Description = '{description}',@Commission = {commission},@Task = '{task}',@Gender = '{gender}',@Provinsi = {provinsi},@Kota = {kota},@CampaignImg = '{img}',@MinFollowers = {minFollowers},@MinAge = {minAge},@MaxAge = {maxAge}, @UserId = '6eb25288-243b-4143-abf9-9fe391da537d'");*/
            var result = _context.Database.ExecuteSqlRaw("EXEC UPSERT_CAMPAIGN {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}", model.CampaignId,
                model.CampaignName, model.ContentType, model.Description, model.Commission, model.Task, model.Gender, model.Provinsi, model.Kota,
                model.CampaignImg, model.MinFollowers, model.MinAge, model.MaxAge, model.UserId, model.UserName, model.Genre);
        }
    }
}
