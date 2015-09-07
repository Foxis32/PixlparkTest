using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PixlparkTest.DataProvider;
using PixlparkTest.Model;

namespace PixlparkTest.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DataOrderProvider _dataOrderProvider;
        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get { return _selectedOrder ?? (_selectedOrder = new Order()); }
            set { Set(() => SelectedOrder, ref _selectedOrder, value); }
        }

        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders => _orders ?? (_orders = new ObservableCollection<Order>());

        private RelayCommand<Order> _selectedOrderChangedCommand;

        public RelayCommand<Order> SelectedOrderChangedCommand
        {
            get
            {
                return _selectedOrderChangedCommand ?? (_selectedOrderChangedCommand = new RelayCommand<Order>(
                    (order) =>
                    {
                        SelectedOrder = order;
                    }));
            }
        }

        public MainViewModel()
        {
            _dataOrderProvider = new DataOrderProvider();
            var orders = _dataOrderProvider.GetOrders();
            foreach (var order in orders)
            {
                Orders.Add(order);
            }
        }
    }
}