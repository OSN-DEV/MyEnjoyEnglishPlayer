using MyEnjoyEnglishPlayer.Data;
using MyEnjoyEnglishPlayer.Repo;
using MyEnjoyEnglishPlayer.UseCase;
using System.Windows;
using System.Windows.Controls;

namespace MyEnjoyEnglishPlayer.UI.Main {

    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class PlayerMainWindow : Window {

        #region Declaration
        private readonly PlayerMainViewModel _viewModel;
        #endregion

        #region Constructor
        public PlayerMainWindow() {
            InitializeComponent();

            this._viewModel = new PlayerMainViewModel(this, new AppSettingDataRepo());
            this.DataContext = this._viewModel;

            this.Closing += (sender, e) => {
                this._viewModel.AppClosing();
            };
        }
        #endregion

        #region Event
        private void cData_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            var listView = sender as ListView;
            this._viewModel.ListDoubleClick(listView.SelectedItem as string);
        }


        private void Slider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e) {
            this._viewModel.SliderChangedDragStart();
        }
        private void Slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e) {
            var slider = sender as Slider;
            this._viewModel.SliderChangedDragComplete(slider.Value);
        }
        #endregion

    }
}
