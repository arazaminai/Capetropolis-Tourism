using RoomModel = CapetropolisTourism.Models.RoomModel;

namespace CapetropolisTourism.Models;

public class HotelModel : BaseModel
{
    // public required int Id { get; set; }
    public required string name { get; set; }
    public required List<RoomModel> rooms { get; set; }
    public required List<string> meals { get; set; }
}

