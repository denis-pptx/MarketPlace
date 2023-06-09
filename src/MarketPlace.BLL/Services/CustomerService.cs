﻿namespace MarketPlace.BLL.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<IEnumerable<Customer>>> GetAsync()
    {
        try
        {
            var customers = await _unitOfWork.CustomerRepository.ListAllAsync();

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = customers
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<bool>> CreateAsync(Customer item)
    {
        try
        {
            var customer = await _unitOfWork.UserRepository.SingleOrDefaultAsync(
                c => c.Login.Trim().ToLower() == item.Login.Trim().ToLower());

            if (customer != null)
            {
                return new()
                {
                    Description = "Login is already used",
                    StatusCode = HttpStatusCode.Conflict
                };
            }

            item.Role = Role.Customer;
            item.Cart = new();
            await _unitOfWork.CustomerRepository.AddAsync(item);

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = true,
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        try
        {
            var customer = await _unitOfWork.CustomerRepository.SingleOrDefaultAsync(c => c.Id == id);
            if (customer == null)
            {
                return new()
                {
                    Description = "Customer not found",
                    StatusCode = HttpStatusCode.NotFound,
                    Data = false
                };
            }

            await _unitOfWork.CustomerRepository.DeleteAsync(customer);

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError,
                Data = false
            };
        }
    }

    public async Task<Response<Customer>> GetByIdAsync(int id)
    {
        try
        {
            var customer = await _unitOfWork.CustomerRepository.SingleOrDefaultAsync(c => c.Id == id);
            if (customer == null)
            {
                return new()
                {
                    Description = "Customer not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = customer
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<Customer>> GetByLoginAsync(string login)
    {
        try
        {
            var customer = await _unitOfWork.CustomerRepository.SingleOrDefaultAsync(c => c.Login == login);
            if (customer == null)
            {
                return new()
                {
                    Description = "Customer not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = customer
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }


    public async Task<Response<bool>> UpdateAsync(Customer item)
    {
        try
        {
            var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(u => u.Login == item.Login);
            if (user != null && user.Id != item.Id)
            {
                return new()
                {
                    Description = "Login is already used",
                    StatusCode = HttpStatusCode.Conflict
                };
            }

            var existingCustomer = await _unitOfWork.CustomerRepository.SingleOrDefaultAsync(c => c.Id == item.Id);
            if (existingCustomer == null)
            {
                return new()
                {
                    Description = "Customer not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

          
            existingCustomer.Login = item.Login;
            existingCustomer.Password = item.Password;
            existingCustomer.Profile!.Age = item.Profile!.Age;
            existingCustomer.Profile!.Email = item.Profile!.Email;
            existingCustomer.Profile!.Phone = item.Profile!.Phone;
            await _unitOfWork.CustomerRepository.UpdateAsync(existingCustomer);

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = true,
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    
}
