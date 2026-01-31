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
}
