using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using GamerPalsBackend.DataObjects.Models;
using MongoDB.Driver;

namespace GamerPalsBackend.DataObjects.Models
{
    public class MongoContext
    {
        private MongoClient client;
        private IMongoDatabase db;
        public IMongoCollection<User> Users => db.GetCollection<User>("User");
        public IMongoCollection<ActiveSearch> ActiveSearchs => db.GetCollection<ActiveSearch>("ActiveSearch");
        public IMongoCollection<Game> Games => db.GetCollection<Game>("Game");
        public IMongoCollection<Language> Languages => db.GetCollection<Language>("Language");
        public IMongoCollection<Role> Roles => db.GetCollection<Role>("Role");
        public IMongoCollection<SearchParameter> SearchParameters =>
            db.GetCollection<SearchParameter>("SearchParameters");
        public IMongoCollection<SystemSetting> SystemSettings => db.GetCollection<SystemSetting>("SystemSetting");

        public IMongoCollection<PrivateChat> PrivateChats => db.GetCollection<PrivateChat>("PrivateChat");

        public IMongoCollection<MongoImage> MongoImages => db.GetCollection<MongoImage>("MongoImage");

        public IMongoCollection<ConnectedService> ConnectedServices =>
            db.GetCollection<ConnectedService>("ConnectedService");

        public MongoContext()
        {
            client =
                new MongoClient(
                    "mongodb+srv://admin:BBQ4j4Nxcnov0fVF@gamerpalsmongo-ofvgs.mongodb.net/test?retryWrites=true");
            db = client.GetDatabase("GamerPals");
            
        }
    }
}
