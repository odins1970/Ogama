﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ogama.Modules.Common.CustomEventArgs;

namespace Ogama.Modules.Recording.SMIInterface.RedM
{
	public class SmiRedmClient : SmiRedmWrapperListener
	{
		private SmiRedmWrapper smiWrapper;
		public bool IsTracking { get; set; }
		public bool IsConnected { get; set; }
		private long lastTime;
		private int screenWidth;
		private int screenHeight;
		private SMISetting smiSettings;
		/// <summary>
		/// Event. Raised, when new gaze data is available.
		/// </summary>
		public event GazeDataChangedEventHandler GazeDataAvailable;

		/// <summary>
		/// Event. Raised, when calibration has finished.
		/// </summary>
		public event EventHandler CalibrationFinished;
		
		/// <summary>
		/// 
		/// </summary>
		public SmiRedmClient()
		{
			this.init();
		}

		/// <summary>
		/// constructor for unit testing
		/// </summary>
		/// <param name="isConnected"></param>
		public SmiRedmClient(bool isConnected, int screenWidth, int screenHeight)
		{
			this.IsConnected = isConnected;
			this.screenWidth = screenWidth;
			this.screenHeight = screenHeight;
		}

		/// <summary>
		/// 
		/// </summary>
		~SmiRedmClient()
		{
			if (this.smiWrapper != null)
			{
				this.smiWrapper.disconnecting();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void init()
		{
			this.smiWrapper = new SmiRedmWrapper();
			this.smiWrapper.initialize();
			this.smiWrapper.register(this);
			this.IsTracking = false;
			this.IsConnected = false;
			this.lastTime = 0;
			Document activeDocument = Document.ActiveDocument;
			if (activeDocument != null)
			{
				this.screenHeight = activeDocument.PresentationSize.Height;
				this.screenWidth = activeDocument.PresentationSize.Width;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="SMISetting"/> to be used within this client.
		/// </summary>
		public SMISetting Settings
		{
			get
			{
				return this.smiSettings;
			}
			set
			{
				this.smiSettings = value;
			}
		}


		
		/// <summary>
		/// 
		/// </summary>
		public void Configure()
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public void Connect()
		{
			try
			{
				this.smiWrapper.receiveip = this.smiSettings.SMIServerAddress;
				this.smiWrapper.receiveport = this.smiSettings.OGAMAServerPort;
				this.smiWrapper.sendip = this.smiSettings.SMIServerAddress;
				this.smiWrapper.sendport = this.smiSettings.SMIServerPort;

				this.smiWrapper.connect();

				this.IsConnected = true;
			}
			catch (Exception e)
			{
				Ogama.ExceptionHandling.ExceptionMethods.HandleExceptionSilent(e);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void Disconnect()
		{
			try
			{
				this.smiWrapper.disconnecting();
				this.IsConnected = false;
			}
			catch (Exception ex)
			{
				Ogama.ExceptionHandling.ExceptionMethods.HandleExceptionSilent(ex);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void StartStreaming()
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public void StopStreaming()
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public void StartTracking()
		{
			this.smiWrapper.startrecording();
			this.IsTracking = true;
		}

		/// <summary>
		/// 
		/// </summary>
		public void StopTracking()
		{
			this.smiWrapper.stoprecording();
			this.IsTracking = false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public long GetTimeStamp()
		{
			return this.lastTime;
		}

		/// <summary>
		/// 
		/// </summary>
		public void Calibrate()
		{
			int calibrationPoints = 9;
			int display = 1;
			bool isPrimaryScreen = Ogama.Modules.Common.Tools.PresentationScreen.GetPresentationScreen().Primary;
			if (isPrimaryScreen)
			{
				display = 0;
			}
			int pointSize = 20;
		
			this.smiWrapper.calibrate(calibrationPoints, display, pointSize);

			this.smiWrapper.validate();

			this.smiWrapper.getaccuracy();

			this.OnCalibrationFinished(new EventArgs());

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public GazeData ExtractTrackerData(EyeTrackingController.SampleStruct data)
		{
			GazeData result = new GazeData();

			float gazePosXLeft = Convert.ToSingle(data.leftEye.gazeX);
			float gazePosXRight = Convert.ToSingle(data.rightEye.gazeX);
			float gazePosYLeft = Convert.ToSingle(data.leftEye.gazeY);
			float gazePosYRight = Convert.ToSingle(data.rightEye.gazeY);

			result.GazePosX = (gazePosXLeft + gazePosXRight) / 2;
			result.GazePosY = (gazePosYLeft + gazePosYRight) / 2;

			long MICROSECONDS = 1000;
			result.Time = (data.timestamp / MICROSECONDS);
			this.lastTime = result.Time;

			result.PupilDiaX = Convert.ToSingle(data.leftEye.diam);
			result.PupilDiaY = Convert.ToSingle(data.rightEye.diam);

			return result;
		}

		/// <summary>
		/// Raised when new gaze data is available.
		/// </summary>
		/// <param name="e"><see cref="GazeDataChangedEventArgs"/> event arguments</param>.
		private void OnGazeDataAvailable(GazeDataChangedEventArgs e)
		{
			if (this.GazeDataAvailable != null)
			{
				this.GazeDataAvailable(this, e);
			}
		}

		private void OnCalibrationFinished(EventArgs e)
		{
			if (this.CalibrationFinished == null)
			{
				return;
			}
			this.CalibrationFinished(this, e);
		}



		private Object LOCK = new Object();

		public void onSampleData(EyeTrackingController.SampleStruct data)
		{
			lock (LOCK)
			{
				if (!IsTracking)
				{
					return;
				}
				if (this.GazeDataAvailable == null)
				{
					return;
				}
				GazeData gazeData = ExtractTrackerData(data);

				GazeDataChangedEventArgs eventArgs = new GazeDataChangedEventArgs(gazeData);

				this.OnGazeDataAvailable(eventArgs);
			}
		}
	}
}
