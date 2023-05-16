﻿namespace MarketPlace.DAL.Entities;

public class CartItem : Entity
{
    public virtual Product? Product { get; set; }
    public int ProductId { get; set; }
    public int CartId { get; set; }
}
