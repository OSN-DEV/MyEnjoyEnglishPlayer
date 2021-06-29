using MyEnjoyEnglishPlayer.AppCommon;
using MyEnjoyEnglishPlayer.Repo.Data;
using OsnCsLib.File;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyEnjoyEnglishPlayer.Repo.Data.Bookmark;

namespace MyEnjoyEnglishPlayer.Repo {
    /// <summary>
    /// 
    /// </summary>
    class BookmarkRepo {

        #region Declaration
        /// <summary>
        /// TSVの項目並び順
        /// </summary>
        private enum ItemType : short {
            /// <summary>
            /// 時間
            /// </summary>
            Time = 0,
            /// <summary>
            /// 時間種別
            /// </summary>
            TimeState,
            /// <summary>
            /// タイトル
            /// </summary>
            Title,
            /// <summary>
            /// 項目数
            /// </summary>
            Count
        }
        /// <summary>
        /// 項目のセパレータ
        /// </summary>
        private readonly char Separator = '\t';
        #endregion

        #region Public Property
        /// <summary>
        /// ブックマークリスト
        /// </summary>
        internal ObservableCollection<Bookmark> BookmarkList {
            private set; get;
        } = new ObservableCollection<Bookmark>();
        #endregion

        #region Public Method
        /// <summary>
        /// データを読み込む
        /// </summary>
        /// <param name="mp3file">MP3のフルパス</param>
        internal void Load(string mp3file) {
            this.BookmarkList.Clear();

            var pptFile = GetPptFilename(mp3file);
            if (null == pptFile) {
                return;
            }

            using (var op = new FileOperator(pptFile, FileOperator.OpenMode.Read)) {
                while (!op.Eof) {
                    try {
                        var data = op.ReadLine().Split(Separator);
                        if ((int)ItemType.Count != data.Length) {
                            System.Diagnostics.Debug.WriteLine(data + " is invalid");
                            continue;
                        }
                        var bookmark = new Bookmark() {
                            Time = data[(int)ItemType.Time],
                            TimeStatus = (TimeState)int.Parse(data[(int)ItemType.TimeState]),
                            Title = data[(int)ItemType.Title]
                        };
                        this.BookmarkList.Add(bookmark);
                    } catch (Exception ex) {
                        System.Diagnostics.Debug.WriteLine("invalid data");
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// データを保存する
        /// </summary>
        internal void Save(string mp3file) {

            var pptFile = GetPptFilename(mp3file);
            if (null == pptFile) {
                return;
            }

            using (var op = new FileOperator(pptFile, FileOperator.OpenMode.Write)) {
                foreach(var bookmark in this.BookmarkList) {
                    op.WriteLine($"{bookmark.Time}{Separator}{bookmark.TimeStatus}{Separator}{bookmark.Title}");
                }
            }
        }
        #endregion

        #region PrivateMethod
        /// <summary>
        /// MP3ファイル名からpptファイル名を取得する
        /// </summary>
        /// <param name="mp3file">mp3ファイル</param>
        /// <returns>pptファイル名(不正なファイルの場合はnullを返却)</returns>
        private string GetPptFilename(string mp3file) {
            // ここまでチェックする必要も感じないけど一応。
            if (!mp3file.EndsWith(Constants.ExtMP3)) {
                return null;
            }
            var pptFile = mp3file.Substring(0, mp3file.Length - Constants.ExtMP3.Length) + Constants.ExtPlayPositionData;
            if (!System.IO.File.Exists(pptFile)) {
                return null;
            }
            return pptFile;
        }
        #endregion

    }
}
