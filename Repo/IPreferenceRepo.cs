using MyEnjoyEnglishPlayer.Data;
using System.Collections.Generic;

namespace MyEnjoyEnglishPlayer.Repo {
    public abstract class IPreferenceRepo {

        #region Declaration
        #endregion

        #region Public Property
        /// <summary>
        /// ウィンドウのX座標
        /// </summary>
        public double X { set; get; }

        /// <summary>
        /// ウィンドウのY座標
        /// </summary>
        public double Y { set; get; }

        /// <summary>
        /// 監視フォルダー
        /// </summary>
        public string ObserveFolder { set; get; } = "";

        /// <summary>
        /// ファイル開始・終了ポイント情報
        /// </summary>
        public List<KeyPair<string, PreferenceDetailData>> StartEndPoints = new List<KeyPair<string, PreferenceDetailData>>();
        #endregion

        #region Public Method
        /// <summary>
        /// データをロードする
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// データを保存する
        /// </summary>
        public abstract void Save();
        #endregion

    }
}
