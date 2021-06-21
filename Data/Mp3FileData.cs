namespace MyEnjoyEnglishPlayer.Data {
    class Mp3FileData {

        #region Declaration
        #endregion

        #region Public Property
        /// <summary>
        /// 表示名称
        /// </summary>
        public string DisplayName { set; get; }

        /// <summary>
        /// ファイルパス
        /// </summary>
        public string FilePath { set; get; }
        #endregion


        #region Public Method
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return this.DisplayName;
        }
        #endregion
    }
}
