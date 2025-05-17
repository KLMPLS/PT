using System;
using System.ComponentModel;

namespace PresentationLayer.Model.API
{
    public interface IBookRecordModel : INotifyPropertyChanged
    {
        int Id { get; set; }
        int CustomerId { get; set; }
        int BookId { get; set; }
        string Type { get; set; }
        DateTime Date { get; set; }
    }
}