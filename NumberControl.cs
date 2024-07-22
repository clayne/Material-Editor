﻿using System;
using System.Windows.Forms;

namespace Material_Editor
{
    public partial class NumberControl : CustomControl
    {
        private Label lbLabel;
        private NumericUpDown num;

        private NumberControl(string label, Func<CustomControl, bool> visibilityCallback, Action<CustomControl> changedCallback, decimal initialValue, int decimalPlaces, decimal increment, decimal minValue, decimal maxValue) : base(label)
        {
            lbLabel.Text = label;
            VisibilityCallback = visibilityCallback;
            ChangedCallback = changedCallback;

            num.Minimum = minValue;
            num.Maximum = maxValue;
            num.DecimalPlaces = decimalPlaces;
            num.Increment = increment;
            num.Value = initialValue;
        }

        public override void CreateControls()
        {
            lbLabel = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                AutoSize = true,
                Name = "lbLabel",
                Text = "Label",
                Tag = this
            };

            num = new NumericUpDown
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                DecimalPlaces = 5,
                Increment = 0.1M,
                Maximum = 100000000M,
                Minimum = -100000000M,
                Name = "num",
                TabIndex = 0,
                Tag = this
            };
            num.ValueChanged += new EventHandler(Num_ValueChanged);
        }

        public static NumberControl ForInteger(string label, Func<CustomControl, bool> visibilityCallback, Action<CustomControl> changedCallback, decimal initialValue = 0, decimal minValue = int.MinValue, decimal maxValue = int.MaxValue)
        {
            return new NumberControl(label, visibilityCallback, changedCallback, initialValue, 0, 1, minValue, maxValue);
        }

        public static NumberControl ForDecimal(string label, Func<CustomControl, bool> visibilityCallback, Action<CustomControl> changedCallback, decimal initialValue = 0, int decimalPlaces = 5, decimal increment = 0.1M, decimal minValue = -100000000, decimal maxValue = 100000000)
        {
            return new NumberControl(label, visibilityCallback, changedCallback, initialValue, decimalPlaces, increment, minValue, maxValue);
        }

        private void Num_ValueChanged(object sender, EventArgs e)
        {
            InvokeChangedCallback();
        }

        public override Label LabelControl
        {
            get { return lbLabel; }
        }

        public override Control Control
        {
            get { return num; }
        }

        public override object GetProperty()
        {
            return num.Value;
        }
    }
}
