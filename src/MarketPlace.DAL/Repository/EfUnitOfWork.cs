﻿namespace MarketPlace.DAL.Repository;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Shop> _shopRepository;
    private readonly IRepository<Seller> _sellerRepository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<Profile> _profileRepository;
    private readonly IRepository<Cart> _cartRepository;

    public EfUnitOfWork(AppDbContext db)
    {
        _db = db;
        _productRepository = new EfRepository<Product>(_db);
        _userRepository = new EfRepository<User>(_db);
        _profileRepository = new EfRepository<Profile>(_db);

        _cartRepository = new EfCartRepository(_db);
        _sellerRepository = new EfSellerRepository(_db);
        _shopRepository = new EfShopRepository(_db);
        _customerRepository = new EfCustomerRepository(_db);
    }

    public IRepository<Product> ProductRepository => _productRepository;

    public IRepository<User> UserRepository => _userRepository;

    public IRepository<Shop> ShopRepository => _shopRepository;
    public IRepository<Seller> SellerRepository => _sellerRepository;

    public IRepository<Customer> CustomerRepository => _customerRepository;
    public IRepository<Profile> ProfileRepository => _profileRepository;
    public IRepository<Cart> CartRepository => _cartRepository;

    public async Task CreateDatabaseAsync()
    {
        await _db.Database.EnsureCreatedAsync();
    }

    public async Task RemoveDatabaseAsync()
    {
       await _db.Database.EnsureDeletedAsync();
    }

    public async Task SaveAllAsync()
    {
        await _db.SaveChangesAsync();
    }
}
