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
    public class DatabaseController : ControllerBase
    {
        private readonly DatabaseService _databaseService;
        public DatabaseController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpPut("{monthId}/SetDailyLoginUnlockTimestamp", Name = "SetDailyLoginUnlockTimestamp")]
        public ActionResult SetDailyLoginUnlockTimestamp(Month monthId)
        {
            var month = _databaseService.GetDailyRewardMonth(monthId);
            if (month == null) return BadRequest("No such month exists!");

            _databaseService.SetDailyLoginUnlockTimestamp(month);
            _databaseService.UpdateDailyLoginUnlockTimestamp(month.monthId, month.loginTasks);
            return Ok("Daily Rewards set the unlock timestamp successfully");
        }

        #region VIP
        [HttpPut("{monthId}/SetVIPPassUnlockTimestamp", Name = "SetVIPPassUnlockTimestamp")]
        public ActionResult SetVIPPassUnlockTimestamp(Month monthId)
        {
            var pass = _databaseService.GetVipPass(monthId);
            if (pass == null) return BadRequest("No such vip pass exists!");

            _databaseService.SetVipPassUnlockTimestamp(pass);
            _databaseService.UpdateVipPass(pass.monthId, pass);
            return Ok("VIP Passes set the unlock timestamp successfully");
        }

        [HttpPut("{monthId}/SetRequiredAmount", Name = "SetRequiredAmount")]
        public ActionResult SetRequiredAmount(Month monthId)
        {
            var pass = _databaseService.GetVipPass(monthId);
            if (pass == null) return BadRequest("No such vip pass exists!");

            _databaseService.SetRequiredAmount(pass);
            _databaseService.UpdateVipPass(pass.monthId, pass);
            return Ok("VIP Passes set the required amount successfully");
        }
        #endregion

        #region Shop
        [HttpGet("GetShopItem", Name = "GetShopItem")]
        public ActionResult<ShopItem> GetShopItem(string productId)
        {
            var shopItem = _databaseService.GetShopItemById(productId);
            if (shopItem == null) return NotFound();

            return Ok(shopItem);
        }

        [HttpPost("AddShopItem", Name = "AddShopItem")]
        public ActionResult AddShopItem([FromBody] ShopItem shopItem)
        {
            _databaseService.AddShopItem(shopItem);
            return CreatedAtAction(nameof(GetShopItem), shopItem.productId, shopItem);
        }
        #endregion

    }
}
