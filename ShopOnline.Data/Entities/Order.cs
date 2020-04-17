﻿using ShopOnline.Data.Enums;
using System;
using System.Collections.Generic;

namespace ShopOnline.Data.Entities
{
    public class Order
    {
        public int ID { set; get; }
        public DateTime OrderDate { set; get; }
        public Guid UserId { set; get; }
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipEmail { set; get; }
        public string ShipPhoneNumber { set; get; }
        public OrderStatus Status { set; get; }

        public List<OrderDetail> OrderDetails { get; set; }
        public AppUser AppUser { get; set; }
    }
}