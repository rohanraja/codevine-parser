﻿using System;
using StackExchange.Redis;

namespace CodeRecordHelpers
{
	public class RedisHelper
    {
		private ConnectionMultiplexer redis;

		public RedisHelper()
        {
        }

        public void AddToQueue(string key, string jsonVal)
		{

			var db = redis.GetDatabase();

			db.ListLeftPush(key, jsonVal);
		}

		public void Connect()
		{
			redis = ConnectionMultiplexer.Connect("localhost");
    	}

        public void Dispose()
		{
			redis.Dispose();
		}

		public bool IsConnected()
		{
			return redis.IsConnected;
		}
	}
}
