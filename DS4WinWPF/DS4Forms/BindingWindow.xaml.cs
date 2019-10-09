﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DS4WinWPF.DS4Forms.ViewModel;

namespace DS4WinWPF.DS4Forms
{
    /// <summary>
    /// Interaction logic for BindingWindow.xaml
    /// </summary>
    public partial class BindingWindow : Window
    {
        private Dictionary<Button, BindAssociation> associatedBindings =
            new Dictionary<Button, BindAssociation>();
        private Dictionary<int, Button> keyBtnMap = new Dictionary<int, Button>();
        private Dictionary<DS4Windows.X360Controls, Button> conBtnMap =
            new Dictionary<DS4Windows.X360Controls, Button>();
        private Dictionary<DS4Windows.X360Controls, Button> mouseBtnMap =
            new Dictionary<DS4Windows.X360Controls, Button>();
        private BindingWindowViewModel bindingVM;
        private Button highlightBtn;

        public BindingWindow(int deviceNum, MappedControl mappedControl)
        {
            InitializeComponent();

            bindingVM = new BindingWindowViewModel(deviceNum, mappedControl);

            guideBtn.Content = "";
            highlightImg.Visibility = Visibility.Hidden;
            highlightLb.Visibility = Visibility.Hidden;

            InitButtonBindings();
            InitKeyBindings();
            InitInfoMaps();
            FindCurrentHighlightButton();

            regBindRadio.IsChecked = !bindingVM.ShowShift;
            shiftBindRadio.IsChecked = bindingVM.ShowShift;
            bindingVM.ActionBinding = bindingVM.CurrentOutBind;
            ChangeForCurrentAction();
        }

        private void OutConBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            string name = button.Tag.ToString();
            highlightLb.Content = name;

            double left = Canvas.GetLeft(button);
            double top = Canvas.GetTop(button);

            Canvas.SetLeft(highlightImg, left + (button.Width / 2.0) - (highlightImg.Height / 2.0));
            Canvas.SetTop(highlightImg, top + (button.Height / 2.0) - (highlightImg.Height / 2.0));

            Canvas.SetLeft(highlightLb, left + (button.Width / 2.0) - (highlightLb.ActualWidth / 2.0));
            Canvas.SetTop(highlightLb, top - 30);

            highlightImg.Visibility = Visibility.Visible;
            highlightLb.Visibility = Visibility.Visible;
        }

