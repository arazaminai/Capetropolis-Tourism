namespace CapetropolisTourism.Models;

public class MealModel : BaseModel
{ 
    public required string OrderId = Guid.NewGuid().ToString();
    public required string FirstName { get; set; }
    public required string Surname { get; set; }
    public required string MealType { get; set; }
    public required string MealDetails { get; set; }
    public required string MealAgent { get; set; }
    public required string HotelName { get; set; }
    public required DateTime OrderDate = DateTime.Now;
}