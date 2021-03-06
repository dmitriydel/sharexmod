﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using HelpersLibMod;
using Microsoft.Win32;
using UploadersLib;

namespace HelpersLib.Hotkeys2
{
    [Serializable]
    public class WorkflowSettings
    {
        [Category(ComponentModelStrings.ActivitiesAfterCapture), Description("Apply default after capture and upload settings."), DefaultValue(true)]
        public bool ApplyDefaultSettings { get; set; }

        [Category(ComponentModelStrings.ActivitiesUploadersText), DefaultValue("text"), Description("Text format e.g. csharp, cpp, etc.")]
        public string TextFormat { get; set; }

        [Category(ComponentModelStrings.ActivitiesUploadersText), DefaultValue("txt"), Description("File extension when saving text to the local hard disk.")]
        public string TextFileExtension { get; set; }

        public DestConfig DestConfig = new DestConfig();
        public List<ExternalProgram> ExternalPrograms = new List<ExternalProgram>();

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }

        public WorkflowSettings()
        {
            ApplyDefaultValues(this);
        }

        public void Clear()
        {
            DestConfig.ImageUploaders.Clear();
            DestConfig.TextUploaders.Clear();
            DestConfig.FileUploaders.Clear();
            DestConfig.LinkUploaders.Clear();
        }

        public void GetDefaults()
        {
            if (ExternalPrograms.Count == 0)
            {
                AddExternalProgram("Paint", "mspaint.exe");
                AddExternalProgram("Paint.NET", "PaintDotNet.exe");
                AddExternalProgram("Adobe Photoshop", "Photoshop.exe");
                AddExternalProgram("IrfanView", "i_view32.exe");
                AddExternalProgram("XnView", "xnview.exe");
            }
        }

        private void AddExternalProgram(string name, string filename)
        {
            ExternalProgram externalProgram = RegistryHelper.FindProgram(name, filename);

            if (externalProgram != null)
            {
                ExternalPrograms.Add(externalProgram);
            }
        }

        public WorkflowSettings Clone()
        {
            return (WorkflowSettings)MemberwiseClone();
        }
    }
}