using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MadameCoco.Customer.Dtos;
using MadameCoco.Shared;

namespace MadameCoco.Customer.Services;

public interface ICustomerService
{
    Task<Response<Entities.Customer>> CreateAsync(CreateCustomerDto customerDto);
    Task<Response<Entities.Customer>> GetByIdAsync(Guid id);
    Task<Response<List<Entities.Customer>>> GetAllAsync();
    Task<Response<bool>> DeleteAsync(Guid id);
    // Update can be added if needed, sticking to minimal requirements for now or as requested
}
