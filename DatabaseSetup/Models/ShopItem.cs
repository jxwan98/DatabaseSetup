using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace DatabaseSetup.Models
{
    public enum CurrencyType
    {
        FREE = 0,
        NANODUST = 1,
        CARGOCOINS = 2,
        CARGOCRYSTALS = 3,
        REALMONEY = 4,
        ADS = 5
    }

    public class ShopItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string productId { get; set; }
        public ItemType itemType { get; set; }
        public Dictionary<CurrencyType, Bundle> paymentMethod { get; set; } = new Dictionary<CurrencyType, Bundle>();
        public string description { get; set; }

        //To use enum as key value, insert following code (replace the enum that you want) inside ConfigureServices of Startup.cs
        //BsonSerializer.RegisterSerializer(new EnumSerializer<CurrencyType>(BsonType.String));
        public ShopItem() {}

        public class Bundle
        {
            public int price { get; set; }
            public int amount { get; set; }

            public Bundle(int price, int amount) 
            {
                this.price = price;
                this.amount = amount;
            }
        }
    }
}
