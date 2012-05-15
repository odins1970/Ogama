﻿// <copyright file="EyeTechSetting.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2010 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian.vosskuehler@fu-berlin.de</email>

#if EYETECH
/** Module Notice:
 * This module implements an additional Ogama Recording module, specifically 
 * made for the EyeTech TM3, but should work on other EyeTech hardware as 
 * well, because of the generic QuickLink API.
 * 
 * The API structure definitions, as wel as the API function summaries are
 * copied from the QuickLink API User's manual (v5.2)
 * 
 * Also, since the API is offered in only native C++ functions and struct are 
 * rewriten using P/Invoke. These snippets are generated by "P/Invoke Interop 
 * Assistent 1.0" (http://clrinterop.codeplex.com/). Due to indifferences from
 * C++ to C# some types from the original .h file are not identical. (i.e: "In
 * C#, the long type is 64 bits, while in C++, it is 32 bits." --
 * http://msdn.microsoft.com/en-us/library/yyaad03b%28VS.90%29.aspx )
 * 
 * Knowing this, some of the function/struct comments are "incorrect".
 */

namespace Ogama.Modules.Recording.EyeTech
{
  using System;
  using System.Collections.Generic;
  using System.Drawing;
  using System.Text;
  using System.Xml.Serialization;

  using VectorGraphics;
  using VectorGraphics.CustomTypeConverter;

  using QuickLinkAPI;


  /// <summary>
  /// Class to save settings for the eyetech eye tracking system.
  /// It is XML serializable and can be stored in a file via 
  /// the <see cref="XmlSerializer"/> class.
  /// <example>XmlSerializer serializer = new XmlSerializer(typeof(EyeTechSetting));</example>
  /// </summary>
  [Serializable]
  public class EyeTechSetting
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    private CalibrationOptions calibrationOptions;
    private ProcessingOptions processingOptions;
    private CameraOptions cameraOptions;
    private ClickingOptions clickingOptions;
    private ToolbarOptions toolbarOptions;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION
    /// <summary>
    /// Initializes a new instance of the EyeTechSetting class.
    /// </summary>
    public EyeTechSetting()
    {
      this.calibrationOptions.Calibration_TargetTime = 10;    // 1sec
      this.calibrationOptions.Calibration_Style = CalibrationStyle.CAL_STYLE_9_POINT;

      this.processingOptions.Processing_SmoothingFactor = 30;
      this.processingOptions.Processing_EyesToProcess = EyesToProcess.EYES_TO_PROC_DUAL_LEFT_AND_RIGHT;
      this.processingOptions.Processing_EnableCapture = true;
      this.processingOptions.Processing_EnableProcessing = true;
      this.processingOptions.Processing_EnableDisplay = true;
      this.processingOptions.Processing_EnableCursorMovement = false;
      this.processingOptions.Processing_EnableClicking = false;
      this.processingOptions.Processing_MaxProcessTime = 50;
      this.processingOptions.Processing_ProcessPriority = ProcessPriority.PROC_PRIORITY_3;

      this.cameraOptions.Camera_BusBandwidthPercentage = 100;
      this.cameraOptions.Camera_ImageROIPercentage = 20;
      this.cameraOptions.Camera_GainMethod = CameraGainMethod.CAM_GAIN_METHOD_AUTO;
      this.cameraOptions.Camera_GainValue = 100;
      this.cameraOptions.Camera_GPIO_1 = CameraGPIOOutput.CAM_GPIO_OUT_LEFT_TRACKING_STATUS;
      this.cameraOptions.Camera_GPIO_2 = CameraGPIOOutput.CAM_GPIO_OUT_RIGHT_TRACKING_STATUS;
      this.cameraOptions.Camera_GPIO_3 = CameraGPIOOutput.CAM_GPIO_OUT_CUSTOM;

      this.clickingOptions.Click_AudibleFeedback = true;
      this.clickingOptions.Click_Delay = 100;
      this.clickingOptions.Click_ZoomFactor = 4;
      this.clickingOptions.Click_Method = ClickMethod.CLICK_METHOD_NONE;
      this.clickingOptions.Blink_PrimaryTime = 3;
      this.clickingOptions.Blink_SecondaryTime = 6;
      this.clickingOptions.Blink_CancelTime = 6;
      this.clickingOptions.Blink_EnableSecondaryClick = true;
      this.clickingOptions.Blink_BothEyesRequired = true;
      this.clickingOptions.Blink_VisualFeedback = true;
      this.clickingOptions.Dwell_BoxSize = 18;
      this.clickingOptions.Dwell_Time = 15;

      this.toolbarOptions.Toolbar_ButtonSizeX = 26;
      this.toolbarOptions.Toolbar_ButtonSizeY = 22;
      this.toolbarOptions.Toolbar_ImageDisplayType = ToolBarImageDisplay.IMG_DISP_LIVE_IMAGE;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    public CalibrationOptions CalibrationOptions {
      get { return this.calibrationOptions; }
      set { this.calibrationOptions = value; }
    }

    public ProcessingOptions ProcessingOptions {
      get { return this.processingOptions; }
      set { this.processingOptions = value; }
    }

    public CameraOptions CameraOptions {
      get { return this.cameraOptions; }
      set { this.cameraOptions = value; }
    }

    public ClickingOptions ClickingOptions {
      get { return this.clickingOptions; }
      set { this.clickingOptions = value; }
    }

    public ToolbarOptions ToolbarOptions {
      get { return this.toolbarOptions; }
      set { this.toolbarOptions = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER
    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER
    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER
    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    public bool GetQuickGlanceSettings()
    {
      if (NativeMethods.GetQGOnFlag())
      {
        NativeMethods.GetCalibrationOptions(ref this.calibrationOptions);
        NativeMethods.GetProcessingOptions(ref this.processingOptions);
        NativeMethods.GetCameraOptions(ref this.cameraOptions);
        NativeMethods.GetClickingOptions(ref this.clickingOptions);
        NativeMethods.GetToolbarOptions(ref this.toolbarOptions);

        return true;
      }

      return false;
    }

    public void SetQuickGlanceSettings()
    {
      NativeMethods.SetCalibrationOptions(this.CalibrationOptions);
      NativeMethods.SetProcessingOptions(this.ProcessingOptions);
      NativeMethods.SetCameraOptions(this.CameraOptions);
      NativeMethods.SetClickingOptions(this.ClickingOptions);
      NativeMethods.SetToolbarOptions(this.ToolbarOptions);
    }

    #endregion //HELPER
  }
}

#endif