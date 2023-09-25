// \file MainForm.cs
// \brief  WinForm demonstration for FlexLib basic transceiver. 
// 
// \copyright  FlexRadio Systems (c) 2021 FlexRadio Systems
// 
// Open Issues:
//     Requires two clicks on refresh button to correctly show spectrum
//     X axis on spectrum chart is not formatted to frequency
//     No audio stream processing
//     Last value in FFT array is always 0 (this is coming from the radio)
// 
// \date 12-21-2021
// \author Dan Quigley

#nullable enable

using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Flex.Smoothlake.FlexLib;

namespace BasicTransceiver
{
    public partial class MainForm : Form
    {
        private const string DEFAULT_ANTENNA = "ANT1";
        private const double DEFAULT_FREQUENCY = 14.1;
        private const string DEFAULT_MODE = "USB";

        private Panadapter? _pan;
        private Radio? _radio;
        private Slice? _slice;

        private bool _streamStarted;

        public double[] fftData = new double[4096];

        public MainForm()
        {
            InitializeComponent();

            var sig = SpectrumPlot.Plot.AddSignal(fftData);
            SpectrumPlot.Plot.XLabel("Frequency");
            SpectrumPlot.Plot.YLabel("Signal (dBm)");
            SpectrumPlot.Plot.YAxis.TickLabelFormat(YAxisLabels);

            sig.FillBelow(Color.DarkCyan);

            SlicePropGrid.Validated += PropGridValidated;
            PanPropGrid.Validated += PropGridValidated;
            RadioPropGrid.Validated += PropGridValidated;

            PanPropGrid.SelectedObject = null;
            SlicePropGrid.SelectedObject = null;
            RadioPropGrid.SelectedObject = null;
        }

        private void ApplicationInit()
        {
            API.ProgramName = "BasicTransceiver";
            API.RadioAdded += RadioAdded;
            API.RadioRemoved += RadioRemoved;
            API.IsGUI = true; // Note - this is required for Panadapter Data / DAX IQ streaming
            API.Init();
        }

        private void Bandwidth_Scroll(object sender, EventArgs e)
        {
            if (_pan == null) return;

            _pan.Bandwidth = 5.0 * (BandWidth.Value / 1000.0);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ApplicationInit();
            RefreshGuiElements();
        }

        private void PanadapterAdded(Panadapter pan, Waterfall fall)
        {
            //if (_radio == null) return;

            // skip any additional Panadapter notifications once we have a good reference
            if (_pan != null) return;

            _pan = pan;

            // The InvokeRequired pattern is used to make sure any changes to a UI element
            // occurs on the UI thread. This is a Windows Forms requirement. 
            if (PanPropGrid.InvokeRequired)
            {
                // Code below here can execute on a different thread than
                // called (Invoke is not blocking) - take care to avoid race conditions
                PanPropGrid.Invoke((Action)PanadapterInit);
            }
            else
            {
                PanadapterInit();
            }
        }

        private void PanadapterDataReady(Panadapter pan, ushort[] data)
        {
            double[] dData = data.Select(x => (double)x).ToArray();

            PlotData(dData);
        }

        private void PanadapterInit()
        {
            PanPropGrid.SelectedObject = _pan;

            if (_pan == null) return;

            _pan.DataReady += PanadapterDataReady;

            _pan.RXAnt = DEFAULT_ANTENNA;
            _pan.CenterFreq = DEFAULT_FREQUENCY;
            _pan.Bandwidth = .025;
            _pan.Width = 4096;
            _pan.Height = 2000;
            _pan.LowDbm = -140.0;
            _pan.HighDbm = 0.0;

            RefreshGuiElements();
        }

        private void PanadapterPanelLabel_Click(object sender, EventArgs e)
        {
            PanPropGrid.Refresh();
        }

        private void PanadapterRemoved(Panadapter pan)
        {
            if (pan == _pan)
                _pan = null;
        }

        private void PlotData(double[] dData)
        {
            // Scales FFT data to the plot buffer
            ScaleFFTData(dData, -140.0, 0, true);

            // The InvokeRequired pattern is used to make sure any changes to a UI element
            // occurs on the UI thread. This is a Windows Forms requirement. 
            if (SpectrumPlot.InvokeRequired)
            {
                // Code below here can execute on a different thread than
                // called (Invoke is not blocking) - take care to avoid race conditions
                SpectrumPlot.Invoke((Action)PlotRefresh);
            }
            else
            {
                PlotRefresh();
            }
        }

