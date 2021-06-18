using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;

using System.Threading.Tasks;
using System.Timers;
using WMPLib;

namespace MyEnjoyEnglishPlayer.Func {
    class MediaPlayer {
        // https://www.usefullcode.net/mp3player/
        #region Declaration
        private WindowsMediaPlayer _player = new WindowsMediaPlayer();
        private Timer _timer = new Timer();
        public enum PlayerState { 
            Unkwnon,
            Preparing,
            Playing,
            Pause,
        }
        private PlayerState _status = PlayerState.Unkwnon;
        #endregion

        #region Public Property
        public bool IsPrepared {
            get { return PlayerState.Preparing < this._status; }
        }
        public bool IsPlaying { 
            get { return this._status == PlayerState.Playing; }
        }

        public string currentPositionString {
            get { return this._player.controls.currentPositionString; }
        }
        public int CurrentPosition {
            set { this._player.controls.currentPosition = value; }
            get { return (int)this._player.controls.currentPosition; }
        }

        public int Duration {
            get { return (int)this._player.controls.currentItem.duration;  }
        }
        #endregion

        #region Public Event
        public delegate void CommonEventHandler();
        public event CommonEventHandler OnPrepared;
        public delegate void PositionChangedEventHandler(int position, string positionString);
        public event PositionChangedEventHandler OnPositionChanged;
        #endregion

        #region Constructor
        public MediaPlayer() {
            // this._player.settings.volume = 20;
            this._player.DurationUnitChange += _player_DurationUnitChange;
            this._player.StatusChange += PlayerStatusChange;
            this._player.PlayStateChange += _player_PlayStateChange;
            this._player.MediaError += _player_MediaError;
            this._player.PositionChange += _player_PositionChange;

            this._timer.Interval = 300;
            this._timer.Enabled = false;
            this._timer.Elapsed += TimerElapsed;
        }


        #endregion

        #region Public Method
        /// <summary>
        /// 再生対象のファイルを準備
        /// </summary>
        /// <param name="file">ファイル</param>
        public void Prepare(string file) {
            this._status = PlayerState.Preparing;
            this._player.URL = file;
        }



        /// <summary>
        /// 再生する
        /// </summary>
        public void Play() {
            if (this._player.playState == WMPPlayState.wmppsPaused) {
                this._player.controls.play();
                this._status = PlayerState.Playing;
                this._timer.Start();
            } else {
                System.Diagnostics.Debug.WriteLine("Play:Invalid state");
            }
        }

        /// <summary>
        /// 一時停止する
        /// </summary>
        public void Pause() {
            if (this._player.playState == WMPPlayState.wmppsPlaying) {
                this._player.controls.pause();
                this._status = PlayerState.Pause;
                this._timer.Stop();
            } else {
                System.Diagnostics.Debug.WriteLine("Pause:Invalid state");
            }
        }
        #endregion

        #region Event
        /// <summary>
        /// 
        /// </summary>
        private void PlayerStatusChange() {
            switch(this._player.playState) {
                case WMPPlayState.wmppsPlaying:
                    if (this._status == PlayerState.Preparing) {
                        this._status = PlayerState.Pause;
                        this._player.controls.pause();
                        this.OnPrepared?.Invoke();

                    }
                    break;
            }
            System.Diagnostics.Debug.WriteLine("this._player.status:" + this._player.status);
            System.Diagnostics.Debug.WriteLine("this._player.playState:" + this._player.playState);
        }

        private void _player_DurationUnitChange(int NewDurationUnit) {
            System.Diagnostics.Debug.WriteLine("_player_DurationUnitChange");
        }

        private void _player_PlayStateChange(int NewState) {
            System.Diagnostics.Debug.WriteLine("_player_PlayStateChange" + NewState);
        }


        private void _player_MediaError(object pMediaObject) {
            System.Diagnostics.Debug.WriteLine("_player_MediaError");
        }

        private void _player_PositionChange(double oldPosition, double newPosition) {
            System.Diagnostics.Debug.WriteLine("_player_PositionChange" + oldPosition + ":" + newPosition);
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e) {
            System.Diagnostics.Debug.WriteLine("_timer_Elapsed");
            this.OnPositionChanged?.Invoke((int)this._player.controls.currentPosition, this._player.controls.currentPositionString);
        }
        #endregion
    }
}
