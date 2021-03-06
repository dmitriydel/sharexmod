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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib;

namespace HelpersLib.Hotkeys2
{
    public partial class FileActionForm : Form
    {
        public ExternalProgram FileAction { get; private set; }

        public FileActionForm()
            : this(new ExternalProgram())
        {
        }

        public FileActionForm(ExternalProgram fileAction)
        {
            FileAction = fileAction;
            InitializeComponent();
            txtName.Text = fileAction.Name ?? "";
            txtPath.Text = fileAction.Path ?? "";
            txtArguments.Text = fileAction.Args ?? "%filepath%";
        }

        private void btnPathBrowse_Click(object sender, EventArgs e)
        {
            Helpers.BrowseFile("ShareX - choose file path", txtPath);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FileAction.Name = txtName.Text;
            FileAction.Path = txtPath.Text;
            FileAction.Args = txtArguments.Text;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}