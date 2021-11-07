using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Infrastructure.Constants
{
    public static class MongoDbConstansts
    {
        // Common
        public const string DbName = "TestDatabase";

        public const string ConnectionString = "mongodb://localhost:27017";

        // Orders
        public const string ClientsCollectionName = "Clients";
        
        public const string OrdersCollectionName = "Orders";

        public const string OrderInfoCollectionName = "OrdersInfo";

        // Workers and users
        public const string UserRolePairsCollectionName = "UserRolePairs";

        public const string WorkersCollectionName = "Workers";

        public const string RolesCollectionName = "Roles";

        // Components and services
        public const string ComponentServicePairsCollectionName = "ComponentServicePairs";

        public const string ComponentsCollectionName = "Components";

        public const string ServicesCollectionName = "Services";
    }
}
