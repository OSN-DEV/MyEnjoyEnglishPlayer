using OsnCsLib.WPFComponent.Bind;
using System.Collections.Generic;

namespace MyEnjoyEnglishPlayer.Data {
    public class PreferenceData : BaseBindable {

        #region Internal Property
        /// <summary>
        /// ウィンドウのX座標
        /// </summary>
        private double _x = 0;
        public double X {
            set { base.SetProperty(ref this._x, value); }
            get { return this._x; }
        }

        /// <summary>
        /// ウィンドウのY座標
        /// </summary>
        private double _y = 0;
        public double Y {
            set { base.SetProperty(ref this._y, value); }
            get { return this._y; }
        }

        /// <summary>
        /// 監視フォルダー
        /// </summary>
        private string _observeFolder = "";
        public string ObserveFolder {
            set { base.SetProperty(ref this._observeFolder, value); }
            get { return this._observeFolder; }
        }

        /// <summary>
        /// ファイル開始・終了ポイント情報
        /// </summary>
        private Dictionary<string, PreferenceDetailData> _startEndPoints = new Dictionary<string, PreferenceDetailData>();
        public Dictionary<string, PreferenceDetailData> StartEndPoints {
            set { 
                base.SetProperty(ref this._startEndPoints, value);
                base.SetProperty(nameof(FileList));
            }
            get { return this._startEndPoints; }
        }

        /// <summary>
        /// 現在再生中のファイル名
        /// </summary>
        private string _currentFile = "";
        public string CurrentFile {
            set {
                base.SetProperty(ref this._currentFile, value);
                base.SetProperty(nameof(IsFileSet));
            }
            get { return this._currentFile; }
        }

        /// <summary>
        /// 再生対象が設定されているかどうか
        /// </summary>
        public bool IsFileSet {
            get { return 0 < this.CurrentFile.Length; }
        }

        /// <summary>
        /// ファイルリスト
        /// </summary>
        public List<string> FileList {
            get {
                var list = new List<string>();

                foreach (var file in this.StartEndPoints) {
                    list.Add(file.Value.DisplayName);
                }
                list.Sort();
                list.Reverse();
                return list;
            }
        }
        #endregion

        #region Constructor
        public PreferenceData() { }
        #endregion

    }
}
