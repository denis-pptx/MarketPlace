﻿namespace MarketPlace.BLL.Interfaces;

public interface IShopService
{
    Task<Response<IEnumerable<Shop>>> GetAsync();

    Task<Response<IEnumerable<Shop>>> GetBySimilarNameAsync(string? name);

    Task<Response<Shop>> GetBySellerLoginAsync(string sellerName);

    Task<Response<bool>> CreateAsync(Shop shop);

    Task<Response<bool>> DeleteAsync(int id);

    Task<Response<Shop>> GetByIdAsync(int id);

    Task<Response<bool>> UpdateAsync(Shop shop);

    Task<Response<SelectList>> GetCategoriesByIdAsync(int id);
}
