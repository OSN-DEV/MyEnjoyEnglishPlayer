using MyEnjoyEnglishPlayer.Data;
using MyEnjoyEnglishPlayer.Func;
using MyEnjoyEnglishPlayer.UseCase;
using OsnCsLib.WPFComponent.Bind;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;


namespace MyEnjoyEnglishPlayer {
    class MainWindowViewModel : BaseBindable {

        #region Declaration
        private readonly MainWindow _window;
        private readonly IPreferenceUseCase _useCase;
        private MediaPlayer _player = new MediaPlayer();
        private bool _isDraggingThumb = false;
        private PreferenceDetailData _currentData;
        #endregion

        #region Public Property
        /// <summary>
        /// Data
        /// </summary>
        public PreferenceData AppData { set; get; }


        public Visibility PlayButtonVIsiblity { 
            get { return this._player.IsPlaying ? Visibility.Collapsed : Visibility.Visible; }
        }

        public Visibility PauseButtonVIsiblity {
            get { return this._player.IsPlaying ? Visibility.Visible : Visibility.Collapsed; }
        }

        private int _duration;
        public int Duration {
            set { 
                base.SetProperty(ref this._duration, value);
                base.SetProperty(nameof(TotalTime));
            }
            get { return this._duration; }
        }

        private long _currentPosition;
        public long CurrentPosition {
            set { base.SetProperty(ref this._currentPosition, value); }
            get { return this._currentPosition; }
        }

        private string _currentTime;
        public string CurrentTime {
            set { base.SetProperty(ref this._currentTime, value); }
            get { return this._currentTime; }
        }

        public string TotalTime {
            get {
                if (this._player.IsPrepared) {
                    return new TimeSpan(0,0, this._duration).ToString(@"mm\:ss");
                } else {
                    return "";
                }
            }
        }

        private int _startPoint;
        public int StartPoint {
            set { base.SetProperty(ref this._startPoint, value); }
            get { return this._startPoint; }
        }

        private int _endPoint;
        public int EndPoint {
            set { base.SetProperty(ref this._endPoint, value); }
            get { return this._endPoint; }
        }

        /// <summary>
        /// フォルダ選択コマンド
        /// </summary>
        public DelegateCommand FolderSelectCommand { set; get; }

        /// <summary>
        /// 前曲コマンド
        /// </summary>
        public DelegateCommand PrevCommand { set; get; }

        /// <summary>
        /// 巻き戻しコマンド
        /// </summary>
        public DelegateCommand RewindCommand { set; get; }

        /// <summary>
        /// 再生コマンド
        /// </summary>
        public DelegateCommand PlayCommand { set; get; }

        /// <summary>
        /// 一時停止コマンド
        /// </summary>
        public DelegateCommand PauseCommand { set; get; }

        /// <summary>
        /// 早送りコマンド
        /// </summary>
        public DelegateCommand ForwardCommand { set; get; }

        /// <summary>
        /// 次曲コマンド
        /// </summary>
        public DelegateCommand NextCommand { set; get; }

        /// <summary>
        /// 開始ポイントコマンド
        /// </summary>
        public DelegateCommand StartPointCommand { set; get; }

        /// <summary>
        /// 開始ポイントコマンド
        /// </summary>
        public DelegateCommand EndPointCommand { set; get; }
        #endregion

        #region Constructor
        public MainWindowViewModel(MainWindow window, IPreferenceUseCase useCase) {
            this._window = window;
            this._useCase = useCase;
            this.Initialize();
        }
        #endregion

        #region Public Method
        /// <summary>
        /// アプリ終了時処理
        /// </summary>
        internal void AppClosing() {
            this.AppData.X = this._window.Left;
            this.AppData.Y = this._window.Top;
            this._useCase.Save(this.AppData);
        }

        /// <summary>
        /// リストダブルクリック
        /// </summary>
        internal void ListDoubleClick(string displayName) {
            this._currentData = this.AppData.StartEndPoints[displayName];
            this.AppData.CurrentFile = displayName;
            this._player.Prepare(this._currentData.FileName);
            this.StartPoint = this._currentData.StartPoint;
            this.EndPoint = this._currentData.EndPoint;
            this._player.CurrentPosition = this.StartPoint; 
            this._player.Play();
        }

        internal void SliderChangedDragStart() {
            this._isDraggingThumb = true;
        }
        /// <summary>
        /// スライダー変更
        /// </summary>
        /// <param name="value"></param>
        internal void SliderChangedDragComplete(double value) {
            this._isDraggingThumb = false;
            this._player.CurrentPosition = (int)value;

        }
        #endregion

        #region Event
        private void PlayerOnPrepared() {
            this.Duration = this._player.Duration;
            this.PlayClick();
        }

