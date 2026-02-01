using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using MadameCoco.Customer.Dtos;
using MadameCoco.Customer.Services;
using MadameCoco.Shared;
using Microsoft.AspNetCore.Mvc;

namespace MadameCoco.Customer.Controllers;

/// <summary>
/// Service endpoint for managing customer data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IValidator<CreateCustomerDto> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomersController"/> class.
    /// </summary>
    /// <param name="customerService">The customer service.</param>
    /// <param name="validator">The customer validator.</param>
    public CustomersController(ICustomerService customerService, IValidator<CreateCustomerDto> validator)
    {
        _customerService = customerService;
        _validator = validator;
    }

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="createCustomerDto">The customer creation payload.</param>
    /// <returns>The created customer or validation errors.</returns>
    /// <response code="201">Returns the newly created customer.</response>
    /// <response code="400">If the input is invalid or validation fails.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Response<Entities.Customer>), 201)]
    [ProducesResponseType(typeof(Response<string>), 400)]
    public async Task<IActionResult> Create(CreateCustomerDto createCustomerDto)
    {
        var validationResult = await _validator.ValidateAsync(createCustomerDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(Response<string>.Fail(validationResult.ToString(), 400));
        }

        var result = await _customerService.CreateAsync(createCustomerDto);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Retrieves a customer by ID.
    /// </summary>
    /// <param name="id">The customer ID.</param>
    /// <returns>The customer details.</returns>
    /// <response code="200">Returns the customer data.</response>
    /// <response code="404">If the customer is not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Response<Entities.Customer>), 200)]
    [ProducesResponseType(typeof(Response<Entities.Customer>), 404)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _customerService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, result);
        }
        return Ok(result);
    }

    /// <summary>
    /// Retrieves all customers.
    /// </summary>
    /// <returns>A list of all customers.</returns>
    /// <response code="200">Returns the list of customers.</response>
    [HttpGet]
    [ProducesResponseType(typeof(Response<List<Entities.Customer>>), 200)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _customerService.GetAllAsync();
        return Ok(result);
    }

    /// <summary>
    /// Deletes a customer by ID.
    /// </summary>
    /// <param name="id">The customer ID.</param>
    /// <returns>Confirmation of deletion.</returns>
    /// <response code="204">If the customer was successfully deleted.</response>
    /// <response code="404">If the customer is not found.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Response<bool>), 204)]
    [ProducesResponseType(typeof(Response<bool>), 404)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _customerService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, result);
        }
        return Ok(result);
    }

    /// <summary>
    /// Validates if a customer exists without retrieving full details.
    /// </summary>
    /// <param name="id">The customer ID.</param>
    /// <returns>True if the customer exists, otherwise false.</returns>
    /// <response code="200">Returns the existence status.</response>
    [HttpGet("{id}/validate")]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> Validate(Guid id)
    {
        var result = await _customerService.ValidateAsync(id);
        return Ok(result.Data); // Returning bool directly as per requirements or wrapped in Response? Requirement says "Validate(uuid): bool". 
                                // To strictly watch signature "bool", we return the data. But API usually returns JSON. 
                                // If strict diagram means return type of method, then Service returns bool. 
                                // API returning boolean JSON is standard.
    }

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="id">The customer ID.</param>
    /// <param name="createCustomerDto">The updated customer data.</param>
    /// <returns>The updated customer details.</returns>
    /// <response code="200">Returns the updated customer.</response>
    /// <response code="400">If the input is invalid.</response>
    /// <response code="404">If the customer is not found.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Response<Entities.Customer>), 200)]
    [ProducesResponseType(typeof(Response<string>), 400)]
    [ProducesResponseType(typeof(Response<Entities.Customer>), 404)]
    public async Task<IActionResult> Update(Guid id, CreateCustomerDto createCustomerDto)
    {
        var validationResult = await _validator.ValidateAsync(createCustomerDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(Response<string>.Fail(validationResult.ToString(), 400));
        }

        var result = await _customerService.UpdateAsync(id, createCustomerDto);
        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, result);
        }
        return Ok(result);
    }
}
