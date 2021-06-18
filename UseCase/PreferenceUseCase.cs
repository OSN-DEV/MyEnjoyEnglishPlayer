using MyEnjoyEnglishPlayer.Data;
using MyEnjoyEnglishPlayer.Repo;

namespace MyEnjoyEnglishPlayer.UseCase {
    class PreferenceUseCase : IPreferenceUseCase {

        #region Declaration
        private readonly IPreferenceRepo _repo;
        #endregion

        #region Constructor
        internal PreferenceUseCase(IPreferenceRepo repo) {
            this._repo = repo;
        }
        #endregion

        #region Internal Method
        /// <summary>
        /// 保存しているデータを取得する
        /// </summary>
        /// <returns>アプリケーションデータ</returns>
        PreferenceData IPreferenceUseCase.Load() {
            this._repo.Load();
            var result = new PreferenceData() {
                X = this._repo.X,
                Y = this._repo.Y,
                ObserveFolder = this._repo.ObserveFolder,
                StartEndPoints = KeyPair<string, PreferenceDetailData>.ToDictionary(this._repo.StartEndPoints)
            };
            return result;
        }

        /// <summary>
        /// データを保存する
        /// </summary>
        /// <param name="data"></param>
        void IPreferenceUseCase.Save(PreferenceData data) {
            this._repo.X = data.X;
            this._repo.Y = data.Y;
            this._repo.ObserveFolder = data.ObserveFolder;
            this._repo.StartEndPoints = KeyPair<string, PreferenceDetailData>.ToList(data.StartEndPoints);
            this._repo.Save();
        }
        #endregion

    }
}
