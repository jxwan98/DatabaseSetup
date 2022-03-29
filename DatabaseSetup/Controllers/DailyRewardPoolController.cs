using DatabaseSetup.Models;
using DatabaseSetup.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseSetup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyRewardPoolController : ControllerBase
    {
        private readonly DailyRewardPoolService _dailyRewardPoolService;
        public DailyRewardPoolController(DailyRewardPoolService dailyRewardPoolService)
        {
            _dailyRewardPoolService = dailyRewardPoolService;
        }

        [HttpPut("{monthId}/SetUnlockTimestamp", Name = "SetUnlockTimestamp")]
        public ActionResult SetUnlockTimestamp(DailyRewardMonth.Month monthId)
        {
            var month = _dailyRewardPoolService.GetDailyRewardMonth(monthId);
            if (month == null) return BadRequest("No such month exists!");

            _dailyRewardPoolService.SetUnlockTimestamp(month);
            _dailyRewardPoolService.UpdateUnlockTimestamp(month.monthId, month.loginTasks);
            return Ok("Set the unlock timestamp successfully");
        }
    }
}
