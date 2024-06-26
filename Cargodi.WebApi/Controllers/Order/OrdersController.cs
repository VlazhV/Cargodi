using System.Security.Claims;
using Cargodi.Business.DTOs.Order.Order;
using Cargodi.Business.DTOs.Order.Payload;
using Cargodi.Business.DTOs.Order.Review;
using Cargodi.Business.Interfaces.Order;
using Cargodi.DataAccess.Constants;
using Cargodi.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cargodi.WebApi.Controllers.Order;

[Route("api/orders")]
[ApiController]
[Authorize]
public class OrdersController: ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IReviewService _reviewService;
    
    public OrdersController(IOrderService orderService, IReviewService reviewService)
    {
        _orderService = orderService;
        _reviewService = reviewService;
    }
    
    [HttpGet] 
    public async Task<ActionResult<IEnumerable<GetOrderInfoDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        if (!User.IsInRole(Roles.Admin) && !User.IsInRole(Roles.Manager))
            return NotFound();
            
        return Ok(await _orderService.GetAllAsync(cancellationToken));
    }
    
    [HttpGet("my")]
    public async Task<ActionResult<IEnumerable<GetOrderInfoDto>>> GetAllOfClientAsync(CancellationToken cancellationToken)
    {
        if (!User.IsInRole(Roles.Client))
            return NotFound();
        
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var userId = long.Parse(userIdString);
        return Ok(await _orderService.GetAllOfClientAsync(userId, cancellationToken));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GetOrderInfoDto>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return Ok(await _orderService.GetByIdAsync(id, User, cancellationToken));
    }
    
    [HttpPost]
    public async Task<ActionResult<GetOrderInfoDto>> CreateAsync([FromQuery] long? customerId, [FromBody] UpdateOrderPayloadsDto orderDto, CancellationToken cancellationToken)
    {		
        return Ok(await _orderService.CreateAsync(customerId, User, orderDto, cancellationToken));
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<GetOrderDto>> UpdateAsync(long id, [FromBody] UpdateOrderDto orderDto, CancellationToken cancellationToken)
    {
        return Ok(await _orderService.UpdateAsync(id, User, orderDto, cancellationToken));
    }
    
    [HttpPatch("{id}")]
    public async Task<ActionResult<GetOrderInfoDto>> UpdatePayloadListAsync
        (long id, [FromBody] IEnumerable<UpdatePayloadDto> payloadDtos, CancellationToken cancellationToken)
    {
        return Ok(await _orderService.UpdatePayloadListAsync(id, User, payloadDtos, cancellationToken));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        await _orderService.DeleteAsync(id, User, cancellationToken);

        return NoContent();
    }
    
    [HttpPut("{id}/status")]
    public async Task<ActionResult<GetOrderDto>> SetStatusAsync(long id, [FromQuery] string status, CancellationToken cancellationToken)
    {
        return Ok(await _orderService.SetStatusAsync(id, status, User, cancellationToken));
    }
    
    [HttpGet("{orderId:long}/reviews")]
    public async Task<ActionResult<List<ReviewDto>>> GetOrderReviewsAsync(long orderId, CancellationToken cancellationToken)
    {
        return Ok(await _reviewService.GetOrderReviewsAsync(orderId, cancellationToken));
    }
    
    [HttpPost("{orderId:long}/reviews")]
    public async Task<ActionResult<ReviewDto>> CreateOrderReview(long orderId, ReviewDto reviewDto, CancellationToken cancellationToken)
    {
        return Ok(await _reviewService.CreateOrderReviewAsync(reviewDto, orderId, cancellationToken));
    }
}