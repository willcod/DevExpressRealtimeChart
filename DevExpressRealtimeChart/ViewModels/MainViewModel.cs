using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using System.Collections.ObjectModel;

namespace DevExpressRealtimeChart.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Point<double>> _datasPoints;

        public ObservableCollection<Point<double>> DatasPoints {
            get { return GetProperty(() => _datasPoints); }
            set { SetProperty(() => _datasPoints, value); }
        }

        public MainViewModel() {
            DatasPoints = new ObservableCollection<Point<double>>();
        }
    }
}