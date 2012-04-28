﻿/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2012  Thomas Braun, Jens Klingen, Robin Krom
 *
 * For more information see: http://getgreenshot.org/
 * The Greenshot project is hosted on Sourceforge: http://sourceforge.net/projects/greenshot/
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 1 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;

using Greenshot.Plugin;
using GreenshotPlugin.Core;

namespace Greenshot.Helpers
{
    /// <summary>
    /// Description of ProcessorHelper.
    /// </summary>
    public static class ProcessorHelper
    {
        private static log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(ProcessorHelper));
        private static Dictionary<string, IProcessor> RegisteredProcessors = new Dictionary<string, IProcessor>();

        /// Initialize the Processors
        static ProcessorHelper()
        {
            foreach (Type ProcessorType in InterfaceUtils.GetSubclassesOf(typeof(IProcessor), true))
            {
                // Only take our own
                if (!"Greenshot.Processors".Equals(ProcessorType.Namespace))
                {
                    continue;
                }
                if (!ProcessorType.IsAbstract)
                {
                    IProcessor Processor;
                    try
                    {
                        Processor = (IProcessor)Activator.CreateInstance(ProcessorType);
                    }
                    catch (Exception e)
                    {
                        LOG.ErrorFormat("Can't create instance of {0}", ProcessorType);
                        LOG.Error(e);
                        continue;
                    }
                    if (Processor.isActive)
                    {
                        LOG.DebugFormat("Found Processor {0} with designation {1}", ProcessorType.Name, Processor.Designation);
                        RegisterProcessor(Processor);
                    }
                    else
                    {
                        LOG.DebugFormat("Ignoring Processor {0} with designation {1}", ProcessorType.Name, Processor.Designation);
                    }
                }
            }
        }

        /// <summary>
        /// Register your Processor here, if it doesn't come from a plugin and needs to be available
        /// </summary>
        /// <param name="Processor"></param>
        public static void RegisterProcessor(IProcessor Processor)
        {
            // don't test the key, an exception should happen wenn it's not unique
            RegisteredProcessors.Add(Processor.Designation, Processor);
        }

        /// <summary>
        /// A simple helper method which will call ProcessCapture for the Processor with the specified designation
        /// </summary>
        /// <param name="designation"></param>
        /// <param name="surface"></param>
        /// <param name="captureDetails"></param>
        public static void ProcessCapture(string designation, ISurface surface, ICaptureDetails captureDetails)
        {
            if (RegisteredProcessors.ContainsKey(designation))
            {
                IProcessor Processor = RegisteredProcessors[designation];
                if (Processor.isActive)
                {
                    Processor.ProcessCapture(surface, captureDetails);
                }
            }
        }
    }
}