﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ShareX.Forms;
using ShareX.Properties;
using UploadersLib;

namespace ShareX
{
    public static class FormsHelper
    {
        private static MainForm _MainForm;
        private static OptionsWindow _OptionsWindow;

        public static MainForm Main
        {
            get
            {
                if (_MainForm == null || _MainForm.IsDisposed)
                    _MainForm = new MainForm() { Icon = Resources.ShareX };

                return _MainForm;
            }
            set
            {
                _MainForm = value;
            }
        }

        public static OptionsWindow Options
        {
            get
            {
                return new OptionsWindow() { Icon = Resources.ShareX }; ;
            }
            set
            {
                _OptionsWindow = value;
            }
        }

        public static void ShowOptions()
        {
            Options.ShowDialog();
            DropboxSyncHelper.SaveAsync();
        }

        public static void ShowUploadersConfig()
        {
            if (Program.UploadersConfig == null)
            {
                Program.UploaderSettingsResetEvent.WaitOne();
            }

            UploadersConfigForm uploaderConfig = new UploadersConfigForm(Program.UploadersConfig, new UploadersAPIKeys()) { Icon = Resources.ShareX };
            uploaderConfig.ShowDialog();
            uploaderConfig.Activate();
            uploaderConfig.Config.SaveAsync(Program.UploadersConfigFilePath);

            Main.AfterUploadersConfigClosed();
            DropboxSyncHelper.SaveAsync();
        }
    }
}