        private void PlayerOnPositionChanged(int position, string positionString) {
            if (!this._isDraggingThumb) {
                this.CurrentPosition = position;
                this.CurrentTime = positionString;
            } else {
            }

        }

        #endregion

        #region Private Method
        /// <summary>
        /// 初期処理
        /// </summary>
        private void Initialize() {
            this.FolderSelectCommand = new DelegateCommand(FolderSelectClick);
            this.PrevCommand = new DelegateCommand(PrevClick);
            this.RewindCommand = new DelegateCommand(RewindClick);
            this.PlayCommand = new DelegateCommand(PlayClick);
            this.PauseCommand = new DelegateCommand(PauseClick);
            this.ForwardCommand = new DelegateCommand(ForwardClick);
            this.NextCommand = new DelegateCommand(NextClick);
            this.StartPointCommand = new DelegateCommand(StartPointClick);
            this.EndPointCommand = new DelegateCommand(EndPointClick);

            //
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            this._window.Title = $"My Enjoy English Player({asm.GetName().Version})";

            //
            this.AppData = this._useCase.Load();
            if (0 < this.AppData.X && 0 < this.AppData.X) {
                this._window.Left = this.AppData.X;
                this._window.Top = this.AppData.Y;
            }

            this.RefreshList();

            //
            this._player.OnPrepared += PlayerOnPrepared;
            this._player.OnPositionChanged += PlayerOnPositionChanged;
        }

        /// <summary>
        /// フォルダ選択クリック
        /// </summary>
        private void FolderSelectClick() {
            using (var dialog = new FolderBrowserDialog()) {
                dialog.Description = "コンテンツを格納しているフォルダを選択してください。";
                dialog.SelectedPath = this.AppData.ObserveFolder;
                if (dialog.ShowDialog() != DialogResult.OK) {
                    return;
                }
                this.AppData.CurrentFile = "";
                this.AppData.ObserveFolder = dialog.SelectedPath;
                this.RefreshList();
            }
        }

        /// <summary>
        /// 前曲クリック
        /// </summary>
        private void PrevClick() {
            this._player.CurrentPosition = this.StartPoint;
        }

        /// <summary>
        /// 巻き戻しクリック
        /// </summary>
        private void RewindClick() {
            if (0 < this._player.CurrentPosition) {
                this._player.CurrentPosition -= 2;
            }
        }

        /// <summary>
        /// 再生クリック
        /// </summary>
        private void PlayClick() {
            this._player.Play();
            this.SetProperty(nameof(this.PlayButtonVIsiblity));
            this.SetProperty(nameof(this.PauseButtonVIsiblity));
        }

        /// <summary>
        /// 一時停止クリック
        /// </summary>
        private void PauseClick() {
            this._player.Pause();
            this.SetProperty(nameof(this.PlayButtonVIsiblity));
            this.SetProperty(nameof(this.PauseButtonVIsiblity));
        }

        /// <summary>
        /// 早送りクリック
        /// </summary>
        private void ForwardClick() {
            if (this._player.CurrentPosition < this._player.Duration) {
                this._player.CurrentPosition+=2;
            }
        }

        /// <summary>
        /// 次曲クリック
        /// </summary>
        private void NextClick() {

        }

        /// <summary>
        /// 開始ポイントクリック
        /// </summary>
        private void StartPointClick() {
            this._currentData.StartPoint = this._player.CurrentPosition;
            this._useCase.Save(this.AppData);
            this.StartPoint = this._currentData.StartPoint;
        }

        /// <summary>
        /// 終了ポイントコマンド
        /// </summary>
        private void EndPointClick() {
            this._currentData.EndPoint = this._player.CurrentPosition;
            this._useCase.Save(this.AppData);
            this.EndPoint = this._currentData.EndPoint;
        }

        /// <summary>
        /// リストの更新
        /// </summary>
        private void RefreshList() {
            var newList = new Dictionary<string, PreferenceDetailData>();
            if (Directory.Exists(this.AppData.ObserveFolder)) {
                var files = Directory.GetFiles(this.AppData.ObserveFolder, "*.mp3");
                foreach (var file in files) {
                    var data = new PreferenceDetailData();
                    var info = new FileInfo(file);
                    data.DisplayName = info.Name.Replace($"{info.Extension}", "");
                    data.FileName = info.FullName;
                    if (this.AppData.StartEndPoints.ContainsKey(data.DisplayName)) {
                        data.StartPoint = this.AppData.StartEndPoints[data.DisplayName].StartPoint;
                        data.EndPoint = this.AppData.StartEndPoints[data.DisplayName].EndPoint;
                    }
                    newList.Add(data.DisplayName, data);
                }
            }
            this.AppData.StartEndPoints = newList;
            this._useCase.Save(this.AppData);
        }
        #endregion

    }
}
