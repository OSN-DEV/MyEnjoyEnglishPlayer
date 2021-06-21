using MyEnjoyEnglishPlayer.AppCommon;
using MyEnjoyEnglishPlayer.Repo.Data;
using System.IO;
using System.Text;

namespace MyEnjoyEnglishPlayer.Repo {
    class AppSettingDataRepo {

        #region Declaration

        internal AppSettingData Data { set; get; }
        #endregion

        #region Public Method
        /// <summary>
        /// データを読み込む
        /// </summary>
        internal void Load() {
            if (System.IO.File.Exists(Constants.AppDataFile)) {
                using (var reader = new StreamReader(Constants.AppDataFile, new UTF8Encoding(false))) {
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(AppSettingData));
                    this.Data = (AppSettingData)serializer.Deserialize(reader);
                }
            } else {
                this.Data = new AppSettingData();
            }
        }

        /// <summary>
        /// データを保存する
        /// </summary>
        internal void Save() {
            using (var writer = new StreamWriter(Constants.AppDataFile, false, new UTF8Encoding(false))) {
                var seralizer = new System.Xml.Serialization.XmlSerializer(typeof(AppSettingData));
                seralizer.Serialize(writer, this.Data);
            }
        }
        #endregion

        #region Private Method
        #endregion
    }
}
