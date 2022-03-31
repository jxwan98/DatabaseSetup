﻿using DatabaseSetup.Context;
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
    public class DailyRewardPoolService
    {
        private readonly IMongoDBContext _mongoContext;
        private readonly IMongoCollection<DailyRewardMonth> _monthCollection;

        public DailyRewardPoolService(IMongoDBContext context)
        {
            _mongoContext = context;
            _monthCollection = _mongoContext.GetCollectionFromGlobalDB<DailyRewardMonth>("DailyRewardPool");
        }

        public DailyRewardMonth GetDailyRewardMonth(Month monthId)
        {
            return _monthCollection.Find(x => x.monthId.Equals(monthId)).FirstOrDefault();
        }

        public void UpdateUnlockTimestamp(Month monthId, List<LoginTask> loginTasks)
        {
            var filter = Builders<DailyRewardMonth>.Filter.Eq("monthId", monthId);
            var update = Builders<DailyRewardMonth>.Update.Set("loginTasks", loginTasks);

            _monthCollection.FindOneAndUpdate(filter, update);
        }

        public void SetUnlockTimestamp(DailyRewardMonth month)
        {
            var loginTasks = month.loginTasks;
            foreach (var item in loginTasks)
            {
                item.timestamp = SetUnlockDate(month.monthId, item.day);
            }
        }

        private uint SetUnlockDate(Month monthId, int day)
        {
            //we are storing the utc timestamp in database
            DateTime unlockDate = new DateTime(DateTime.Today.Date.Year, (int)monthId, day, 0, 0, 0, DateTimeKind.Local);
            return TimeHelper.DateTimeToTimestamp(unlockDate);
        }
    }
}
