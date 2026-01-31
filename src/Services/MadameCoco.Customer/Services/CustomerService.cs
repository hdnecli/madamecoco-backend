using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MadameCoco.Customer.Data;
using MadameCoco.Customer.Dtos;
using MadameCoco.Shared;
using Microsoft.EntityFrameworkCore;

namespace MadameCoco.Customer.Services;

public class CustomerService : ICustomerService
{
    private readonly CustomerDbContext _context;

    public CustomerService(CustomerDbContext context)
    {
        _context = context;
    }

    public async Task<Response<Entities.Customer>> CreateAsync(CreateCustomerDto customerDto)
    {
        var customer = new Entities.Customer
        {
            Name = customerDto.Name,
            Email = customerDto.Email,
            Address = customerDto.Address
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return Response<Entities.Customer>.Success(customer, 201);
    }

    public async Task<Response<Entities.Customer>> GetByIdAsync(Guid id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return Response<Entities.Customer>.Fail("Customer not found", 404);
        }
        return Response<Entities.Customer>.Success(customer, 200);
    }

    public async Task<Response<List<Entities.Customer>>> GetAllAsync()
    {
        var customers = await _context.Customers.ToListAsync();
        return Response<List<Entities.Customer>>.Success(customers, 200);
    }

    public async Task<Response<bool>> DeleteAsync(Guid id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return Response<bool>.Fail("Customer not found", 404);
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return Response<bool>.Success(true, 204);
    }

    public async Task<Response<bool>> ValidateAsync(Guid id)
    {
        var exists = await _context.Customers.AnyAsync(c => c.Id == id);
        return Response<bool>.Success(exists, 200);
    }

    public async Task<Response<Entities.Customer>> UpdateAsync(Guid id, CreateCustomerDto customerDto)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return Response<Entities.Customer>.Fail("Customer not found", 404);
        }

        customer.Name = customerDto.Name;
        customer.Email = customerDto.Email;
        customer.Address = customerDto.Address;
        customer.UpdatedAt = DateTime.UtcNow;

        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();

        return Response<Entities.Customer>.Success(customer, 200);
    }
}
