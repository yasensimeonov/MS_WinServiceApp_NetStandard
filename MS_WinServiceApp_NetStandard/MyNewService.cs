﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MS_WinServiceApp_NetStandard
{
    public partial class MyNewService : ServiceBase
    {
        private int eventId = 1;

        public MyNewService()
        {
            InitializeComponent();
            eventLog1 = new EventLog();
            if (!EventLog.SourceExists("MySource"))
            {
                EventLog.CreateEventSource("MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("In OnStart.");

            // Set up a timer that triggers every minute.
            Timer timer = new Timer
            {
                Interval = 60000 // 60 seconds
            };
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In OnStop.");
        }
    }
}
