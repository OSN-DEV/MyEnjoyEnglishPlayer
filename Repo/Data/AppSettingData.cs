namespace MyEnjoyEnglishPlayer.Repo.Data {
    /// <summary>
    /// app setting data
    /// </summary>
    public class AppSettingData {

        #region Public Property
        /// <summary>
        /// ウィンドウのX座標
        /// </summary>
        public double X { set; get; } = -1;

        /// <summary>
        /// ウィンドウのY座標
        /// </summary>
        public double Y { set; get; } = -1;

        /// <summary>
        /// ウィンドウの幅
        /// </summary>
        public double Width { set; get; }

        /// <summary>
        /// ウィンドウの高さ
        /// </summary>
        public double Height { set; get; }

        /// <summary>
        /// 監視ディレクトリ
        /// </summary>
        public string WatchedDirectory { set; get; }

        /// <summary>
        /// 前回再生していたファイル
        /// </summary>
        public string LastPlayedFile { set; get; }

        /// <summary>
        /// 前回再生位置
        /// </summary>
        public double LastPosition { set; get; }

        /// <summary>
        /// 再生速度
        /// </summary>
        public double PlaySpeed { set; get; }

        /// <summary>
        /// ボリューム
        /// </summary>
        public int Volume { set; get; }

        /// <summary>
        /// リピートモード
        /// </summary>
        public bool IsRepeatOn { set; get; }
        #endregion

        #region Public Method
        /// <summary>
        /// インスタンスのクローンa
        /// </summary>
        /// <returns></returns>
        public AppSettingData Clone() {
            var data = new AppSettingData() {
                X = this.X,
                Y = this.Y,
                Width = this.Width,
                Height = this.Height,
                WatchedDirectory = this.WatchedDirectory,
                LastPlayedFile = this.LastPlayedFile,
                LastPosition = this.LastPosition,
                PlaySpeed = this.PlaySpeed,
                Volume = this.Volume,
                IsRepeatOn = this.IsRepeatOn,
            };

            return data;
        }
        #endregion
    }
}
