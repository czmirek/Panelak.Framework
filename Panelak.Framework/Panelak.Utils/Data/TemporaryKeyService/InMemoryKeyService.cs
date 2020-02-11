namespace Panelak.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading.Tasks;


    /// <summary>
    /// Service used to store and retrieve confirmation keys such as email confirmation key
    /// and password reset confirmation key. The keys are stored in memory with set expiration.
    /// </summary>
    public class InMemoryKeyService : ITemporaryKeyService
    {
        private readonly object dictLock = new object();
        private readonly Dictionary<string, TemporaryKey> keyDict = new Dictionary<string, TemporaryKey>();
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly Task expirationCheckTask;

        /// <summary>
        /// Creates a new instance of <see cref="InMemoryKeyService"/>.
        /// </summary>
        /// <param name="dateTimeProvider">Date time provider</param>
        private InMemoryKeyService(IDateTimeProvider dateTimeProvider)
        {
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));

            //starts the dictionary expiration task
            expirationCheckTask = new Task(() =>
            {
                while (true)
                {
                    RemoveExpiredKeys();
                    Task.Delay(5000).Wait();
                }
            });
            expirationCheckTask.Start();
        }

        /// <summary>
        /// Thread safe search and removal of expired items in the dictionary
        /// </summary>
        private void RemoveExpiredKeys()
        {
            lock (dictLock)
            {
                var toExpire = keyDict.Where(tpk => tpk.Value.ExpirationTime < dateTimeProvider.Now)
                                      .Select(tpk => tpk.Key)
                                      .ToList();



                foreach (string key in toExpire)
                    keyDict.Remove(key);
            }
        }

        /// <summary>
        /// Singleton instance
        /// </summary>
        private static InMemoryKeyService instance = null;

        /// <summary>
        /// Singleton factory instance
        /// </summary>
        /// <param name="dateTimeProvider">Date time provider</param>
        /// <returns>Singleton temporary key service instance</returns>
        public static InMemoryKeyService GetInstance(IDateTimeProvider dateTimeProvider)
        {
            if (instance != null)
                return instance;

            instance = new InMemoryKeyService(dateTimeProvider);
            return instance;

        }

        public string CreateRandomKey()
        {
            byte[] randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(randomBytes);

            string base64Val = Convert.ToBase64String(randomBytes);

            //url safe value
            base64Val = base64Val.Replace("+", "").Replace("/", "").Replace("=", "").Replace("-", "");   

            return base64Val;
        }

        /// <summary>
        /// Creates a new key for given email, keytype and expiration and
        /// stores it in memory
        /// </summary>
        public void Store<T>(string key, T value, TimeSpan expireAfter)
        {
            lock (dictLock)
            {
                if (!keyDict.ContainsKey(key))
                    keyDict.Add(key, null);

                keyDict[key] = new TemporaryKey(key, value, dateTimeProvider.Now + expireAfter);
            }
        }

        /// <summary>
        /// Attempts to retreive the saved <paramref name="value"/> from memory.
        /// Returns false if not found.
        /// </summary>
        public bool TryRetreive<T>(string key, out T value)
        {
            value = default;

            lock (dictLock)
            {
                if (!keyDict.ContainsKey(key))
                    return false;

                if (!keyDict[key].Value.GetType().Equals(typeof(T)))
                    return false;

                value = (T)keyDict[key].Value;

                keyDict.Remove(key);
                return true;
            }
        }

        public bool TryPeek<T>(string key, out T value)
        {
            value = default;

            lock (dictLock)
            {
                if (!keyDict.ContainsKey(key))
                    return false;

                if (!keyDict[key].Value.GetType().Equals(typeof(T)))
                    return false;

                value = (T)keyDict[key].Value;
                return true;
            }
        }

        private class TemporaryKey
        {
            public TemporaryKey(string key, object value, DateTime expirationTime)
            {
                Key = key ?? throw new ArgumentNullException(nameof(key));
                Value = value ?? throw new ArgumentNullException(nameof(value));
                ExpirationTime = expirationTime;
            }

            public string Key { get; }
            public object Value { get; }
            public DateTime ExpirationTime { get; }
        }
    }
}
