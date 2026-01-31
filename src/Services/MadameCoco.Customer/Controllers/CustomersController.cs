using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using MadameCoco.Customer.Dtos;
using MadameCoco.Customer.Services;
using MadameCoco.Shared;
using Microsoft.AspNetCore.Mvc;

namespace MadameCoco.Customer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IValidator<CreateCustomerDto> _validator;

    public CustomersController(ICustomerService customerService, IValidator<CreateCustomerDto> validator)
    {
        _customerService = customerService;
        _validator = validator;
    }

    [HttpPost]
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _customerService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, result);
        }
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _customerService.GetAllAsync();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _customerService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, result);
        }
        return Ok(result);
    }

    [HttpGet("{id}/validate")]
    public async Task<IActionResult> Validate(Guid id)
    {
        var result = await _customerService.ValidateAsync(id);
        return Ok(result.Data); // Returning bool directly as per requirements or wrapped in Response? Requirement says "Validate(uuid): bool". 
                                // To strictly watch signature "bool", we return the data. But API usually returns JSON. 
                                // If strict diagram means return type of method, then Service returns bool. 
                                // API returning boolean JSON is standard.
    }

    [HttpPut("{id}")]
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
