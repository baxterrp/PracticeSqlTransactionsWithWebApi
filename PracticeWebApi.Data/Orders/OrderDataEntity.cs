﻿namespace PracticeWebApi.Data.Orders
{
    public class OrderDataEntity
    {
        public string Id { get; set; }

        // started, completed, canceled
        public string Status { get; set; }
        public string UserId { get; set; }
    }
}
