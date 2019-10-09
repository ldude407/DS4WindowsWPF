﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using DS4Windows;

namespace DS4WinWPF.DS4Forms.ViewModel
{
    public class BindingWindowViewModel
    {
        private int deviceNum;
        private bool use360Mode;
        private MappedControl mappedControl;
        private OutBinding currentOutBind;
        private OutBinding shiftOutBind;
        private OutBinding actionBinding;
        private bool showShift;
        private bool rumbleActive;

        public bool Using360Mode
        {
            get => use360Mode;
        }
        public int DeviceNum { get => deviceNum; }
        public MappedControl MappedControl { get => mappedControl; }
        public OutBinding CurrentOutBind { get => currentOutBind; }
        public OutBinding ShiftOutBind { get => shiftOutBind; }
        public OutBinding ActionBinding
        {
            get => actionBinding;
            set
            {
                actionBinding = value;
            }
        }

        public bool ShowShift { get => showShift; set => showShift = value; }
        public bool RumbleActive { get => rumbleActive; set => rumbleActive = value; }

        public BindingWindowViewModel(int deviceNum, MappedControl mappedControl)
        {
            this.deviceNum = deviceNum;
            use360Mode = Global.outDevTypeTemp[deviceNum] == OutContType.X360;
            this.mappedControl = mappedControl;
            currentOutBind = new OutBinding();
            shiftOutBind = new OutBinding();
            shiftOutBind.shiftBind = true;
            PopulateCurrentBinds();
        }

        private void PopulateCurrentBinds()
        {
            DS4ControlSettings setting = mappedControl.Setting;
            bool sc = setting.keyType.HasFlag(DS4KeyType.ScanCode);
            currentOutBind.input = setting.control;
            shiftOutBind.input = setting.control;
            if (setting.action != null)
            {
                switch(setting.actionType)
                {
                    case DS4ControlSettings.ActionType.Button:
                        currentOutBind.outputType = OutBinding.OutType.Button;
                        currentOutBind.control = (X360Controls)setting.action;
                        break;
                    case DS4ControlSettings.ActionType.Default:
                        currentOutBind.outputType = OutBinding.OutType.Default;
                        break;
                    case DS4ControlSettings.ActionType.Key:
                        currentOutBind.outputType = OutBinding.OutType.Key;
                        currentOutBind.outkey = (int)setting.action;
                        currentOutBind.hasScanCode = sc;

                        break;
                    case DS4ControlSettings.ActionType.Macro:
                        currentOutBind.outputType = OutBinding.OutType.Macro;
                        currentOutBind.macro = (int[])setting.action;
                        break;
                }
            }
            else
            {
                currentOutBind.outputType = OutBinding.OutType.Default;
            }

            if (!string.IsNullOrEmpty(setting.extras))
            {
                currentOutBind.ParseExtras(setting.extras);
            }

            if (setting.shiftAction != null)
            {
                sc = setting.shiftKeyType.HasFlag(DS4KeyType.ScanCode);
                switch (setting.shiftAction)
                {
                    case DS4ControlSettings.ActionType.Button:
                        shiftOutBind.shiftBind = true;
                        shiftOutBind.shiftTrigger = setting.shiftTrigger;
                        shiftOutBind.outputType = OutBinding.OutType.Button;
                        shiftOutBind.control = (X360Controls)setting.shiftAction;
                        break;
                    case DS4ControlSettings.ActionType.Default:
                        shiftOutBind.shiftBind = true;
                        shiftOutBind.shiftTrigger = setting.shiftTrigger;
                        shiftOutBind.outputType = OutBinding.OutType.Default;
                        break;
                    case DS4ControlSettings.ActionType.Key:
                        shiftOutBind.shiftBind = true;
                        shiftOutBind.shiftTrigger = setting.shiftTrigger;
                        shiftOutBind.outputType = OutBinding.OutType.Key;
                        shiftOutBind.outkey = (int)setting.shiftAction;
                        shiftOutBind.hasScanCode = sc;
                        break;
                    case DS4ControlSettings.ActionType.Macro:
                        shiftOutBind.shiftBind = true;
                        shiftOutBind.shiftTrigger = setting.shiftTrigger;
                        shiftOutBind.outputType = OutBinding.OutType.Macro;
                        shiftOutBind.macro = (int[])setting.shiftAction;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(setting.shiftExtras))
            {
                shiftOutBind.ParseExtras(setting.shiftExtras);
            }
        }
    }

    public class BindAssociation
    {
        public enum OutType : uint
        {
            Default,
            Key,
            Button,
            Macro
        }

        public OutType outputType;
        public X360Controls control;
        public int outkey;

        public bool IsMouse()
        {
            return outputType == OutType.Button && (control >= X360Controls.LeftMouse && control < X360Controls.Unbound);
        }

        public static bool IsMouseRange(X360Controls control)
        {
            return control >= X360Controls.LeftMouse && control < X360Controls.Unbound;
        }
    }

    public class OutBinding
    {
        public enum OutType : uint
        {
            Default,
            Key,
            Button,
            Macro
        }

        public DS4Controls input;
        public bool toggle;
        public bool hasScanCode;
        public OutType outputType;
        public int outkey;
        public int[] macro;
        public X360Controls control;
        public bool shiftBind;
        public int shiftTrigger;
        private int heavyRumble = 255;
        private int lightRumble = 255;
        private int flashRate;
        private int mouseSens = 25;
        private DS4Color extrasColor = new DS4Color(255,255,255);

