namespace CapetropolisTourism.Models;

public class AgentModel : BaseModel
{
    // public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Telephone { get; set; }
    public required List<string> MealServices { get; set; }
}
