﻿#region license
// ==============================================================================
// Microsoft patterns & practices Enterprise Library
// Enterprise Library Quick Start
// ==============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ==============================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Web;

namespace SlabReconfigurationWebRole.Events
{
    [EventSource(Name = "QuickStartEventSource")]
    public sealed class QuickStartEventSource : EventSource
    {
        private static readonly Lazy<QuickStartEventSource> log = new Lazy<QuickStartEventSource>(() => new QuickStartEventSource());

        public static QuickStartEventSource Log
        {
            get { return log.Value; }
        }

        [Event(1, Level = EventLevel.Verbose)]
        internal void SendingMessage(string recipient, string message)
        {
            if (this.IsEnabled())
            {
                this.WriteEvent(1, recipient, message);
            }
        }

        [Event(2, Level = EventLevel.Informational)]
        internal void MessageSent(string recipient)
        {
            if (this.IsEnabled())
            {
                this.WriteEvent(2, recipient);
            }
        }

        [NonEvent]
        internal void MessageSendingFailed(string recipient, Exception exception)
        {
            if (this.IsEnabled())
            {
                this.MessageSendingFailed(recipient, exception.ToString());
            }
        }

        [Event(3, Level = EventLevel.Error)]
        private void MessageSendingFailed(string recipient, string exceptionInformation)
        {
            this.WriteEvent(3, recipient, exceptionInformation);
        }
    }
}