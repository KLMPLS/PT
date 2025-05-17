using System.ComponentModel;

namespace PresentationLayer.Model.API
{
    public interface IInventoryStateModel : INotifyPropertyChanged
    {
        int Id { get; set; }
        int Available { get; set; }
    }
}