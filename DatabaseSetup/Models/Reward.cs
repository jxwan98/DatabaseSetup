using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseSetup.Models
{
    public enum ItemType
    {
        cargoCoin = 0,
        cargoCrystal = 1,
        avatar = 2,
        skillPoint = 3,
        popup = 4,
        token = 5,
        premiumToken = 6,
        popperArm = 7,
        nanodust = 8,
        popupSkin = 9,
        popperSkin = 10,
        emote = 11,
        energyOrb = 12
    }

    public class Reward
    {
        public ItemType type { get; set; }
        public int value { get; set; }
        public string stringValue { get; set; }
        public int amount { get; set; }
    }
}
