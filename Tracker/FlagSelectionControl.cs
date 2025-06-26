using System.ComponentModel;

namespace Tracker
{
    [DefaultBindingProperty("FlagValue")]
    public partial class FlagSelectionControl : UserControl, INotifyPropertyChanged
    {
        public FlagSelectionControl()
        {
            InitializeComponent();
        }

        public event EventHandler<FlagChangedEventArgs>? FlagChanged;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            //radioButtonNone.Checked = flagValue == "None";
            //radioButtonIdea.Checked = flagValue == "Idea";
            //radioButtonIOwe.Checked = flagValue == "I_Owe";
            //radioButtonTheyOwe.Checked = flagValue == "They_Owe";
        }

        private string flagValue = "None";

        public event PropertyChangedEventHandler? PropertyChanged;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public string FlagValue
        {
            get { return flagValue; }
            set
            {
                flagValue = value;
                radioButtonNone.Checked = flagValue == "None";
                radioButtonIdea.Checked = flagValue == "Idea";
                radioButtonIOwe.Checked = flagValue == "I_Owe";
                radioButtonTheyOwe.Checked = flagValue == "They_Owe";
            }
        }

        private void SelectionChanged(object sender, EventArgs e)
        {
            switch (sender)
            {
                case RadioButton rb when rb == radioButtonNone:
                    if (rb.Checked) flagValue = "None";
                    break;
                case RadioButton rb when rb == radioButtonIdea:
                    if (rb.Checked) flagValue = "Idea";
                    break;
                case RadioButton rb when rb == radioButtonIOwe:
                    if (rb.Checked) flagValue = "I_Owe";
                    break;
                case RadioButton rb when rb == radioButtonTheyOwe:
                    if (rb.Checked) flagValue = "They_Owe";
                    break;
                default:
                    flagValue = "None";
                    break;
            }
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FlagValue)));

            FlagChanged?.Invoke(this, new FlagChangedEventArgs(flagValue));
        }

        public class FlagChangedEventArgs(string flagValue) : EventArgs
        {
            public string FlagValue { get; } = flagValue;
        }
    }
}
