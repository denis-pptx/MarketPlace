﻿namespace MarketPlace.DAL.Entities;

public class CartItem : Entity
{
    public int Quantity { get; set; }
    public virtual Product? Product { get; set; }
    public int ProductId { get; set; }

    public virtual Cart? Cart { get; set; }
    public int CartId { get; set; }
}
