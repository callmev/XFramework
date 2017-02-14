using System;

namespace XFramework.DDD.Domain
{
    public class ObjectId
    {
        public static string GenerateNewStringId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
