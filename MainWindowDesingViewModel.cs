using System.Collections.Generic;

namespace MyEnjoyEnglishPlayer {
    class MainWindowDesingViewModel {
        public DummyProfile AppData { set; get; } = new DummyProfile();

        public class DummyProfile {
            /// <summary>
            /// ファイルリスト
            /// </summary>
            public List<string> FileList { set; get; } = new List<string> {
            { "ファイル１" },{ "ファイル２" }
        };

            /// <summary>
            /// 現在再生中のファイル名
            /// </summary>
            public string CurrentFile { set; get; } = "04月23日放送分";

            /// <summary>
            /// 現在の再生時間
            /// </summary>
            public string CurrentTime { set; get; } = "00:05";

            /// <summary>
            /// 総再生時間
            /// </summary>
            public string TotalTime { set; get; } = "04:49";

            /// <summary>
            /// 再生開始位置
            /// </summary>
            public string StartPoint { get; set; } = "00:30";

            /// <summary>
            /// 再生終了位置
            /// </summary>
            public string EndPoint { get; set; } = "04:26";

            /// <summary>
            /// 再生対象が設定されているかどうか
            /// </summary>
            public bool IsFileSet { get; set; } = false;
        }
    }
}
