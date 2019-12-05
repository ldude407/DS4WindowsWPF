﻿using DS4Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DS4WinWPF.DS4Forms.ViewModels
{
    public class ProfileSettingsViewModel
    {
        private int device;
        public int Device { get => device; }

        private int funcDevNum;
        public int FuncDevNum { get => funcDevNum; }

        public System.Windows.Media.Color MainColor
        {
            get
            {
                ref DS4Color color = ref Global.MainColor[device];
                return new System.Windows.Media.Color()
                {
                    A = 255,
                    R = color.red,
                    G = color.green,
                    B = color.blue
                };
            }
        }
        public event EventHandler MainColorChanged;

        public string MainColorString
        {
            get
            {
                ref DS4Color color = ref Global.MainColor[device];
                return $"#FF{color.red.ToString("X2")}{color.green.ToString("X2")}{color.blue.ToString("X2")}";
                /*return new System.Windows.Media.Color()
                {
                    A = 255,
                    R = color.red,
                    G = color.green,
                    B = color.blue
                }.ToString();
                */
            }
        }
        public event EventHandler MainColorStringChanged;

        public int MainColorR
        {
            get => Global.MainColor[device].red;
            set
            {
                Global.MainColor[device].red = (byte)value;
                MainColorRChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler MainColorRChanged;

        public string MainColorRString
        {
            get => $"#{ Global.MainColor[device].red.ToString("X2")}FF0000";
        }
        public event EventHandler MainColorRStringChanged;

        public int MainColorG
        {
            get => Global.MainColor[device].green;
            set
            {
                Global.MainColor[device].green = (byte)value;
                MainColorGChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler MainColorGChanged;

        public string MainColorGString
        {
            get => $"#{ Global.MainColor[device].green.ToString("X2")}00FF00";
        }
        public event EventHandler MainColorGStringChanged;

        public int MainColorB
        {
            get => Global.MainColor[device].blue;
            set
            {
                Global.MainColor[device].blue = (byte)value;
                MainColorBChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler MainColorBChanged;

        public string MainColorBString
        {
            get => $"#{ Global.MainColor[device].blue.ToString("X2")}0000FF";
        }
        public event EventHandler MainColorBStringChanged;

        public string LowColor
        {
            get
            {
                ref DS4Color color = ref Global.LowColor[device];
                return $"#FF{color.red.ToString("X2")}{color.green.ToString("X2")}{color.blue.ToString("X2")}";
            }
        }
        public event EventHandler LowColorChanged;

        public int LowColorR
        {
            get => Global.LowColor[device].red;
            set
            {
                Global.LowColor[device].red = (byte)value;
                LowColorRChanged?.Invoke(this, EventArgs.Empty);
                LowColorRStringChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler LowColorRChanged;

        public string LowColorRString
        {
            get => $"#{ Global.LowColor[device].red.ToString("X2")}FF0000";
        }
        public event EventHandler LowColorRStringChanged;

        public int LowColorG
        {
            get => Global.LowColor[device].green;
            set
            {
                Global.LowColor[device].green = (byte)value;
                LowColorGChanged?.Invoke(this, EventArgs.Empty);
                LowColorGStringChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler LowColorGChanged;

        public string LowColorGString
        {
            get => $"#{ Global.LowColor[device].green.ToString("X2")}00FF00";
        }
        public event EventHandler LowColorGStringChanged;

        public int LowColorB
        {
            get => Global.LowColor[device].blue;
            set
            {
                Global.LowColor[device].blue = (byte)value;
                LowColorBChanged?.Invoke(this, EventArgs.Empty);
                LowColorBStringChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler LowColorBChanged;

        public string LowColorBString
        {
            get => $"#{ Global.LowColor[device].blue.ToString("X2")}0000FF";
        }
        public event EventHandler LowColorBStringChanged;

        public System.Windows.Media.Color LowColorMedia
        {
            get
            {
                ref DS4Color color = ref Global.LowColor[device];
                return new System.Windows.Media.Color()
                {
                    A = 255,
                    R = color.red,
                    B = color.blue,
                    G = color.green
                };
            }
        }

        public int FlashTypeIndex
        {
            get => Global.FlashType[device];
            set => Global.FlashType[device] = (byte)value;
        }

        public int FlashAt
        {
            get => Global.FlashAt[device];
            set => Global.FlashAt[device] = value;
        }

        public string FlashColor
        {
            get
            {
                ref DS4Color color = ref Global.FlashColor[device];
                if (color.red == 0 && color.green == 0 && color.blue == 0)
                {
                    color = ref Global.MainColor[device];
                }

                return $"#FF{color.red.ToString("X2")}{color.green.ToString("X2")}{color.blue.ToString("X2")}";
            }
        }
        public event EventHandler FlashColorChanged;

        public System.Windows.Media.Color FlashColorMedia
        {
            get
            {
                ref DS4Color color = ref Global.FlashColor[device];
                if (color.red == 0 && color.green == 0 && color.blue == 0)
                {
                    color = ref Global.MainColor[device];
                }

                return new System.Windows.Media.Color()
                {
                    A = 255,
                    R = color.red,
                    B = color.blue,
                    G = color.green
                };
            }
        }

        public int ChargingType
        {
            get => Global.ChargingType[device];
            set
            {
                Global.ChargingType[device] = value;
                ChargingColorVisibleChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool ColorBatteryPercent
        {
            get => Global.LedAsBatteryIndicator[device];
            set
            {
                Global.LedAsBatteryIndicator[device] = value;
            }
        }

        public string ChargingColor
        {
            get
            {
                ref DS4Color color = ref Global.ChargingColor[device];
                return $"#FF{color.red.ToString("X2")}{color.green.ToString("X2")}{color.blue.ToString("X2")}";
            }
        }
        public event EventHandler ChargingColorChanged;

        public System.Windows.Media.Color ChargingColorMedia
        {
            get
            {
                ref DS4Color color = ref Global.ChargingColor[device];
                return new System.Windows.Media.Color()
                {
                    A = 255,
                    R = color.red,
                    B = color.blue,
                    G = color.green
                };
            }
        }

        public Visibility ChargingColorVisible
        {
            get => Global.ChargingType[device] == 3 ? Visibility.Visible : Visibility.Hidden;
        }
        public event EventHandler ChargingColorVisibleChanged;

        public double Rainbow
        {
            get => Global.Rainbow[device];
            set
            {
                Global.Rainbow[device] = value;
                RainbowChanged?.Invoke(this, EventArgs.Empty);
                RainbowExistsChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler RainbowChanged;

        public bool RainbowExists
        {
            get => Global.Rainbow[device] != 0.0;
        }

        public event EventHandler RainbowExistsChanged;

        public double MaxSatRainbow
        {
            get => Global.MaxSatRainbow[device] * 100.0;
            set => Global.MaxSatRainbow[device] = value / 100.0;
        }

        public int RumbleBoost
        {
            get => Global.RumbleBoost[device];
            set => Global.RumbleBoost[device] = (byte)value;
        }

        private bool heavyRumbleActive;
        public bool HeavyRumbleActive
        {
            get => heavyRumbleActive;
            set
            {
                heavyRumbleActive = value;
                HeavyRumbleActiveChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler HeavyRumbleActiveChanged;

        private bool lightRumbleActive;
        public bool LightRumbleActive
        {
            get => lightRumbleActive;
            set
            {
                lightRumbleActive = value;
                LightRumbleActiveChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler LightRumbleActiveChanged;

        public bool UseControllerReadout
        {
            get => Global.DS4Mapping;
            set => Global.DS4Mapping = value;
        }

        public bool MouseAcceleration
        {
            get => Global.MouseAccel[device];
            set => Global.MouseAccel[device] = value;
        }

        public bool EnableTouchpadToggle
        {
            get => Global.EnableTouchToggle[device];
            set => Global.EnableTouchToggle[device] = value;
        }

        public bool LaunchProgramExists
        {
            get => !string.IsNullOrEmpty(Global.LaunchProgram[device]);
            set
            {
                if (!value) ResetLauchProgram();
            }
        }
        public event EventHandler LaunchProgramExistsChanged;

        public string LaunchProgram
        {
            get => Global.LaunchProgram[device];
        }
        public event EventHandler LaunchProgramChanged;

        public string LaunchProgramName
        {
            get
            {
                string temp = Global.LaunchProgram[device];
                if (!string.IsNullOrEmpty(temp))
                {
                    temp = Path.GetFileNameWithoutExtension(temp);
                }
                else
                {
                    temp = "Browse";
                }

                return temp;
            }
        }
        public event EventHandler LaunchProgramNameChanged;

        public ImageSource LaunchProgramIcon
        {
            get
            {
                ImageSource exeicon = null;
                string path = Global.LaunchProgram[device];
                if (File.Exists(path) && Path.GetExtension(path) == ".exe")
                {
                    using (Icon ico = Icon.ExtractAssociatedIcon(path))
                    {
                        exeicon = Imaging.CreateBitmapSourceFromHIcon(ico.Handle, Int32Rect.Empty,
                            BitmapSizeOptions.FromEmptyOptions());
                        exeicon.Freeze();
                    }
                }

                return exeicon;
            }
        }
        public event EventHandler LaunchProgramIconChanged;

        public bool DInputOnly
        {
            get => Global.DinputOnly[device];
            set => Global.DinputOnly[device] = value;
        }

        public bool FlushHid
        {
            get => Global.FlushHIDQueue[device];
            set => Global.FlushHIDQueue[device] = value;
        }

        public bool IdleDisconnectExists
        {
            get => Global.IdleDisconnectTimeout[device] != 0;
            set
            {
                Global.IdleDisconnectTimeout[device] = value ? 5 : 0;
                IdleDisconnectChanged?.Invoke(this, EventArgs.Empty);
                IdleDisconnectExistsChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler IdleDisconnectExistsChanged;

        public int IdleDisconnect
        {
            get => Global.IdleDisconnectTimeout[device];
            set
            {
                int temp = Global.IdleDisconnectTimeout[device];
                if (temp == value) return;
                Global.IdleDisconnectTimeout[device] = value;
                IdleDisconnectChanged?.Invoke(this, EventArgs.Empty);
                IdleDisconnectExistsChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler IdleDisconnectChanged;

        private int tempBtPollRate;
        public int TempBTPollRateIndex
        {
            get => tempBtPollRate;
            set => tempBtPollRate = value;
        }

        public int ControllerTypeIndex
        {
            get
            {
                int type = 0;
                switch (Global.OutContType[device])
                {
                    case OutContType.X360:
                        type = 0;
                        break;

                    case OutContType.DS4:
                        type = 1;
                        break;

                    default: break;
                }

                return type;
            }
        }

        private int tempControllerIndex;
        public int TempControllerIndex
        {
            get => tempControllerIndex; set
            {
                tempControllerIndex = value;
                Global.outDevTypeTemp[device] = TempConType;
            }
        }

        public OutContType TempConType
        {
            get
            {
                OutContType result = OutContType.None;
                switch (tempControllerIndex)
                {
                    case 0:
                        result = OutContType.X360; break;
                    case 1:
                        result = OutContType.DS4; break;
                    default: result = OutContType.X360; break;
                }
                return result;
            }
        }

        public int GyroOutModeIndex
        {
            get
            {
                int index = 0;
                switch (Global.GyroOutputMode[device])
                {
                    case GyroOutMode.Controls:
                        index = 0; break;
                    case GyroOutMode.Mouse:
                        index = 1; break;
                    case GyroOutMode.MouseJoystick:
                        index = 2; break;
                    default: break;
                }

                return index;
            }
            set
            {
                GyroOutMode temp = GyroOutMode.Controls;
                switch(value)
                {
                    case 0: break;
                    case 1:
                        temp = GyroOutMode.Mouse; break;
                    case 2:
                        temp = GyroOutMode.MouseJoystick; break;
                    default: break;
                }

                Global.GyroOutputMode[device] = temp;
            }
        }


        public OutContType ContType
        {
            get => Global.OutContType[device];
        }

        public int SASteeringWheelEmulationAxisIndex
        {
            get => (int)Global.SASteeringWheelEmulationAxis[device];
            set => Global.SASteeringWheelEmulationAxis[device] = (SASteeringWheelEmulationAxisType)value;
        }

        private int[] saSteeringRangeValues =
            new int[9] { 90, 180, 270, 360, 450, 720, 900, 1080, 1440 };
        public int SASteeringWheelEmulationRangeIndex
        {
            get
            {
                int index = 360;
                switch(Global.SASteeringWheelEmulationRange[device])
                {
                    case 90:
                        index = 0; break;
                    case 180:
                        index = 1; break;
                    case 270:
                        index = 2; break;
                    case 360:
                        index = 3; break;
                    case 450:
                        index = 4; break;
                    case 720:
                        index = 5; break;
                    case 900:
                        index = 6; break;
                    case 1080:
                        index = 7; break;
                    case 1440:
                        index = 8; break;
                    default: break;
                }

                return index;
            }
            set
            {
                int temp = saSteeringRangeValues[value];
                Global.SASteeringWheelEmulationRange[device] = temp;
            }
        }

        public int SASteeringWheelEmulationRange
        {
            get => Global.SASteeringWheelEmulationRange[device];
            set => Global.SASteeringWheelEmulationRange[device] = value;
        }

        public double LSDeadZone
        {
            get => Math.Round(Global.LSModInfo[device].deadZone / 127d, 2);
            set
            {
                double temp = Global.LSModInfo[device].deadZone / 127d;
                if (temp == value) return;
                Global.LSModInfo[device].deadZone = (int)Math.Round(value * 127d);
                LSDeadZoneChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler LSDeadZoneChanged;

        public double RSDeadZone
        {
            get => Math.Round(Global.RSModInfo[device].deadZone / 127d, 2);
            set
            {
                double temp = Global.RSModInfo[device].deadZone / 127d;
                if (temp == value) return;
                Global.RSModInfo[device].deadZone = (int)Math.Round(value * 127d);
                RSDeadZoneChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler RSDeadZoneChanged;

        public double LSMaxZone
        {
            get => Global.LSModInfo[device].maxZone / 100.0;
            set => Global.LSModInfo[device].maxZone = (int)(value * 100.0);
        }

        public double RSMaxZone
        {
            get => Global.RSModInfo[device].maxZone / 100.0;
            set => Global.RSModInfo[device].maxZone = (int)(value * 100.0);
        }

        public double LSAntiDeadZone
        {
            get => Global.LSModInfo[device].antiDeadZone / 100.0;
            set => Global.LSModInfo[device].antiDeadZone = (int)(value * 100.0);
        }

        public double RSAntiDeadZone
        {
            get => Global.RSModInfo[device].antiDeadZone / 100.0;
            set => Global.RSModInfo[device].antiDeadZone = (int)(value * 100.0);
        }

        public double LSSens
        {
            get => Global.LSSens[device];
            set => Global.LSSens[device] = value;
        }

        public double RSSens
        {
            get => Global.RSSens[device];
            set => Global.RSSens[device] = value;
        }

        public bool LSSquareStick
        {
            get => Global.SquStickInfo[device].lsMode;
            set => Global.SquStickInfo[device].lsMode = value;
        }

        public bool RSSquareStick
        {
            get => Global.SquStickInfo[device].rsMode;
            set => Global.SquStickInfo[device].rsMode = value;
        }

        public double LSSquareRoundness
        {
            get => Global.SquStickInfo[device].lsRoundness;
            set => Global.SquStickInfo[device].lsRoundness = value;
        }

        public double RSSquareRoundness
        {
            get => Global.SquStickInfo[device].rsRoundness;
            set => Global.SquStickInfo[device].rsRoundness = value;
        }

        public int LSOutputCurveIndex
        {
            get => Global.getLsOutCurveMode(device);
            set
            {
                Global.setLsOutCurveMode(device, value);
                LSCustomCurveSelectedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public int RSOutputCurveIndex
        {
            get => Global.getRsOutCurveMode(device);
            set
            {
                Global.setRsOutCurveMode(device, value);
                RSCustomCurveSelectedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public int LSCurve
        {
            get => Global.LSCurve[device];
            set => Global.LSCurve[device] = value;
        }

        public int RSCurve
        {
            get => Global.RSCurve[device];
            set => Global.RSCurve[device] = value;
        }

        public double LSRotation
        {
            get => Global.LSRotation[device] * 180.0 / Math.PI;
            set => Global.LSRotation[device] = value * Math.PI / 180.0;
        }

        public double RSRotation
        {
            get => Global.RSRotation[device] * 180.0 / Math.PI;
            set => Global.RSRotation[device] = value * Math.PI / 180.0;
        }

        public bool LSCustomCurveSelected
        {
            get => Global.getLsOutCurveMode(device) == 6;
        }
        public event EventHandler LSCustomCurveSelectedChanged;

        public bool RSCustomCurveSelected
        {
            get => Global.getRsOutCurveMode(device) == 6;
        }
        public event EventHandler RSCustomCurveSelectedChanged;

        public string LSCustomCurve
        {
            get => Global.lsOutBezierCurveObj[device].CustomDefinition;
            set => Global.lsOutBezierCurveObj[device].InitBezierCurve(value, BezierCurve.AxisType.LSRS, true);
        }

        public string RSCustomCurve
        {
            get => Global.rsOutBezierCurveObj[device].CustomDefinition;
            set => Global.rsOutBezierCurveObj[device].InitBezierCurve(value, BezierCurve.AxisType.LSRS, true);
        }

        public double L2DeadZone
        {
            get => Global.L2ModInfo[device].deadZone / 255.0;
            set
            {
                double temp = Global.L2ModInfo[device].deadZone / 255.0;
                if (temp == value) return;
                Global.L2ModInfo[device].deadZone = (byte)(value * 255.0);
                L2DeadZoneChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler L2DeadZoneChanged;

        public double R2DeadZone
        {
            get => Global.R2ModInfo[device].deadZone / 255.0;
            set
            {
                double temp = Global.R2ModInfo[device].deadZone / 255.0;
                if (temp == value) return;
                Global.R2ModInfo[device].deadZone = (byte)(value * 255.0);
                R2DeadZoneChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler R2DeadZoneChanged;

        public double L2MaxZone
        {
            get => Global.L2ModInfo[device].maxZone / 100.0;
            set => Global.L2ModInfo[device].maxZone = (int)(value * 100.0);
        }

        public double R2MaxZone
        {
            get => Global.R2ModInfo[device].maxZone / 100.0;
            set => Global.R2ModInfo[device].maxZone = (int)(value * 100.0);
        }

        public double L2AntiDeadZone
        {
            get => Global.L2ModInfo[device].antiDeadZone / 100.0;
            set => Global.L2ModInfo[device].antiDeadZone = (int)(value * 100.0);
        }

        public double R2AntiDeadZone
        {
            get => Global.R2ModInfo[device].antiDeadZone / 100.0;
            set => Global.R2ModInfo[device].antiDeadZone = (int)(value * 100.0);
        }

        public double L2Sens
        {
            get => Global.L2Sens[device];
            set => Global.L2Sens[device] = value;
        }

        public double R2Sens
        {
            get => Global.R2Sens[device];
            set => Global.R2Sens[device] = value;
        }

        public int L2OutputCurveIndex
        {
            get => Global.getL2OutCurveMode(device);
            set
            {
                Global.setL2OutCurveMode(device, value);
                L2CustomCurveSelectedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public int R2OutputCurveIndex
        {
            get => Global.getR2OutCurveMode(device);
            set
            {
                Global.setR2OutCurveMode(device, value);
                R2CustomCurveSelectedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool L2CustomCurveSelected
        {
            get => Global.getL2OutCurveMode(device) == 6;
        }
        public event EventHandler L2CustomCurveSelectedChanged;

        public bool R2CustomCurveSelected
        {
            get => Global.getR2OutCurveMode(device) == 6;
        }
        public event EventHandler R2CustomCurveSelectedChanged;

        public string L2CustomCurve
        {
            get => Global.l2OutBezierCurveObj[device].CustomDefinition;
            set => Global.l2OutBezierCurveObj[device].InitBezierCurve(value, BezierCurve.AxisType.L2R2, true);
        }

        public string R2CustomCurve
        {
            get => Global.r2OutBezierCurveObj[device].CustomDefinition;
            set => Global.r2OutBezierCurveObj[device].InitBezierCurve(value, BezierCurve.AxisType.L2R2, true);
        }

        public double SXDeadZone
        {
            get => Global.SXDeadzone[device];
            set
            {
                double temp = Global.SXDeadzone[device];
                if (temp == value) return;
                Global.SXDeadzone[device] = value;
                SXDeadZoneChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler SXDeadZoneChanged;

        public double SZDeadZone
        {
            get => Global.SZDeadzone[device];
            set
            {
                double temp = Global.SZDeadzone[device];
                if (temp == value) return;
                Global.SZDeadzone[device] = value;
                SZDeadZoneChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler SZDeadZoneChanged;

        public double SXMaxZone
        {
            get => Global.SXMaxzone[device];
            set => Global.SXMaxzone[device] = value;
        }

        public double SZMaxZone
        {
            get => Global.SZMaxzone[device];
            set => Global.SZMaxzone[device] = value;
        }

        public double SXAntiDeadZone
        {
            get => Global.SXAntiDeadzone[device];
            set => Global.SXAntiDeadzone[device] = value;
        }

        public double SZAntiDeadZone
        {
            get => Global.SZAntiDeadzone[device];
            set => Global.SZAntiDeadzone[device] = value;
        }

        public double SXSens
        {
            get => Global.SXSens[device];
            set => Global.SXSens[device] = value;
        }

        public double SZSens
        {
            get => Global.SZSens[device];
            set => Global.SZSens[device] = value;
        }

        public int SXOutputCurveIndex
        {
            get => Global.getSXOutCurveMode(device);
            set
            {
                Global.setSXOutCurveMode(device, value);
                SXCustomCurveSelectedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public int SZOutputCurveIndex
        {
            get => Global.getSZOutCurveMode(device);
            set
            {
                Global.setSZOutCurveMode(device, value);
                SZCustomCurveSelectedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool SXCustomCurveSelected
        {
            get => Global.getSXOutCurveMode(device) == 6;
        }
        public event EventHandler SXCustomCurveSelectedChanged;

        public bool SZCustomCurveSelected
        {
            get => Global.getSZOutCurveMode(device) == 6;
        }
        public event EventHandler SZCustomCurveSelectedChanged;

        public string SXCustomCurve
        {
            get => Global.sxOutBezierCurveObj[device].CustomDefinition;
            set => Global.sxOutBezierCurveObj[device].InitBezierCurve(value, BezierCurve.AxisType.SA, true);
        }

        public string SZCustomCurve
        {
            get => Global.szOutBezierCurveObj[device].CustomDefinition;
            set => Global.szOutBezierCurveObj[device].InitBezierCurve(value, BezierCurve.AxisType.SA, true);
        }

        public bool UseTouchMouse
        {
            get => !Global.UseTPforControls[device];
            set
            {
                bool temp = !Global.UseTPforControls[device];
                if (temp == value) return;
                Global.UseTPforControls[device] = !value;
                UseTouchMouseChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler UseTouchMouseChanged;

        public bool UseTouchControls
        {
            get => Global.UseTPforControls[device];
            set
            {
                bool temp = Global.UseTPforControls[device];
                if (temp == value) return;
                Global.UseTPforControls[device] = value;
                UseTouchControlsChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler UseTouchControlsChanged;

        public bool TouchSenExists
        {
            get => Global.TouchSensitivity[device] != 0;
            set
            {
                Global.TouchSensitivity[device] = value ? (byte)100 : (byte)0;
                TouchSenExistsChanged?.Invoke(this, EventArgs.Empty);
                TouchSensChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler TouchSenExistsChanged;

        public int TouchSens
        {
            get => Global.TouchSensitivity[device];
            set
            {
                int temp = Global.TouchSensitivity[device];
                if (temp == value) return;
                Global.TouchSensitivity[device] = (byte)value;
                if (value == 0) TouchSenExistsChanged?.Invoke(this, EventArgs.Empty);
                TouchSensChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler TouchSensChanged;

        public bool TouchScrollExists
        {
            get => Global.ScrollSensitivity[device] != 0;
            set
            {
                Global.ScrollSensitivity[device] = value ? (byte)100 : (byte)0;
                TouchScrollExistsChanged?.Invoke(this, EventArgs.Empty);
                TouchScrollChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler TouchScrollExistsChanged;

        public int TouchScroll
        {
            get => Global.ScrollSensitivity[device];
            set
            {
                int temp = Global.ScrollSensitivity[device];
                if (temp == value) return;
                Global.ScrollSensitivity[device] = value;
                if (value == 0) TouchScrollExistsChanged?.Invoke(this, EventArgs.Empty);
                TouchScrollChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler TouchScrollChanged;

        public bool TouchTapExists
        {
            get => Global.TapSensitivity[device] != 0;
            set
            {
                Global.TapSensitivity[device] = value ? (byte)100 : (byte)0;
                TouchTapExistsChanged?.Invoke(this, EventArgs.Empty);
                TouchTapChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler TouchTapExistsChanged;

        public int TouchTap
        {
            get => Global.TapSensitivity[device];
            set
            {
                int temp = Global.TapSensitivity[device];
                if (temp == value) return;
                Global.TapSensitivity[device] = (byte)value;
                if (value == 0) TouchTapExistsChanged?.Invoke(this, EventArgs.Empty);
                TouchTapChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler TouchTapChanged;

        public bool TouchDoubleTap
        {
            get => Global.DoubleTap[device];
            set
            {
                Global.DoubleTap[device] = value;
            }
        }
        
        public bool TouchJitter
        {
            get => Global.TouchpadJitterCompensation[device];
            set => Global.TouchpadJitterCompensation[device] = value;
        }

        private int[] touchpadInvertToValue = new int[4] { 0, 2, 1, 3 };
        public int TouchInvertIndex
        {
            get
            {
                int invert = Global.TouchpadInvert[device];
                int index = Array.IndexOf(touchpadInvertToValue, invert);
                return index;
            }
            set
            {
                int invert = touchpadInvertToValue[value];
                Global.TouchpadInvert[device] = invert;
            }
        }

        public bool LowerRightTouchRMB
        {
            get => Global.LowerRCOn[device];
            set
            {
                Global.LowerRCOn[device] = value;
            }
        }

        public bool StartTouchpadOff
        {
            get => Global.StartTouchpadOff[device];
            set
            {
                Global.StartTouchpadOff[device] = value;
            }
        }

        public bool TouchTrackball
        {
            get => Global.TrackballMode[device];
            set => Global.TrackballMode[device] = value;
        }

        public double TouchTrackballFriction
        {
            get => Global.TrackballFriction[device];
            set => Global.TrackballFriction[device] = value;
        }


        public bool GyroMouseTurns
        {
            get => Global.GyroMouseStickTriggerTurns[device];
            set => Global.GyroMouseStickTriggerTurns[device] = value;
        }

        public int GyroSensitivity
        {
            get => Global.GyroSensitivity[device];
            set => Global.GyroSensitivity[device] = value;
        }

        public int GyroVertScale
        {
            get => Global.GyroSensVerticalScale[device];
            set => Global.GyroSensVerticalScale[device] = value;
        }

        public int GyroMouseEvalCondIndex
        {
            get => Global.getSATriggerCond(device) ? 0 : 1;
            set => Global.SetSaTriggerCond(device, value == 0 ? "and" : "or");
        }

        public int GyroMouseXAxis
        {
            get => Global.GyroMouseHorizontalAxis[device];
            set => Global.GyroMouseHorizontalAxis[device] = value;
        }

        public bool GyroMouseInvertX
        {
            get => (Global.GyroInvert[device] & 2) == 2;
            set
            {
                if (value)
                {
                    Global.GyroInvert[device] |= 2;
                }
                else
                {
                    Global.GyroInvert[device] &= ~2;
                }
            }
        }

        public bool GyroMouseInvertY
        {
            get => (Global.GyroInvert[device] & 1) == 1;
            set
            {
                if (value)
                {
                    Global.GyroInvert[device] |= 1;
                }
                else
                {
                    Global.GyroInvert[device] &= ~1;
                }
            }
        }

        public bool GyroMouseSmooth
        {
            get => Global.GyroSmoothing[device];
            set => Global.GyroSmoothing[device] = value;
        }

        public double GyroMouseSmoothWeight
        {
            get => Global.GyroSmoothingWeight[device];
            set => Global.GyroSmoothingWeight[device] = value;
        }

        public int GyroMouseDeadZone
        {
            get => Global.GyroMouseDeadZone[device];
            set
            {
                Global.SetGyroMouseDeadZone(device, value, App.rootHub);

            }
        }

        public bool GyroMouseToggle
        {
            get => Global.GyroMouseToggle[device];
            set
            {
                Global.SetGyroMouseToggle(device, value, App.rootHub);
            }
        }

        public bool GyroMouseStickTurns
        {
            get => Global.GyroMouseStickTriggerTurns[device];
            set
            {
                Global.GyroMouseStickTriggerTurns[device] = value;
            }
        }

        public bool GyroMouseStickToggle
        {
            get => Global.GyroMouseStickToggle[device];
            set
            {
                Global.SetGyroMouseStickToggle(device, value, App.rootHub);
            }
        }

        public int GyroMouseStickDeadZone
        {
            get => Global.GyroMouseStickInf[device].deadZone;
            set => Global.GyroMouseStickInf[device].deadZone = value;
        }

        public int GyroMouseStickMaxZone
        {
            get => Global.GyroMouseStickInf[device].maxZone;
            set => Global.GyroMouseStickInf[device].maxZone = value;
        }

        public double GyroMouseStickAntiDeadX
        {
            get => Global.GyroMouseStickInf[device].antiDeadX;
            set => Global.GyroMouseStickInf[device].antiDeadX = value;
        }

        public double GyroMouseStickAntiDeadY
        {
            get => Global.GyroMouseStickInf[device].antiDeadY;
            set => Global.GyroMouseStickInf[device].antiDeadY = value;
        }

        public int GyroMouseStickVertScale
        {
            get => Global.GyroMouseStickInf[device].vertScale;
            set => Global.GyroMouseStickInf[device].vertScale = value;
        }

        public int GyroMouseStickEvalCondIndex
        {
            get => Global.GetSAMouseStickTriggerCond(device) ? 0 : 1;
            set => Global.SetSaMouseStickTriggerCond(device, value == 0 ? "and" : "or");
        }

        public int GyroMouseStickXAxis
        {
            get => Global.GyroMouseStickHorizontalAxis[device];
            set => Global.GyroMouseStickHorizontalAxis[device] = value;
        }

        public bool GyroMouseStickInvertX
        {
            get => (Global.GyroMouseStickInf[device].inverted & 2) == 2;
            set
            {
                if (value)
                {
                    Global.GyroMouseStickInf[device].inverted |= 2;
                }
                else
                {
                    Global.GyroMouseStickInf[device].inverted &= 1;
                }
            }
        }

        public bool GyroMouseStickInvertY
        {
            get => (Global.GyroInvert[device] & 1) == 1;
            set
            {
                if (value)
                {
                    Global.GyroInvert[device] |= 1;
                }
                else
                {
                    Global.GyroInvert[device] &= 2;
                }
            }
        }

        public bool GyroMouseStickSmooth
        {
            get => Global.GyroMouseStickInf[device].useSmoothing;
            set => Global.GyroMouseStickInf[device].useSmoothing = value;
        }

        public double GyroMousetickSmoothWeight
        {
            get => Global.GyroMouseStickInf[device].smoothWeight;
            set => Global.GyroMouseStickInf[device].smoothWeight = value;
        }
        
        private string touchDisInvertString = "None";
        public string TouchDisInvertString
        {
            get => touchDisInvertString;
            set
            {
                touchDisInvertString = value;
                TouchDisInvertStringChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        
        public event EventHandler TouchDisInvertStringChanged;

        private string gyroMouseTrigDisplay = "Always On";
        public string GyroMouseTrigDisplay
        {
            get => gyroMouseTrigDisplay;
            set
            {
                gyroMouseTrigDisplay = value;
                GyroMouseTrigDisplayChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler GyroMouseTrigDisplayChanged;

        private string gyroMouseStickTrigDisplay = "Always On";
        public string GyroMouseStickTrigDisplay
        {
            get => gyroMouseStickTrigDisplay;
            set
            {
                gyroMouseStickTrigDisplay = value;
                GyroMouseStickTrigDisplayChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler GyroMouseStickTrigDisplayChanged;

        public ProfileSettingsViewModel(int device)
        {
            this.device = device;
            funcDevNum = device < 4 ? device : 0;
            tempControllerIndex = ControllerTypeIndex;
            Global.outDevTypeTemp[device] = OutContType.X360;
            tempBtPollRate = Global.BTPollRate[device];

            MainColorChanged += ProfileSettingsViewModel_MainColorChanged;
            MainColorRChanged += (sender, args) =>
            {
                MainColorRStringChanged?.Invoke(this, EventArgs.Empty);
                MainColorStringChanged?.Invoke(this, EventArgs.Empty);
            };
            MainColorGChanged += (sender, args) =>
            {
                MainColorGStringChanged?.Invoke(this, EventArgs.Empty);
                MainColorStringChanged?.Invoke(this, EventArgs.Empty);
            };
            MainColorBChanged += (sender, args) =>
            {
                MainColorBStringChanged?.Invoke(this, EventArgs.Empty);
                MainColorStringChanged?.Invoke(this, EventArgs.Empty);
            };
        }

        private void ProfileSettingsViewModel_MainColorChanged(object sender, EventArgs e)
        {
            MainColorStringChanged?.Invoke(this, EventArgs.Empty);
            MainColorRChanged?.Invoke(this, EventArgs.Empty);
            MainColorGChanged?.Invoke(this, EventArgs.Empty);
            MainColorBChanged?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateFlashColor(System.Windows.Media.Color color)
        {
            Global.FlashColor[device] = new DS4Color() { red = color.R, green = color.G, blue = color.B };
            FlashColorChanged?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateMainColor(System.Windows.Media.Color color)
        {
            Global.MainColor[device] = new DS4Color() { red = color.R, green = color.G, blue = color.B };
            MainColorChanged?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateLowColor(System.Windows.Media.Color color)
        {
            ref DS4Color lowColor = ref Global.LowColor[device];
            lowColor.red = color.R;
            lowColor.green = color.G;
            lowColor.blue = color.B;

            LowColorChanged?.Invoke(this, EventArgs.Empty);
            LowColorRChanged?.Invoke(this, EventArgs.Empty);
            LowColorGChanged?.Invoke(this, EventArgs.Empty);
            LowColorBChanged?.Invoke(this, EventArgs.Empty);
            LowColorRStringChanged?.Invoke(this, EventArgs.Empty);
            LowColorGStringChanged?.Invoke(this, EventArgs.Empty);
            LowColorBStringChanged?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateForcedColor(System.Windows.Media.Color color)
        {
            if (device < 4)
            {
                DS4Color dcolor = new DS4Color() { red = color.R, green = color.G, blue = color.B };
                DS4LightBar.forcedColor[device] = dcolor;
                DS4LightBar.forcedFlash[device] = 0;
                DS4LightBar.forcelight[device] = true;
            }
        }

        public void StartForcedColor(System.Windows.Media.Color color)
        {
            if (device < 4)
            {
                DS4Color dcolor = new DS4Color() { red = color.R, green = color.G, blue = color.B };
                DS4LightBar.forcedColor[device] = dcolor;
                DS4LightBar.forcedFlash[device] = 0;
                DS4LightBar.forcelight[device] = true;
            }
        }

        public void EndForcedColor()
        {
            if (device < 4)
            {
                DS4LightBar.forcedColor[device] = new DS4Color(0, 0, 0);
                DS4LightBar.forcedFlash[device] = 0;
                DS4LightBar.forcelight[device] = false;
            }
        }

        public void UpdateChargingColor(System.Windows.Media.Color color)
        {
            ref DS4Color chargeColor = ref Global.ChargingColor[device];
            chargeColor.red = color.R;
            chargeColor.green = color.G;
            chargeColor.blue = color.B;
            ChargingColorChanged?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateLaunchProgram(string path)
        {
            Global.LaunchProgram[device] = path;
            LaunchProgramExistsChanged?.Invoke(this, EventArgs.Empty);
            LaunchProgramChanged?.Invoke(this, EventArgs.Empty);
            LaunchProgramNameChanged?.Invoke(this, EventArgs.Empty);
            LaunchProgramIconChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ResetLauchProgram()
        {
            Global.LaunchProgram[device] = string.Empty;
            LaunchProgramExistsChanged?.Invoke(this, EventArgs.Empty);
            LaunchProgramChanged?.Invoke(this, EventArgs.Empty);
            LaunchProgramNameChanged?.Invoke(this, EventArgs.Empty);
            LaunchProgramIconChanged?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateTouchDisInvert(ContextMenu menu)
        {
            int index = 0;
            List<int> triggerList = new List<int>();
            List<string> triggerName = new List<string>();
            
            foreach(MenuItem item in menu.Items)
            {
                if (item.IsChecked)
                {
                    triggerList.Add(index);
                    triggerName.Add(item.Header.ToString());
                }
                
                index++;
            }

            if (triggerList.Count == 0)
            {
                triggerList.Add(-1);
                triggerName.Add("None");
            }

            Global.TouchDisInvertTriggers[device] = triggerList.ToArray();
            TouchDisInvertString = string.Join(", ", triggerName.ToArray());
        }

        public void PopulateTouchDisInver(ContextMenu menu)
        {
            int[] triggers = Global.TouchDisInvertTriggers[device];
            int itemCount = menu.Items.Count;
            List<string> triggerName = new List<string>();
            foreach (int trigid in triggers)
            {
                if (trigid >= 0 && trigid < itemCount - 1)
                {
                    MenuItem current = menu.Items[trigid] as MenuItem;
                    current.IsChecked = true;
                    triggerName.Add(current.Header.ToString());
                }
                else if (trigid == -1)
                {
                    triggerName.Add("None");
                    break;
                }
            }

            if (triggerName.Count == 0)
            {
                triggerName.Add("None");
            }

            TouchDisInvertString = string.Join(", ", triggerName.ToArray());
        }

        public void UpdateGyroMouseTrig(ContextMenu menu, bool alwaysOnChecked)
        {
            int index = 0;
            List<int> triggerList = new List<int>();
            List<string> triggerName = new List<string>();

            int itemCount = menu.Items.Count;
            MenuItem alwaysOnItem = menu.Items[itemCount - 1] as MenuItem;
            if (alwaysOnChecked)
            {
                for (int i = 0; i < itemCount - 1; i++)
                {
                    MenuItem item = menu.Items[i] as MenuItem;
                    item.IsChecked = false;
                }
            }
            else
            {
                alwaysOnItem.IsChecked = false;
                foreach (MenuItem item in menu.Items)
                {
                    if (item.IsChecked)
                    {
                        triggerList.Add(index);
                        triggerName.Add(item.Header.ToString());
                    }

                    index++;
                }
            }

            if (triggerList.Count == 0)
            {
                triggerList.Add(-1);
                triggerName.Add("Always On");
                alwaysOnItem.IsChecked = true;
            }

            Global.SATriggers[device] = string.Join(",", triggerList.ToArray());
            GyroMouseTrigDisplay = string.Join(", ", triggerName.ToArray());
        }

        public void PopulateGyroMouseTrig(ContextMenu menu)
        {
            string[] triggers = Global.SATriggers[device].Split(',');
            int itemCount = menu.Items.Count;
            List<string> triggerName = new List<string>();
            foreach (string trig in triggers)
            {
                bool valid = int.TryParse(trig, out int trigid);
                if (valid && trigid >= 0 && trigid < itemCount - 1)
                {
                    MenuItem current = menu.Items[trigid] as MenuItem;
                    current.IsChecked = true;
                    triggerName.Add(current.Header.ToString());
                }
                else if (valid && trigid == -1)
                {
                    MenuItem current = menu.Items[itemCount - 1] as MenuItem;
                    current.IsChecked = true;
                    triggerName.Add("Always On");
                    break;
                }
            }

            if (triggerName.Count == 0)
            {
                MenuItem current = menu.Items[itemCount - 1] as MenuItem;
                current.IsChecked = true;
                triggerName.Add("Always On");
            }

            GyroMouseStickTrigDisplay = string.Join(", ", triggerName.ToArray());
        }

        public void UpdateGyroMouseStickTrig(ContextMenu menu, bool alwaysOnChecked)
        {
            int index = 0;
            List<int> triggerList = new List<int>();
            List<string> triggerName = new List<string>();

            int itemCount = menu.Items.Count;
            MenuItem alwaysOnItem = menu.Items[itemCount - 1] as MenuItem;
            if (alwaysOnChecked)
            {
                for (int i = 0; i < itemCount - 1; i++)
                {
                    MenuItem item = menu.Items[i] as MenuItem;
                    item.IsChecked = false;
                }
            }
            else
            {
                alwaysOnItem.IsChecked = false;
                foreach (MenuItem item in menu.Items)
                {
                    if (item.IsChecked)
                    {
                        triggerList.Add(index);
                        triggerName.Add(item.Header.ToString());
                    }

                    index++;
                }
            }

            if (triggerList.Count == 0)
            {
                triggerList.Add(-1);
                triggerName.Add("Always On");
                alwaysOnItem.IsChecked = true;
            }

            Global.SAMousestickTriggers[device] = string.Join(",", triggerList.ToArray());
            GyroMouseStickTrigDisplay = string.Join(", ", triggerName.ToArray());
        }

        public void PopulateGyroMouseStickTrig(ContextMenu menu)
        {
            string[] triggers = Global.SAMousestickTriggers[device].Split(',');
            int itemCount = menu.Items.Count;
            List<string> triggerName = new List<string>();
            foreach (string trig in triggers)
            {
                bool valid = int.TryParse(trig, out int trigid);
                if (valid && trigid >= 0 && trigid < itemCount - 1)
                {
                    MenuItem current = menu.Items[trigid] as MenuItem;
                    current.IsChecked = true;
                    triggerName.Add(current.Header.ToString());
                }
                else if (valid && trigid == -1)
                {
                    MenuItem current = menu.Items[itemCount-1] as MenuItem;
                    current.IsChecked = true;
                    triggerName.Add("Always On");
                    break;
                }
            }

            if (triggerName.Count == 0)
            {
                MenuItem current = menu.Items[itemCount - 1] as MenuItem;
                current.IsChecked = true;
                triggerName.Add("Always On");
            }

            GyroMouseStickTrigDisplay = string.Join(", ", triggerName.ToArray());
        }

        public void LaunchCurveEditor(string customDefinition)
        {
            // Custom curve editor web link clicked. Open the bezier curve editor web app usign the default browser app and pass on current custom definition as a query string parameter.
            // The Process.Start command using HTML page doesn't support query parameters, so if there is a custom curve definition then lookup the default browser executable name from a sysreg.
            string defaultBrowserCmd = String.Empty;
            try
            {
                if (!String.IsNullOrEmpty(customDefinition))
                {
                    string progId = String.Empty;
                    using (RegistryKey userChoiceKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\http\\UserChoice"))
                    {
                        progId = userChoiceKey?.GetValue("Progid")?.ToString();
                    }

                    if (!String.IsNullOrEmpty(progId))
                    {
                        using (RegistryKey browserPathCmdKey = Registry.ClassesRoot.OpenSubKey($"{progId}\\shell\\open\\command"))
                        {
                            defaultBrowserCmd = browserPathCmdKey?.GetValue(null).ToString();
                        }

                        if (!String.IsNullOrEmpty(defaultBrowserCmd))
                        {
                            int iStartPos = (defaultBrowserCmd[0] == '"' ? 1 : 0);
                            defaultBrowserCmd = defaultBrowserCmd.Substring(iStartPos, defaultBrowserCmd.LastIndexOf(".exe") + 4 - iStartPos);
                            if (Path.GetFileName(defaultBrowserCmd).ToLower() == "launchwinapp.exe")
                                defaultBrowserCmd = String.Empty;
                        }

                        // Fallback to IE executable if the default browser HTML shell association is for some reason missing or is not set
                        if (String.IsNullOrEmpty(defaultBrowserCmd))
                            defaultBrowserCmd = "C:\\Program Files\\Internet Explorer\\iexplore.exe";

                        if (!File.Exists(defaultBrowserCmd))
                            defaultBrowserCmd = String.Empty;
                    }
                }

                // Launch custom bezier editor webapp using a default browser executable command or via a default shell command. The default shell exeution doesn't support query parameters.
                if (!String.IsNullOrEmpty(defaultBrowserCmd))
                    System.Diagnostics.Process.Start(defaultBrowserCmd, $"\"file:///{Global.exedirpath}\\BezierCurveEditor\\index.html?curve={customDefinition.Replace(" ", "")}\"");
                else
                    System.Diagnostics.Process.Start($"{Global.exedirpath}\\BezierCurveEditor\\index.html");

            }
            catch (Exception ex)
            {
                AppLogger.LogToGui($"ERROR. Failed to open {Global.exedirpath}\\BezierCurveEditor\\index.html web app. Check that the web file exits or launch it outside of DS4Windows application. {ex.Message}", true);
            }
        }

        public void UpdateLateProperties()
        {
            tempControllerIndex = ControllerTypeIndex;
            Global.outDevTypeTemp[device] = Global.OutContType[device];
            tempBtPollRate = Global.BTPollRate[device];
        }
    }
}
