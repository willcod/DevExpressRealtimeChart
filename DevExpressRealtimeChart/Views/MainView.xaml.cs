using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Core;
using DevExpressRealtimeChart.ViewModels;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace DevExpressRealtimeChart.Views
{
    /// <summary>
    /// Interaction logic for View1.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        private CancellationTokenSource _cts;

        public MainView() {
            InitializeComponent();
            _cts = new CancellationTokenSource();
            this.Loaded += (sender, args) => {
                var parent = Window.GetWindow(this);
                parent.Closing += (o, eventArgs) => {
                    _cts.Cancel();
                };
            };

            (new Thread(new ThreadStart(Update))).Start();
        }

        private void Update() {
            while (_cts.Token.IsCancellationRequested == false) {
                Dispatcher.BeginInvoke(new Action(() => {
                    var model = (MainViewModel)this.DataContext;
                    var DatasPoints = model.DatasPoints;

                    DatasPoints.Add(new Point<double>(DatasPoints.Count, Math.Sin(Math.PI * (double)DatasPoints.Count / 60.0)));
                }));

                Thread.Sleep(1);
            }
        }

        private void ChartControl_OnBoundDataChanged(object sender, RoutedEventArgs e) {
            AxisX2D axisX = ((XYDiagram2D)RealtimeChart.Diagram).ActualAxisX;
            var maxRangeValue = axisX.ActualWholeRange.ActualMaxValue;
            axisX.ActualVisualRange.SetMinMaxValues((double)maxRangeValue - 1000, maxRangeValue);
        }
    }
}