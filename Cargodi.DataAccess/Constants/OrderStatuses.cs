using Cargodi.DataAccess.Entities.Order;

namespace Cargodi.DataAccess.Constants;

public static class OrderStatuses
{
    public static OrderStatus Processing { get => new OrderStatus() { Id = 1, Name = "processing" }; }
    public static OrderStatus Accepted { get => new OrderStatus() { Id = 2, Name = "accepted" }; }
    public static OrderStatus Completing { get => new OrderStatus() { Id = 3, Name = "completing" }; }
    public static OrderStatus Declined { get => new OrderStatus() { Id = 4, Name = "declined" }; }
    public static OrderStatus Completed { get => new OrderStatus() { Id = 5, Name = "completed" }; }      
}