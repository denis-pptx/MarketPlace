﻿namespace MarketPlace.WEB.ViewModels;

public class SellerListViewModel
{
    public IEnumerable<Seller> Sellers { get; set; } = null!;
    public SelectList Shops { get; set; } = null!;
    public int ShopId { get; set; }
}