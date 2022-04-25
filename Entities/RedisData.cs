using System;

namespace Entities
{
    public class RedisData
    {
        public object Value { get; set; }

        DateTime? ExpirationDate { get; set; }

        public bool IsExpired
        {
            get
            {
                if (!ExpirationDate.HasValue)
                    return false;

                if (DateTime.Now < ExpirationDate.Value)
                    return false;

                return true;
            }
        }

        public void SetExpirationDate(int expiresAfter)
        {
            DateTime now = DateTime.Now.AddSeconds(expiresAfter);
            ExpirationDate = now;
        }

        public void RemoveExpirationDate()
        {
            ExpirationDate = null;
        }
    }
}
