namespace CapetropolisTourism.Models;

public class RoomModel
{
    public required string roomType { get; set; }
    public required int price { get; set; }
    public required List<string> features { get; set; }
}
