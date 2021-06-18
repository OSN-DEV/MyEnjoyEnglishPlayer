using MyEnjoyEnglishPlayer.Data;
using MyEnjoyEnglishPlayer.Repo;
using MyEnjoyEnglishPlayer.UseCase;
using System.Windows;
using System.Windows.Controls;

namespace MyEnjoyEnglishPlayer {

    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : Window {

        #region Declaration
        private readonly MainWindowViewModel _viewModel;
        #endregion

        #region Constructor
        public MainWindow() {
            InitializeComponent();

            this._viewModel = new MainWindowViewModel(this, new PreferenceUseCase(new PreferenceXmlRepo() ));
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
