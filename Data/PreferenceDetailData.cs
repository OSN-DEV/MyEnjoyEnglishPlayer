using OsnCsLib.WPFComponent.Bind;

namespace MyEnjoyEnglishPlayer.Data {
    public class PreferenceDetailData : BaseBindable {

        #region Public Property
        /// <summary>
        /// 表示名称
        /// </summary>
        private string _displayName;
        public string DisplayName {
            set { base.SetProperty(ref this._displayName, value); }
            get { return this._displayName; }
        }

        /// <summary>
        /// ファイル名
        /// </summary>
        private string _fileName;
        public string FileName {
            set { base.SetProperty(ref this._fileName, value); }
            get { return this._fileName; }
        }

        /// <summary>
        /// 再生開始位置
        /// </summary>
        private int _startPoint;
        public int StartPoint {
            set { base.SetProperty(ref this._startPoint, value); }
            get { return this._startPoint; }
        }

        /// <summary>
        /// 再生終了位置
        /// </summary>
        private int _endPoint;
        public int EndPoint {
            set { base.SetProperty(ref this._endPoint, value); }
            get { return this._endPoint; }
        }
        #endregion

    }
}
