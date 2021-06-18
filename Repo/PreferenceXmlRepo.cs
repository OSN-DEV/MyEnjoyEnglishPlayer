using MyEnjoyEnglishPlayer.AppCommon;
using System.IO;
using System.Text;

namespace MyEnjoyEnglishPlayer.Repo {
    public class PreferenceXmlRepo : IPreferenceRepo {

        #region Declaration
        #endregion

        #region Public Method
        /// <summary>
        /// データを読み込む
        /// </summary>
        public override void Load() {
            if (System.IO.File.Exists(Constants.AppDataFile)) {
                using (var reader = new StreamReader(Constants.AppDataFile, new UTF8Encoding(false))) {
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(PreferenceXmlRepo));
                    this.Copy((PreferenceXmlRepo)serializer.Deserialize(reader));
                }
            } else {
                
            }
        }

        /// <summary>
        /// データを保存する
        /// </summary>
        public override void Save() {
            using (var writer = new StreamWriter(Constants.AppDataFile, false, new UTF8Encoding(false))) {
                var seralizer = new System.Xml.Serialization.XmlSerializer(typeof(PreferenceXmlRepo));
                seralizer.Serialize(writer, this);
            }
        }
        #endregion


        #region Private Method
        /// <summary>
        /// メンバーの情報をコピーする。
        /// </summary>
        private void Copy(PreferenceXmlRepo src) {
            this.X = src.X;
            this.Y = src.Y;
            this.ObserveFolder = src.ObserveFolder;
            this.StartEndPoints = src.StartEndPoints;
        }
        #endregion
    }
}
