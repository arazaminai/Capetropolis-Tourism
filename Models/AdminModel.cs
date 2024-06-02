using CapetropolisTourism.DataStore;

namespace CapetropolisTourism.Models;

public class AdminModel
{
    public int totalBookings { 
        get
        {
            return bookings.Count;
        }
        set
        {
            totalBookings = value;
        }
    }
    public int totalIncome { 
        get
        {
        // add all the prices of the bookings
            return bookings.Sum(booking => booking.Price);
        }
        set
        {
            totalIncome = value;
        }
    }
    public int totalGuests {
        get
        {
            return bookings.Sum(booking => booking.Guests);
        }
        set
        {
            totalGuests = value;
        }
    }
    private List<BookingModel> bookings;
    private List<HotelModel> hotels;

    public AdminModel()
    {
        this.bookings = new Data<BookingModel>("Bookings").GetData();
        this.hotels = new Data<HotelModel>("Hotels").GetData();
    }

    public List<BookingModel> getHotelSales(string hotelName)
    {
        return this.bookings.Where(booking => booking.Hotel == hotelName).ToList();
    }

    public List<SaleModel> getHotelSales()
    {
        List<SaleModel> hotelSales = new List<SaleModel>();
        foreach (HotelModel hotel in this.hotels)
        {
            List<BookingModel> hotelBookings = getHotelSales(hotel.name);
            SaleModel hotelSale = new SaleModel();
            hotelSale.hotelName = hotel.name;
            hotelSale.totalBookings = hotelBookings.Count;
            hotelSale.totalIncome = hotelBookings.Sum(booking => booking.Price);
            hotelSale.totalGuests = hotelBookings.Sum(booking => booking.Guests);
            
            hotelSales.Add(hotelSale);
        }
        return hotelSales;
    }

    public List<CustomerModel> getCustomers()
    {
        List<CustomerModel> customers = new List<CustomerModel>();
        foreach (BookingModel booking in this.bookings)
        {
            CustomerModel customer = new CustomerModel();
            customer.Name = booking.FirstName + " " + booking.Surname;
            customer.Date = booking.Date.ToString("dd-MM-yyyy");
            customer.PhoneNumber = booking.Telephone;
            customer.Hotel = booking.Hotel;
            customer.RoomType = booking.RoomType;
            customer.Guests = booking.Guests;
            customer.Amount = booking.Price.ToString();
            customers.Add(customer);
        }
        return customers;
    }
}

public class SaleModel
{
    public string hotelName { get; set; }
    public int totalBookings { get; set; }
    public int totalIncome { get; set; }
    public int totalGuests { get; set; }
}

public class CustomerModel
{
    public string Name { get; set; }
    public string Date { get; set; }
    public string PhoneNumber { get; set; }
    public string Hotel { get; set; }
    public string RoomType { get; set; }
    public int Guests { get; set; }
    public string Amount { get; set; }
}