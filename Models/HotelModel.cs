using RoomModel = CapetropolisTourism.Models.RoomModel;

namespace CapetropolisTourism.Models;

public class HotelModel
{
    public required string name { get; set; }
    public required List<RoomModel> rooms { get; set; }
    public required List<string> meals { get; set; }
}