        private void OutConBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            highlightImg.Visibility = Visibility.Hidden;
            highlightLb.Visibility = Visibility.Hidden;
        }

        private void OutputKeyBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OutputButtonBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeForCurrentAction()
        {
            OutBinding bind = bindingVM.ActionBinding;
            extrasGB.DataContext = bind;
            modePanel.DataContext = bind;
            topOptsPanel.DataContext = bind;
            shiftTriggerCombo.Visibility = bind.IsShift() ? Visibility.Visible : Visibility.Hidden;
        }

        private void FindCurrentHighlightButton()
        {
            if (highlightBtn != null)
            {
                highlightBtn.Background = SystemColors.ControlBrush;
            }

            OutBinding binding = bindingVM.CurrentOutBind;
            if (binding.outputType == OutBinding.OutType.Default)
            {
                DS4Windows.X360Controls defaultBind = DS4Windows.Global.defaultButtonMapping[(int)binding.input];
                if (!OutBinding.IsMouseRange(defaultBind))
                {
                    if (conBtnMap.TryGetValue(defaultBind, out Button tempBtn))
                    {
                        OutConBtn_MouseEnter(tempBtn, null);
                        //tempBtn.Background = new SolidColorBrush(Colors.LimeGreen);
                    }
                }
                else
                {
                    if (mouseBtnMap.TryGetValue(binding.control, out Button tempBtn))
                    {
                        tempBtn.Background = new SolidColorBrush(Colors.LimeGreen);
                        highlightBtn = tempBtn;
                    }
                }
            }
            else if (binding.outputType == OutBinding.OutType.Button)
            {
                if (!binding.IsMouse())
                {
                    if (conBtnMap.TryGetValue(binding.control, out Button tempBtn))
                    {
                        OutConBtn_MouseEnter(tempBtn, null);
                        //tempBtn.Background = new SolidColorBrush(Colors.LimeGreen);
                    }
                }
                else
                {
                    if (mouseBtnMap.TryGetValue(binding.control, out Button tempBtn))
                    {
                        tempBtn.Background = new SolidColorBrush(Colors.LimeGreen);
                        highlightBtn = tempBtn;
                    }
                }
            }
            else if (binding.outputType == OutBinding.OutType.Key)
            {
                if (keyBtnMap.TryGetValue(binding.outkey, out Button tempBtn))
                {
                    tempBtn.Background = new SolidColorBrush(Colors.LimeGreen);
                    highlightBtn = tempBtn;
                }
            }
        }

        private void InitInfoMaps()
        {
            foreach(KeyValuePair<Button, BindAssociation> pair in associatedBindings)
            {
                Button button = pair.Key;
                BindAssociation binding = pair.Value;
                if (binding.outputType == BindAssociation.OutType.Button)
                {
                    if (!binding.IsMouse())
                    {
                        conBtnMap.Add(binding.control, button);
                    }
                    else
                    {
                        mouseBtnMap.Add(binding.control, button);
                    }
                }
                else if (binding.outputType == BindAssociation.OutType.Key)
                {
                    if (keyBtnMap.ContainsKey(binding.outkey))
                    {
                        keyBtnMap.Add(binding.outkey, button);
                    }
                    
                }
            }
        }

        private void InitButtonBindings()
        {
            associatedBindings.Add(aBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.A });
            aBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(bBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.B });
            bBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(xBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.X });
            xBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(yBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.Y });
            yBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(lbBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.LB });
            lbBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(ltBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.LT });
            ltBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(rbBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.RB });
            rbBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(rtBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.RT });
            rtBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(backBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.Back });
            backBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(startBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.Start });
            startBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(guideBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.Guide });
            guideBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(lsbBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.LS });
            lsbBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(lsuBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.LYNeg });
            lsuBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(lsrBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.LXPos });
            lsrBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(lsdBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.LYPos });
            lsdBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(lslBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.LXNeg });
            lslBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(dpadUBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.DpadUp });
            dpadUBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(dpadRBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.DpadRight });
            dpadRBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(dpadDBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.DpadDown });
            dpadDBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(dpadLBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.DpadLeft });
            dpadLBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(rsbBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.RS });
            rsbBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(rsuBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.RYNeg });
            rsuBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(rsrBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.RXPos });
            rsrBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(rsdBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.RYPos });
            rsdBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(rslBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.RXNeg });
            rslBtn.Click += OutputButtonBtn_Click;

            associatedBindings.Add(mouseUpBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.MouseUp });
            mouseUpBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(mouseDownBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.MouseDown });
            mouseDownBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(mouseLeftBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.MouseLeft });
            mouseLeftBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(mouseRightBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.MouseRight });
            mouseRightBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(mouseLBBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.LeftMouse });
            mouseLBBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(mouseMBBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.MiddleMouse });
            mouseMBBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(mouseRBBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.RightMouse });
            mouseRBBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(mouseWheelUBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.WUP });
            mouseWheelUBtn.Click += OutputButtonBtn_Click;
            associatedBindings.Add(mouseWheelDBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Button, control = DS4Windows.X360Controls.WDOWN });
            mouseWheelDBtn.Click += OutputButtonBtn_Click;
        }

        private void InitKeyBindings()
        {
            associatedBindings.Add(escBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x1B });
            escBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(f1Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x70 });
            f1Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(f2Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x71 });
            f2Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(f3Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x72 });
            f3Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(f4Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x73 });
            f4Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(f5Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x74 });
            f5Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(f6Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x75 });
            f6Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(f7Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x76 });
            f7Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(f8Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x77 });
            f8Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(f9Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x78 });
            f9Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(f10Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x79 });
            f10Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(f11Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x7A });
            f11Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(f12Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x7B });
            f12Btn.Click += OutputKeyBtn_Click;

            associatedBindings.Add(oem3Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xC0 });
            oem3Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(oneBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x31 });
            oneBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(twoBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x32 });
            twoBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(threeBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x33 });
            threeBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(fourBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x34 });
            fourBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(fiveBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x35 });
            fiveBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(sixBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x36 });
            sixBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(sevenBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x37 });
            sevenBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(eightBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x38 });
            eightBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(nineBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x39 });
            nineBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(zeroBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x30 });
            zeroBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(minusBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xBD });
            minusBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(equalBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xBB });
            equalBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(bsBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x08 });
            bsBtn.Click += OutputKeyBtn_Click;

            associatedBindings.Add(tabBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x09 });
            tabBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(qKey,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x51 });
            qKey.Click += OutputKeyBtn_Click;
            associatedBindings.Add(wKey,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x57 });
            wKey.Click += OutputKeyBtn_Click;
            associatedBindings.Add(eKey,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x45 });
            eKey.Click += OutputKeyBtn_Click;
            associatedBindings.Add(rKey,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x52 });
            rKey.Click += OutputKeyBtn_Click;
            associatedBindings.Add(tKey,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x55 });
            tKey.Click += OutputKeyBtn_Click;
            associatedBindings.Add(yKey,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x59 });
            yKey.Click += OutputKeyBtn_Click;
            associatedBindings.Add(uKey,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x55 });
            uKey.Click += OutputKeyBtn_Click;
            associatedBindings.Add(iKey,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x49 });
            iKey.Click += OutputKeyBtn_Click;
            associatedBindings.Add(oKey,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x4F });
            oKey.Click += OutputKeyBtn_Click;
            associatedBindings.Add(pKey,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x50 });
            pKey.Click += OutputKeyBtn_Click;
            associatedBindings.Add(lbracketBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xDB });
            lbracketBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(rbracketBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xDD });
            rbracketBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(bSlashBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xDC });
            bSlashBtn.Click += OutputKeyBtn_Click;

            associatedBindings.Add(capsLBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x14 });
            capsLBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(aKeyBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x41 });
            aKeyBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(sBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x53 });
            sBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(dBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x44 });
            dBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(fBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x46 });
            fBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(gBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x47 });
            gBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(hBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x48 });
            hBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(jBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x48 });
            jBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(kBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x4B });
            kBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(lBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x4C });
            lBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(semicolonBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xBA });
            semicolonBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(aposBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xDE });
            aposBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(enterBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x0D });
            enterBtn.Click += OutputKeyBtn_Click;

            associatedBindings.Add(lshiftBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x10 });
            lshiftBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(zBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x5A });
            zBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(xKeyBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x58 });
            xKeyBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(cBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x43 });
            cBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(vBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x56 });
            vBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(bKeyBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x42 });
            bKeyBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(nBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x4E });
            nBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(mBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x4D });
            mBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(commaBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xBC });
            commaBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(periodBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xBE });
            periodBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(bslashBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xBF });
            bslashBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(rshiftBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xA1 });
            rshiftBtn.Click += OutputKeyBtn_Click;

            associatedBindings.Add(lctrlBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xA2 });
            lctrlBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(lWinBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x5B });
            lWinBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(laltBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x12 });
            laltBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(spaceBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x20 });
            spaceBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(raltBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xA5 });
            raltBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(rctrlBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xA3 });
            rctrlBtn.Click += OutputKeyBtn_Click;

            associatedBindings.Add(prtBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x2C });
            prtBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(sclBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x91 });
            sclBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(brkBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x13 });
            brkBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(insBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x2D });
            insBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(homeBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x24 });
            homeBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(pgupBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x21 });
            pgupBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(delBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x2E });
            delBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(endBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x23 });
            endBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(pgdwBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x22 });
            pgdwBtn.Click += OutputKeyBtn_Click;

            associatedBindings.Add(uarrowBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x26 });
            uarrowBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(larrowBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x25 });
            larrowBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(darrowBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x28 });
            darrowBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(rarrowBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x27 });
            rarrowBtn.Click += OutputKeyBtn_Click;

            associatedBindings.Add(prevTrackBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xB1 });
            prevTrackBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(stopBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xB2 });
            stopBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(playBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xB3 });
            playBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(nextTrackBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xB0 });
            nextTrackBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(volupBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xAF });
            volupBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(numlockBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x90 });
            numlockBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(numdivideBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x64 });
            numdivideBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(nummultiBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x6A });
            nummultiBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(numminusBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x6D });
            numminusBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(voldownBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xAE });
            voldownBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(num7Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x67 });
            num7Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(num8Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x68 });
            num8Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(num9Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x69 });
            num9Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(numplusBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x6B });
            numplusBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(volmuteBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0xAD });
            volmuteBtn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(num4Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x64 });
            num4Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(num5Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x65 });
            num5Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(num6Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x66 });
            num6Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(num1Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x61 });
            num1Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(num2Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x62 });
            num2Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(num3Btn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x63 });
            num3Btn.Click += OutputKeyBtn_Click;
            associatedBindings.Add(numEnterBtn,
                new BindAssociation() { outputType = BindAssociation.OutType.Key, outkey = 0x13 });
            numEnterBtn.Click += OutputKeyBtn_Click;
        }

        private void RegBindRadio_Click(object sender, RoutedEventArgs e)
        {
            if (regBindRadio.IsChecked == true)
            {
                bindingVM.ActionBinding = bindingVM.CurrentOutBind;
                ChangeForCurrentAction();
            }
            
        }

        private void ShiftBindRadio_Click(object sender, RoutedEventArgs e)
        {
            if (shiftBindRadio.IsChecked == true)
            {
                bindingVM.ActionBinding = bindingVM.ShiftOutBind;
                ChangeForCurrentAction();
            }
        }

        private void TestRumbleBtn_Click(object sender, RoutedEventArgs e)
        {
            int deviceNum = bindingVM.DeviceNum;
            if (deviceNum < 4)
            {
                DS4Windows.DS4Device d = App.rootHub.DS4Controllers[deviceNum];
                if (d != null)
                {
                    if (!bindingVM.RumbleActive)
                    {
                        bindingVM.RumbleActive = true;
                        d.setRumble((byte)Math.Min(255, bindingVM.ActionBinding.LightRumble),
                            (byte)Math.Min(255, bindingVM.ActionBinding.HeavyRumble));
                        testRumbleBtn.Content = Properties.Resources.StopText;
                    }
                    else
                    {
                        bindingVM.RumbleActive = false;
                        d.setRumble(0, 0);
                        testRumbleBtn.Content = Properties.Resources.TestText;
                    }
                }
            }
        }
    }
}
