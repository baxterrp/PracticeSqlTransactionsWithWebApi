using PracticeWebApi.CommonClasses.Users;

namespace PracticeWebApi.CommonClasses.Orders
{
    public class Order : BaseResource
    {
        public string Status { get; set; }
        public Cart Cart { get; set; }
        public User User { get; set; }
    }
}
