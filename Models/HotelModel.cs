using RoomModel = CapetropolisTourism.Models.RoomModel;

namespace CapetropolisTourism.Models;

public class HotelModel
{
    public string name { get; set; }
    public List<RoomModel> rooms { get; set; }
}

