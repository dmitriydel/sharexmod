﻿#region License Information (GPL v3)

/*
    ShareX - A program that allows you to take screenshots and share any file type
    Copyright (C) 2012 ShareX Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkey;
using ScreenCapture;
using UploadersLib.HelperClasses;

namespace ShareX
{
    public class Settings : SettingsBase<Settings>
    {
        #region Main Form

        public int SelectedImageUploaderDestination = 0;
        public int SelectedTextUploaderDestination = 0;
        public int SelectedFileUploaderDestination = 0;
        public int SelectedURLShortenerDestination = 0;
        public bool ShowClipboardContentViewer = true;

        #endregion Main Form

        #region Settings Form

        // General
        public bool ShowTray = true;

        public bool AutoCheckUpdate = true;
        public bool ClipboardAutoCopy = true;
        public bool URLShortenAfterUpload = false;
        public bool AutoPlaySound = true;

        // Hotkeys / Workflows

        public List<Workflow> Workflows1 = new List<Workflow>();

        // Upload
        public bool UseCustomUploadersConfigPath = false;

        public string CustomUploadersConfigPath = string.Empty;
        public int UploadLimit = 5;
        public int BufferSizePower = 3;

        // Image - Location
        public string ScreenshotsPath2 = Program.ScreenshotsRootPath;

        // Image - Quality
        public EImageFormat ImageFormat = EImageFormat.PNG;

        public int ImageJPEGQuality = 90;
        public GIFQuality ImageGIFQuality = GIFQuality.Default;
        public int ImageSizeLimit = 512;
        public EImageFormat ImageFormat2 = EImageFormat.JPEG;

        // Image - Resize
        public bool ImageAutoResize = false;

        public bool ImageKeepAspectRatio = false;
        public bool ImageUseSmoothScaling = true;
        public ImageScaleType ImageScaleType = ImageScaleType.Percentage;
        public int ImageScalePercentageWidth = 100;
        public int ImageScalePercentageHeight = 100;
        public int ImageScaleToWidth = 100;
        public int ImageScaleToHeight = 100;
        public int ImageScaleSpecificWidth = 100;
        public int ImageScaleSpecificHeight = 100;

        // Clipboard upload
        public bool ClipboardUploadAutoDetectURL = true;

        // Test: %y %mo %mon %mon2 %d %h %mi %s %ms %w %w2 %pm %rn %ra %width %height %app %ver
        public string NameFormatPattern = "%y-%mo-%d_%h-%mi-%s";

        // Capture
        public bool ShowCursor = false;

        public bool CaptureTransparent = true;
        public bool CaptureShadow = true;
        public bool CaptureAnnotateImage = false;
        public bool CaptureCopyImage = false;
        public bool CaptureSaveImage = false;
        public string SaveImageSubFolderPattern = "%y-%mo";
        public bool CaptureUploadImage = true;
        public SurfaceOptions SurfaceOptions = new SurfaceOptions();

        // History
        public bool SaveHistory = true;

        public bool UseCustomHistoryPath = false;
        public string CustomHistoryPath = string.Empty;
        public int HistoryMaxItemCount = -1;

        // Proxy
        public ProxyInfo ProxySettings = new ProxyInfo();

        // Advanced
        [Category(ComponentModelStrings.SettingsInteraction), DefaultValue(false), Description("Show after capture wizard. Dynamically choose actions after capture")]
        public bool ShowAfterCaptureWizard { get; set; }
        [Category(ComponentModelStrings.SettingsInteraction), DefaultValue(false), Description("Show clipboard options after host upload is completed. Dynamically choose which link format to be copied to the clipboad.")]
        public bool ShowClipboardOptionsWizard { get; set; }
        [Category(ComponentModelStrings.InputsClipboard), DefaultValue(false), Description("When a folder path is in the clipboard, upload the folder index instead of the folder path as part of Clipboard Upload.")]
        public bool IndexFolderWhenPossible { get; set; }

        #endregion Settings Form

        #region Methods

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }

        public Settings()
        {
            ApplyDefaultValues(this);
        }

        #endregion Methods
    }
}