﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using HelpersLib.Hotkeys2;

namespace HelpersLib.Hotkeys2
{
    public class Workflow
    {
        public EHotkey Hotkey;
        public HotkeySetting HotkeyConfig = new HotkeySetting();
        public List<EActivity> Activities = new List<EActivity>();

        [Category(ComponentModelStrings.ActivitiesUploadersText), DefaultValue("text"), Description("Text format e.g. csharp, cpp, etc.")]
        public string TextFormat { get; set; }

        public Workflow()
        {
            ApplyDefaultValues(this);
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