        public bool HasScanCode { get => hasScanCode; }
        public bool Toggle { get => toggle; }
        public int ShiftTrigger { get => shiftTrigger; }
        public int HeavyRumble { get => heavyRumble; set => heavyRumble = value; }
        public int LightRumble { get => lightRumble; set => lightRumble = value; }
        public int FlashRate
        {
            get => flashRate;
            set
            {
                flashRate = value;
                FlashRateChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler FlashRateChanged;

        public bool UseFlashRate
        {
            get
            {
                return flashRate != 0;
            }
        }
        public event EventHandler UseFlashRateChanged;
        public int MouseSens
        {
            get => mouseSens;
            set
            {
                mouseSens = value;
                MouseSensChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler MouseSensChanged;

        private bool useMouseSens;
        public bool UseMouseSens
        {
            get
            {
                return useMouseSens;
            }
            set
            {
                useMouseSens = value;
                UseMouseSensChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler UseMouseSensChanged;

        private bool useExtrasColor;
        public bool UseExtrasColor {
            get
            {
                return useExtrasColor;
            }
            set
            {
                useExtrasColor = value;
                UseExtrasColorChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler UseExtrasColorChanged;

        public int ExtrasColorR
        {
            get => extrasColor.red;
            set
            {
                extrasColor.red = (byte)value;
                ExtrasColorRChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler ExtrasColorRChanged;

        public string ExtrasColorRString
        {
            get
            {
                string temp = $"#{extrasColor.red.ToString("X2")}FF0000";
                return temp;
            }
        }
        public event EventHandler ExtrasColorRStringChanged;
        public int ExtrasColorG
        {
            get => extrasColor.green;
            set
            {
                extrasColor.green = (byte)value;
                ExtrasColorGChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler ExtrasColorGChanged;

        public string ExtrasColorGString
        {
            get
            {
                string temp = $"#{ extrasColor.green.ToString("X2")}00FF00";
                return temp;
            }
        }
        public event EventHandler ExtrasColorGStringChanged;

        public int ExtrasColorB
        {
            get => extrasColor.blue;
            set
            {
                extrasColor.blue = (byte)value;
                ExtrasColorBChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler ExtrasColorBChanged;

        public string ExtrasColorBString
        {
            get
            {
                string temp = $"#{extrasColor.blue.ToString("X2")}0000FF";
                return temp;
            }
        }
        public event EventHandler ExtrasColorBStringChanged;

        private int shiftTriggerIndex;
        public int ShiftTriggerIndex { get => shiftTriggerIndex; set => shiftTriggerIndex = value; }

        public string DefaultColor
        {
            get
            {
                string color;
                if (outputType == OutType.Default)
                {
                    color =  Colors.LimeGreen.ToString();
                }
                else
                {
                    color = SystemColors.ControlBrush.Color.ToString();
                }

                return color;
            }
        }

        public string UnboundColor
        {
            get
            {
                string color;
                if (outputType == OutType.Button && control == X360Controls.Unbound)
                {
                    color = Colors.LimeGreen.ToString();
                }
                else
                {
                    color = SystemColors.ControlBrush.Color.ToString();
                }

                return color;
            }
        }

        public OutBinding()
        {
            ExtrasColorRChanged += OutBinding_ExtrasColorRChanged;
            ExtrasColorGChanged += OutBinding_ExtrasColorGChanged;
            ExtrasColorBChanged += OutBinding_ExtrasColorBChanged;
            FlashRateChanged += (sender, e) =>
            {
                UseFlashRateChanged?.Invoke(this, EventArgs.Empty);
            };
            MouseSensChanged += (sender, e) =>
            {
                UseMouseSens = mouseSens != 0;
            };
            UseExtrasColorChanged += OutBinding_UseExtrasColorChanged;
        }

        private void OutBinding_ExtrasColorBChanged(object sender, EventArgs e)
        {
            ExtrasColorBStringChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OutBinding_ExtrasColorGChanged(object sender, EventArgs e)
        {
            ExtrasColorGStringChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OutBinding_ExtrasColorRChanged(object sender, EventArgs e)
        {
            ExtrasColorRStringChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OutBinding_UseExtrasColorChanged(object sender, EventArgs e)
        {
            if (!useExtrasColor)
            {
                ExtrasColorR = 255;
                ExtrasColorG = 255;
                ExtrasColorB = 255;
            }
        }

        public bool IsShift()
        {
            return shiftBind;
        }

        public bool IsMouse()
        {
            return outputType == OutType.Button && (control >= X360Controls.LeftMouse && control < X360Controls.Unbound);
        }

        public static bool IsMouseRange(X360Controls control)
        {
            return control >= X360Controls.LeftMouse && control < X360Controls.Unbound;
        }

        public void ParseExtras(string extras)
        {
            string[] temp = extras.Split(',');
            if (temp.Length == 9)
            {
                int.TryParse(temp[0], out heavyRumble);
                int.TryParse(temp[1], out lightRumble);
                int.TryParse(temp[2], out int useColor);
                if (useColor == 1)
                {
                    byte.TryParse(temp[3], out extrasColor.red);
                    byte.TryParse(temp[4], out extrasColor.green);
                    byte.TryParse(temp[5], out extrasColor.blue);
                    int.TryParse(temp[6], out flashRate);
                }
                else
                {
                    extrasColor.red = extrasColor.green = extrasColor.blue = 255;
                    flashRate = 0;
                }

                int.TryParse(temp[7], out int useM);
                if (useM == 1)
                {
                    useMouseSens = true;
                    int.TryParse(temp[8], out mouseSens);
                }
                else
                {
                    useMouseSens = false;
                    mouseSens = 25;
                }
            }
        }

        public string CompileExtras()
        {
            string result = $"{heavyRumble},{lightRumble},{(useExtrasColor ? "1" : "0")},{extrasColor.red},{extrasColor.green},{extrasColor.blue},{flashRate},{(useMouseSens ? "1" : "0")},{mouseSens}";
            return result;
        }
    }
}