        private void PlotRefresh()
        {
            try
            {
                SpectrumPlot.Refresh(false, true);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        private void PropGridValidated(object? sender, EventArgs e)
        {
            RefreshGuiElements();
        }

        // Note: This method finds and connects with the first radio which broadcasts its presence on a LAN
        // this code should be modified to present the user with a list of discovered radios and allow them to
        // select the radio they wish to use. 
        private void RadioAdded(Radio radio)
        {
            if (_radio != null) return;

            _radio = radio;

            // The InvokeRequired pattern is used to make sure any changes to a UI element
            // occurs on the UI thread. This is a Windows Forms requirement. 
            if (SlicePropGrid.InvokeRequired)
            {
                // Code below here can execute on a different thread than
                // called (Invoke is not blocking) - take care to avoid race conditions
                RadioPropGrid.Invoke((Action)RadioInit);
            }
            else
            {
                RadioInit();
            }
        }

        private void RadioInit()
        {
            if (_radio == null) return;

            RadioPropGrid.SelectedObject = _radio;

            _radio.PanadapterAdded += PanadapterAdded;
            _radio.PanadapterRemoved += PanadapterRemoved;

            _radio.SliceAdded += SliceAdded;
            _radio.SliceRemoved += SliceRemoved;

            _radio.Connect();
        }

        private void RadioPanelLabel_Click(object sender, EventArgs e)
        {
            RadioPropGrid.Refresh();
        }

        private void RadioRemoved(Radio radio)
        {
            if (radio != _radio) return;

            _slice = null;
            _pan = null;
            _radio = null;

            _streamStarted = false;
        }

        private void RefreshGuiElements()
        {
            // The InvokeRequired pattern is used to make sure any changes to a UI element
            // occurs on the UI thread. This is a Windows Forms issue. 
            if (SpectrumPlot.InvokeRequired)
            {
                // Code below here can execute on a different thread than
                // called (Invoke is not blocking) - take care to avoid race conditions
                SpectrumPlot.Invoke((Action)RefreshGuiElements);
            }

            SlicePropGrid.Refresh();
            PanPropGrid.Refresh();
            RadioPropGrid.Refresh();
            SpectrumPlot.Refresh();
        }

        private void ScaleFFTData(double[] arr, double min, double max, bool invert)
        {
            double m = (max - min) / (arr.Max() - arr.Min());
            double c = min - arr.Min() * m;
            for (int i = 0; i < fftData.Length; i++)
            {
                // avoid math errors in rendering engine
                if (double.IsNaN(arr[i]) || double.IsInfinity(arr[i]))
                    arr[i] = 0.0;
                fftData[i] = (m * arr[i] + c) * (invert ? -1.0 : 1.0);
            }

            // Use end expression to set last array location to an average value
            //fftData[^1] = fftData.Average();
        }

        private void SliceAdded(Slice slc)
        {
            // skip any additional Slice notifications once we have a good reference
            if (_slice != null) return;

            _slice = slc;

            // The InvokeRequired pattern is used to make sure any changes to a UI element
            // occurs on the UI thread. This is a Windows Forms issue. 
            if (SlicePropGrid.InvokeRequired)
            {
                // Code below here can execute on a different thread than
                // called (Invoke is not blocking) - take care to avoid race conditions
                SlicePropGrid.Invoke((Action)SliceInit);
            }
            else
            {
                SliceInit();
            }
        }

        private void SliceInit()
        {
            // If wanting to process 24kHz audio stream for the slice (post demod data),
            // uncomment the code below and routines at the end of this file. 
            
            //_radio.RXRemoteAudioStreamAdded += _radio_RXRemoteAudioStreamAdded;
            //_radio.RequestRXRemoteAudioStream(isCompressed: false);

            SlicePropGrid.SelectedObject = _slice;

            if (_slice == null) return;

            _slice.Freq = DEFAULT_FREQUENCY;
            _slice.RXAnt = DEFAULT_ANTENNA;
            _slice.TXAnt = DEFAULT_ANTENNA;
            _slice.DemodMode = DEFAULT_MODE;

            RefreshGuiElements();
        }

        private void SlicePanelLabel_Click(object sender, EventArgs e)
        {
            SlicePropGrid.Refresh();
        }

        private void SliceRemoved(Slice slc)
        {
            if (slc == _slice)
                _slice = null;
        }

        private void StartRefresh_Click(object sender, EventArgs e)
        {
            if (!_streamStarted)
            {
                StartStreaming();
                StartRefresh.Text = @"Refresh";
            }

            RefreshGuiElements();
        }

        private void StartStreaming()
        {
            if (_radio == null) return;

            // When radios connect, if there is an existing panadapter and slice
            // they are automatically assigned to this Radio instance. 

            if (_radio.PanadapterList.Count == 0)
            {
                _radio.RequestPanafall();
                return;
            }

            _streamStarted = true;

            SpectrumPlot.Plot.AxisAutoY();
            SpectrumPlot.Plot.AxisAutoX(0.1);
        }

        private void Tune_CheckedChanged(object sender, EventArgs e)
        {
            if (_radio == null) return;

            // warning: toggles TX at 10W!  
            // Make sure you have a dummy load or an antenna!
            _radio.TunePower = 10;
            _radio.TXTune = Tune.Checked;
        }

        // Y axis formatter
        private static string YAxisLabels(double y) => ((140.0 - y) * -1.0).ToString("N0");

        //private RXRemoteAudioStream? _rxRemoteAudioStream;
        //public RXRemoteAudioStream? RxRemoteAudioStream
        //{
        //    get => _rxRemoteAudioStream;
        //    set => _rxRemoteAudioStream = value;
        //}

        //private void _radio_RXRemoteAudioStreamAdded(RXRemoteAudioStream remoteAudioRX)
        //{
        //    // skip any additional RX Remote Audio notifications once we have a good reference
        //    if (_rxRemoteAudioStream != null) return;

        //    _rxRemoteAudioStream = remoteAudioRX;
        //    _rxRemoteAudioStream.DataReady += _rxRemoteAudioStream_DataReady;
        //}

        //private void _rxRemoteAudioStream_DataReady(RXAudioStream rxAudioStream, float[] rx_data)
        //{
        //    // do something useful with the streaming rx_data
        //}
    }
}