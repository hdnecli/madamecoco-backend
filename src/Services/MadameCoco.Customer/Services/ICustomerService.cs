using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MadameCoco.Customer.Dtos;
using MadameCoco.Shared;

namespace MadameCoco.Customer.Services;

/// <summary>
/// Defines the contract for customer-related operations.
/// </summary>
public interface ICustomerService
{
    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customerDto">The customer creation data.</param>
    /// <returns>A response containing the created customer.</returns>
    Task<Response<Entities.Customer>> CreateAsync(CreateCustomerDto customerDto);

    /// <summary>
    /// Retrieves a customer by their unique identifier.
    /// </summary>
    /// <param name="id">The customer ID.</param>
    /// <returns>A response containing the customer details.</returns>
    Task<Response<Entities.Customer>> GetByIdAsync(Guid id);

    /// <summary>
    /// Retrieves all customers.
    /// </summary>
    /// <returns>A response containing a list of customers.</returns>
    Task<Response<List<Entities.Customer>>> GetAllAsync();

    /// <summary>
    /// Deletes a customer by their unique identifier.
    /// </summary>
    /// <param name="id">The customer ID.</param>
    /// <returns>A response indicating success or failure.</returns>
    Task<Response<bool>> DeleteAsync(Guid id);

    /// <summary>
    /// Validates if a customer exists.
    /// </summary>
    /// <param name="id">The customer ID.</param>
    /// <returns>A response containing true if the customer exists, otherwise false.</returns>
    Task<Response<bool>> ValidateAsync(Guid id);

    /// <summary>
    /// Updates an existing customer's details.
    /// </summary>
    /// <param name="id">The customer ID.</param>
    /// <param name="customerDto">The new customer data.</param>
    /// <returns>A response containing the updated customer.</returns>
    Task<Response<Entities.Customer>> UpdateAsync(Guid id, CreateCustomerDto customerDto); // Reusing DTO for simplicity, ideally UpdateCustomerDto
}
