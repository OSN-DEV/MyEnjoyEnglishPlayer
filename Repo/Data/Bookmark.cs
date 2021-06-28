using OsnCsLib.WPFComponent.Bind;

namespace MyEnjoyEnglishPlayer.Repo.Data {
    public class Bookmark : BaseBindable {

        #region Declaration
        public enum TimeState: short {
            /// <summary>
            /// 指定なし
            /// </summary>
            None,       
            /// <summary>
            /// 開始時間
            /// </summary>
            Start,
            /// <summary>
            /// 終了時間
            /// </summary>
            End
        }
        #endregion

        #region Public Property
        /// <summary>
        /// 時間
        /// </summary>
        private string _time = "";
        public string Time {
            set { base.SetProperty(ref this._time, value); }
            get { return this._time; }
        }

        /// <summary>
        /// 時間種別
        /// </summary>
        private TimeState _timeStatus = TimeState.None;
        public TimeState TimeStatus {
            set { base.SetProperty(ref this._timeStatus, value); }
            get { return this._timeStatus; }
        }

        private string _title = "";
        public string Title {
            set { base.SetProperty(ref this._title, value); }
            get { return this._title; }
        }
        #endregion

    }
}
