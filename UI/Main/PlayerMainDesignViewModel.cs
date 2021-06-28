using MyEnjoyEnglishPlayer.Data;
using MyEnjoyEnglishPlayer.Repo.Data;
using System.Collections.Generic;

namespace MyEnjoyEnglishPlayer.UI.Main {
    class PlayerMainDesignViewModel {
        /// <summary>
        /// MP3ファイルリスト
        /// </summary>
        public List<Mp3FileData> MP3FileList { set; get; } = new List<Mp3FileData> {
            { new Mp3FileData(){ DisplayName="dddd" } },{ new Mp3FileData(){ DisplayName="dddd" } }
        };

        /// <summary>
        /// ブックマークリスト
        /// </summary>
        public List<Bookmark> BookmarkList { set; get; } = new List<Bookmark> {
            { new Bookmark() { Time = "00:30", TimeStatus = Bookmark.TimeState.Start, Title = "開始" } },
            { new Bookmark() { Time = "01:30", TimeStatus = Bookmark.TimeState.None, Title = "中間" } },
            { new Bookmark() { Time = "03:45", TimeStatus = Bookmark.TimeState.End, Title = "終了" } }
        };






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




        public DummyProfile AppData { set; get; } = new DummyProfile();


    }
}
