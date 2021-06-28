using System;
using System.Windows;
using System.Windows.Controls;
using static MyEnjoyEnglishPlayer.Repo.Data.Bookmark;

namespace MyEnjoyEnglishPlayer.Component {
    /// <summary>
    /// Status Button
    /// </summary>
    public partial class TimeStatusButton : UserControl {

        #region Constructor
        public TimeStatusButton() {
            InitializeComponent();
        }
        #endregion

        #region Public Event
        public class ResultChangedEventArgs : EventArgs {
            public long Id { private set; get; }
            public TimeState TimeStatus { private set; get; }
            public ResultChangedEventArgs(long id, TimeState status) {
                this.Id = id;
                this.TimeStatus = status;
            }
        }
        public EventHandler<ResultChangedEventArgs> OnTimeStatusChanged;
        public event EventHandler<ResultChangedEventArgs> TimeStatusChanged {
            add { OnTimeStatusChanged += value; }
            remove { OnTimeStatusChanged -= value; }
        }
        #endregion

        #region Public Property
        static TimeStatusButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimeStatusButton), new FrameworkPropertyMetadata(typeof(TimeStatusButton)));
        }

        public static readonly DependencyProperty TimeStatusProperty =
                                                        DependencyProperty.Register("TimeStatus",
                                                                                    typeof(TimeState),
                                                                                    typeof(TimeStatusButton),
                                                                                    new FrameworkPropertyMetadata());

        public TimeState TimeStatus {
            get { return (TimeState)GetValue(TimeStatusProperty); }
            set {
                SetValue(TimeStatusProperty, value);
            }
        }
        #endregion

        #region Event
        private void StatusButton_Click(object sender, EventArgs e) {
            switch (this.TimeStatus) {
                case TimeState.None:
                    this.TimeStatus = TimeState.Start;
                    break;
                case TimeState.Start:
                    this.TimeStatus = TimeState.End;
                    break;
                default:
                    this.TimeStatus = TimeState.None;
                    break;
            }

            var args = new ResultChangedEventArgs(long.Parse(this.Tag.ToString()), (TimeState)this.TimeStatus);
            this.OnTimeStatusChanged?.Invoke(this, args);
        }
        #endregion
    }
}
