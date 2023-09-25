// \file Program.cs
// \brief <brief description>
// 
// \copyright  FlexRadio Systems (c) 2021 FlexRadio Systems
//             Unauthorized use, duplication or distribution of this software is
//             strictly prohibited by law.
// 
// \date 11-19-2021

using System;
using System.IO;
using System.Linq;
using System.Threading;
using Flex.Smoothlake.FlexLib;

namespace StreamingDemo
{
    class Program
    {
        static private Panadapter _pan;
        static private Radio _radio;
        static private RXRemoteAudioStream _rxRemoteAudioStream;
        static private Slice _slice;

        static private string csv_output_path = @"cognitive_radio_output.csv";

        private static void _pan_DataReady(Panadapter pan, ushort[] data)
        {
            double[] myData = data.Select(x => (double)x).ToArray();

            Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "," + String.Join(",", myData));
            File.AppendAllText(csv_output_path, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "," + String.Join(",", myData) + Environment.NewLine);
        }

        private static void _radio_PanadapterAdded(Panadapter pan, Waterfall fall)
        {
            // skip any additional Pandapter notifications once we have a good reference
            if (_pan != null) return;

            _pan = pan;

            /*
            _pan.CenterFreq = 14.2;
            _pan.Bandwidth = 14.0;
            _pan.Width = 4096;
            _pan.Height = 2000;
            _pan.LowDbm = -140.0;
            _pan.HighDbm = 0.0;
            */

            _pan.CenterFreq = 15;
            _pan.Bandwidth = 14;
            _pan.Width = 4096;
            _pan.Height = 2000;
            _pan.LowDbm = -140.0;
            _pan.HighDbm = 0.0;

            _pan.DataReady += _pan_DataReady;

            // now that we have a good reference to a Panadapter, request a Slice
            //_radio.RequestSlice(_pan, "USB", 14.2, "ANT1");
        }

        private static void _radio_PanadapterRemoved(Panadapter pan)
        {
            if (pan == _pan)
                _pan = null;
        }

        private static void _radio_RXRemoteAudioStreamAdded(RXRemoteAudioStream remoteAudioRX)
        {
            // skip any additional RX Remote Audio notifications once we have a good reference
            if (_rxRemoteAudioStream != null) return;

            _rxRemoteAudioStream = remoteAudioRX;
            _rxRemoteAudioStream.DataReady += _rxRemoteAudioStream_DataReady;
        }

        private static void _radio_SliceAdded(Slice slc)
        {
            // skip any additional Slice notifications once we have a good reference
            if (_slice != null) return;

            // if wanting to stream 24kHz Slice (post demod data), use the code below
            _radio.RXRemoteAudioStreamAdded += _radio_RXRemoteAudioStreamAdded;
            _radio.RequestRXRemoteAudioStream(isCompressed: false);
        }

        private static void _radio_SliceRemoved(Slice slc)
        {
            if (slc == _slice)
                _slice = null;
        }

        private static void _rxRemoteAudioStream_DataReady(RXAudioStream rxAudioStream, float[] rx_data)
        {
            // do something useful with the streaming rx_data
        }

        static void API_RadioAdded(Radio radio)
        {
            if (_radio == null)
            {
                _radio = radio;
                _radio.Connect();

                StartStreaming();
            }
        }

        static void API_RadioRemoved(Radio radio)
        {
            if (radio == _radio)
            {
                _pan = null;
                _slice = null;
                _radio = null;
            }
        }

        static void Main()
        {
            API.ProgramName = "StreamingDemo";
            API.RadioAdded += new API.RadioAddedEventHandler(API_RadioAdded);
            API.RadioRemoved += new API.RadioRemovedEventHandler(API_RadioRemoved);

            API.IsGUI = true; // Note - this is required for Panadapter Data / DAX IQ streaming

            API.Init();
            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        static void StartStreaming()
        {
            _radio.PanadapterAdded += _radio_PanadapterAdded;
            _radio.PanadapterRemoved += _radio_PanadapterRemoved;

            _radio.SliceAdded += _radio_SliceAdded;
            _radio.SliceRemoved += _radio_SliceRemoved;

            _radio.RequestPanafall();
        }
    }
}