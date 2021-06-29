namespace MyEnjoyEnglishPlayer.AppCommon {
    class Constants {

        internal static readonly string AppSettingDataFile = OsnCsLib.Common.Util.GetAppPath() + @"app.setting.data";

        /// <summary>
        /// アプリケーションデータ
        /// </summary>
        internal static readonly string AppDataFile = OsnCsLib.Common.Util.GetAppPath() + @"app.data";

        /// <summary>
        /// 再生情報データファイルの拡張子
        /// </summary>
        internal static readonly string ExtPlayPositionData = "ppt";

        /// <summary>
        /// MP3ファイルの拡張子
        /// </summary>
        internal static readonly string ExtMP3 = "mp3";
    }
}
