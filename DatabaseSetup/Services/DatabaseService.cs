using DatabaseSetup.Context;
using DatabaseSetup.Helpers;
using DatabaseSetup.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseSetup.Services
{
    public class DatabaseService
    {
        private readonly IMongoDBContext _mongoContext;
        private readonly IMongoCollection<DailyRewardMonth> _monthCollection;
        private readonly IMongoCollection<VIPPass> _vipCollection;
        private readonly IMongoCollection<ShopItem> _shopCollection;

        public DatabaseService(IMongoDBContext context)
        {
            _mongoContext = context;
            _monthCollection = _mongoContext.GetCollectionFromGlobalDB<DailyRewardMonth>("DailyRewardPool");
            _vipCollection = _mongoContext.GetCollectionFromGlobalDB<VIPPass>("VIPPasses");
            _shopCollection = _mongoContext.GetCollectionFromGlobalDB<ShopItem>("ShopItem");
        }

        #region DailyLogin
        public DailyRewardMonth GetDailyRewardMonth(Month monthId)
        {
            return _monthCollection.Find(x => x.monthId.Equals(monthId)).FirstOrDefault();
        }

        public void UpdateDailyLoginUnlockTimestamp(Month monthId, List<LoginTask> loginTasks)
        {
            var filter = Builders<DailyRewardMonth>.Filter.Eq(x => x.monthId, monthId);
            var update = Builders<DailyRewardMonth>.Update.Set(x => x.loginTasks, loginTasks);

            _monthCollection.FindOneAndUpdate(filter, update);
        }

        public void SetDailyLoginUnlockTimestamp(DailyRewardMonth month)
        {
            var loginTasks = month.loginTasks;
            foreach (var item in loginTasks)
            {
                item.timestamp = SetUnlockDate(month.monthId, item.day);
            }
        }
        #endregion

        #region VIP Pass
        public void CreateVIPPass(VIPPass pass)
        {
            _vipCollection.InsertOne(pass);
        }

        public VIPPass GetVipPass(Month monthId)
        {
            return _vipCollection.Find(x => x.monthId.Equals(monthId)).FirstOrDefault();
        }

        public void UpdateVipPass(Month monthId, VIPPass pass)
        {
            var filter = Builders<VIPPass>.Filter.Eq(x => x.monthId, monthId);

            _vipCollection.FindOneAndReplace(filter, pass);
        }

        public void SetVipPassUnlockTimestamp(VIPPass pass)
        {
            var loginTasks = pass.loginTasks;
            foreach (var item in loginTasks)
            {
                item.timestamp = SetUnlockDate(pass.monthId, item.day);
            }
        }

        public void SetRequiredAmount(VIPPass pass)
        {
            int multiplier = 2;

            var loginTasks = pass.loginTasks;
            for (int i = 0; i < loginTasks.Count; i++)
            {
                loginTasks[i].requiredAmount = multiplier * i * 1000;
            }
        }
        #endregion

        #region ShopItem
        public void AddShopItem(ShopItem shopItem)
        {
            _shopCollection.InsertOne(shopItem);
        }

        public ShopItem GetShopItemById(string productId)
        {
            var filter = Builders<ShopItem>.Filter.Eq(x => x.productId, productId);
            
            return _shopCollection.Find(filter).Single();
        }
        #endregion

        private uint SetUnlockDate(Month monthId, int day)
        {
            //we are storing the utc timestamp in database
            DateTime unlockDate = new DateTime(DateTime.Today.Date.Year, (int)monthId, day, 0, 0, 0, DateTimeKind.Local);
            return TimeHelper.DateTimeToTimestamp(unlockDate);
        }
    }
}
