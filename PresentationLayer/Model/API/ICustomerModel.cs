using System.ComponentModel;

namespace PresentationLayer.Model.API
{
    public interface ICustomerModel : INotifyPropertyChanged
    {
        int Id { get; set; }
        string Name { get; set; }
        string Email { get; set; }
    }
}