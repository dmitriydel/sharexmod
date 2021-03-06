﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using HelpersLib.Hotkeys2;
using HelpersLibMod;
using UploadersLib;

namespace HelpersLib.Hotkeys2
{
    public class Workflow
    {
        public EHotkey Hotkey = EHotkey.FullScreen;
        public HotkeySetting HotkeyConfig = new HotkeySetting();

        /// <summary>
        /// Added for r125 compatibility
        /// </summary>
        public List<EActivity> Activities = new List<EActivity>();

        public Subtask Subtasks = Subtask.CopyImageToClipboard | Subtask.SaveToFile | Subtask.UploadToRemoteHost;
        public AfterUploadTasks AfterUploadTasks = AfterUploadTasks.CopyURLToClipboard;
        public WorkflowSettings Settings = new WorkflowSettings();

        public Workflow()
        {
            ApplyDefaultValues(this);
        }

        public Workflow Clone()
        {
            return (Workflow)MemberwiseClone();
        }

        public Workflow(EHotkey hotkey, HotkeySetting hotkeyConfig)
            : this(hotkey.GetDescription(), hotkeyConfig, true)
        {
            this.Hotkey = hotkey;
        }

        public Workflow(string description, HotkeySetting hotkeyConfig, bool bProtected)
        {
            this.HotkeyConfig = hotkeyConfig;
            this.HotkeyConfig.SystemHotkey = bProtected;
            this.HotkeyConfig.Tag = Helpers.GetRandomAlphanumeric(12);
            this.HotkeyConfig.Description = description;
        }

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }
    }
}