namespace CapetropolisTourism.Models;

public class BookingModel
{
    // short unique id 8 characters
    public required string Id { get; set; } = Guid.NewGuid().ToString().Substring(0, 8);    
    public required string FirstName { get; set; }
    public required string DateOfBirth { get; set; }
    public required string Surname { get; set; }
    public required string Address { get; set; }
    public required string Initials { get; set; }
    public required string Telephone { get; set; }
    public required string Email { get; set; }
    public required string Meals { get; set; }
    public required string IdType { get; set; }
    public required string RoomType { get; set; }
    public required string IdNumber { get; set; }
    public required int Price { get; set; }
    public required string Feature { get; set; }
    public required DateTime Date { get; set; } = DateTime.Now.Date;
    // date do not display time
    // public required string DateString { get; set; } = DateTime.Now.Date.ToString("dd/MM/yyyy")
}

